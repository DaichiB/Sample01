using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayerItem : MonoBehaviour {

    protected int playerDamage;
    [SerializeField]
    Image[] playerLife;
    [SerializeField]
    Sprite[] LifeIcon;

    public void Init()
    {

        playerDamage = 0;
        for(int i = 0; i < 3; i++)
            playerLife[i].sprite = LifeIcon[0];

    }

    public bool MissAttac()
    {

        playerLife[playerDamage].sprite = LifeIcon[1];
        playerDamage++;
        if (playerDamage == 3) return false;
        else return true;

    }

}
