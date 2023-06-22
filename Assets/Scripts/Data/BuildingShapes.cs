using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShapeName { TwoGridLine, SquareL, Square, HornedSquare }
public abstract class BuildingShapes : MonoBehaviour
{
    public static bool[][] GetShapeFromShapeName(ShapeName shapeName)
    {
        return ShapeDictionary[shapeName];
    }

    static Dictionary<ShapeName, bool[][]> ShapeDictionary = new Dictionary<ShapeName, bool[][]>
    {
        {ShapeName.TwoGridLine, new bool[][]{new bool[]{true,true}}},
        {ShapeName.SquareL, new bool[][]{new bool[]{true,false},new bool[]{true,true}} },
        {ShapeName.Square, new bool[][]{new bool[]{true,true},new bool[]{true,true}}},
        {ShapeName.HornedSquare, new bool[][]{new bool[]{true,true,true},new bool[]{true,true,false}} }
    };
}
