using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/05
/// 최종수정일 : 
/// 설명 : 
/// 
/// 캐릭터 선택창의 모든  UI 관리
/// </summary>
public class CharacterUIManager : MonoBehaviour
{
    [SerializeField] private CharacterStatsUI characterStatsUI;
    [SerializeField] private CharacterUpgradeInfoUI characterUpgradeInfoUI;
    [SerializeField] private CharacterTalkBoxUI characterTalkBoxUI;
    [SerializeField] private Transform openConditionPanel;
    [SerializeField] private GameObject startStageButton;
    [SerializeField] private GameObject startStageButtonNotAvailable;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private Text nameText;

    [SerializeField] private Text stagePreviewText;
    [SerializeField] private Image stagePreivewIcon;

    [SerializeField] private GameObject micePreviewModel;
    [SerializeField] private GameObject lailaPreviewModel;
    [SerializeField] private GameObject joePreviewModel;
    [SerializeField] private GameObject aileyPreviewModel;
    [SerializeField] private GameObject micePreviewModelNotAvailable;
    [SerializeField] private GameObject lailaPreviewModelNotAvailable;
    [SerializeField] private GameObject joePreviewModelNotAvailable;
    [SerializeField] private GameObject aileyPreviewModelNotAvailable;

    /*private void Start()
    {
        Refresh();
    }*/

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (GameManager.characterSelected == CharacterType.None)
            GameManager.SelectCharacter(CharacterType.Mice);

        SwitchName();
        SwitchPreviewModel();
        if (PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).isAvailable)
        {
            resetButton.SetActive(true);
            openConditionPanel.gameObject.SetActive(false);
            startStageButton.SetActive(true);
            startStageButtonNotAvailable.SetActive(false);
            characterStatsUI.Refresh();
            characterUpgradeInfoUI.Refresh();
            characterTalkBoxUI.Refresh();
        }
        else
        {
            resetButton.SetActive(false);
            SetOpenConditionPanelText();
            openConditionPanel.gameObject.SetActive(true);
            startStageButton.SetActive(false);
            startStageButtonNotAvailable.SetActive(true);
            characterStatsUI.Clear();
            characterUpgradeInfoUI.Clear();
            characterTalkBoxUI.Refresh();
        }


        StageInfo info = StageInfoAssets.GetStageInfo(PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).stageLastPlayed);

        if (info != null)
        {
            stagePreviewText.text = $"{((Chapter)info.chapter)} {info.stage}층";
            stagePreivewIcon.sprite = info.icon;
        }
    }

    public void Next()
    {
        if (GameManager.characterSelected < CharacterType.Ailey)
        {
            GameManager.SelectCharacter(GameManager.characterSelected + 1);
            Refresh();
        }
            
    }

    public void Prev()
    {
        if (GameManager.characterSelected > CharacterType.Mice)
        {
            GameManager.SelectCharacter(GameManager.characterSelected - 1);
            Refresh();
        }   
    }

    private void SwitchName()
    {
        switch (GameManager.characterSelected)
        {
            case CharacterType.None:
                break;
            case CharacterType.Mice:
                nameText.text = "마이스";
                break;
            case CharacterType.Laila:
                nameText.text = "라일라";
                break;
            case CharacterType.DrillGgabijo:
                nameText.text = "드릴깨비조";
                break;
            case CharacterType.Ailey:
                nameText.text = "에일리";
                break;
            default:
                break;
        }
    }

    private void SwitchPreviewModel()
    {
        DeactivateAllModels();
        switch (GameManager.characterSelected)
        {
            case CharacterType.None:
                break;
            case CharacterType.Mice:
                if (PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).isAvailable)
                    micePreviewModel.SetActive(true);
                else
                    micePreviewModelNotAvailable.SetActive(true);
                break;
            case CharacterType.Laila:
                if (PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).isAvailable)
                    lailaPreviewModel.SetActive(true);
                else
                    lailaPreviewModelNotAvailable.SetActive(true);
                break;
            case CharacterType.DrillGgabijo:
                if (PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).isAvailable)
                    joePreviewModel.SetActive(true);
                else
                    joePreviewModelNotAvailable.SetActive(true);
                break;
            case CharacterType.Ailey:
                if (PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).isAvailable)
                    aileyPreviewModel.SetActive(true);
                else
                    aileyPreviewModelNotAvailable.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void DeactivateAllModels()
    {
        micePreviewModel.SetActive(false);
        lailaPreviewModel.SetActive(false);
        joePreviewModel.SetActive(false);
        aileyPreviewModel.SetActive(false);
        micePreviewModelNotAvailable.SetActive(false);
        lailaPreviewModelNotAvailable.SetActive(false);
        joePreviewModelNotAvailable.SetActive(false);
        aileyPreviewModelNotAvailable.SetActive(false);
    }

    private void SetOpenConditionPanelText()
    {
        string conditionComment = "";
        switch (GameManager.characterSelected)
        {
            case CharacterType.None:
                break;
            case CharacterType.Mice:
                break;
            case CharacterType.Laila:
                conditionComment = $"[ 마이스 ] 로 \n 모든 층 클리어 ";
                break;
            case CharacterType.DrillGgabijo:
                conditionComment = "[ 라일라 ] 로 \n 모든 층 클리어 ";
                break;
            case CharacterType.Ailey:
                conditionComment = "[ 드릴깨비조 ] 로 \n 모든 층 클리어 ";
                break;
            default:
                break;
        }

        openConditionPanel.GetChild(1).GetComponent<Text>().text = conditionComment;
    }
}