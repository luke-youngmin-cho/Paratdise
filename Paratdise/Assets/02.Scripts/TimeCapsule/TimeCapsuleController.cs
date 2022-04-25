
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/08
/// 최종수정일 : 2022/04/25
/// 설명 : 
/// 
/// 타임캡슐 
/// </summary>

public class TimeCapsuleController : MonoBehaviour
{
    public bool isPickedUp = false;

    //============================================================================
    //*************************** Public Methods *********************************
    //============================================================================

    /// <summary>
    /// 스테이지 내에서 해당 스토리조각을 획득하기위해 호출해야하는 함수
    /// </summary>    
    public void PickUp()
    {
        if (isPickedUp) return;
        isPickedUp = true;

        PlayerData data = PlayerDataManager.data;
        data.AddPieceOfStory(GetRandomPieceOfStory());
        PlayerDataManager.data = data;

        PieceOfStoryPopUp.instance.PopUp();
    }

    //============================================================================
    //*************************** Private Methods ********************************
    //============================================================================

    private int GetRandomPieceOfStory()
    {
        // todo-> 확률에 따라 나누어야함.
        int index = Random.Range(0, PieceOfStoryAssets.instance.piecesOfStory.Count);
        return index;
    }
}
