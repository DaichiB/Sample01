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

    // Use this for initialization
    void Start () {

        this.gameObject.SetActive(true);
        Init(EnemyManeger.Selected);

		
	}

    public void Init(EnemyData enemyData) {

        body.material.color = enemyData.enemyColor;
        Crown.gameObject.SetActive(enemyData.enemyLV > 1);
        enemyHP.text = enemyData.enemyHP+ "/ " + enemyData.enemyHP;

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

}
