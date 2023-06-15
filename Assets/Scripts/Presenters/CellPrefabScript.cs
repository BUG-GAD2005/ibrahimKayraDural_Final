using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellPrefabScript : MonoBehaviour
{
    [SerializeField] Image BuildingImage;
    [SerializeField] TextMeshProUGUI GoldCostText;
    [SerializeField] TextMeshProUGUI GemCostText;

    public void SetValues(Sprite buildingSprite, float goldCost, float gemCost)
    {
        BuildingImage.sprite = buildingSprite;
        GoldCostText.text = goldCost.ToString();
        GemCostText.text = gemCost.ToString();
    }
}
