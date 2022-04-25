using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인벤토리 데이터 생성, 저장, 불러오기 담당클래스.
/// </summary>
public class InventoryDataManager
{
    private static InventoryDataManager _instance;
    public static InventoryDataManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InventoryDataManager();
            }

            return _instance;
        }
    }

    public static InventoryData data
    {
        get
        {
            InventoryData tmpData;
            if (!instance.dataDictionary.TryGetValue(GameManager.characterSelected, out tmpData))
            {
                tmpData = LoadData(GameManager.characterSelected);
            }
            return tmpData;
        }

        set
        {
            SaveData(value);
        }
    }
    private Dictionary<CharacterType, InventoryData> dataDictionary = new Dictionary<CharacterType, InventoryData>();


    //============================================================================
    //*************************** Public Methods *********************************
    //============================================================================

    public static void CreateData(CharacterType characterType)
    {
        if (!System.IO.Directory.Exists($"{Application.persistentDataPath}/InventoryDatas"))
            System.IO.Directory.CreateDirectory($"{Application.persistentDataPath}/InventoryDatas");

        string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/Inventory_{characterType}_{LoginManager.nickName}.json";
        InventoryData tmpData = LoadDefaultData();
        string jsonData = JsonConvert.SerializeObject(tmpData, Formatting.Indented);
        Debug.Log($"Inventory data of {characterType} {LoginManager.nickName} Created");
        instance.dataDictionary.Add(characterType, tmpData);
        System.IO.File.WriteAllText(jsonPath, jsonData);
    }

    public static InventoryData LoadDefaultData()
    {
        InventoryData tmpData = null;
        TextAsset textData = Resources.Load<TextAsset>("InventoryDataDefault/Inventory_Default");
        if (textData != null)
            tmpData = JsonConvert.DeserializeObject<InventoryData>(textData.ToString());
        else
            Debug.Log($"Failed to load InventoryData_Default");

        return tmpData;
    }

    public static void SaveData(InventoryData data)
    {
        string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/Inventory_{GameManager.characterSelected}_{LoginManager.nickName}.json";
        //Debug.Log($"save items : {data.items.Count} , {jsonPath}");
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        //Debug.Log($"Inventory data Saved");
        System.IO.File.WriteAllText(jsonPath, jsonData);
    }

    public static InventoryData LoadData(CharacterType characterType)
    {
        InventoryData tmpData = null;
        string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/Inventory_{characterType}_{LoginManager.nickName}.json";
        if (System.IO.File.Exists(jsonPath))
        {
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            tmpData = JsonConvert.DeserializeObject<InventoryData>(jsonData);
            if (instance.dataDictionary.ContainsKey(characterType))
                instance.dataDictionary[characterType] = tmpData;
            else
                instance.dataDictionary.Add(characterType, tmpData);
            //Debug.Log($"Inventory data of {nickName} Loaded");
        }
        else
        {
            CreateData(characterType);
        }
            
        return tmpData;
    }

    public static void LoadAll()
    {
        foreach (var sub in PlayerDataManager.data.charactersData)
        {
            string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/Inventory_{sub.type}_{LoginManager.nickName}.json";
            if (System.IO.File.Exists(jsonPath))
            {
                string jsonData = System.IO.File.ReadAllText(jsonPath);
                if (instance.dataDictionary.ContainsKey(sub.type))
                    instance.dataDictionary[sub.type] = JsonConvert.DeserializeObject<InventoryData>(jsonData);
                else
                    instance.dataDictionary.Add(sub.type, JsonConvert.DeserializeObject<InventoryData>(jsonData));
                //Debug.Log($"Inventory data of {nickName} Loaded");
            }
            else
            {
                CreateData(sub.type);
            }
        }
    }

    public static void RemoveData(string nickName)
    {
        string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/Inventory_{nickName}.json";
        if (System.IO.File.Exists(jsonPath))
            System.IO.File.Delete(jsonPath);
    }

}