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
    [SerializeField] private GameObject endingCardInfoPanel;

    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void RefreshItemList()
    {
        if (PlayerDataManager.data == null) return;

        for (int i = slots.Count - 1; i > -1; i--)
            Destroy(slots[i]);
        slots.Clear();
        
        for (int i = 0; i < EndingCardAssets.instance.endingCardPairs.Length; i++)
        {
            GameObject slot = Instantiate(slotPrefab, itemContent);
            slots.Add(slot);
            if (PlayerDataManager.data.endingCards[i])
            {   
                Debug.Log($"엔딩카드 {i} 있음");
                EndingCard endingCard = EndingCardAssets.GetEndingCard(i, true);
                slot.GetComponent<EndingCardSlot>().SetInfo(endingCard.icon, endingCard.title, endingCard.index);
            }
            else
            {
                Debug.Log($"엔딩카드 {i} 없음");
                EndingCard endingCard = EndingCardAssets.GetEndingCard(i, false);
                slot.GetComponent<EndingCardSlot>().SetInfo(endingCard.icon, endingCard.title, endingCard.index);
            }
        }

    }

    public void ActiveInfoPanel(Sprite icon, string title, string indexText)
    {
        endingCardInfoPanel.GetComponent<EndingCardInfoPanel>().Setup(icon, title, indexText);
        endingCardInfoPanel.SetActive(true);
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