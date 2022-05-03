using System.Collections.Generic;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 2022/05/03
/// 설명 : 
/// 
/// 캐릭터의 타입과 해금 여부 데이터 
/// 선택지이력, 스토리조각 데이터 
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