using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StegeSelectIcon : MonoBehaviour {

    public EnemyData.Type enemy;
    public Renderer body;
    public GameObject Crown;
    

    public void Init() {

            EnemyData data = EnemyManeger.Instance.FindData(enemy);
            this.gameObject.SetActive(data.IsAlive);
            body.material.color = data.enemyColor;
            Crown.SetActive(data.enemyLV > 1);
            
    }


}

