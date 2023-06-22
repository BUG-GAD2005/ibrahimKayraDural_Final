using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] GameObject GridSquarePrefab;

    [Header("Values")]
    [SerializeField] float squareSize = 4;
    [SerializeField] int lengthX = 10, lengthY = 10;
    [SerializeField] float offset = .05f;

    Transform GridSquareParent;
    List<GameObject> GridSquaresAsGo = new List<GameObject>();
    List<GridSquare> GridSquaresAsScript = new List<GridSquare>();

    void Start()
    {
        GridSquareParent = new GameObject("GridSquareParent").transform;
        GridSquareParent.parent = transform;
        GridSquareParent.transform.localPosition = Vector3.zero;

        GenerateGrid();
    }

    public void GenerateGrid()
    {
        DestroyGridSquares();

        GridSquaresAsGo = new List<GameObject>();
        GridSquaresAsScript = new List<GridSquare>();

        for (int y = 0; y < lengthY; y++)
        {
            for (int x = 0; x < lengthX; x++)
            {
                GameObject tempGO = Instantiate(GridSquarePrefab, Vector3.zero, Quaternion.identity, GridSquareParent);
                tempGO.transform.localScale = new Vector3(1, 1, 0) * squareSize;
                float tempGoScale = tempGO.GetComponent<BoxCollider2D>().size.x;

                Vector3 targetPos = GridSquareParent.transform.position;
                targetPos += new Vector3(x * (squareSize * tempGoScale + offset), -y * (squareSize * tempGoScale + offset), 0);
                tempGO.transform.position = targetPos;

                GridSquaresAsGo.Add(tempGO);
                GridSquaresAsScript.Add(tempGO.GetComponent<GridSquare>());
            }
        }
    }

    void DestroyGridSquares() 
    {
        foreach (Transform child in GridSquareParent)
        {
            Destroy(child.gameObject);
        }
    }
}
