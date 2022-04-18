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

public class ItemController : MonoBehaviour
{
    public Item item;
    public int num = 1;
    public bool pickUpEnable = false;
    public bool isPickedUp = false;

    //============================================================================
    //*************************** Public Methods *********************************
    //============================================================================

    /// <summary>
    /// �������� ������ �ش� ������ ȹ���ϱ����� ȣ���ؾ��ϴ� �Լ�
    /// </summary>    
    public void PickUp(Test_Player player)
    {
        if (pickUpEnable == false || isPickedUp) return;
        isPickedUp = true;
        InventoryData data = InventoryDataManager.data;
        StageManager.EarnItem(item);
        Destroy(gameObject);
    }

}
