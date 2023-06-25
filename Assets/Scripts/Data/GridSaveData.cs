using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridSaveData : MonoBehaviour
{
    public int[] occupiedGridIndexes;

    public GridSaveData(GridMaker gridMaker)
    {
        occupiedGridIndexes = gridMaker.GetOccupiedGridIndexes();
    }
}
