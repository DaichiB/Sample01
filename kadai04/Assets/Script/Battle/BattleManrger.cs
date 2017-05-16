using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManrger : MonoBehaviour {

    public Renderer body;
    public GameObject Crown;
    public Text enemyHP;
    public Image werknessElement;
    public Slider Defence;
    public Slider MagicRes;

    [SerializeField]
    private Sprite iconFire;
    [SerializeField]
    private Sprite iconIce;
    [SerializeField]
    private Sprite iconThunder;

    [SerializeField]
    GameObject ruletteTable, buttonStart, buttonStop;

    [SerializeField]
    Image message;

    bool onRuletteTurn;

    int enemyDamage;

    EnemyData enemy;

    [SerializeField]
    Sprite[] messageSprites;

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
        Init(EnemyManeger.Selected);
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

            yield return new WaitForSeconds(5f);

            SceneManager.LoadScene("SelectBattleGame");


        }
        else
        if(type == MessageType.Lose)
        {



        }

    }

    public void Init(EnemyData enemyData) {

        enemy = enemyData;
        body.material.color = enemyData.enemyColor;
        Crown.gameObject.SetActive(enemyData.enemyLV > 1);

        enemyHP.text = (enemyData.enemyHP-0)+ "/ " + enemyData.enemyHP;

        switch (enemyData.Werkness)
        {

            case EnemyData.Element.Fire:
                werknessElement.sprite = iconFire;
                break;

            case EnemyData.Element.Ice:
                werknessElement.sprite = iconIce;
                break;
            case EnemyData.Element.Thunder:
                werknessElement.sprite = iconThunder;
                break;
            case EnemyData.Element.None:
                werknessElement.gameObject.SetActive(false);
                break;

        }

        Defence.value = enemyData.enemyDefenceValue;
        MagicRes.value = enemyData.enemyMagicResValue;

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
        
        if (tryAttack())
            UpdataEnemyHp();

    }

    // Update is called once per frame
    void Update()
    {

        if (onRuletteTurn == true)
        {

            ruletteTable.transform.Rotate(0, 0, 5f, Space.World);

        }

    }

    void UpdataEnemyHp()
    {

        enemyDamage =enemyDamage+ 50;
        if (enemyDamage >= enemy.enemyHP) {
            enemyDamage = enemy.enemyHP;
            StartCoroutine(AppearMesseage((MessageType)1));
        }
        
        enemyHP.text = (enemy.enemyHP - enemyDamage) + "/ " + enemy.enemyHP;

    }

    bool tryAttack()
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
