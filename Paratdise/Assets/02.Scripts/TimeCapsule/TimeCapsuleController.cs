
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
        PieceOfStory pieceOfStory = PieceOfStoryAssets.GetRandomPieceOfStory();
        StageManager.EarnPieceOfStory(pieceOfStory.index);

        switch (pieceOfStory.rarity)
        {
            case PieceOfStoryRarity.Common:
                Player.instance.hp += Player.instance.hpMax / 10;
                break;
            case PieceOfStoryRarity.Uncommon:
                Player.instance.hp += Player.instance.hpMax / 8;
                break;
            case PieceOfStoryRarity.Rare:
                Player.instance.hp += Player.instance.hpMax / 6;
                break;
            case PieceOfStoryRarity.Heroic:
                Player.instance.hp += Player.instance.hpMax / 4;
                break;
            default:
                break;
        }

        PieceOfStoryPopUp.instance.PopUp(pieceOfStory.icon, pieceOfStory.title, pieceOfStory.description, pieceOfStory.index, pieceOfStory.rarity);
        Destroy(this.gameObject);
    }

}
