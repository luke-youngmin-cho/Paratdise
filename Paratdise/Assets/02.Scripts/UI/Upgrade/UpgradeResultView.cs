using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/06
/// ���������� :
/// ���� : 
/// 
/// ��ȭ ���� ���ġ�� �̸� �����ִ� â
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
