using System;
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
    int enemyCrearCount = 0;
    public int StegeLv{get{ return stegeLv; } }
    int stegeLv = 1;

    
    public enum DebugStege
    {
        play,
        Lv1,
        Lv2,
        Lv3
    }

    public DebugStege DebugStegeLv = DebugStege.Lv1;

    const string CREAR_TEXT = "crear";

    
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        stegeLv = 1;
    }
    
    public void Select(EnemyData.Type type)
    {

        selected = FindData(type);

    }

    public EnemyData FindData(EnemyData.Type type)
    {

        return enemyDataList.Find(item => item.type == type);

    }

    public void CrearEnemy(EnemyData.Type type, Transform parent)
    {

        
        EnemyData enemy = FindData(type);
        enemy.EnemyDaedOrArrive();
        AchievementKind kind = (AchievementKind)Enum.Parse(typeof(AchievementKind), (CREAR_TEXT + type.ToString()));
        AchievementManeger.Instance.AddAchievement(kind);
        
        enemyCrearCount++;
        if (stegeLv == 1 && enemyCrearCount >= 4)
            stegeLv = 2;
        else if (stegeLv == 2 && enemyCrearCount >= 8)
            stegeLv = 3;


    }

    public void StegeDebug()
    {
        if(DebugStegeLv != DebugStege.play)
        {
            stegeLv = (int)DebugStegeLv;
            for(int i = 0; i < enemyDataList.Count; i++)
            {
                EnemyData enemy = enemyDataList[i];
                if (enemy.enemyLV < stegeLv) enemy.EnemyDaedOrArrive();
            }
        }
    }


}
