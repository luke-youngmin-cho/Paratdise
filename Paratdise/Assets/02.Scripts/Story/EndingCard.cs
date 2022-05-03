using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 엔딩카드
/// 진행 이력에따라 유저가 받는 보상
/// </summary>
[CreateAssetMenu(fileName = "New EndingCard", menuName = "EndingCard/Create New EndingCard")]
public class EndingCard : ScriptableObject
{
    public int index;
    public string title;
    public Sprite icon;
}