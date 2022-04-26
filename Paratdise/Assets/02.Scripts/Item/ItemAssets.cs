using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 아이템 정보 또는 아이템 프리팹을 가져다 쓰기위한 클래스
/// </summary>
public class ItemAssets : MonoBehaviour
{
    private static ItemAssets _instance;
    public static ItemAssets instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<ItemAssets>("Assets/ItemAssets"));
                DontDestroyOnLoad(_instance.gameObject);
            }                
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public List<Item> items = new List<Item>();
    public List<GameObject> itemPrefabs = new List<GameObject>();

    public Item GetItemByName(string itemName)
    {
        Item tmpItem = null;
        foreach (var item in items)
        {
            Debug.Log($"아이템 리소스 검색중 : {item.itemName} ? {itemName}"); 
            if (item.itemName == itemName)
            {
                tmpItem = item;
                break;
            }
        }
        return tmpItem;
    }
    public GameObject GetItemPrefabByName(string itemName)
    {
        GameObject tmpPrefab = null;
        foreach (var item in itemPrefabs)
        {
            if (item.GetComponent<ItemController>().item.itemName == itemName)
                tmpPrefab = item;
        }
        return tmpPrefab;
    }

}