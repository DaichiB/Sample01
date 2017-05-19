using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
[System.Serializable]
public class EnemyManeger : SingletonMonoBehaviour<EnemyManeger> {

    public List<EnemyData> enemyDataList; //敵データのリスト

    public static EnemyData Selected { get { return selected; } }
    public static EnemyData selected;

    
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
            DontDestroyOnLoad(this.gameObject);
            
    }
    
    public void Select(EnemyData.Type type)
    {

        selected = FindData(type);

    }

    public EnemyData FindData(EnemyData.Type type)
    {

        return enemyDataList.Find(item => item.type == type);

    }

    public void CrearEnemy(EnemyData.Type type)
    {

        /*
        EnemyData enemy = FindData(type);
        enemy.IsAlive = false;
        */

        foreach(EnemyData enemy in enemyDataList)
        {
            if (enemy.type == type) enemy.EnemyDaed();
        }


    }

}
