using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
    bool _isOccupied;
    public bool IsOccupied => _isOccupied;

    int _index;
    public int Index => _index;

    public void Occupy()
    {
        _isOccupied = true;
    }
    public void UnOccupy()
    {
        _isOccupied = false;
    }
    public void SetIndex(int i) => _index = i;
}
