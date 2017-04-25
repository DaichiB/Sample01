using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManrger : MonoBehaviour {

    public Renderer body;
    public GameObject Crown;

    // Use this for initialization
    void Start () {

        this.gameObject.SetActive(true);
        Init(EnemyManeger.Selected);

		
	}

    public void Init(EnemyData enemyData) {

        body.material.color = enemyData.enemyColor;
        Crown.gameObject.SetActive(enemyData.enemyLV > 1);

    }

    public void OnClickReturn()
    {

        SceneManager.LoadScene("SelectBattleGame");

    }

}
