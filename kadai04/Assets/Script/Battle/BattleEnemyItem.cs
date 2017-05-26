using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnemyItem : MonoBehaviour {

    [SerializeField]
    Slider lifeGarge, Defence, MagicRes,gargeDelayed;
    [SerializeField]
    Text enemyHP;
    [SerializeField]
    Image werknessElement;

    [SerializeField]
    private List<Sprite> elementIcon;//0:normal,1:critical,2:fire,3:ice,4:thunder

    const string ENEMY_HP = "{0} / {1}";

    int enemyDamage, enemyMaxHP, currentHP;

    public void SetEnemyData(EnemyData enemyData)
    {
        enemyDamage = 0;
        enemyMaxHP = enemyData.enemyHP;
        enemyHP.text = string.Format(ENEMY_HP, enemyMaxHP - 0, enemyMaxHP);
        werknessElement.sprite = elementIcon[(int)enemyData.Werkness];   

        Defence.value = enemyData.enemyDefenceValue;
        MagicRes.value = enemyData.enemyMagicResValue;
        lifeGarge.maxValue = enemyData.enemyHP;
        lifeGarge.value = enemyData.enemyHP;
        gargeDelayed.maxValue = enemyData.enemyHP;
        gargeDelayed.value = enemyData.enemyHP;
        currentHP = (int)lifeGarge.value;

    }

    public bool UpdataEnemyHp(int Damage, AttackKind kind)
    {
        int defence=0;
        if (kind == AttackKind.attack) defence = (int)Defence.value*10;
        else if (kind == AttackKind.magic) defence = (int)MagicRes.value*10;

        if (Damage > defence)
            enemyDamage = enemyDamage + Damage-defence;
        
        if (enemyDamage >= enemyMaxHP)
        {
            enemyDamage = enemyMaxHP;
            enemyHP.text = string.Format(ENEMY_HP, (enemyMaxHP - enemyDamage), enemyMaxHP);
            lifeGarge.value = enemyMaxHP - enemyDamage;
            return false;
        }

        //enemyHP.text = string.Format(ENEMY_HP, (enemyMaxHP - enemyDamage), enemyMaxHP);
        lifeGarge.value = enemyMaxHP - enemyDamage;
        StartCoroutine(ActionGaegeDelayed());
        StartCoroutine(ActionTextGaegeDelayed());
        return true;

    }

    IEnumerator ActionGaegeDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        ValueTransition(gargeDelayed.value, lifeGarge.value, 0.8f, 0.2f, "OnSliderDelayedUpdate", iTween.EaseType.linear);
    }

    IEnumerator ActionTextGaegeDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        ValueTransition(gargeDelayed.value, lifeGarge.value, 1.0f, 0, "OnTextUpdate", iTween.EaseType.linear);
    }



    private void ValueTransition(float from, float to, float duration, float delay,
        string update, iTween.EaseType easetype)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", from,
            "to", to,
            "time", duration,
            "delay", delay,
            "onupdate", update,
            "easetype", easetype
            ));
    }

    private void OnSliderDelayedUpdate(float value)
    {
        gargeDelayed.value = value;
        
    }

    private void OnTextUpdate(int life)
    {
        enemyHP.text = string.Format("{0} / {1}", life, enemyMaxHP);
    }

}
