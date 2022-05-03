using System.Collections.Generic;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 2022/05/03
/// ���� : 
/// 
/// ĳ������ Ÿ�԰� �ر� ���� ������ 
/// �������̷�, ���丮���� ������ 
/// </summary>
[System.Serializable]
public class CharacterData
{
    public CharacterType type;
    public bool isAvailable;
    public int stageSaved;
    public int stageLastPlayed;
    public ToolsLevel toolsLevel;
    public SelectionHistroy[] selectionHistories = new SelectionHistroy[8];
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
public struct SelectionHistroy
{
    public int chapter;
    public bool selected1;
    public bool selected2;
}