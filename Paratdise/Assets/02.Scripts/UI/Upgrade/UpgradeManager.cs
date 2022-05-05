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
/// ��ȭ UI �Ŵ���
/// ��ȭ �õ� �����ø��� ��� ��ȭ UI ������. 
/// </summary>
public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance; 
    public bool isReady = false;
    [SerializeField] private GameObject loadingUIPrefab;
    private GameObject loadingUI;
    [SerializeField] private Image bg;
    [SerializeField] private Sprite[] bgSprites;
    [SerializeField] private UpgradeResourcesView[] upgradeResourcesViews;
    [SerializeField] private UpgradeResultView[] upgradeResultViews;
    [SerializeField] private GameObject[] upgradeButtons;
    [SerializeField] private GameObject[] upgradeWithAdButtons;
    [SerializeField] private GameObject waitingUpgradePanel;

    //====================================================================
    //*********************** Public Methods *****************************
    //====================================================================
    public bool TryUpgrade(UpgradeType type)
    {
        bool isOK = false;
        int i = 0;
        for (; i < upgradeResourcesViews.Length; i++)
        {
            if (upgradeResourcesViews[i].upgradeType == type)
            {
                isOK = upgradeResourcesViews[i].Refresh(out bool rewardAdAvailable);
                break;
            }
        }

        if (isOK)
        {
            waitingUpgradePanel.SetActive(true);
            Invoke("DeactiveWaitngPanel", 2f);
            // ������ �Ҹ�
            upgradeResourcesViews[i].SpendItemsToUpgrade();

            // ������
            switch (upgradeResourcesViews[i].upgradeType)
            {
                case UpgradeType.None:
                    break;
                case UpgradeType.DiggingForce:
                    PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).toolsLevel.diggingForceLevel++;
                    break;
                case UpgradeType.Attack:
                    PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).toolsLevel.AttackLevel++;
                    break;
                case UpgradeType.Speed:
                    PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).toolsLevel.speedLevel++;
                    break;
                case UpgradeType.Luck:
                    PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).toolsLevel.luckLevel++;
                    break;
                default:
                    break;
            }
            // �������� ������ ����
            PlayerDataManager.SaveData();

            // UI ����
            for (int j = 0; j < upgradeResourcesViews.Length; j++)
            {
                bool isOKToUpgrade = upgradeResourcesViews[j].Refresh(out bool rewardAdAvailable);
                upgradeResultViews[j].Refresh();

                // ���� ���� ��ȭ �������� üũ
                if (rewardAdAvailable)
                    upgradeWithAdButtons[j].SetActive(true);
                else
                    upgradeWithAdButtons[j].SetActive(false);

                upgradeButtons[j].GetComponent<Button>().interactable = isOKToUpgrade;
            }
        }
        return isOK;
    }

    public bool TryUpgradeWithAd(UpgradeType type)
    {
        bool isOK = false;
        int i = 0;
        for (; i < upgradeResourcesViews.Length; i++)
        {
            if (upgradeResourcesViews[i].upgradeType == type)
            {
                upgradeResourcesViews[i].Refresh(out isOK);
                break;
            }
        }

        if (isOK)
        {
            waitingUpgradePanel.SetActive(true);
            Invoke("DeactiveWaitngPanel", 2f);
            // ������ �Ҹ�
            upgradeResourcesViews[i].SpendItemsToUpgrade();

            // ������
            switch (upgradeResourcesViews[i].upgradeType)
            {
                case UpgradeType.None:
                    break;
                case UpgradeType.DiggingForce:
                    PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).toolsLevel.diggingForceLevel++;
                    break;
                case UpgradeType.Attack:
                    PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).toolsLevel.AttackLevel++;
                    break;
                case UpgradeType.Speed:
                    PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).toolsLevel.speedLevel++;
                    break;
                case UpgradeType.Luck:
                    PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).toolsLevel.luckLevel++;
                    break;
                default:
                    break;
            }
            // �������� ������ ����
            PlayerDataManager.SaveData();

            // UI ����
            for (int j = 0; j < upgradeResourcesViews.Length; j++)
            {
                bool isOKToUpgrade = upgradeResourcesViews[j].Refresh(out bool rewardAdAvailable);
                upgradeResultViews[j].Refresh();

                // ���� ���� ��ȭ �������� üũ
                if (rewardAdAvailable)
                    upgradeWithAdButtons[j].SetActive(true);
                else
                    upgradeWithAdButtons[j].SetActive(false);

                upgradeButtons[j].GetComponent<Button>().interactable = isOKToUpgrade;
            }
        }
        return isOK;
    }

    public void RefreshAll()
    {
        switch (GameManager.characterSelected)
        {
            case CharacterType.None:
                break;
            case CharacterType.Mice:
                bg.sprite = bgSprites[0];
                break;
            case CharacterType.Laila:
                bg.sprite = bgSprites[1];
                break;
            case CharacterType.DrillGgabijo:
                bg.sprite = bgSprites[2];
                break;
            case CharacterType.Ailey:
                bg.sprite = bgSprites[3];
                break;
            default:
                break;
        }

        // UI ����
        for (int j = 0; j < upgradeResourcesViews.Length; j++)
        {
            bool isOKToUpgrade = upgradeResourcesViews[j].Refresh(out bool rewardAdAvailable);
            upgradeResultViews[j].Refresh();

            // ���� ���� ��ȭ �������� üũ
            if (rewardAdAvailable)
                upgradeWithAdButtons[j].SetActive(true);
            else
                upgradeWithAdButtons[j].SetActive(false);

            upgradeButtons[j].GetComponent<Button>().interactable = isOKToUpgrade;
        }       
    }


    //====================================================================
    //*********************** Private Methods ****************************
    //====================================================================

    private void Awake()
    {
        loadingUI = Instantiate(loadingUIPrefab);
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(E_OnEnable());
    }

    IEnumerator E_OnEnable()
    {
        yield return new WaitUntil(() => UpgradeInfoTable.instance.isReady);
        RefreshAll();
        isReady = true;
        Destroy(loadingUI);
    }

    private void DeactiveWaitngPanel()
    {
        waitingUpgradePanel.SetActive(false);
    }
}
