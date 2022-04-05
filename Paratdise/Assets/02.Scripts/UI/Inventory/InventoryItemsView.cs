using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Presentor for interaction among inventory item UI <-> ShortKey & Battle Field
/// </summary>
namespace YM
{
    public class InventoryItemsView : MonoBehaviour
    {
        public bool isReady = false;
        public Transform itemContent;
        public GameObject inventoryItemPrefab;
        private List<GameObject> inventoryItems = new List<GameObject>();
        public GameObject slotPrefab;
        [HideInInspector] public InventorySlot[] slots;
        public int totalSlots = 36;
        public GameObject selectedItem;
        public Camera cam;


        //============================================================================
        //************************* Public Methods ***********************************
        //============================================================================

        public void AddItem(Item item, int num)
        {
            int remains = num;
            Debug.Log($"Do Add Item {remains}");
            while (remains > 0)
            {
                InventoryItemHandlerBase handler = FindItemHandlerEnoughSpace(item);
                if (handler != null)
                {
                    Debug.Log("Increase number of exist item controller");
                    if (handler.item.numMax - handler.itemNum >= remains)
                    {
                        handler.itemNum += remains;
                        remains -= remains;
                    }
                    else
                    {
                        handler.itemNum += (handler.item.numMax - handler.itemNum);
                        remains -= (handler.item.numMax - handler.itemNum);
                    }
                }
                else
                {
                    InventorySlot slot = FindEmptySlot();
                    Debug.Log("Newly create Inventory item controller");
                    if (slot != null)
                    {
                        GameObject go = Instantiate(inventoryItemPrefab, slot.transform);
                        handler = go.GetComponent<InventoryItemHandlerBase>();
                        handler.item = item;
                        handler.slotNumber = slot.num;
                        handler.inventoryItemObject = go;
                        handler.itemPrefab = ItemAssets.instance.GetItemPrefabByName(item.itemName);
                        if (handler.item.numMax - handler.itemNum >= remains)
                        {
                            handler.itemNum += remains;
                            remains -= remains;
                        }
                        else
                        {
                            handler.itemNum += (handler.item.numMax - handler.itemNum);
                            remains -= (handler.item.numMax - handler.itemNum);
                        }
                        slot.SetItemHere(handler);
                        AddItemToList(go);
                        Debug.Log("Newly create Inventory item added to list");
                        // update data
                        //InventoryDataManager.instance.data.AddData(item.type, item.itemName, num, slot.num);
                    }
                }
            }
        }

        public void SetItem(Item item, int num, int slotNum)
        {
            InventorySlot slot = GetSlot(slotNum);
            if (slot != null)
            {
                GameObject go = Instantiate(inventoryItemPrefab, slot.transform);
                InventoryItemHandlerBase handler = go.GetComponent<InventoryItemHandlerBase>();
                handler.item = item;
                handler.slotNumber = slot.num;
                handler.inventoryItemObject = go;
                handler.itemPrefab = ItemAssets.instance.GetItemPrefabByName(item.itemName);
                handler.itemNum = num;
                slot.SetItemHere(handler);
                AddItemToList(go);
            }
        }
        public InventoryItemHandlerBase GetInventoryItemHandlerBySlotNum(int slotNum)
        {
            InventoryItemHandlerBase[] handlers = GetComponentsInChildren<InventoryItemHandlerBase>();
            foreach (var handler in handlers)
            {
                if (handler.slotNumber == slotNum)
                {
                    return handler;
                }
            }
            return null;
        }

        public bool TryGetInventoryItemHandlerByName(string itemName, out InventoryItemHandlerBase inventoryItemHandlerBase)
        {
            inventoryItemHandlerBase = null;
            foreach (var slot in slots)
            {
                if (slot.handler != null &&
                    slot.handler.item != null)
                {
                    Debug.Log($"{ slot.handler.item.itemName } {itemName}");
                    if (slot.handler.item.itemName == itemName)
                    {
                        inventoryItemHandlerBase = slot.handler;
                        return true;
                    }
                }
            }
            return false;
        }

        public InventorySlot GetSlot(int slotNum)
        {
            return slots[slotNum];
        }

        public void ResetSlot(int slotNum)
        {
            slots[slotNum].Clear();
        }

        public void AddItemToList(GameObject go)
        {
            inventoryItems.Add(go);
        }

        public void RemoveItemFromList(GameObject go)
        {
            inventoryItems.Remove(go);
        }


        //============================================================================
        //************************* Private Methods **********************************
        //============================================================================

        private void Start()
        {
            for (int i = 0; i < totalSlots; i++)
            {
                InventorySlot tmpSlot = Instantiate(slotPrefab, itemContent).GetComponent<InventorySlot>();
                tmpSlot.num = i;
            }
            slots = GetComponentsInChildren<InventorySlot>();
            isReady = true;
        }

        private void Update()
        {
            if (selectedItem != null)
            {
                Vector3 pos = Input.mousePosition;
                selectedItem.transform.position = pos;
            }
        }

        private InventorySlot FindEmptySlot()
        {
            InventorySlot slot = null;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].isEmpty)
                {
                    slot = slots[i];
                    break;
                }
            }
            return slot;
        }

        private InventoryItemHandlerBase FindItemHandlerEnoughSpace(Item item)
        {
            InventoryItemHandlerBase controller = null;
            InventoryItemHandlerBase[] controllers = GetComponentsInChildren<InventoryItemHandlerBase>();
            //Debug.Log(controllers.Length);
            for (int i = 0; i < controllers.Length; i++)
            {
                if ((item.itemName == controllers[i].item.itemName) &&
                    (controllers[i].addPossibleNum > 0))
                {
                    controller = controllers[i];
                    break;
                }
            }
            return controller;
        }
    }

}
