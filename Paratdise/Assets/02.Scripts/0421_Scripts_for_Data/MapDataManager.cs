using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵 데이터의 생성, 저장 및 불러오기
/// </summary>

public class MapDataManager
{
    private static MapDataManager _instance;
    public static MapDataManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new MapDataManager();
            return _instance;
        }
    }

    public static bool isLoaded
    {
        get
        {
            return data == null ? false : true;
        }
    }

    public static MapData data;


    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public static void CreateData(int stage, MapData mapData)
    {
        if (!System.IO.Directory.Exists($"{Application.persistentDataPath}/MapDatas"))
            System.IO.Directory.CreateDirectory($"{Application.persistentDataPath}/MapDatas");

        string jsonPath = $"{Application.persistentDataPath}/MapDatas/Map_{stage}.json";
        string jsonData = JsonConvert.SerializeObject(mapData, Formatting.Indented);
        System.IO.File.WriteAllText(jsonPath, jsonData);
        Debug.Log($"Map data of stage {stage} is saved");
    }

    public static void CreateDefaultData(int stage, MapData mapData)
    {
        if (!System.IO.Directory.Exists($"{Application.persistentDataPath}/MapDatasDefault"))
            System.IO.Directory.CreateDirectory($"{Application.persistentDataPath}/MapDatasDefault");

        string jsonPath = $"{Application.persistentDataPath}/MapDatasDefault/Map_{stage}.json";
        string jsonData = JsonConvert.SerializeObject(mapData,
                                                      Formatting.None,
                                                      new JsonSerializerSettings()
                                                      {
                                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                      });
        System.IO.File.WriteAllText(jsonPath, jsonData);
        Debug.Log($"Map data of stage {stage} is created as default");
    }

    public static bool TryLoadMapData(int stage, out MapData mapData)
    {
        mapData = null;
        string jsonPath = $"{Application.persistentDataPath}/MapDatas/Map_{stage}.json";
        if (System.IO.File.Exists(jsonPath))
        {
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            mapData = JsonConvert.DeserializeObject<MapData>(jsonData);
            data = mapData;
        }
        return mapData == null ? false : true;
    }

    public static MapData LoadDefaultMapData(int stage)
    {
        MapData tmpData = null;
        TextAsset textData = Resources.Load<TextAsset>($"MapDatasDefault/Map_{stage}");
        if (textData != null)
            tmpData = JsonConvert.DeserializeObject<MapData>(textData.ToString());
        else
            Debug.Log($"Failed to load map_Default");

        return tmpData;
    }

    public static void SaveMapData(int stage, MapData mapData)
    {
        if (data == null) return;
        string jsonPath = $"{Application.persistentDataPath}/MapDatas/Map_{stage}.json";
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        System.IO.File.WriteAllText(jsonPath, jsonData);
        Debug.Log($"Map data of stage {stage} is saved");
    }

    public static void RemoveMapData(int stage)
    {
        string jsonPath = $"{Application.persistentDataPath}/MapDatas/Map_{stage}.json";
        if (System.IO.File.Exists(jsonPath))
        {
            System.IO.File.Delete(jsonPath);
            Debug.Log($"Map data of {stage} Removed");
        }
    }
}