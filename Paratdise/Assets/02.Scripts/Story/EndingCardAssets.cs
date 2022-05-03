using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 엔딩카드를 가져다쓰기위한 클래스
/// </summary>
public class EndingCardAssets : MonoBehaviour
{
    public static EndingCardAssets _instance;

    public static EndingCardAssets instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<EndingCardAssets>("Assets/EndingCardAssets"));
                DontDestroyOnLoad(_instance.gameObject);
            }   
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public List<EndingCard> endingCards = new List<EndingCard>();

    public static EndingCard GetEndingCard(int index)
    {
        Debug.Log($"찾을 엔딩카드 인덱스 : {index}");
        foreach (var item in instance.endingCards)
        {
            Debug.Log($"엔딩카드 조회 : {item}, {item.index}");
        }

        return instance.endingCards.Find(x => x.index == index);
    }
}