using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/03
/// ���������� : 
/// ���� : 
/// 
/// ��Ȱ��ư
/// </summary>
public class RespawnButton : MonoBehaviour
{
    public void OnClick()
    {
        StageManager.Respawn();
    }
}
