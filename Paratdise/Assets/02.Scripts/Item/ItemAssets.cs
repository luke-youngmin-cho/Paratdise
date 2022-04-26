using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/04
/// ���������� : 
/// ���� : 
/// 
/// ������ ���� �Ǵ� ������ �������� ������ �������� Ŭ����
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
            Debug.Log($"������ ���ҽ� �˻��� : {item.itemName} ? {itemName}"); 
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