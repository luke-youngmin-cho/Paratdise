using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/04
/// ���������� : 
/// ���� : 
/// 
/// �κ��丮�� �������� ��ư
/// </summary>

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryUI;
    public void OnClick()
    {
        inventoryUI.SetActive(true);
    }
}