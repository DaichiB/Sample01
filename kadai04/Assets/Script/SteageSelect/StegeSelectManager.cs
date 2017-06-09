using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class StegeSelectManager : MonoBehaviour
{

    public StegeSelectPlanet planet;
    public StegeSelectBoad boad;
    public CharactorMaking charactorMaking;
    [SerializeField]
    public GameObject planetObjects, Canvas3D;

    // Use this for initialization
    public void Start()
    { //本来はopen

        EnemyManeger.Instance.StegeDebug();
        this.gameObject.SetActive(true); //設定されているgameObjectを表示状態に
        InitIcons();
        boad.Close();
        charactorMaking.Open();
        planetObjects.SetActive(true);
        //planetObjects.SetActive(false);
        //StartCoroutine(StartPlanet());

    }

    void InitIcons()
    {

        StegeSelectIcon[] icons = GetComponentsInChildren<StegeSelectIcon>(true);
        foreach (StegeSelectIcon icon in icons)
        {

            icon.Init();

        }

    }

    public void OnSelectEnemy(StegeSelectIcon icon)
    {

        EnemyManeger.Instance.Select(icon.enemy);
        if (EnemyManeger.Selected.IsAlive)
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

    /*IEnumerator StartPlanet()
    {
        while (PlayerCharactorManager.Instance.IsSelect != true) yield return new WaitForSeconds(0.2f);
        planetObjects.SetActive(true);
        StartAnimetion();
    }*/

    void StartAnimetion()
    {
        /*
        iTween.MoveFrom(Canvas3D, iTween.Hash(
            "y", 6,
            "time", 2.0f,
            "delay",0.5f
            ));
        iTween.RotateAdd(planetObjects, iTween.Hash(
            "y", 360,
            "time", 5.0f
            ));*/


    }


}
