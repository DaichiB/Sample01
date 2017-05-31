using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    [SerializeField]
    TestManager testManeger;

    private void Start()
    {
       // Debug.Log("start");
       StartCoroutine( testManeger.Popup(this.gameObject.transform));
    }
    
}
