using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/26
/// 최종수정일 : 
/// 설명 : 
/// 
/// 연출완료후 엔딩카드 획득 하는 팝업
/// </summary>
public class EndingCardEarnedPopUpOnStory : MonoBehaviour
{
    private EndingCard endingCardToEarn;

    private void OnEnable()
    {
        EarnEndingCard();
        if (endingCardToEarn != null)
        {
            transform.Find("Icon").GetComponent<Image>().sprite = endingCardToEarn.icon;
            transform.Find("Title").GetComponent<Text>().text = endingCardToEarn.title;
            transform.Find("Index").GetComponent<Text>().text = endingCardToEarn.index.ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    private void EarnEndingCard()
    {
        SelectionHistroy[] histories = PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).selectionHistories;

        int select1Count = 0;
        int select2Count = 0;
        foreach (var history in histories)
        {
            if (history.selected1)
                select1Count++;
            else if (history.selected2)
                select2Count++;
        }

        int endingCardIndex = 0;
        Debug.Log($"seletion histroys : {select1Count + select2Count}");
        if (select1Count + select2Count == 4)
        {
            endingCardIndex = 1 + select2Count + ((int)GameManager.characterSelected - 1) * 5;
            endingCardToEarn = EndingCardAssets.GetEndingCard(endingCardIndex, true);

            Debug.Log($"Ending card to earn : {endingCardToEarn}");
            if (PlayerDataManager.data.endingCards[endingCardIndex] == false)
            {
                PlayerDataManager.data.endingCards[endingCardIndex] = true;
                PlayerDataManager.SaveData();
            }
                
        }
        else
        {   
            Debug.LogError("선택지 히스토리에 문제가있습니다. 선택지 합이 4가 넘음");
        }
    }
}