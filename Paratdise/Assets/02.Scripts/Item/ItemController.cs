using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/04
/// ���������� : 
/// ���� : 
/// 
/// ��Ʋ�ʵ忡 ���������� �������� ����Ŭ����. 
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
