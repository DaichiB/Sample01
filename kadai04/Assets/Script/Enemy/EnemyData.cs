using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemyData : MonoBehaviour {

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

    public string name;
    public Type type = Type.TallMan;

}
