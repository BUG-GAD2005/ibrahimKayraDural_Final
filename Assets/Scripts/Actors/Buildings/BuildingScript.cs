using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    [SerializeField] GameObject ShapePrefab;
    [SerializeField] GameObject ProgressBarPrefab;

    ProgressBar progressbar;
    SO_Building buildingData;
    BuildingSquare[] buildingSquares;

    float targetSpeed;
    float currentProgress;
    int gemToGenerate, goldToGenerate;

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
        else if(isInstantiated)
        {
            currentProgress = Mathf.Min(currentProgress + targetSpeed * Time.deltaTime, 1);
            RefreshSlider();

            if (currentProgress == 1) GenerateResources();
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

        targetSpeed = 1 / buildingData.GenerateDuration;
        gemToGenerate = buildingData.GemToGenerate;
        goldToGenerate = buildingData.GoldToGenerate;

        isInstantiated = true;
    }

    void GenerateResources()
    {
        GameManager.instance.AddGold(goldToGenerate);
        GameManager.instance.AddGem(gemToGenerate);

        currentProgress = 0;
        RefreshSlider();
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
        }

        CreateProgressBar(buildingSquares[0].OccupiedGridSquare.transform.position);
        transform.position = buildingSquares[0].OccupiedGridSquare.transform.position;

        isPlacable = false;
    }
    void CancelBuilding()
    {
        Destroy(gameObject);
    }

    void CreateProgressBar(Vector3 position)
    {
        GameObject tempGO = Instantiate(ProgressBarPrefab, transform.position, Quaternion.identity, transform);
        Vector3 targetScale = GameManager.instance.Grid.SquareSize * new Vector3(1, 1, 1);
        targetScale.z = 1;

        progressbar = tempGO.GetComponent<ProgressBar>();
        progressbar.Initiate(targetScale, position);
    }

    void RefreshSlider()
    {
        if (progressbar == null) return;

        progressbar.SetValue(currentProgress);
    }
}
