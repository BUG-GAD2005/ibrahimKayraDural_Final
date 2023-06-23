using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] GameObject GridSquarePrefab;
    [SerializeField] GameObject background;

    [Header("Values")]
    [SerializeField] float _squareSize = 2.5f;
    [SerializeField] int lengthX = 10, lengthY = 10;
    [SerializeField] float _offset = .025f;
    [SerializeField] float _backgroundOffset = .1f;

    public float SquareSize => _squareSize;
    public float Offset => _offset;

    Transform GridSquareParent;

    List<GameObject> _gridSquaresAsGo = new List<GameObject>();
    List<GridSquare> _gridSquaresAsScript = new List<GridSquare>();

    public List<GameObject> GridSquaresAsGo => _gridSquaresAsGo;
    public List<GridSquare> GridSquaresAsScript => _gridSquaresAsScript;

    void Start()
    {
        CreateGridParent();

        GenerateGrid();

        if (GridSquaresAsGo.Count > 0)
        {
            if (GridSquaresAsGo[0].TryGetComponent(out BoxCollider2D bc2d))
            {
                Vector3 targetVector = new Vector3(0, 0, 1);
                targetVector.x += lengthX * (bc2d.size.x * _squareSize + _offset) + _backgroundOffset;
                targetVector.y += lengthY * (bc2d.size.y * _squareSize + _offset) + _backgroundOffset;

                background.transform.localScale = targetVector;
                targetVector = new Vector3(background.transform.localScale.x / 2, -background.transform.localScale.y / 2, 0);
                targetVector += new Vector3(-1, 1, 0) * (bc2d.size.x * _squareSize + _backgroundOffset) / 2;
                background.transform.localPosition = targetVector;
            }
        }
    }

    void CreateGridParent()
    {
        GridSquareParent = new GameObject("GridSquareParent").transform;
        GridSquareParent.parent = transform;
        GridSquareParent.transform.localPosition = Vector3.zero;
    }

    public void GenerateGrid()
    {
        DestroyGridSquares();

        _gridSquaresAsGo = new List<GameObject>();
        _gridSquaresAsScript = new List<GridSquare>();

        for (int y = 0; y < lengthY; y++)
        {
            for (int x = 0; x < lengthX; x++)
            {
                GameObject tempGO = Instantiate(GridSquarePrefab, Vector3.zero, Quaternion.identity, GridSquareParent);
                tempGO.transform.localScale = new Vector3(1, 1, 0) * _squareSize;
                float tempGoScale = tempGO.GetComponent<BoxCollider2D>().size.x;

                Vector3 targetPos = GridSquareParent.transform.position;
                targetPos += new Vector3(x * (_squareSize * tempGoScale + _offset), -y * (_squareSize * tempGoScale + _offset), 0);
                tempGO.transform.position = targetPos;

                _gridSquaresAsGo.Add(tempGO);
                _gridSquaresAsScript.Add(tempGO.GetComponent<GridSquare>());
            }
        }

        SendGridToGM();
    }

    public void Clear()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach(GameObject go in buildings)
        {
            Destroy(go);
        }

        foreach(GridSquare gs in _gridSquaresAsScript)
        {
            gs.UnOccupy();
        }
    }
    void DestroyGridSquares() 
    {
        foreach (Transform child in GridSquareParent)
        {
            Destroy(child.gameObject);
        }
    }
    void SendGridToGM()
    {
        if (GameManager.instance == null) return;

        GameManager.instance.SetGrid(this);
    }
}
