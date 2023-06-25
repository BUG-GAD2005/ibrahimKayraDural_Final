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
    public bool isPlacable = false;

    int _occupiedGridSquareIndex = -1;
    public int OccupiedGridSquareIndex => _occupiedGridSquareIndex;
    string _buildingDataName = "UNKNOWN";
    public string BuildingDataName => _buildingDataName;

    void Awake()
    {
        SaveLoadManager.buildingList.Add(this);
    }
    void Update()
    {
        if (isPlacable)
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            transform.position = targetPos;

            if (Input.GetMouseButtonDown(1))
            {
                DestroyBuilding();
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

    public void InstantiateBuilding(SO_Building buildingData, int placedGridIndex = -1)
    {
        this.buildingData = buildingData;
        _buildingDataName = buildingData.name;

        GameObject tempGO = Instantiate(ShapePrefab, Vector3.zero, Quaternion.identity, transform);
        tempGO.transform.localPosition = Vector3.zero;

        if (tempGO.TryGetComponent(out ShapePrefabScript sps))
        {
            buildingSquares = sps.CreateShape(this.buildingData);
        }

        targetSpeed = 1 / buildingData.GenerateDuration;
        gemToGenerate = buildingData.GemToGenerate;
        goldToGenerate = buildingData.GoldToGenerate;

        if(placedGridIndex != -1)
        {
            _occupiedGridSquareIndex = placedGridIndex;
        }

        isInstantiated = true;
    }
    public void PlaceBuildingFromLoad()
    {
        if (OccupiedGridSquareIndex == -1) return;

        Vector3 targetPos = GameManager.instance.Grid.GetPositionFromIndex(OccupiedGridSquareIndex);
        transform.position = targetPos;

        PlaceProgressBar();
        isPlacable = false;
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
            DestroyBuilding();
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
            DestroyBuilding();
            return;
        }

        foreach (BuildingSquare bs in buildingSquares)
        {
            bs.Place();
        }

        PlaceProgressBar();

        isPlacable = false;
    }

    public void DestroyBuilding()
    {
        SaveLoadManager.buildingList.Remove(this);

        Destroy(gameObject);
    }

    void PlaceProgressBar()
    {
        GridSquare tempGS = buildingSquares[0]?.OccupiedGridSquare;

        if (tempGS != null)
        {
            CreateProgressBar(tempGS.transform.position);
            transform.position = tempGS.transform.position;
            _occupiedGridSquareIndex = tempGS.Index;
        }
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
