using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StegeSelectManager : MonoBehaviour {

    public StegeSelectPlanet planet;
    public StegeSelectBoad boad;

	// Use this for initialization
	public void Start () { //本来はopen

        this.gameObject.SetActive(true); //設定されているgameObjectを表示状態に
        boad.Close();
		
	}
	
	public void OnSelectEnemy()
    {

        //planet.RorateOnSelect()

        OnRotateComplete();

    }

    public void OnRotateComplete()
    {

        boad.Open();

    }

}
