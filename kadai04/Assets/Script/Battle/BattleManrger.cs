using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManrger : MonoBehaviour {

    public Renderer body;
    public GameObject Crown;

    [SerializeField]
    BattleEnemyItem enemyItem;
    [SerializeField]
    BattlePlayerItem playerItem;

    [SerializeField]
    GameObject ruletteTable, buttonStart, buttonStop;

    protected bool onRuletteTurn;

    protected int enemyDamage;
    EnemyData enemy;

    [SerializeField]
    Image message;
    [SerializeField]
    Sprite[] messageSprites;
    protected bool enemyArive, playerArive;

    // Use this for initialization

        enum MessageType
    {
        BattleStart,
        Win,
        Lose
    }

    void Start () {

        this.gameObject.SetActive(true);
        onRuletteTurn = false;
        buttonStart.SetActive(true);
        buttonStop.SetActive(false);
        message.gameObject.SetActive(false);
        enemyDamage = 0;
        enemy = EnemyManeger.Selected;
        enemyArive = true;
        playerArive = true;
        Init();
        StartCoroutine(AppearMesseage((MessageType)0));
		
	}

    IEnumerator AppearMesseage(MessageType type)
    {
        
        if(type== MessageType.BattleStart)
        {
            message.sprite = messageSprites[0];

            yield return new WaitForSeconds(0.2f);

            message.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            Debug.Log("Waited!");

            message.gameObject.SetActive(false);
        
        }else
        if (type == MessageType.Win)
        {

            message.sprite = messageSprites[1];
            message.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene("SelectBattleGame");


        }
        else
        if(type == MessageType.Lose)
        {

            message.sprite = messageSprites[2];
            message.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene("SelectBattleGame");

        }

    }

    public void Init() {

        body.material.color = enemy.enemyColor;
        Crown.gameObject.SetActive(enemy.enemyLV > 1);

        enemyItem.SetEnemyData(enemy);

    }

    public void OnClickReturn()
    {

        SceneManager.LoadScene("SelectBattleGame");

    }

    public void OnClickStart()
    {

        onRuletteTurn = true;
        buttonStart.SetActive(false);
        buttonStop.SetActive(true);

    }
    public void OnClickStop()
    {

        onRuletteTurn = false;
        buttonStart.SetActive(true);
        buttonStop.SetActive(false);

        if (TryAttack()) enemyArive = enemyItem.UpdataEnemyHp(50);
        else playerArive = playerItem.MissAttac();

        if (enemyArive == false) StartCoroutine(AppearMesseage((MessageType)1));
        if (playerArive == false) StartCoroutine(AppearMesseage((MessageType)2));

    }

    // Update is called once per frame
    void Update()
    {

        if (onRuletteTurn == true)
        {

            ruletteTable.transform.Rotate(0, 0, 5f, Space.World);

        }

    }

    bool TryAttack()
    {

        RectTransform temp = ruletteTable.GetComponent<RectTransform>();
        Vector3 nowRotation = temp.transform.localRotation.eulerAngles;

        if (nowRotation.z >= 0 && nowRotation.z <= 60) return true;
        if (nowRotation.z >= 80 && nowRotation.z <= 100) return true;
        if (nowRotation.z >= 120 && nowRotation.z <= 160) return true;
        if (nowRotation.z >= 180 && nowRotation.z <= 220) return true;
        if (nowRotation.z >= 240 && nowRotation.z <= 280) return true;
        if (nowRotation.z >= 300 && nowRotation.z <= 340) return true;

        return false;


    }

}
