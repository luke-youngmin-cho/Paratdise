using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// �÷��̾� �����͸� ����, ���� �� �ҷ�����
/// </summary>
namespace YM
{
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
        public static bool isLoaded
        {
            get
            {
                return data == null ? false : true;
            }
        }

        public static PlayerData data;


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
                playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
                data = playerData;
                Debug.Log($"Successfully loaded Player data of {nickName}");
                DisplayGameState.SetDiscription($"Suceeded to load player data from {jsonPath}");
            }
            else
                DisplayGameState.SetDiscription($"Failed to load player data from {jsonPath}");
            return playerData == null ? false : true;
        }

        public static PlayerData LoadDefaultData()
        {
            PlayerData tmpData = null;

            TextAsset textData = Resources.Load<TextAsset>("PlayerDataDefault/Player_Default");
            if(textData != null)
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

}