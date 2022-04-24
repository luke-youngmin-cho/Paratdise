using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 타임캡슐을 파괴하면 얻을 수 있는 스토리 조각 데이터
/// </summary>
/// 
[CreateAssetMenu(fileName = "New PieceOfStory", menuName = "PieceOfStory/Create New PieceOfStory")]
public class PieceOfStory : ScriptableObject
{
    public int index;
    public string date;
    public string title;
    public string content;
    public Sprite icon;
    public PieceOfStoryRarity rarity;
}

public enum PieceOfStoryRarity
{
    Common,
    Uncommon,
    Rare,
    Heroic,
}