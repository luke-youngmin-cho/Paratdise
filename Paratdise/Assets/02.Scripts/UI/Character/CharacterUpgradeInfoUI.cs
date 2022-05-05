using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/05
/// 최종수정일 : 
/// 설명 : 
/// 
/// 캐릭터 선택창에서 강화정보
/// </summary>
public class CharacterUpgradeInfoUI : MonoBehaviour
{
    [SerializeField] private Text diggingForce;
    [SerializeField] private Text attack;
    [SerializeField] private Text speed;
    [SerializeField] private Text luck;
    
    public void Refresh()
    {
        PlayerData data = PlayerDataManager.data;
        CharacterData characterData = data.GetCharacterData(GameManager.characterSelected);

        diggingForce.text = $"1 ( + {UpgradeInfoTable.GetTotalAdditionalValue(UpgradeType.DiggingForce, characterData.toolsLevel.diggingForceLevel)} )";
        attack.text = $"1 ( + {UpgradeInfoTable.GetTotalAdditionalValue(UpgradeType.Attack, characterData.toolsLevel.AttackLevel)} )";
        speed.text = $"1 ( + {UpgradeInfoTable.GetTotalAdditionalValue(UpgradeType.Speed, characterData.toolsLevel.speedLevel)} )";
        luck.text = $"1 ( + {UpgradeInfoTable.GetTotalAdditionalValue(UpgradeType.Luck, characterData.toolsLevel.luckLevel)} )";
    }

    public void Clear()
    {
        diggingForce.text = "???";
        attack.text = "???";
        speed.text = "???";
        luck.text = "???";
    }

}
