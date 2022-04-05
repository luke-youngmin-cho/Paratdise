using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 아이템 정보 클래스. 
/// </summary>
namespace YM
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
    public class Item : ScriptableObject
    {
        public ItemType type;
        public string itemName;
        public int price;
        public int numMax = 99;
        public Sprite icon;
    }

    [System.Serializable]
    public enum ItemType
    {
        Equip,
        Spend,
        ETC,
        Cash
    }
}
