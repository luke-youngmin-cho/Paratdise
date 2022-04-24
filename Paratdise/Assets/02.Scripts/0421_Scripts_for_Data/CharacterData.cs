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
    public ToolsLevel toolsLevel;
    public History history;
}

[System.Serializable]
public enum CharacterType
{
    None,
    Mice,
    Laila,
    DrillGgabijo,
    Eily
}

[System.Serializable]
public struct ToolsLevel
{
    public int widthLevel;
    public int heightLevel;
    public int strengthLevel;
    public int luckLevel;
}

[System.Serializable]
[System.Flags]
public enum History
{
    Prologue,
    Chapter1,
    Chapter2,
    Chapter3,
    Chapter4,        
    Chapter5,   
    Chapter6,
    Chapter7,
    Chapter8,
    Chapter9,
    Chapter10,
}