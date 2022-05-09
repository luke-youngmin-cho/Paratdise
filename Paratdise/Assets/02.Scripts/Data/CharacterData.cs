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
    public Stats stats;
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
    Ailey
}

[System.Serializable]
public struct Stats
{
    public float hp;
    public float moveSpeed;
    public float coldResistance;
    public float mentality;
}

[System.Serializable]
public struct ToolsLevel
{
    public int diggingForceLevel;
    public int AttackLevel;
    public int speedLevel;
    public int luckLevel;
}

[System.Serializable]
public struct SelectionHistroy
{
    public int chapter;
    public bool selected1;
    public bool selected2;
}