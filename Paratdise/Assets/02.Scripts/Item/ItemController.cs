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
    /// 스테이지 내에서 해당 아이템 획득하기위해 호출해야하는 함수
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
