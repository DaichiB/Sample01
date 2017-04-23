using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StegeSelectBoad : MonoBehaviour {

	public void Close()
    {

        this.gameObject.SetActive(false);// 設定されているgameObjectを非表示に

    }

    public void Open()
    {

        this.gameObject.SetActive(true);

    }

    public void OnClickClose()
    {

        Close();

    }

}
