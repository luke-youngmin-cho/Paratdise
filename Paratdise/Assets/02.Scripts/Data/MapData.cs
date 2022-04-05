using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// �� ������
/// </summary>
namespace YM
{
    [System.Serializable]
    public class MapData : ScriptableObject
    {
        public List<MapElement> MapElements_Boundary; // ��� ��Ÿ�� ����Ʈ
        public List<MapElement> MapElements_Basic; // �⺻ ��Ÿ�� ����Ʈ
        public List<MapElement> MapElements_FluidBundle; // ��ü �� ��� ����Ʈ
        public List<MapElement> MapElements_Event; // �̺�Ʈ ��� ����Ʈ
        public MapElement MapElement_Start; // ���� ��Ÿ�� 
        public MapElement MapElement_End; // �� ��Ÿ�� 
    }
}

