using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/05
/// 최종수정일 : 
/// 설명 : 
/// 
/// 캐릭터 선택창에서 스탯
/// </summary>
public class CharacterStatsUI : MonoBehaviour
{
    [SerializeField] private Text hp;
    [SerializeField] private Text moveSpeed;
    [SerializeField] private Text coldResistance;
    [SerializeField] private Text mentality;

    public void Refresh()
    {
        // HP
        float additionalHP = 0;
        for (int i = 0; i < PlayerDataManager.data.piecesOfStory.Length; i++)
        {
            if (PlayerDataManager.data.piecesOfStory[i])
            {
                if (PieceOfStoryAssets.GetRariry(i) == PieceOfStoryRarity.Heroic)
                    additionalHP += 0.25f;
                else
                    additionalHP += 0.125f;
            }
        }

        PlayerData data = PlayerDataManager.data;
        CharacterData characterData = data.GetCharacterData(GameManager.characterSelected);
        hp.text = $"{characterData.stats.hp} ( + {additionalHP} )";

        // Speed
        moveSpeed.text = $"{characterData.stats.moveSpeed} ( + 0 )";

        // cold resistance
        coldResistance.text = $"{characterData.stats.coldResistance} ( + 0 )";

        // mentality
        mentality.text = $"{characterData.stats.mentality} ( + 0 )";
    }

    public void Clear()
    {
        hp.text = "???";
        moveSpeed.text = "???";
        coldResistance.text = "???";
        mentality.text = "???";
    }

}
