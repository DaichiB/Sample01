using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleMessageBoad : MonoBehaviour {

    [SerializeField]
    Image messegeImage;
    [SerializeField]
    List<Sprite> messege;//0:lose, 1:win
    [SerializeField]
    Button rematch, quit;
    [SerializeField]
    Image TextCongrats;
    
    public delegate void OnClickDel();
    OnClickDel onClickRemath;

    public void Init(MessageType type, EnemyData.Type enemy = EnemyData.Type.TallMan, OnClickDel onClick = null)
    {
        if (type == MessageType.lose)
        {
            this.gameObject.SetActive(true);
            messegeImage.sprite = messege[0];
            messegeImage.SetNativeSize();
            rematch.gameObject.SetActive(true);
            quit.gameObject.SetActive(true);
            TextCongrats.gameObject.SetActive(false);

            onClickRemath = onClick;
        }
        else if (type == MessageType.win && enemy == EnemyData.Type.DarkKing)
        {
            this.gameObject.SetActive(true);
            messegeImage.sprite = messege[1];
            messegeImage.SetNativeSize();
            Vector3 size = new Vector3(0.8f, 0.8f, 1);
            messegeImage.gameObject.transform.localScale = size;
            rematch.gameObject.SetActive(false);
            quit.gameObject.SetActive(false);
            TextCongrats.gameObject.SetActive(true);
        }
    }

    public void OnClickRematch()
    {
        if (onClickRemath != null)
        {
            onClickRemath();
            this.gameObject.SetActive(false);
        }
        else Debug.LogError("function is null");
    }

    public void OnClickQuit()
    {

        SceneManager.LoadScene("SelectBattleGame");

    }

}
