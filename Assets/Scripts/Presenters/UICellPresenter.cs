using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICellPresenter : MonoBehaviour
{
    [SerializeField] GameObject CellPrefab;
    [SerializeField] Transform CellParent;

    void Start()
    {
        SO_Building[] buildingsFound = Resources.LoadAll<SO_Building>("Buildings");

        foreach (SO_Building b in buildingsFound) AddCell(b);
    }

    void AddCell(SO_Building cellData)
    {
        GameObject instantiatedCell = Instantiate(CellPrefab, CellParent);

        if (instantiatedCell.TryGetComponent(out CellPrefabScript cps))
            cps.SetValues(cellData.BuildingSprite, cellData.GoldCost, cellData.GemCost);
        else
            Destroy(instantiatedCell);
    }
}
