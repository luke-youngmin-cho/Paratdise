using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/09
/// 최종수정일 : 
/// 설명 : 
/// 
/// 엔딩카드 UI
/// </summary>

public class EndingCardsView : MonoBehaviour
{
    public static EndingCardsView instance;
    public Transform itemContent;
    public GameObject slotPrefab;
    public List<GameObject> slots = new List<GameObject>();
    [SerializeField] private GameObject diaryInfoPanel;

    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void RefreshItemList()
    {
        if (PlayerDataManager.data == null) return;

        for (int i = slots.Count - 1; i > -1; i--)
            Destroy(slots[i]);
        slots.Clear();
        
        for (int i = 0; i < PlayerDataManager.data.piecesOfStory.Length; i++)
        {
            if (PlayerDataManager.data.piecesOfStory[i])
            {
                GameObject slot = Instantiate(slotPrefab, itemContent);
                slots.Add(slot);
                Debug.Log($"스토리조각 {i} 있음");
                PieceOfStory pieceOfStory = PieceOfStoryAssets.instance.piecesOfStory.Find(x => x.index == i);
                slot.GetComponent<DiarySlot>().SetInfo(pieceOfStory.icon, pieceOfStory.title, pieceOfStory.description);
            }
        }

    }

    public void ActiveInfoPanel(Sprite icon, string title, string discription)
    {
        diaryInfoPanel.GetComponent<DiaryCapsuleInfoPanel>().Setup(icon, title, discription);
        diaryInfoPanel.SetActive(true);
    }

    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItemList();
    }

}