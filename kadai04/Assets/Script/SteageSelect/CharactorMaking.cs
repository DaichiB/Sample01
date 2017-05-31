using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorMaking : MonoBehaviour
{

    [SerializeField]
    Image charactor;
    [SerializeField]
    List<Toggle> toggles;
    [SerializeField]
    Slider sliderPlayerScale;

    PlayerCharactor selected;
    float playerScale;
    [SerializeField]
    Transform content;

    public void Open()
    {
        bool isSelect = PlayerCharactorManager.Instance.IsSelect;
        this.gameObject.SetActive(!isSelect);
        if (!isSelect)
        {
            int isOn;
            for (isOn = 0; isOn < toggles.Count; isOn++)
                if (toggles[isOn].isOn) break;

            playerScale = sliderPlayerScale.value;
            UpdateScale();
            Select(isOn);
        }
    }

    public void Select(int index)
    {
        PlayerCharactor chara = (PlayerCharactor)index;
        PlayerCharactorData data = PlayerCharactorManager.Instance.SelectData(chara);

        charactor.sprite = data.pictCharactor;

        selected = chara;
    }

    public void ClickSelect()
    {
        PlayerCharactorManager.Instance.PlayerSelected(selected);
        this.gameObject.SetActive(false);
    }

    public void ScaleChange(float scale)
    {
        playerScale = scale;
        UpdateScale();
    }

    void UpdateScale()
    {
        Vector3 newScale = Vector3.one * (playerScale * 0.5f + 0.5f);
        content.localScale = newScale;
    }

}
