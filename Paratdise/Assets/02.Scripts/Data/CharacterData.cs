using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// ĳ������ Ÿ�԰� �ر� ���� ������ 
/// </summary>
namespace YM
{
    [System.Serializable]
    public struct CharacterData
    {
        public CharacterType type;
        public bool isAvailable;
    }

    [System.Serializable]
    public enum CharacterType
    {
        None,
        Mise,
        Laila,
        ggabirilldjo,
        Eily
    }
}
