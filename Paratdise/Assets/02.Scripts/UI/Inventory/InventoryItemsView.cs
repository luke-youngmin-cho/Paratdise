using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인벤토리 창에 아이템을 추가해서 슬롯에 놓는것을 관리하는 클래스
/// </summary>

public class InventoryItemsView : MonoBehaviour
{
    public new ItemTag tag;
    public ItemType type;

    public Transform itemContent;
    public GameObject slotPrefab;
    private List<GameObject> slots = new List<GameObject>();

    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    /// <summary>
    /// 인벤토리 데이터에서 존재하는 아이템의 이름으로
    /// ItemAssets 으로부터 해당 아이템 정보 받아오고, 
    /// 이 view 클래스의 타입과 태그에 맞는 아이템들만 슬롯에 생성함.
    /// </summary>
    public void RefreshItemList()
    {
        if (InventoryDataManager.data == null) return;

        for (int i = slots.Count - 1; i > -1; i--)
            Destroy(slots[i]);
        slots.Clear();

        foreach (var data in InventoryDataManager.data.itemsData)
        {
            Item item = ItemAssets.instance.GetItemByName(data.itemName);
            if ((item.tag == tag) &&
               (item.type == type) &&
               (data.num > 0))
            {
                GameObject slot = Instantiate(slotPrefab, itemContent);
                slots.Add(slot);
                slot.GetComponent<InventorySlot>().SetInfo(item.icon, item.name, data.num, item.discription);
            }
        }
    }
    

    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void OnEnable()
    {
        RefreshItemList();
    }

}