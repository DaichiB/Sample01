using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManrger : MonoBehaviour
{

    public Renderer body;
    public GameObject Crown;

    [SerializeField]
    BattleEnemyItem enemyItem;
    [SerializeField]
    BattlePlayerItem playerItem;
    [SerializeField]
    StegeSelectIcon selectIcon;

    [SerializeField]
    GameObject ruletteTable, buttonStart, buttonStop;

    protected int enemyDamage;
    EnemyData enemy;

    [SerializeField]
    Image message;
    [SerializeField]
    Sprite[] messageSprites;
    protected bool enemyArive, playerArive;

    bool isStopClick = false;
    float speadRoulette = 0;
    public float MAX_SPEAD = 360f;


    // Use this for initialization

    enum BattleState
    {
        none,
        messeage,//メッセージ表示
        idle,//待機状態
        turn,//ルーレット回転
        stopping//ルーレット減速
    }

    BattleState state = BattleState.none;




    void Start()
    {

        this.gameObject.SetActive(true);
        buttonStart.SetActive(true);
        buttonStop.SetActive(false);
        message.gameObject.SetActive(false);
        enemyDamage = 0;
        enemy = EnemyManeger.Selected;
        enemyArive = true;
        playerArive = true;
        state = BattleState.none;
        Init();
        Debug.Log(enemy.IsAlive);
        StartCoroutine(AppearMesseage((MessageType)0));

    }

    IEnumerator AppearMesseage(MessageType type)
    {

        state = BattleState.messeage;
        if (type == MessageType.battleStart)
        {
            message.sprite = messageSprites[0];

            yield return new WaitForSeconds(0.2f);

            message.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            Debug.Log("Waited!");

            message.gameObject.SetActive(false);

        }
        else
        if (type == MessageType.win)
        {

            message.sprite = messageSprites[1];
            message.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene("SelectBattleGame");


        }
        else
        if (type == MessageType.lose)
        {

            message.sprite = messageSprites[2];
            message.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene("SelectBattleGame");

        }
        state = BattleState.idle;

    }

    public void Init()
    {

        body.material.color = enemy.enemyColor;
        Crown.gameObject.SetActive(enemy.enemyLV > 1);
        playerItem.Init();
        enemyItem.SetEnemyData(enemy);

    }

    public void OnClickReturn()
    {

        SceneManager.LoadScene("SelectBattleGame");

    }

    public void OnClickStart()
    {
        if (state != BattleState.idle) return;

        state = BattleState.turn;
        buttonStart.SetActive(false);
        buttonStop.SetActive(true);

    }
    public void OnClickStop()
    {

        if (state != BattleState.turn)
        {
            Debug.LogError("Error:Click Stop when state is not turn!");
            return;
        }

        state = BattleState.stopping;
        buttonStart.SetActive(true);
        buttonStop.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (state == BattleState.turn || state == BattleState.stopping)
        {
            switch (state)
            {
                case BattleState.turn:
                    if (speadRoulette < MAX_SPEAD)
                        speadRoulette += 5f;
                    break;
                case BattleState.stopping:
                    speadRoulette -= 5f;
                    break;
            }



            ruletteTable.transform.Rotate(0, 0, speadRoulette * Time.deltaTime, Space.World);

            if (speadRoulette <= 0)
            {
                state = BattleState.idle;
                UpdateStatus();
            }
        }

    }

    void UpdateStatus()
    {
        AttackType result = TryAttack();
        int damage = 0;
        AttackKind kind;

        if (result != AttackType.miss)
        {

            if (result == AttackType.charge) playerItem.HitCharge();
            else
            {
                if (result == AttackType.normal)
                {
                    damage = playerItem.ValueAttack;
                    kind = AttackKind.attack;
                }
                else if (result == AttackType.critical)
                {
                    damage = playerItem.ValueAttack * 2;
                    kind = AttackKind.attack;
                }
                else
                {
                    if (result == enemy.Werkness) damage = playerItem.ValueMagic * 2;
                    else damage = playerItem.ValueMagic;

                    kind = AttackKind.magic;
                }

                enemyArive = enemyItem.UpdataEnemyHp(damage, kind);
                playerItem.ReturnAttackAndMagicValue();
            }
        }
        else playerArive = playerItem.MissAttack();

        Debug.LogFormat("Attack:{0}, damage:{1}", result.ToString(), damage);
        if (enemyArive == false)
        {

            EnemyManeger.Instance.CrearEnemy(enemy.type);
            selectIcon.ActionEnemyDead();
            StartCoroutine(AppearMesseage((MessageType)1));

        }
        if (playerArive == false) StartCoroutine(AppearMesseage((MessageType)2));
    }

    AttackType TryAttack()
    {

        RectTransform temp = ruletteTable.GetComponent<RectTransform>();
        Vector3 nowRotation = temp.transform.localRotation.eulerAngles;

        if (nowRotation.z >= 0 && nowRotation.z <= 60) return AttackType.normal;
        if (nowRotation.z >= 80 && nowRotation.z <= 100) return AttackType.critical;
        if (nowRotation.z >= 120 && nowRotation.z <= 160) return AttackType.fire;
        if (nowRotation.z >= 180 && nowRotation.z <= 220) return AttackType.ice;
        if (nowRotation.z >= 240 && nowRotation.z <= 280) return AttackType.thunder;
        if (nowRotation.z >= 300 && nowRotation.z <= 340) return AttackType.charge;

        return AttackType.miss;


    }

}
