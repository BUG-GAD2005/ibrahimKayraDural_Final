using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
    bool _isOccupied;
    public bool IsOccupied => _isOccupied;

    public void Occupy()
    {
        _isOccupied = true;
    }
    public void UnOccupy()
    {
        _isOccupied = false;
    }
}
