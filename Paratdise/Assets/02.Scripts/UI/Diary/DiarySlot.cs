using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/09
/// 최종수정일 : 
/// 설명 : 
/// 
/// 다이어리 UI 슬롯
/// </summary>

public class DiarySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _title;
    private string _discription;

    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void SetInfo(Sprite icon, string title, string discription)
    {
        _icon.sprite = icon;
        _title.text = title;
        _discription = discription;
    }


    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void OnMouseDown()
    {
        DiaryView.instance.ActiveInfoPanel(_icon.sprite, _title.text, _discription);
    }
}
