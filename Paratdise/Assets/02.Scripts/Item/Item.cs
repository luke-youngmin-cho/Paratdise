using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/04
/// ���������� : 
/// ���� : 
/// 
/// ������ ���� Ŭ����. 
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
