using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Building")]
public class SO_Building : ScriptableObject
{
    [SerializeField] string _buildingName = "UNNAMED";
    [SerializeField] Sprite _menuImage;
    [SerializeField] int _gemCost, _goldCost, _gemToGenerate, _goldToGenerate;
    [SerializeField] float _generateDuration;

    //getters
    public string BuildingName => _buildingName;
    public Sprite MenuImage => _menuImage;
    public int GemCost => _gemCost;
    public int GoldCost => _goldCost;
    public int GemToGenerate => _gemToGenerate;
    public int GoldToGenerate => _goldToGenerate;
    public float GenerateDuration => _generateDuration;
}
