using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/05
/// ���������� : 
/// ���� : 
/// 
/// �������� Ÿ�̸� UI
/// </summary>
public class StageTimer : MonoBehaviour
{
    [SerializeField] private Text timeText;
    // Update is called once per frame
    void Update()
    {
        if (StageManager.instance != null)
            timeText.text = StageManager.instance.GetTimer().ToString();
    }
}
