using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/04
/// ���������� : 
/// ���� : 
/// 
/// ������ ���� Ŭ����. 
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