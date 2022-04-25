/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 2022/04/23
/// ���� : 
/// 
/// ĳ������ Ÿ�԰� �ر� ���� ������ 
/// �������̷�, ���丮���� ������ �߰�
/// </summary>


[System.Serializable]
public class CharacterData
{
    public CharacterType type;
    public bool isAvailable;
    public int stageSaved;
    public int stageLastPlayed;
    public ToolsLevel toolsLevel;
    public int[] selectionHistory;
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
