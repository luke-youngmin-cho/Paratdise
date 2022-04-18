using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인벤토리의 아이템 데이터 목록 클래스
/// </summary>

[System.Serializable]
public class InventoryData
{
    public List<ItemData> itemsData = new List<ItemData>();

    /// <summary>
    /// 아이템 추가
    /// 기존에 해당 아이템 데이터 있을경우 개수만 늘림.
    /// 없을경우 새로 만들어서 추가
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
    /// 아이템 삭제.
    /// 삭제할 아이템이 데이터에 있으면 
    /// 수량을 비교해서 원래 데이터 수량이 더 많을경우 아이템 개수만 수정
    /// 아니면 아예 삭제
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