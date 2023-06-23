using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePrefabScript : MonoBehaviour
{
    [SerializeField] GameObject GridSquarePrefab;

    List<BuildingSquare> buildingSquares = new List<BuildingSquare>();
    bool isCreated;

    public BuildingSquare[] CreateShape(SO_Building data)
    {
        if (isCreated) return null;

        ShapeName shape = data.Shape;

        float squareSize = GameManager.instance.Grid.SquareSize;
        float offset = GameManager.instance.Grid.Offset;

        bool[][] shapeBool = BuildingShapes.GetShapeFromShapeName(shape);

        for(int y = 0; y < shapeBool.Length; y++)
        {
            for(int x = 0; x < shapeBool[y].Length; x++)
            {
                if(shapeBool[y][x])
                {
                    GameObject tempGO = Instantiate(GridSquarePrefab, Vector3.zero, Quaternion.identity, transform);
                    tempGO.transform.localScale = new Vector3(1, 1, 0) * squareSize;
                    float tempGoScale = tempGO.GetComponent<BoxCollider2D>().size.x;

                    Vector3 targetPos = transform.position;
                    targetPos += new Vector3(x * (squareSize * tempGoScale + offset), -y * (squareSize * tempGoScale + offset), 0);
                    tempGO.transform.position = targetPos;

                    if(tempGO.TryGetComponent(out BuildingSquare BSq))
                    {
                        buildingSquares.Add(BSq);
                        BSq.SetImage(data);
                    }
                }
            }
        }

        isCreated = true;
        return buildingSquares.ToArray();
    }
}
