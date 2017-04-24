using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StegeSelectBoad : MonoBehaviour {

    //[SerializeField]
    public Text enemyName;
    public Image imageEnemy; //敵のイメージデータ
    public Image imageCrown;

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

    }

}
