using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/22
/// ���������� : 
/// ���� : 
/// 
/// �ٽ� �������� �����ϴ� ��ư
/// </summary>
public class ReplayButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.StartStage(GameManager.currentStage);
    }
}
