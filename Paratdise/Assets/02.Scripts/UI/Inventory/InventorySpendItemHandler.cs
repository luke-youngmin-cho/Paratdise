using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 소비 인벤토리 아이템을 유저가 다루기 위한 확장 클래스.
/// </summary>
namespace YM
{
    public class InventorySpendItemHandler : InventoryItemHandlerBase
    {
        override public void OnPointerClick(PointerEventData eventData)
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
                        InventorySlot tmpSlot = null;
                        if (result.gameObject.TryGetComponent(out tmpSlot))
                        {
                            slot = tmpSlot;
                        }
                        //Check All UI. (if not exist, drop item to field)
                        CanvasRenderer tmpCanvasRenderer = null;
                        if (result.gameObject.TryGetComponent<CanvasRenderer>(out tmpCanvasRenderer))
                        {
                            if (tmpCanvasRenderer.gameObject != this.gameObject)
                                canvasRenderer = tmpCanvasRenderer;
                        }
                        Debug.Log(result.gameObject.name);
                    }
                    // Clicked on slot
                    if (slot != null)
                        slot.SetItemHere(this);

                    if (canvasRenderer == null)
                        DropItem();
                    else
                        Debug.Log(canvasRenderer.name);
                }
            }
        }

    }

}
