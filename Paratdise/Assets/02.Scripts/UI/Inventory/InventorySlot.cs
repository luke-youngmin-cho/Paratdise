using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Fit position for inventory item 
/// </summary>
namespace YM
{
    public class InventorySlot : MonoBehaviour
    {
        public bool isEmpty = true;
        public int num;
        public InventoryItemHandlerBase handler;

        public void SetItemHere(InventoryItemHandlerBase newController)
        {
            if (handler != null)
            {
                InventoryView.instance.GetItemsViewByItemType(handler.item.type).GetSlot(newController.slotNumber).handler = handler;
                handler.slotNumber = newController.slotNumber;
                handler.DeselectItem();
            }

            handler = newController;
            handler.slotNumber = num;
            handler.DeselectItem();
            isEmpty = false;
        }

        public void Clear()
        {
            isEmpty = true;
            handler = null;
        }


    }
}

