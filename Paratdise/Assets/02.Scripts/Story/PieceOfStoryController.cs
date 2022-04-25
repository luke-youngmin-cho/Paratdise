using UnityEngine;

public class PieceOfStoryController : MonoBehaviour 
{
    public PieceOfStory pieceOfStory;

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
        data.AddPieceOfStory(pieceOfStory.index);
        PlayerDataManager.data = data;
    }
}