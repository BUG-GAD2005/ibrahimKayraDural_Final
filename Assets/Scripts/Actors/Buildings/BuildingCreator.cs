using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : MonoBehaviour
{
    [SerializeField] GameObject BuildingPrefab;

    public void CreateBuildingUnderCursor(SO_Building data)
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject tempGO = Instantiate(BuildingPrefab, targetPos, Quaternion.identity);
        if(tempGO.TryGetComponent(out BuildingScript bs))
        {
            bs.isPlacable = true;
            bs.InstantiateBuilding(data);
        }
    }
}
