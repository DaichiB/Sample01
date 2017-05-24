using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AchievementItemData
{
    public AchievementKind kind;
    public string AchievementTitle;
    public Sprite AchievementTextImage;
    public string AchievementText;
    public bool IsCrear { get { return isCrear; } }
    bool isCrear = false;

    public void AchievementCrear()
    {
        isCrear = true;
    }

}

public class AchievementItem : MonoBehaviour {

    [SerializeField]
    Image AchievementText;
    [SerializeField]
    GameObject cover;

    public void Init(int count)
    {
        AchievementItemData data = AchievementManeger.Instance.Select(count);

        AchievementText.sprite = data.AchievementTextImage;
        AchievementText.SetNativeSize();
        cover.SetActive(!(data.IsCrear));
    }

}
