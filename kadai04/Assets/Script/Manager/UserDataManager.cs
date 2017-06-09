using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : SingletonMonoBehaviour<UserDataManager>{

    private int stegeLv_and_crearEnemyCount_SaveData;
    public int stegeLv;
    public int crearEnemyCount_inStege;

    private int playerChara_SaveData;
    public bool isSelectChara;
    public PlayerCharactor playerChara;

    private int achievementCrearData_SaveData;
    public List<bool> achievementCrearData;

    const string ENEMY_MANAGER = "stege_lv_and_crear_enemy_count";
    const string PLAYER_CHARACTOR_MANAGER = "player_chara";
    const string ACHIEVEMENT_MANAGER = "achievement_crear_data";

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        stegeLv_and_crearEnemyCount_SaveData = PlayerPrefs.GetInt(ENEMY_MANAGER, 10);

        playerChara_SaveData = PlayerPrefs.GetInt(PLAYER_CHARACTOR_MANAGER, 0);
        achievementCrearData_SaveData = PlayerPrefs.GetInt(ACHIEVEMENT_MANAGER, 0);
    }

    void GetEnemyManagerData()
    {
        stegeLv = stegeLv_and_crearEnemyCount_SaveData / 10;
        crearEnemyCount_inStege = stegeLv_and_crearEnemyCount_SaveData % 10;
    }

    void GetPlayerCharactorManagerData()
    {
        if (playerChara_SaveData >= 10)
            isSelectChara = true;
        playerChara = (PlayerCharactor)(playerChara_SaveData % 10);
    }

    void GetAchievementManagerData()
    {
        int count = AchievementManeger.Instance.datas.Count;
        int temp = achievementCrearData_SaveData;
        for(int i = 0; i < count; i++)
        {
            if (temp % 2 == 1)
                achievementCrearData[i] = true;
            else achievementCrearData[i] = false;

            temp = temp >> 1;
        }
    }

}
