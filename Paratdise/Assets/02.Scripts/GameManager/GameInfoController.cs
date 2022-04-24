using System.Collections.Generic;

public static class GameInfoController
{
    public static UpgradeInfo upgradeInfo = new UpgradeInfo();

    public static List<CharacterInfo> characterInfos = new List<CharacterInfo>()
    {
        new CharacterInfo(1, "���̽�", 1.5f, 0.5f,10, 0, 1.5f, 5, 1, "",10,10, ToolType.Spoon, CharacterType.Mice),
        new CharacterInfo(2, "���϶�", 1.2f, 0.4f,10, 0, 1.6f, 8, 2, "",11,9, ToolType.Shovel,CharacterType.Laila),
        new CharacterInfo(3, "����帱��", 1.8f, 0.8f,11, 2, 1.3f, 6, 3, "",9,10, ToolType.Drill,CharacterType.DrillGgabijo),
        new CharacterInfo(4, "���ϸ�", 1.5f, 0.4f,10, 1, 1.7f, 4, 4, "",10,12,  ToolType.PickAxe,CharacterType.Eily)
    };

    public static List<ToolInfo> toolInfos = new List<ToolInfo>()//�ʱⰪ
    {
        new ToolInfo(1, "��Ǭ", 1, 1, 1, 1, 0, ToolType.Spoon),
        new ToolInfo(2, "��", 1, 1, 1, 1.5f, 0, ToolType.Shovel),
        new ToolInfo(3, "�帱", 2,1,1,3,0, ToolType.Drill),
        new ToolInfo(4, "���",1,3,1.2f,2,0, ToolType.PickAxe)
    };

    public static List<ToolInfo> usingTools = new List<ToolInfo>()//������ ����� ���� ����
    {
        new ToolInfo(1, "��Ǭ", 1, 1, 1, 1, 0, ToolType.Spoon),
        new ToolInfo(2, "��", 1, 1, 1, 1.5f, 0, ToolType.Shovel),
        new ToolInfo(3, "�帱", 2,1,1,3,0, ToolType.Drill),
        new ToolInfo(4, "���",1,3,1.2f,2,0, ToolType.PickAxe)
    };

    public static string[,] characterScripts = new string[4, 3]
    {
        {"���� ����� �Ʊ� ���� ���̾�.","������ ����;�","�������� ������ �ɱ�" },
        {"�۴ٰ� ���� ��! ������ �ָ� ���ϱ�!", "���� �̿�.. �� ���� ���� �ž�!", "���ư���;�..." },
        { "��, �� ũ�� ưư����! �� ��������...", "���� ���ڸ� �Ӹ��� ����� �ϴ� ��!", "������ �� ���� �� ����!"},
        {"����� ����...","�������� �ؾ��� ���̾�.","���� �;�" }
    };

    public static ToolInfo GetToolByCharacter(CharacterType _characterType, List<ToolInfo> toolList = null)
    {
        ToolType _toolType = characterInfos.Find(x => x.characterType == _characterType).toolType;
        if (toolList == null)
            return toolInfos.Find(x => x.toolType == _toolType);
        else
            return toolList.Find(x => x.toolType == _toolType);
    }
}
