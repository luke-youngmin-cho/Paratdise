using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/22
/// ���������� : 
/// ���� : 
/// 
/// ������������ �����ϴ� ��ư
/// </summary>
public class NextStageButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.StartStage(GameManager.currentStage + 1);
    }
}
