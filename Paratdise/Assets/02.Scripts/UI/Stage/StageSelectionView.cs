using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 2022/04/10
/// 설명 : 
/// 
/// 스테이지 선택창
/// </summary>

public class StageSelectionView : MonoBehaviour
{
    public static StageSelectionView instance;
    private int _stageSelected;
    public int stageSelected
    {
        set
        {
            _stageSelected = value;
            RefreshPreviewPanel();
        }

        get
        {
            return _stageSelected;
        }
    }
    [SerializeField] private StageSelectButton stageSelectButtonPrefab;
    [SerializeField] private RectTransform contents;
    [SerializeField] private Transform doubleCheckPanel;
    [SerializeField] private Transform stagePreviewPanel;
    [SerializeField] private Transform dropItemSlotPrefab;
    private StageSelectButton[] stageViews;
    
    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================
  
    private void RefreshPreviewPanel()
    {
        StageInfo info = StageInfoAssets.GetStageInfo(stageSelected);

        if (stagePreviewPanel.Find("StageTitle") == null)
            Debug.Log("Can't find stagetitle");
        if (stagePreviewPanel.Find("StageTitle").GetComponent<Text>() == null)
            Debug.Log("no text");

        if (info == null)
            Debug.Log($"Can't get stageinfo of {stageSelected}");
        else
        {
            stagePreviewPanel.Find("ChapterTitle").GetComponent<Text>().text = $"{((Chapter)info.chapter)}";
            stagePreviewPanel.Find("StageTitle").GetComponent<Text>().text = $"{info.stage} 층";
            stagePreviewPanel.Find("StagePreview").GetComponent<Image>().sprite = info.icon;
            Transform content = stagePreviewPanel.Find("ItemDropList").GetChild(0).GetChild(0);

            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }

            foreach (var item in info.dropItemList)
            {
                Instantiate(dropItemSlotPrefab, content).GetComponent<Image>().sprite = item.icon;
                Debug.Log($"{info.stage} 층 의 드롭아이템 {item.name} ");
            }
        }
    }


    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================
    private void Awake()
    {
        instance = this;
        stageViews = contents.GetComponentsInChildren<StageSelectButton>();
    }

    private void Start()
    {
        SetUP();
    }

    private void SetUP()
    {
        /*UniTask.Create(async () =>
        {
            await UniTask.WaitUntil(() => GameManager.instance != null);
            Debug.Log($"Stage Selection View : Start() called");
            Vector2 buttonSize = stageSelectButtonPrefab.GetComponent<RectTransform>().rect.size;
            contents.GetComponent<RectTransform>().rect.Set(0, 1000, 300, 2500);
            int dir = 1;
            Vector2 pos = new Vector2(contents.rect.width / 8, buttonSize.y);
            int xIndex = 0;

            Debug.Log($"Stage Selection View : Trying to get map infos...");
            for (int i = 1; i <= MapInfoAssets.instance.mapInfos.Count; i++)
            {
                pos.x += (xIndex * dir * buttonSize.x);
                pos.y += buttonSize.y;

                if (i % 3 == 0)
                {
                    dir *= -1;
                    xIndex = 0;
                }

                xIndex++;

                StageSelectButton button = Instantiate(stageSelectButtonPrefab, contents).GetComponent<StageSelectButton>();
                button.stage = i;
                button.playDoubleCheckPanel = doubleCheckPanel;
                button.transform.position = pos;
                button.transform.GetComponentInChildren<Text>().text = i.ToString();
                stageViews.Add(button);

            }
            Debug.Log($"Stage Selection View : Activating stage views...");
            ActiveStageViewsOpened();

            int stageIdx = PlayerDataManager.data.GetStageLastPlayed(GameManager.characterSelected);

            // 해당 캐릭터로 스테이지 진행한적 없으면 프롤로그 진행
            if (stageIdx == 0)
            {
                PlayPrologue();
                stageIdx = 1;
            }

            stageSelected = stageIdx;

        });*/

        ActiveStageViewsOpened();
        contents.localPosition = new Vector2(0, contents.rect.height);

        int stageIdx = PlayerDataManager.data.GetStageLastPlayed(GameManager.characterSelected);

        // 해당 캐릭터로 스테이지 진행한적 없으면 프롤로그 진행
        if ((stageIdx == 0) && 
            (GameManager.characterSelected != CharacterType.Mice))
        {
            PlayPrologue();
            stageIdx = 1;
        }

        stageSelected = stageIdx;
    }

    private void PlayPrologue()
    {
        Debug.Log($"Stage Selection View : Starting Prologue...");
        UniTask.Create(async () =>
        {
            await UniTask.WaitUntil(() => StoryAssets.instance != null);

            StoryPlayer.instance.StartStory(StoryAssets.instance.GetStory(GameManager.characterSelected, 0));

        });
    }
    private void ActiveStageViewsOpened()
    {
        for (int i = 1; i <= stageViews.Length; i++)
        {
            if (i > PlayerDataManager.data.GetStageSaved(GameManager.characterSelected))
                stageViews[i - 1].isActivated = false;
            else
                stageViews[i - 1].isActivated = true;
        }
    }    
}

public enum Chapter
{
    프롤로그 = 0,
    화산지대 = 1,
    수정동굴 = 2,
    연구소 = 3,
    잔해더미 = 4,
}