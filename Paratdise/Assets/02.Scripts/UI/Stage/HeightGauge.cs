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
/// �÷��̾��� ���� ���̸� ��Ÿ���� ��
/// </summary>
public class HeightGauge : MonoBehaviour
{
    [SerializeField] private Slider slider;
    void Update()
    {
        if (Player.instance != null && 
            MapCreater.instance != null)
            slider.value = (Player.instance.transform.position.y - MapCreater.instance.mapTile_Start.position.y) / (MapCreater.mapHeight - 2);
    }
}
