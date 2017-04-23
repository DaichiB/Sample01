using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class StegeSelectManager : MonoBehaviour {

    public StegeSelectPlanet planet;
    public StegeSelectBoad boad;
    public EnemyManeger enemy;
    public StegeSelectIcon icon;

	// Use this for initialization
	public void Start () { //本来はopen

        this.gameObject.SetActive(true); //設定されているgameObjectを表示状態に
        boad.Close();
		
	}
	
	public void OnSelectEnemy(StegeSelectIcon icon)
    {

        enemy.Select(icon.enemy);
        planet.RorateOnSelect(icon.transform, OnRotateComplete);

    }

    public void OnRotateComplete()
    {

        boad.Open();

    }


}
