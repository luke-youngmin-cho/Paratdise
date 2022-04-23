using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 2022/04/23
/// ���� : 
/// 
/// ĳ������ Ÿ�԰� �ر� ���� ������ 
/// �������̷�, ���丮���� ������ �߰�
/// </summary>


[Serializable]
public class CharacterData
{
    public CharacterType type;
    public bool isAvailable;
    public int stageSaved;
    public int stageLastPlayed;
    public ToolsLevel toolsLevel;
    public long selectionHistory; // ��Ʈ�� ������
    public long piecesOfStory; // ��Ʈ�� ������
}

[Serializable]
public enum CharacterType
{
    None,
    Mice,
    Laila,
    DrillGgabijo,
    Eily
}

[Serializable]
public struct ToolsLevel
{
    public int widthLevel;
    public int heightLevel;
    public int strengthLevel;
    public int luckLevel;
}
