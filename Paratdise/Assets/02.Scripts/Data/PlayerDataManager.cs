using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어 데이터를 생성, 저장 및 불러오기
/// </summary>
public class PlayerDataManager
{
    private static PlayerDataManager _instance;
    public static PlayerDataManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerDataManager();
            return _instance;
        }
    }

    private static PlayerData _data;
    public static PlayerData data
    {
        get
        {
            if (_data == null)
                _data = LoadData();
            return _data;
        }
        set
        {
            _data = value;
            SaveData();
        }
    }

    private int tryCount = 0;

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public static void CreateData(string nickName)
    {
        data = LoadDefaultData();
        data.nickName = nickName;
        if (!System.IO.Directory.Exists($"{Application.persistentDataPath}/PlayerDatas"))
            System.IO.Directory.CreateDirectory($"{Application.persistentDataPath}/PlayerDatas");
        SaveData(nickName);

        // 테스트용 계정
        if (nickName == "master")
        {
            PlayerData tmpData = data;
            CharacterData tmpCharacterData = data.GetCharacterData(CharacterType.Mice);
            tmpCharacterData.stageSaved = 16;
            tmpCharacterData.stageLastPlayed = 16;
            tmpCharacterData.toolsLevel = new ToolsLevel()
            {
                strengthLevel = 99,
                heightLevel = 1,
                luckLevel = 1,
                widthLevel = 1,
            };
            tmpData.SetCharacterData(tmpCharacterData);
            data = tmpData;
            SaveData();
            Debug.Log("마스터 계정이 생성되었습니다.");
        }
    }

    public static void CreateDefaultData(PlayerData playerData)
    {
        if (!System.IO.Directory.Exists($"{Application.persistentDataPath}/PlayerDataDefault"))
            System.IO.Directory.CreateDirectory($"{Application.persistentDataPath}/PlayerDataDefault");
        string jsonPath = $"{Application.persistentDataPath}/PlayerDataDefault/Player_Default.json";
        string jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);
        System.IO.File.WriteAllText(jsonPath, jsonData);
        Debug.Log($"Player_Default  data is created");
    }

    public static PlayerData LoadData()
    { 
        string jsonPath = $"{Application.persistentDataPath}/PlayerDatas/Player_{LoginManager.nickName}.json";
        if (System.IO.File.Exists(jsonPath))
        {
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            return JsonConvert.DeserializeObject<PlayerData>(jsonData);
        }
        else
            throw new Exception("failed to load player data");
            
    }

    public static bool TryLoadData(string nickName, out PlayerData playerData)
    {
        playerData = null;
        string jsonPath = $"{Application.persistentDataPath}/PlayerDatas/Player_{nickName}.json";
        DisplayGameState.SetDiscription(jsonPath);
        if (System.IO.File.Exists(jsonPath))
        {
            DisplayGameState.SetDiscription($"Trinyg to load player data from {jsonPath}...");
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            DisplayGameState.SetDiscription($"Read to load player data from {jsonPath}...");
            Debug.Log(jsonData);
            playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            data = playerData;
            Debug.Log($"Successfully loaded Player data of {nickName}");
            DisplayGameState.SetDiscription($"Suceeded to load player data from {jsonPath}");
        }
        else
        {
            DisplayGameState.SetDiscription($"Failed to load player data from {jsonPath}");
            if (instance.tryCount < 2)
            {
                CreateData(nickName);
                return TryLoadData(nickName, out playerData);
            }
        }

        instance.tryCount++;
        return playerData == null ? false : true;
    }

    public static PlayerData LoadDefaultData()
    {
        PlayerData tmpData = null;

        TextAsset textData = Resources.Load<TextAsset>("PlayerDataDefault/Player_Default");
        if (textData != null)
            tmpData = JsonConvert.DeserializeObject<PlayerData>(textData.ToString());
        else
            Debug.Log($"Failed to load Player_Default");


        /*string jsonPath = $"{Application.persistentDataPath}/PlayerDataDefault/Player_Default.json";
        if (System.IO.File.Exists(jsonPath))
        {
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            tmpData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
        }
        else
            Debug.Log($"Failed to load Player_Default , Default path -> {jsonPath}");*/

        return tmpData;
    }

    public static void SaveData(string nickName)
    {
        if (data == null) return;
        string jsonPath = $"{Application.persistentDataPath}/PlayerDatas/Player_{nickName}.json";
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        System.IO.File.WriteAllText(jsonPath, jsonData);
        Debug.Log($"Player_{nickName}  data is saved");
    }

    public static void SaveData()
    {
        if (data == null) return;
        
        if (!System.IO.Directory.Exists($"{Application.persistentDataPath}/PlayerDatas"))
        {
            System.IO.Directory.CreateDirectory($"{Application.persistentDataPath}/PlayerDatas");
        }

        string jsonPath = $"{Application.persistentDataPath}/PlayerDatas/Player_{LoginManager.nickName}.json";
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        System.IO.File.WriteAllText(jsonPath, jsonData);
        Debug.Log($"Player_{LoginManager.nickName}  data is saved");
    }

    public static void SaveData(string nickName, PlayerData playerData)
    {
        data = playerData;
        string jsonPath = $"{Application.persistentDataPath}/PlayerDatas/Player_{nickName}.json";
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        System.IO.File.WriteAllText(jsonPath, jsonData);
        Debug.Log($"Player_{nickName}  data is saved");
    }

    public static void RemoveData(string nickName)
    {
        string jsonPath = $"{Application.persistentDataPath}/PlayerDatas/Player_{nickName}.json";
        if (System.IO.File.Exists(jsonPath))
        {
            System.IO.File.Delete(jsonPath);
            Debug.Log($"Player_ {nickName}   data is removed ");
        }
    }
}