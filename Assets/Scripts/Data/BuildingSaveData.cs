using UnityEngine;

[System.Serializable]
public class BuildingSaveData
{
    public int PlacedGridIndex;
    public string BuildingDataName;
    public BuildingSaveData(BuildingScript building)
    {
        PlacedGridIndex = building.OccupiedGridSquareIndex;
        BuildingDataName = building.BuildingDataName;
    }
}
