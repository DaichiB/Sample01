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
    [SerializeField]
    Slider AttackGarge, MagicGarge;

    public int ValueAttack { get { return (int)AttackGarge.value *10; } }
    public int ValueMagic { get { return (int)MagicGarge.value *10; } }

    public int attack = 4, magic = 5;

    public void Init()
    {

        playerDamage = 0;
        for(int i = 0; i < 3; i++)
            playerLife[i].sprite = LifeIcon[0];

        PlayerCharactorData playerData = PlayerCharactorManager.Instance.PlayerData;

        AttackGarge.value = playerData.attackNum;
        MagicGarge.value = playerData.magicNum;

    }

    public bool MissAttack()
    {

        playerLife[playerDamage].sprite = LifeIcon[1];
        playerDamage++;
        if (playerDamage == 3) return false;
        else return true;

    }

    public void HitCharge()
    {

        AttackGarge.value = AttackGarge.value * 2;
        MagicGarge.value = MagicGarge.value * 2;

    }

    public void ReturnAttackAndMagicValue()
    {
        AttackGarge.value = attack;
        MagicGarge.value = magic;
    }

}
