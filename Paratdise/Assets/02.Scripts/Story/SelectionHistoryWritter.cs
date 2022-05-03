using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 스토리 진행중 선택지에 대한 기록을 데이터에 쓰기위한 클래스
/// </summary>
public class SelectionHistoryWritter : MonoBehaviour
{
    [SerializeField] private int _chapter;
    [SerializeField] private bool _selection1;
    [SerializeField] private bool _selection2;
    
    public void WriteSelectionHistory()
    {
        PlayerData data = PlayerDataManager.data;
        SelectionHistroy tmpHistory = new SelectionHistroy()
        {
            chapter = _chapter,
            selected1 = _selection1,
            selected2 = _selection2,
        };
        CharacterData characterData = data.GetCharacterData(GameManager.characterSelected);
        characterData.selectionHistories[_chapter] = tmpHistory;
        data.SetCharacterData(characterData);
        PlayerDataManager.data = data;
    }
}