using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] GameObject buildingPrefab;

    public static GridMaker gridMaker;
    public static List<BuildingScript> buildingList = new List<BuildingScript>();

    int[] occupiedGridIndexes = null;

    const string GRID_SUB = "/grid";
    const string BUILDING_SUB = "/building";
    const string BUILDING_COUNT_SUB = "/building.count";

    void Awake()
    {
        LoadGrid();
        LoadBuilding();
    }
    void OnApplicationQuit()
    {
        SaveGrid();
        SaveBuilding();
    }

    void SaveGrid()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string gridPath = Application.persistentDataPath + GRID_SUB;

        FileStream gridStream = new FileStream(gridPath, FileMode.Create);
        GridSaveData gridSaveData = new GridSaveData(gridMaker);

        formatter.Serialize(gridStream, gridSaveData);
        gridStream.Close();
    }
    void LoadGrid()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + GRID_SUB;

        if(File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            GridSaveData data = formatter.Deserialize(stream) as GridSaveData;
            stream.Close();

            occupiedGridIndexes = data.occupiedGridIndexes;
            GridSquare[] tempGOs = gridMaker.GridSquaresAsScript.ToArray();

            foreach (int i in occupiedGridIndexes)
            {
                tempGOs[i].Occupy();
            }
        }
    }

    void SaveBuilding()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + BUILDING_SUB;
        string countPath = Application.persistentDataPath + BUILDING_COUNT_SUB;

        FileStream countStream = new FileStream(countPath, FileMode.Create);
        formatter.Serialize(countStream, buildingList.Count);
        countStream.Close();

        for (int i = 0; i < buildingList.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            BuildingSaveData data = new BuildingSaveData(buildingList[i]);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    void LoadBuilding()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + BUILDING_SUB;
        string countPath = Application.persistentDataPath + BUILDING_COUNT_SUB;
        int buildingCount = 0;

        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);

            buildingCount = (int)formatter.Deserialize(countStream);
            countStream.Close();
        }
        else
        {
            Debug.LogError("Count path not found in " + countPath);
        }

        for (int i = 0; i < buildingCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                BuildingSaveData data = formatter.Deserialize(stream) as BuildingSaveData;
                stream.Close();

                BuildingScript buildingScript = Instantiate(buildingPrefab).GetComponent<BuildingScript>();

                SO_Building[] buildingsFound = Resources.LoadAll<SO_Building>("Buildings");
                SO_Building so_building = null;

                foreach (SO_Building b in buildingsFound)
                {
                    if(b.name == data.BuildingDataName)
                    {
                        so_building = b;
                    }
                }

                if (so_building != null)
                {
                    buildingScript.InstantiateBuilding(so_building);
                    buildingScript.PlaceBuildingFromLoad();
                }
            }
            else
            {
                Debug.LogError("Path not found in " + path + i);
            }
        }
    }
}