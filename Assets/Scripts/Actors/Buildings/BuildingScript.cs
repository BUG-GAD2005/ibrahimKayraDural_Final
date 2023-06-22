using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    [SerializeField] GameObject ShapePrefab;

    SO_Building buildingData;
    bool isInstantiated;

    void Update()
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

    public void InstantiateBuilding(SO_Building buildingData)
    {
        this.buildingData = buildingData;

        GameObject tempGO = Instantiate(ShapePrefab, Vector3.zero, Quaternion.identity, transform);
        tempGO.transform.localPosition = Vector3.zero;

        if (tempGO.TryGetComponent(out ShapePrefabScript sps))
        {
            sps.CreateShape(this.buildingData.Shape);
        }

        isInstantiated = true;
    }

    void PlaceBuilding()
    {
        Destroy(gameObject);
    }
    void CancelBuilding()
    {
        Destroy(gameObject);
    }
}
