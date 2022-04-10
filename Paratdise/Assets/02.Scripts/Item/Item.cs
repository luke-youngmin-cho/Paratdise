using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 아이템 정보 클래스. 
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public ItemTag tag;
    public ItemType type;
    public string itemName;
    public string discription;
    public int num;
    public Sprite icon;
}

[System.Serializable]
public enum ItemType
{
    None,
    Special,
    Resources,
    ETC
}
[System.Serializable]
public enum ItemTag
{
    None,
    Chapter1,
    Chapter2,
    Chapter3,
    Chapter4,
}