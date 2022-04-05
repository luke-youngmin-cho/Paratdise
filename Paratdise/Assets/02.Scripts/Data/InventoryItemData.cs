using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인벤토리의 아이템 데이터
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

