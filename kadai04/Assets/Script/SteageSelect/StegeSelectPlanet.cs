using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class StegeSelectPlanet : MonoBehaviour {

    public float speed = 360.0f;//ドラッグによる回転速度
    public float friction = 0.1f;//フリック時の減速係数
    Vector2 rotVel;//現在の回転速度

   　//ドラッグ時に移動した距離を更新
    public void OnDrag(BaseEventData data)
    {
        PointerEventData pointer = data as PointerEventData;
        rotVel.y = -pointer.delta.x * speed;
    }

    // 

	// Update is called once per frame
	void Update () {
        //回転させる
        this.transform.Rotate(new Vector3(0, rotVel.y, 0), Space.World);
        //回転の減速
        rotVel.y *= (1 - friction);
		
	}

    
    //後で書き直す
    System.Action onRotateCompete; //定義済みデリゲート

    //選択したキャラが正面を向くように回転
    public void RorateOnSelect(Transform enemy, System.Action onRotateCompete)
    {
        this.onRotateCompete = onRotateCompete;
        Vector3 from = enemy.position;
        Debug.Log(from);
        from.y = 0;
        from.Normalize();
        Vector3 to = Vector3.back;
        Vector3 euler = Quaternion.FromToRotation(from, to).eulerAngles;

        if (euler.y > 180)
            euler.y -= 360;

        iTween.RotateAdd(gameObject, iTween.Hash(
            "y", euler.y,
            "islocal", true,
            "speed", 60.0f,
            "oncomplete", "OnRotateComplete",//"oncomplete","T":アニメーション終了後に関数Tを実行
            "oncompletetarget", gameObject,//アニメーション終了語に実行する関数が入っているgameObject
            "easetype", iTween.EaseType.easeInOutQuad
            ));
    }

    //回転終了
    void OnRotateComplete()
    {
        onRotateCompete();
    }
}
