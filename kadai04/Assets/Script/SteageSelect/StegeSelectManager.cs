using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class StegeSelectManager : MonoBehaviour {

    public StegeSelectPlanet planet;
    public StegeSelectBoad boad;

	// Use this for initialization
	public void Start () { //本来はopen

        EnemyManeger.Instance.StegeDebug();
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

        EnemyManeger.Instance.Select(icon.enemy);
        planet.RorateOnSelect(icon.transform, OnRotateComplete);

    }

    public void OnRotateComplete()
    {

        boad.Open(EnemyManeger.Selected);

    }

    public void OnClickAchivement()
    {
        SceneManager.LoadScene("Achievement");
    }


}
