using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/09
/// 최종수정일 : 
/// 설명 : 
/// 
/// 스테이지 정보를 가지고있는 객체
/// </summary>

[CreateAssetMenu(fileName = "New StageInfo", menuName = "StageInfo/Create New StageInfo")]
public class StageInfo : ScriptableObject
{
    public int chapter;
    public int stage;
    public Sprite icon;
    public List<Item> dropItemList;

}