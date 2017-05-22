using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AchievementManeger : SingletonMonoBehaviour<AchievementManeger> {

    public List<AchievementItemData> datas;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public AchievementItemData Select(int num)
    {
        return datas[num];
    }

}
