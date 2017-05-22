using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Achievement : MonoBehaviour {

    [SerializeField]
    Transform parent;
    [SerializeField]
    GameObject prefab;

    public int AchievementCount=15;

	// Use this for initialization
	void Start () {

        StartCoroutine(CreateItem(AchievementCount));
		
	}

    IEnumerator CreateItem(int count)
    {
        for (int i = 0; i < count; i++)
        {
            AchievementItem Achie = GameObject.Instantiate(prefab, parent).GetComponent<AchievementItem>();
            Achie.Init(i);
            AnimationAchievement(Achie.gameObject);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnClickReturn()
    {
        SceneManager.LoadScene("SelectBattleGame");
    }

    public void AnimationAchievement(GameObject achie)
    {
        iTween.RotateFrom(achie, iTween.Hash(
            "z", 60.0f,
            "isLocal", true,
            "time", 2.0f,
            "easetype", iTween.EaseType.easeOutElastic
            ));
           
    }
	
}
