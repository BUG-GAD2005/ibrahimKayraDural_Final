using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    [SerializeField] GameObject ShapePrefab;

    SO_Building buildingData;
    BuildingSquare[] buildingSquares;
    bool isInstantiated;
    bool isPlacable = true;

    void Update()
    {
        if (isPlacable)
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            transform.position = targetPos;

            if (Input.GetMouseButtonDown(1))
            {
                CancelBuilding();
            }
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding();
            } 
        }
    }

    public void InstantiateBuilding(SO_Building buildingData)
    {
        this.buildingData = buildingData;

        GameObject tempGO = Instantiate(ShapePrefab, Vector3.zero, Quaternion.identity, transform);
        tempGO.transform.localPosition = Vector3.zero;

        if (tempGO.TryGetComponent(out ShapePrefabScript sps))
        {
            buildingSquares = sps.CreateShape(this.buildingData);
        }

        isInstantiated = true;
    }

    void PlaceBuilding()
    {
        if (isPlacable == false) return;
        if (isInstantiated == false)
        {
            CancelBuilding();
            return;
        }

        bool cannotPlace = false;
        foreach (BuildingSquare bs in buildingSquares)
        {
            if (bs.IsPlacable == false)
            {
                cannotPlace = true;
                break;
            }
        }
        if (cannotPlace)
        {
            CancelBuilding();
            return;
        }

        List<Vector3> moveVectors = new List<Vector3>();
        foreach (BuildingSquare bs in buildingSquares)
        {
            bs.Place();
            moveVectors.Add(bs.OccupiedGridSquare.transform.position);
        }

        Vector2 MinMaxPositionX = buildingSquares[0].OccupiedGridSquare.transform.position.x * Vector2.one;
        Vector2 MinMaxPositionY = buildingSquares[0].OccupiedGridSquare.transform.position.y * Vector2.one;

        foreach (Vector3 v in moveVectors)
        {
            MinMaxPositionX.x = Mathf.Min(v.x, MinMaxPositionX.x);
            MinMaxPositionX.y = Mathf.Max(v.x, MinMaxPositionX.y);
            MinMaxPositionY.x = Mathf.Min(v.y, MinMaxPositionY.x);
            MinMaxPositionY.y = Mathf.Max(v.y, MinMaxPositionY.y);
        }


        Vector3 averagePosition = Vector3.zero;
        averagePosition.x = MinMaxPositionX.x + ((MinMaxPositionX.y - MinMaxPositionX.x) / 2);
        averagePosition.y = MinMaxPositionY.x + ((MinMaxPositionY.y - MinMaxPositionY.x) / 2);

        transform.position = buildingSquares[0].OccupiedGridSquare.transform.position;

        isPlacable = false;
    }
    void CancelBuilding()
    {
        Destroy(gameObject);
    }
}
