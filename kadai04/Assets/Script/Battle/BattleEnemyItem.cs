using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnemyItem : MonoBehaviour {

    [SerializeField]
    Slider lifeGarge, Defence, MagicRes;
    [SerializeField]
    Text enemyHP;
    [SerializeField]
    Image werknessElement;

    [SerializeField]
    private Sprite[] elementIcon;//0:fire, 1:ice,2:thunder

    const string ENEMY_HP = "{0} / {1}";

    protected int enemyDamage, enemyMaxHP;

    public void SetEnemyData(EnemyData enemyData)
    {
        enemyDamage = 0;
        enemyMaxHP = enemyData.enemyHP;
        enemyHP.text = string.Format(ENEMY_HP, enemyMaxHP - 0, enemyMaxHP);

        switch (enemyData.Werkness)
        {

            case EnemyData.Element.Fire:
                werknessElement.sprite = elementIcon[0];
                break;

            case EnemyData.Element.Ice:
                werknessElement.sprite = elementIcon[1];
                break;
            case EnemyData.Element.Thunder:
                werknessElement.sprite = elementIcon[2];
                break;
            case EnemyData.Element.None:
                werknessElement.gameObject.SetActive(false);
                break;

        }

        Defence.value = enemyData.enemyDefenceValue;
        MagicRes.value = enemyData.enemyMagicResValue;
        lifeGarge.maxValue = enemyData.enemyHP;
        lifeGarge.value = enemyData.enemyHP;

    }

    public bool UpdataEnemyHp(int Damage)
    {

        enemyDamage += Damage;
        if (enemyDamage >= enemyMaxHP)
        {
            enemyDamage = enemyMaxHP;
            enemyHP.text = string.Format(ENEMY_HP, enemyMaxHP - enemyDamage, enemyMaxHP);
            lifeGarge.value -= Damage;
            return false;
        }

        enemyHP.text = string.Format(ENEMY_HP, enemyMaxHP - enemyDamage, enemyMaxHP);
        lifeGarge.value -= Damage;
        return true;

    }

}
