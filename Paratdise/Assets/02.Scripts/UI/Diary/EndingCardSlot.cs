using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/05
/// 최종수정일 : 
/// 설명 : 
/// 
/// 다이어리의 엔딩카드 슬롯
/// </summary>
public class EndingCardSlot : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _title;
    [SerializeField] private Text _index;


    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void SetInfo(Sprite icon, string title, int index)
    {
        _icon.sprite = icon;
        _title.text = title;
        _index.text = "#" + index.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DiaryView.instance.ActiveEndingCardInfoPanel(_icon.sprite, _title.text, _index.text);
    }


}