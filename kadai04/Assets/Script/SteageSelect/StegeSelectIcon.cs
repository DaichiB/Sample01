using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StegeSelectIcon : MonoBehaviour {

    public EnemyManeger enemyManeger;

    public EnemyData.Type enemy;
    public Renderer body;
    public GameObject Crown;

    public void Init() {

        if (enemyManeger != null)
        {
            EnemyData data = enemyManeger.FindData(enemy);
            body.material.color = data.enemyColor;
            Crown.SetActive(data.enemyLV > 1);
            
        }
    }


}

