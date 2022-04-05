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
namespace YM
{
    [System.Serializable]
    public class InventoryData
    {
        public List<InventoryItemData> items;

        /// <summary>
        /// 아이템 추가
        /// </summary>
        /// <param name="type"> 아이템 종류 (equip, spend, etc, cash ) </param>
        /// <param name="itemName"> Scriptableobject item 의 이름 </param>
        /// <param name="num"> 수량 </param>
        /// <param name="slotID"> 인벤토리 슬롯 ID </param>
        public void AddData(ItemType type, string itemName, int num, int slotNum)
        {
            InventoryItemData matchedData =
                items.Find(x => x.type == type && x.slotID == slotNum);
            items.Remove(matchedData);
            items.Add(new InventoryItemData { type = type, itemName = itemName, num = num, slotID = slotNum });
            InventoryDataManager.SaveData();
        }

        /// <summary>
        /// try to find item with type & name , remove data
        /// </summary>
        public void RemoveData(ItemType type, string itemName, int slotNum)
        {
            InventoryItemData matchedData =
                items.Find(x => x.type == type && x.itemName == itemName && x.slotID == slotNum);
            items.Remove(matchedData);
            InventoryDataManager.SaveData();
        }
    }


}

