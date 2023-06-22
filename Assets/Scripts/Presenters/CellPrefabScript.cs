using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellPrefabScript : MonoBehaviour
{
    [SerializeField] Image BuildingImage;
    [SerializeField] Button _button;
    [SerializeField] GameObject OnOffFilter;
    [SerializeField] TextMeshProUGUI GoldCostText;
    [SerializeField] TextMeshProUGUI GemCostText;

    GameManager gm;
    int goldCost, gemCost;
    bool _isEnabled;

    public bool IsEnabled => _isEnabled;
    public Button Buton => _button;

    public void SetValues(Sprite buildingSprite, int goldCost, int gemCost)
    {
        BuildingImage.sprite = buildingSprite;

        GoldCostText.text = goldCost.ToString();
        this.goldCost = goldCost;

        GemCostText.text = gemCost.ToString();
        this.gemCost = gemCost;

        gm = GameManager.instance;

        if (gm != null)
        {
            gm.event_ValueChanged_Gold += event_GoldValueChanged;
            gm.event_ValueChanged_Gem += event_GemValueChanged;
        }
    }

    void event_GemValueChanged(object sender, int e)
    {
        ToggleCellEnableness(CostIsAffordable());
    }
    void event_GoldValueChanged(object sender, int e)
    {
        ToggleCellEnableness(CostIsAffordable());
    }

    bool CostIsAffordable()
    {
        if (gm == null) return false;

        int playerGold = gm.Gold;
        int playerGem = gm.Gem;

        return goldCost <= playerGold && gemCost <= playerGem;
    }
    void ToggleCellEnableness(bool setTo)
    {
        OnOffFilter.SetActive(!setTo);
        if (_button != null) _button.interactable = setTo;

        _isEnabled = setTo;
    }
}
