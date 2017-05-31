using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AchievementManeger : SingletonMonoBehaviour<AchievementManeger> {

    [SerializeField]
    public PopupAchievement prefab;

    public List<AchievementItemData> datas;

    public static bool IsPopup{get{return isPopup;}}
    static bool isPopup=false;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    Queue<AchievementKind> kinds = new Queue<AchievementKind>();



    public AchievementItemData Select(int num)
    {
        return datas[num];
    }

    public IEnumerator PopupAchievement(Transform parent)
    {
        isPopup = true;
        while (kinds.Count > 0)
        {
            AchievementKind kind = kinds.Dequeue();
            AchievementItemData data = datas[(int)kind];
            if (data.IsCrear == false)
            {
                data.AchievementCrear();
                PopupAchievement pop = GameObject.Instantiate(prefab, parent).GetComponent<PopupAchievement>();
                pop.Init(data);
                yield return new WaitForSeconds(4.5f);
                Destroy(pop.gameObject);
                CheckComplete();
            }
        }
        isPopup = false;

    }

    public void CheckComplete()
    {

        for (int i = 0; i < datas.Count; i++)
        {
            if (!(datas[i].IsCrear)) return;
        }

        AddAchievement(AchievementKind.complete);

    }

    public bool CheckKinds()
    {
        if (kinds.Count > 0) return true;
        else return false;
    }

    public void AddAchievement(AchievementKind kind)
    {
        kinds.Enqueue(kind);
    }

    

}
