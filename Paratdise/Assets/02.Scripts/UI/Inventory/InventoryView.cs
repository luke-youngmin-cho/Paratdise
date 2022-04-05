using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 장비, 소비, 기타, 캐시 아이템 창을 가자고있음.
/// </summary>
namespace YM
{
    public class InventoryView : MonoBehaviour
    {
        public static InventoryView instance;
        public bool isReady
        {
            get
            {
                return equipItemsViewInstance.isReady &&
                       spendItemsViewInstance.isReady &&
                       etcItemsViewInstance.isReady &&
                       cashItemsViewInstance.isReady;
            }
        }
        public InventoryItemsView equipItemsViewInstance;
        public InventoryItemsView spendItemsViewInstance;
        public InventoryItemsView etcItemsViewInstance;
        public InventoryItemsView cashItemsViewInstance;


        //============================================================================
        //************************* Public Methods ***********************************
        //============================================================================

        public InventoryItemsView GetItemsViewByItemType(ItemType type)
        {
            InventoryItemsView itemsView = null;
            switch (type)
            {
                case ItemType.Equip:
                    itemsView = equipItemsViewInstance;
                    break;
                case ItemType.Spend:
                    itemsView = spendItemsViewInstance;
                    break;
                case ItemType.ETC:
                    itemsView = etcItemsViewInstance;
                    break;
                case ItemType.Cash:
                    itemsView = cashItemsViewInstance;
                    break;
                default:
                    break;
            }
            return itemsView;
        }


        //============================================================================
        //************************* Private Methods **********************************
        //============================================================================

        private void Awake()
        {
            instance = this;
            equipItemsViewInstance.gameObject.SetActive(true);
            spendItemsViewInstance.gameObject.SetActive(true);
            etcItemsViewInstance.gameObject.SetActive(true);
            cashItemsViewInstance.gameObject.SetActive(true);
        }

        private void Start()
        {
            StartCoroutine(E_Start());
        }

        IEnumerator E_Start()
        {
            yield return new WaitUntil(() => isReady);
            equipItemsViewInstance.gameObject.SetActive(false);
            spendItemsViewInstance.gameObject.SetActive(false);
            etcItemsViewInstance.gameObject.SetActive(false);
            cashItemsViewInstance.gameObject.SetActive(false);
        }

    }
}
