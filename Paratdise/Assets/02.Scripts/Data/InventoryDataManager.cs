using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인벤토리 데이터 생성, 저장, 불러오기 담당클래스.
/// </summary>
namespace YM
{
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

        public static InventoryData data;
        public static bool isLoaded
        {
            get
            {
                return data != null;
            }
        }
        public static bool isApplied = false;


        //============================================================================
        //*************************** Public Methods *********************************
        //============================================================================

        public static void CreateData(string nickName)
        {
            if (!System.IO.Directory.Exists($"{Application.persistentDataPath}/InventoryDatas"))
                System.IO.Directory.CreateDirectory($"{Application.persistentDataPath}/InventoryDatas");

            string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/Inventory_{nickName}.json";
            data = LoadDefaultData();
            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            Debug.Log($"Inventory data of {nickName} Created");
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

        public static void SaveData()
        {
            if (data == null) return;

            string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/Inventory_{PlayerDataManager.data.nickName}.json";
            //Debug.Log($"save items : {data.items.Count} , {jsonPath}");
            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            //Debug.Log($"Inventory data Saved");
            System.IO.File.WriteAllText(jsonPath, jsonData);
        }

        public static void LoadData(string nickName)
        {
            string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/Inventory_{nickName}.json";
            if (System.IO.File.Exists(jsonPath))
            {
                string jsonData = System.IO.File.ReadAllText(jsonPath);
                data = JsonConvert.DeserializeObject<InventoryData>(jsonData);
                //Debug.Log($"Inventory data of {nickName} Loaded");
            }
            else
                Debug.LogError($"Failed to load : InventoryData ,{nickName} -> {jsonPath}");
        }

        public static bool TryLoadData(string nickName, out InventoryData inventoryData)
        {
            inventoryData = null;
            string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/Inventory_{nickName}.json";
            if (System.IO.File.Exists(jsonPath))
            {
                string jsonData = System.IO.File.ReadAllText(jsonPath);
                data = inventoryData = JsonConvert.DeserializeObject<InventoryData>(jsonData);
                //Debug.Log($"Inventory data of {nickName} Loaded");
            }
            return inventoryData != null;
        }

        public static void ApplyData()
        {
            for (int i = 0; i < data.items.Count; i++)
            {
                Item item = ItemAssets.instance.GetItemByName(data.items[i].itemName);
                //Debug.Log($"Applying Inventory Data , item : {item != null}, {data.items[i].itemName} {data.items[i].num}, {data.items[i].slotID}");
                InventoryView.instance.GetItemsViewByItemType(item.type).SetItem(item,
                                                                                 data.items[i].num,
                                                                                 data.items[i].slotID);
                //Debug.Log($"Inventory data set {data.items[i].num},{data.items[i].slotNum}");
            }
            //Debug.Log($"Inventory data Applied");
            isApplied = true;
        }

        public static void RemoveData(string nickName)
        {
            string jsonPath = $"{Application.persistentDataPath}/InventoryDatas/Inventory_{nickName}.json";
            if (System.IO.File.Exists(jsonPath))
                System.IO.File.Delete(jsonPath);
        }

    }



}
