using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인벤토리 내의 아이템을 유저가 다룰 수 있도록 함.
/// </summary>
namespace YM
{
    public class InventoryItemHandlerBase : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector] public int slotNumber = -1;
        public GameObject inventoryItemObject;
        [HideInInspector] public Item item;
        [HideInInspector] public GameObject itemPrefab;

        private int _itemNum;
        public int itemNum
        {
            set
            {
                _itemNum = value;
                if (_itemNum > 1)
                    inventoryItemObject.transform.GetChild(2).GetComponent<Text>().text = _itemNum.ToString();
                else if (_itemNum == 1)
                    inventoryItemObject.transform.GetChild(2).GetComponent<Text>().text = "";
                else
                {
                    InventoryView.instance.GetItemsViewByItemType(item.type).GetSlot(slotNumber).Clear();
                    InventoryDataManager.data.RemoveData(item.type, item.name, slotNumber);
                    Destroy(this.gameObject);
                }
                if (GameManager.gameState > GameState.LoadPlayerData)
                {
                    if (InventoryDataManager.data == null)
                        Debug.Log("no inventotry data");
                    InventoryDataManager.data.AddData(item.type, item.name, _itemNum, slotNumber);
                }                    
            }
            get
            {
                return _itemNum;
            }
        }
        public int addPossibleNum
        {
            get
            {
                return item.numMax - itemNum;
            }
        }

        // UI Raycast event
        [HideInInspector] public GraphicRaycaster _Raycaster;
        [HideInInspector] public PointerEventData _PointerEventData;
        [HideInInspector] public EventSystem _EventSystem;


        //============================================================================
        //************************* Public Methods ***********************************
        //============================================================================
        public void SelectItem()
        {
            InventoryView.instance.GetItemsViewByItemType(item.type).selectedItem = this.gameObject;
            transform.SetParent(PreloadedUIManager.instance.transform);
        }
        public void DeselectItem()
        {
            InventoryView.instance.GetItemsViewByItemType(item.type).selectedItem = null;
            InventorySlot slot = InventoryView.instance.GetItemsViewByItemType(item.type).GetSlot(slotNumber);
            transform.SetParent(slot.transform);
            transform.position = transform.parent.position;
        }

        virtual public void UseItem()
        {
            if (itemNum < 1) return;
            itemPrefab.GetComponent<ItemController>().OnUseEvent();
            itemNum--;
        }

        virtual public void DropItem()
        {
            GameObject go = Instantiate(itemPrefab, Player.instance.transform.position, Quaternion.identity);
            go.GetComponent<ItemController>().num = _itemNum;

            go.transform.SetParent(null);
            InventoryView.instance.GetItemsViewByItemType(item.type).GetSlot(slotNumber).Clear();
            InventoryDataManager.data.RemoveData(item.type, item.name, slotNumber);
            Destroy(this.gameObject);
        }

        virtual public void OnPointerClick(PointerEventData eventData)
        {
            if (InventoryView.instance.GetItemsViewByItemType(item.type).selectedItem == null)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                    SelectItem();
                else if (eventData.button == PointerEventData.InputButton.Right)
                    UseItem();
            }
            else
            {
                if (eventData.button == PointerEventData.InputButton.Right)
                    DeselectItem();
                else if (eventData.button == PointerEventData.InputButton.Left)
                {
                    _PointerEventData = new PointerEventData(_EventSystem);
                    _PointerEventData.position = Input.mousePosition;
                    List<RaycastResult> results = new List<RaycastResult>();
                    _Raycaster.Raycast(_PointerEventData, results);

                    InventorySlot slot = null;
                    CanvasRenderer canvasRenderer = null;
                    foreach (RaycastResult result in results)
                    {
                        //Check InventorySlot
                        if (result.gameObject.TryGetComponent(out InventorySlot tmpSlot))
                        {
                            slot = tmpSlot;
                        }
                        //Check All UI. (if not exist, drop item to field)
                        if (result.gameObject.TryGetComponent<CanvasRenderer>(out CanvasRenderer tmpCanvasRenderer))
                        {
                            if (tmpCanvasRenderer.gameObject != this.gameObject)
                                canvasRenderer = tmpCanvasRenderer;
                        }
                        Debug.Log(result.gameObject.name);
                    }
                    // Clicked on slot
                    if (slot != null)
                    {
                        InventoryView.instance.GetItemsViewByItemType(item.type).GetSlot(slotNumber).Clear();
                        slot.SetItemHere(this);
                    }

                    if (canvasRenderer == null)
                        DropItem();
                    else
                        Debug.Log(canvasRenderer.name);
                }
            }
        }


        //============================================================================
        //************************* Private Methods **********************************
        //============================================================================
        private void Start()
        {
            _Raycaster = PreloadedUIManager.instance.GetComponent<GraphicRaycaster>();
            _EventSystem = FindObjectOfType<EventSystem>();
            inventoryItemObject.transform.GetChild(1).GetComponent<Image>().sprite = item.icon;
            itemPrefab = ItemAssets.instance.GetItemPrefabByName(item.itemName);
        }


    }

}
