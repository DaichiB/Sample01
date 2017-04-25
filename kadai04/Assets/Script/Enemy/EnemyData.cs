using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class EnemyData {

    public enum Type {

        TallMan,
        FireMan,
        IceMan,
        ClayMan,
        TallKing,
        FireKing,
        IceKing,
        ClayKing,
        DarkKing,
        Max,
        
    };

    public enum Element {

        Fire,
        Ice,
        Thunder,
        None

    };

    public string enemyName;
    public Type type = Type.TallMan;
    public Color enemyColor = Color.white;
    public int enemyLV; //1:～Man, 2:～King, 3:ボス
    public int enemyHP;
    public int enemyDefenceValue;
    public int enemyMagicResValue;
    public Element Werkness;

}
