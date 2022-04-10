using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/04
/// ���������� : 
/// ���� : 
/// 
/// �κ��丮 â�� �������� �߰��ؼ� ���Կ� ���°��� �����ϴ� Ŭ����
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
    /// �κ��丮 �����Ϳ��� �����ϴ� �������� �̸�����
    /// ItemAssets ���κ��� �ش� ������ ���� �޾ƿ���, 
    /// �� view Ŭ������ Ÿ�԰� �±׿� �´� �����۵鸸 ���Կ� ������.
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