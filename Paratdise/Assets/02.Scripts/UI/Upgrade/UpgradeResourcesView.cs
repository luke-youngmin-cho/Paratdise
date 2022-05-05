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
/// ��ȭ�� �ʿ��� ������ �����ִ� â. 
/// ��ᰡ ���ڶ��� �κ��丮�� ���� �����۰����� ���������� �ٲ���. 
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

        // ���� ���׷��̵� �ҋ� �ʿ��� �������
        var resourcesInfo = UpgradeInfoTable.GetUpgradeResourcesInfo(upgradeType, tmpLevel);

        // ��� ���޾ƿö� (���� �ִ�ġ)
        if (resourcesInfo == null)
        {
            for (int i = 0; i < resourceViews.Length; i++)
                resourceViews[i].SetActive(false);

            return false;
        }
            

        // �� ������ ���� �ʿ��� ��� �������� �ٸ�
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

                // �������ִ� ������ ����
                int num = InventoryDataManager.data.itemsData.Find(x => x.itemName == resourcesInfo[i].item.name).num;
                Text numText = resourceViews[i].transform.GetChild(1).GetChild(0).GetComponent<Text>();
                numText.text = num.ToString();

                // �ʿ��� ������ ����
                int numRequired = resourcesInfo[i].num;
                resourceViews[i].transform.GetChild(2).GetComponent<Text>().text = numRequired.ToString();

                // �������ִ� ������ ���� ���ڶ�� ���������κ���
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

            // �ʿ��� ������ ����
            int numRequired = resourcesInfo[i].num;
            resourceViews[i].transform.GetChild(2).GetComponent<Text>().text = numRequired.ToString();

            // ���� �κ��丮 �����Ϳ��� ����
            ItemData spendItemData = new ItemData
            {
                itemName = resourcesInfo[i].item.name,
                num = numRequired
            };
            InventoryDataManager.data.RemoveData(spendItemData);

            // �������ִ� ������ ����
            int num = InventoryDataManager.data.itemsData.Find(x => x.itemName == resourcesInfo[i].item.name).num;
            Text numText = resourceViews[i].transform.GetChild(1).GetChild(0).GetComponent<Text>();
            numText.text = num.ToString();
        }

    }
}
