using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/06
/// 최종수정일 :
/// 설명 : 
/// 
/// 강화 후의 결과치를 미리 보여주는 창
/// </summary>
public class UpgradeResultView : MonoBehaviour
{
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private Text resultValueText;
    [SerializeField] private Text currentValueText;

    //====================================================================
    //*********************** Public Methods *****************************
    //====================================================================

    public void Refresh()
    {
        int tmpLevel = 0;
        CharacterData characterData = PlayerDataManager.data.GetCharacterData(GameManager.characterSelected);
        switch (upgradeType)
        {
            case UpgradeType.None:
                break;
            case UpgradeType.DiggingForce:
                tmpLevel = characterData.toolsLevel.diggingForceLevel;
                break;
            case UpgradeType.Attack:
                tmpLevel = characterData.toolsLevel.AttackLevel;
                break;
            case UpgradeType.Speed:
                tmpLevel = characterData.toolsLevel.speedLevel;
                break;
            case UpgradeType.Luck:
                tmpLevel = characterData.toolsLevel.luckLevel;
                break;
            default:
                break;
        }

        var upgradeUnit = UpgradeInfoTable.GetUpgradeValue(upgradeType);

        if (tmpLevel >= UpgradeInfoTable.instance.resourceDictionary[upgradeType].Length)
            resultValueText.text = "Max";
        else
            resultValueText.text = ((tmpLevel + 1) * upgradeUnit).ToString();
        currentValueText.text = (tmpLevel * upgradeUnit).ToString();
    }
}
