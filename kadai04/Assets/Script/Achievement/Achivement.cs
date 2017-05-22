using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Achivement : MonoBehaviour {

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
            GameObject Achie = (GameObject)Instantiate(prefab, parent);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnClickReturn()
    {
        SceneManager.LoadScene("SelectBattleGame");
    }
	
}
