using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

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

    [SerializeField]
    ScreenOverlay camera;

    [SerializeField]
    GameObject MessegeBoad;


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

    [SerializeField]
    Transform Canvas3D, CanvasBattle;
    [SerializeField]
    GameObject[] effectPrefab;

    [SerializeField]
    BattleMessageBoad messageBoad;


    void Start()
    {

        this.gameObject.SetActive(true);
        buttonStart.SetActive(true);
        buttonStop.SetActive(false);
        messageBoad.gameObject.SetActive(false);
        enemy = EnemyManeger.Selected;
        state = BattleState.none;
        camera.intensity = 0;
        Init();
        Debug.Log(enemy.IsAlive);

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
            yield return new WaitForSeconds(2f);
            messageBoad.Init(type, enemy.type);
            yield return new WaitForSeconds(3f);
            while (AchievementManeger.IsPopup) yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("SelectBattleGame");


        }
        else
        if (type == MessageType.lose)
        {

            message.sprite = messageSprites[2];
            message.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);
            while (AchievementManeger.IsPopup) yield return new WaitForSeconds(0.5f);
            //SceneManager.LoadScene("SelectBattleGame");
            messageBoad.Init(type,enemy.type, Init);

        }
        state = BattleState.idle;

    }

    public void Init()
    {
        message.gameObject.SetActive(false);
        enemyDamage = 0;
        enemyArive = true;
        playerArive = true;
        body.material.color = enemy.enemyColor;
        Crown.gameObject.SetActive(enemy.enemyLV > 1);
        playerItem.Init();
        enemyItem.SetEnemyData(enemy);
        StartCoroutine(AppearMesseage((MessageType)0));

    }

    public void OnClickReturn()
    {

        SceneManager.LoadScene("SelectBattleGame");

    }

    public void OnClickStart()
    {
        if (state != BattleState.idle) return;

        state = BattleState.turn;
        StartCoroutine(ChengeButton(BattleState.turn));

    }
    public void OnClickStop()
    {

        if (state != BattleState.turn)
        {
            Debug.LogError("Error:Click Stop when state is not turn!");
            return;
        }

        state = BattleState.stopping;
        StartCoroutine(ChengeButton(BattleState.stopping));
        AchievementManeger.Instance.AddAchievement(AchievementKind.firstBattle);

    }

    // Update is called once per frame
    void Update()
    {
        if(AchievementManeger.Instance.CheckKinds() && !(AchievementManeger.IsPopup))
            StartCoroutine(AchievementManeger.Instance.PopupAchievement(CanvasBattle));

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
                    AchievementManeger.Instance.AddAchievement(AchievementKind.hitCritical);
                }
                else
                {

                    damage = playerItem.ValueMagic;
                    kind = AttackKind.magic;
                }

                damage = CheckWerkness(damage, result);
                AttackEffect(result);
                enemyArive = enemyItem.UpdataEnemyHp(damage, kind);
                Shake(result == enemy.Werkness);
                playerItem.ReturnAttackAndMagicValue();
            }
        }
        else {
            playerArive = playerItem.MissAttack();
            StartCoroutine(EffectDamage());
            AchievementManeger.Instance.AddAchievement(AchievementKind.missAttack);
        }

        Debug.LogFormat("Attack:{0}, damage:{1}", result.ToString(), damage);
        if (enemyArive == false)
        {

            EnemyManeger.Instance.CrearEnemy(enemy.type, CanvasBattle);
            selectIcon.ActionEnemyDead();
            StartCoroutine(AppearMesseage((MessageType)1));

        }
        if (playerArive == false) {
            AchievementManeger.Instance.AddAchievement(AchievementKind.loseBattle);
            StartCoroutine(AppearMesseage((MessageType)2));
        }
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

    void AttackEffect(AttackType type)
    {

        if ((int)type > 4)
        {
            Debug.LogError( "Error Attack Type");
            return;
        }
        Vector3 pos = new Vector3(0, 0.02f, -0.05f);
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        GameObject eff = (GameObject)Instantiate(effectPrefab[(int)type], pos, rot, Canvas3D);
        GameObject.Destroy(eff, 2.0f);

    }

    int CheckWerkness(int damage, AttackType type)
    {

        if (enemy.Werkness == type)
        {
            AchievementManeger.Instance.AddAchievement(AchievementKind.hitWeakness);
            return damage * 2;
        }
        return damage;

    }

    IEnumerator EffectDamage()
    {
        camera.intensity = 1;
        yield return new WaitForSeconds(0.5f);
        camera.intensity = 0;
    }

    IEnumerator ChengeButton(BattleState state)
    {
        if (state == BattleState.turn)
        {
            //while (speadRoulette < MAX_SPEAD) yield return new WaitForSeconds(0.3f);
            yield return new WaitForSeconds(0.5f);
            buttonStart.SetActive(false);
            buttonStop.SetActive(true);
        }
        else if (state == BattleState.stopping)
        {
            //while (speadRoulette > 0) yield return new WaitForSeconds(0.3f);
            yield return new WaitForSeconds(0.5f);
            buttonStop.SetActive(false);
            buttonStart.SetActive(true);
        }
        else yield break;
    }

    void Shake(bool isWeakness)
    {
        float degree = isWeakness ? 0.06f : 0.02f;
        iTween.ShakePosition(selectIcon.gameObject, iTween.Hash(
            "x", degree,
            "y", degree,
            "islocal", true,
            "time", 0.5f
            ));
    }

}
