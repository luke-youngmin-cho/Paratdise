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


[System.Serializable]
public class CharacterData
{
    public CharacterType type;
    public bool isAvailable;
    public int stageSaved;
    public int stageLastPlayed;
}

[System.Serializable]
public enum CharacterType
{
    None,
    Mise,
    Laila,
    DrillGgabijo,
    Eily
}