using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인벤토리 UI의 슬롯.
/// </summary>

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _name;
    [SerializeField] private Text _num;
    private string _discription;

    public void SetInfo(Sprite icon, string name, int num, string discription)
    {
        _icon.sprite = icon;
        _name.text = name;
        _num.text = num.ToString();
        _discription = discription;
    }

    private void OnMouseDown()
    {
        InventoryView.instance.ActiveInfoPanel(_icon.sprite, _name.text, _discription);
    }
}
