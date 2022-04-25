using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 스토리조각 에셋을 가져다 쓰기위한 클래스
/// </summary>
public class PieceOfStoryAssets : MonoBehaviour
{
    public static PieceOfStoryAssets _instance;

    public static PieceOfStoryAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<PieceOfStoryAssets>("Assets/PieceOfStoryAssets"));
            return _instance;
        }
    }

    public List<PieceOfStory> piecesOfStory = new List<PieceOfStory>();

    public static PieceOfStory GetPieceOfStory(int index) =>
        instance.piecesOfStory.Find(x => x.index == index);


    public static PieceOfStory GetRandomPieceOfStory() =>
        instance.piecesOfStory[Random.Range(0, instance.piecesOfStory.Count)];
    

    public static PieceOfStoryRarity GetRariry(int index) =>
        instance.piecesOfStory.Find(x => x.index == index).rarity;
}