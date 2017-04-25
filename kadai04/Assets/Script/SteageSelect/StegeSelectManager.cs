using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class StegeSelectManager : MonoBehaviour {

    public StegeSelectPlanet planet;
    public StegeSelectBoad boad;
    public EnemyManeger enemy;
    //public StegeSelectIcon icon;

	// Use this for initialization
	public void Start () { //本来はopen

        this.gameObject.SetActive(true); //設定されているgameObjectを表示状態に
        InitIcons();
        boad.Close();
		
	}

    void InitIcons()
    {

        StegeSelectIcon[] icons = GetComponentsInChildren<StegeSelectIcon>(true);
        foreach(StegeSelectIcon icon in icons)
        {

            icon.Init();

        }

    }
	
	public void OnSelectEnemy(StegeSelectIcon icon)
    {

        enemy.Select(icon.enemy);
        planet.RorateOnSelect(icon.transform, OnRotateComplete);

    }

    public void OnRotateComplete()
    {

        boad.Open(EnemyManeger.Selected);

    }


}
