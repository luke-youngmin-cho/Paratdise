using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/09
/// 최종수정일 : 
/// 설명 : 
/// 
/// 다이어리 UI 슬롯 클릭시 나타날 패널
/// </summary>
public class EndingCardInfoPanel : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _title;
    [SerializeField] private Text _index;


    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void Setup(Sprite icon, string title, string indexText) {
        _icon.sprite = icon;
        _title.text = title;
        _index.text = indexText;
    }
}