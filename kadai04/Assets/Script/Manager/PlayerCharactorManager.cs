using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCharactorManager : SingletonMonoBehaviour<PlayerCharactorManager> {
   
    public List<PlayerCharactorData> playerDatas;

    public PlayerCharactorData PlayerData { get { return playerData; } }
    PlayerCharactorData playerData;
    public bool IsSelect { get { return isSelect; } }
    bool isSelect = false;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public PlayerCharactorData SelectData(PlayerCharactor chara)
    {
        return playerDatas[(int)chara]; 
    }

    public void PlayerSelected(PlayerCharactor chara)
    {
        playerData = playerDatas[(int)chara];
        isSelect = true;
    }

}
