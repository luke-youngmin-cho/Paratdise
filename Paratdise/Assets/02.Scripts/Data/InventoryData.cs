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
namespace YM
{
    [System.Serializable]
    public class InventoryData
    {
        public List<InventoryItemData> items;

        /// <summary>
        /// ������ �߰�
        /// </summary>
        /// <param name="type"> ������ ���� (equip, spend, etc, cash ) </param>
        /// <param name="itemName"> Scriptableobject item �� �̸� </param>
        /// <param name="num"> ���� </param>
        /// <param name="slotID"> �κ��丮 ���� ID </param>
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

