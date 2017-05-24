using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupAchievement : MonoBehaviour {

    [SerializeField]
    Text achievementTitle, achievementText;

    public void Init(AchievementItemData data)
    {
        achievementTitle.text = data.AchievementTitle;
        achievementText.text = data.AchievementText;
    }

}
