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
namespace YM
{
    public class InventoryButton : MonoBehaviour
    {
        public void OnClick()
        {
            PreloadedUIManager.instance.inventoryView.SetActive(true);
        }
    }

}
