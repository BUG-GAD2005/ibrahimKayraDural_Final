using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingSaveData : MonoBehaviour
{
    public int PlacedGridIndex;
    public string BuildingDataName;
    public BuildingSaveData(BuildingScript building)
    {
        PlacedGridIndex = building.OccupiedGridSquareIndex;
        BuildingDataName = building.BuildingDataName;
    }
}
