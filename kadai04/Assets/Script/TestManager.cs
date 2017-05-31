using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour {

    [SerializeField]
    public PopupAchievement prefab;

    public bool isPopup = false;

    public IEnumerator Popup(Transform parent)
    {
        //Debug.Log("start");
        if (isPopup) yield return new WaitForSeconds(5);
        isPopup = true;
        GameObject pop = (GameObject)Instantiate(prefab.gameObject, parent);
        yield return new WaitForSeconds(3);
        Destroy(pop);
        Debug.Log("消えた？");
        isPopup = false;
    }

}
