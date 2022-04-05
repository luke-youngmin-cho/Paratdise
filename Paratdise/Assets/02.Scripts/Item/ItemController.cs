using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 배틀필드에 떨어져있을 아이템을 위한클래스. 
/// </summary>
namespace YM
{
    public class ItemController : MonoBehaviour
    {
        public Item item;
        public int num = 1;
        public bool pickUpEnable = false;
        public bool isPickedUp = false;

        //============================================================================
        //*************************** Public Methods *********************************
        //============================================================================

        public virtual void OnUseEvent()
        {
            // Override this method for every single of item.
        }

        public void PickUp(Player player)
        {
            if (pickUpEnable == false || isPickedUp) return;
            isPickedUp = true;
            InventoryView.instance.GetItemsViewByItemType(item.type).AddItem(item, num);
            Destroy(gameObject);
        }

    }

}
