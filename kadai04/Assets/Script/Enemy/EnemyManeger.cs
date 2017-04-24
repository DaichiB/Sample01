using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
[System.Serializable]
public class EnemyManeger : MonoBehaviour {

    public List<EnemyData> enemyDataList; //敵データのリスト

    public EnemyData Selected { get { return selected; } }
    EnemyData selected;

    public void Select(EnemyData.Type type)
    {

        selected = FindData(type);

    }

    public EnemyData FindData(EnemyData.Type type)
    {

        return enemyDataList.Find(item => item.type == type);

    }

}
