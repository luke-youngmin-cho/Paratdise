using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// �κ��丮�� ������ ������
/// </summary>
namespace YM
{
    [System.Serializable]
    public struct InventoryItemData
    {   
        public ItemType type;
        public string itemName;
        public int num;
        public int slotID;
    }
}

