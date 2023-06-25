using UnityEngine;

[System.Serializable]
public class GridSaveData
{
    public int[] occupiedGridIndexes;

    public GridSaveData(GridMaker gridMaker)
    {
        occupiedGridIndexes = gridMaker.GetOccupiedGridIndexes();
    }
}
