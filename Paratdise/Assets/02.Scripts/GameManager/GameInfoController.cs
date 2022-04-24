using System.Collections.Generic;

public static class GameInfoController
{
    public static UpgradeInfo upgradeInfo = new UpgradeInfo();

    public static List<CharacterInfo> characterInfos = new List<CharacterInfo>()
    {
        new CharacterInfo(1, "마이스", 1.5f, 0.5f,10, 0, 1.5f, 5, 1, "",10,10, ToolType.Spoon, CharacterType.Mice),
        new CharacterInfo(2, "라일라", 1.2f, 0.4f,10, 0, 1.6f, 8, 2, "",11,9, ToolType.Shovel,CharacterType.Laila),
        new CharacterInfo(3, "깨비드릴조", 1.8f, 0.8f,11, 2, 1.3f, 6, 3, "",9,10, ToolType.Drill,CharacterType.DrillGgabijo),
        new CharacterInfo(4, "에일리", 1.5f, 0.4f,10, 1, 1.7f, 4, 4, "",10,12,  ToolType.PickAxe,CharacterType.Eily)
    };

    public static List<ToolInfo> toolInfos = new List<ToolInfo>()//초기값
    {
        new ToolInfo(1, "스푼", 1, 1, 1, 1, 0, ToolType.Spoon),
        new ToolInfo(2, "삽", 1, 1, 1, 1.5f, 0, ToolType.Shovel),
        new ToolInfo(3, "드릴", 2,1,1,3,0, ToolType.Drill),
        new ToolInfo(4, "곡괭이",1,3,1.2f,2,0, ToolType.PickAxe)
    };

    public static List<ToolInfo> usingTools = new List<ToolInfo>()//실제로 사용할 도구 정보
    {
        new ToolInfo(1, "스푼", 1, 1, 1, 1, 0, ToolType.Spoon),
        new ToolInfo(2, "삽", 1, 1, 1, 1.5f, 0, ToolType.Shovel),
        new ToolInfo(3, "드릴", 2,1,1,3,0, ToolType.Drill),
        new ToolInfo(4, "곡괭이",1,3,1.2f,2,0, ToolType.PickAxe)
    };

    public static string[,] characterScripts = new string[4, 3]
    {
        {"나는 평범한 아기 쥐일 뿐이야.","지상을 보고싶어","숟가락만 가지고 될까" },
        {"작다고 하지 마! 빠르고 멀리 보니까!", "엄마 미워.. 난 여길 떠날 거야!", "돌아가고싶어..." },
        { "헷, 난 크고 튼튼하지! 좀 느리지만...", "몸이 나쁘면 머리가 고생을 하는 법!", "누구도 날 막을 수 없어!"},
        {"평범한 나라도...","누군가는 해야할 일이야.","쉬고 싶어" }
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
