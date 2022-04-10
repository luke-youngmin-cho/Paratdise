using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인벤토리를 열기위한 버튼
/// </summary>

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryUI;
    public void OnClick()
    {
        inventoryUI.SetActive(true);
    }
}