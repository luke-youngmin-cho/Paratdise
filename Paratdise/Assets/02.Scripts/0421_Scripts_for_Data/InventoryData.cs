using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// �κ��丮�� ������ ������ ��� Ŭ����
/// </summary>

[System.Serializable]
public class InventoryData
{
    public List<ItemData> itemsData = new List<ItemData>();

    /// <summary>
    /// ������ �߰�
    /// ������ �ش� ������ ������ ������� ������ �ø�.
    /// ������� ���� ���� �߰�
    /// </summary>
    public void AddData(ItemData item)
    {
        ItemData tmpData;
        if (itemsData.Exists(x => (x.itemName == item.itemName)))
        {
            tmpData = itemsData.Find(x => (x.itemName == item.itemName));
            itemsData.Remove(tmpData);
            tmpData.num += item.num;
            itemsData.Add(tmpData);
        }
        else
        {
            tmpData = new ItemData()
            {
                itemName = item.itemName,
                num = item.num,
            };
            itemsData.Add(tmpData);
        }
        InventoryDataManager.SaveData(this);
    }

    /// <summary>
    /// ������ ����.
    /// ������ �������� �����Ϳ� ������ 
    /// ������ ���ؼ� ���� ������ ������ �� ������� ������ ������ ����
    /// �ƴϸ� �ƿ� ����
    /// </summary>
    public void RemoveData(ItemData item)
    {
        if (itemsData.Exists(x => (x.itemName == item.itemName)))
        {
            ItemData tmpData = itemsData.Find(x => (x.itemName == item.itemName));
            itemsData.Remove(tmpData);
            if(tmpData.num > item.num)
            {
                tmpData.num -= item.num;
                itemsData.Add(tmpData);
            }   
            InventoryDataManager.SaveData(this);
        }
    }
}