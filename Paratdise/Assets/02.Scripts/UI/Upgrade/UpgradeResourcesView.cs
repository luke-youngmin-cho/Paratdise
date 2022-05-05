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
/// 강화에 필요한 재료들을 보여주는 창. 
/// 재료가 모자랄시 인벤토리의 남은 아이템개수를 빨간색으로 바꿔줌. 
/// </summary>
public class UpgradeResourcesView : MonoBehaviour
{
    public UpgradeType upgradeType;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject[] resourceViews;

    //====================================================================
    //*********************** Public Methods *****************************
    //====================================================================

    public bool Refresh(out bool rewardAdAvailable)
    {
        bool isOK = true;
        rewardAdAvailable = false;
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

        // 다음 업그레이드 할떄 필요한 재료정보
        var resourcesInfo = UpgradeInfoTable.GetUpgradeResourcesInfo(upgradeType, tmpLevel);

        // 재료 못받아올때 (레벨 최대치)
        if (resourcesInfo == null)
        {
            for (int i = 0; i < resourceViews.Length; i++)
                resourceViews[i].SetActive(false);

            return false;
        }
            

        // 뷰 갯수와 실제 필요한 재료 종류수가 다름
        if (resourcesInfo.Count != resourceViews.Length)
            return false;

        int lackOfNumber = 0;
        for (int i = 0; i < resourceViews.Length; i++)
        {
            if (resourcesInfo[i] != null)
            {
                Text t = resourceViews[i].transform.GetChild(0).GetComponent<Text>();
                Debug.Log(resourcesInfo[i].item);
                resourceViews[i].transform.GetChild(0).GetComponent<Text>().text = resourcesInfo[i].item.name;
                resourceViews[i].transform.GetChild(1).GetComponent<Image>().sprite = resourcesInfo[i].item.icon;

                // 가지고있는 아이템 개수
                int num = InventoryDataManager.data.itemsData.Find(x => x.itemName == resourcesInfo[i].item.name).num;
                Text numText = resourceViews[i].transform.GetChild(1).GetChild(0).GetComponent<Text>();
                numText.text = num.ToString();

                // 필요한 아이템 개수
                int numRequired = resourcesInfo[i].num;
                resourceViews[i].transform.GetChild(2).GetComponent<Text>().text = numRequired.ToString();

                // 가지고있는 아이템 수가 모자라면 빨간색으로변경
                if (num < numRequired)
                {
                    numText.color = Color.red;
                    isOK = false;

                    lackOfNumber += numRequired - num;
                }
                else
                    numText.color = Color.black;

                if (resourceViews[i].activeSelf == false)
                    resourceViews[i].SetActive(true);
            }
        }

        if (isOK == false && 
            lackOfNumber < 15)
            rewardAdAvailable = true;

        return isOK;
    }

    public void SpendItemsToUpgrade()
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

        var resourcesInfo = UpgradeInfoTable.GetUpgradeResourcesInfo(upgradeType, tmpLevel);


        for (int i = 0; i < resourceViews.Length; i++)
        {
            resourceViews[i].transform.GetChild(0).GetComponent<Text>().text = resourcesInfo[i].item.name;
            resourceViews[i].transform.GetChild(1).GetComponent<Image>().sprite = resourcesInfo[i].item.icon;

            // 필요한 아이템 개수
            int numRequired = resourcesInfo[i].num;
            resourceViews[i].transform.GetChild(2).GetComponent<Text>().text = numRequired.ToString();

            // 보유 인벤토리 데이터에서 제거
            ItemData spendItemData = new ItemData
            {
                itemName = resourcesInfo[i].item.name,
                num = numRequired
            };
            InventoryDataManager.data.RemoveData(spendItemData);

            // 가지고있는 아이템 개수
            int num = InventoryDataManager.data.itemsData.Find(x => x.itemName == resourcesInfo[i].item.name).num;
            Text numText = resourceViews[i].transform.GetChild(1).GetChild(0).GetComponent<Text>();
            numText.text = num.ToString();
        }

    }
}
