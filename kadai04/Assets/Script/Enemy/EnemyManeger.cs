using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManeger : MonoBehaviour {

    public List<EnemyData> enemyDataList; //敵データのリスト

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
