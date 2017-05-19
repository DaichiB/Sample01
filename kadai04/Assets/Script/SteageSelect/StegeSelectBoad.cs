using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StegeSelectBoad : MonoBehaviour {

    //[SerializeField]
    public Text enemyName;
    public Image imageEnemy; //敵のイメージデータ
    public Image imageCrown;
    public Text enemyHP;
    public Image werknessElement;
    public Slider Defence;
    public Slider MagicRes;

    [SerializeField]
    private Sprite iconFire;
    [SerializeField]
    private Sprite iconIce;
    [SerializeField]
    private Sprite iconThunder;

    public void Close()
    {

        this.gameObject.SetActive(false);// 設定されているgameObjectを非表示に

    }

    public void Open(EnemyData enemyData)
    {

        this.gameObject.SetActive(true);
        InitEnemyImage(enemyData);

    }

    public void OnClickClose()
    {

        Close();

    }

    void InitEnemyImage(EnemyData enemyData)
    {

        enemyName.text = enemyData.enemyName;
        imageEnemy.color = enemyData.enemyColor;
        imageCrown.gameObject.SetActive(enemyData.enemyLV>1);
        enemyHP.text = "HP: " + enemyData.enemyHP;

        switch(enemyData.Werkness){

            case AttackType.fire:
                werknessElement.sprite = iconFire;
                break;

            case AttackType.ice:
                werknessElement.sprite = iconIce;
                break;
            case AttackType.thunder:
                werknessElement.sprite = iconThunder;
                break;
            case AttackType.normal:
                werknessElement.gameObject.SetActive(false);
                break;

        }

        Defence.value = enemyData.enemyDefenceValue;
        MagicRes.value = enemyData.enemyMagicResValue;

    }

    public void OnBattleButton()
    {

        SceneManager.LoadScene("Battle");
        //BattleManrger.Init(enemyScript.Selected);

    }

}
