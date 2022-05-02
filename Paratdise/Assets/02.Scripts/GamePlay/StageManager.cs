using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 스테이지의 진행을 관리할 클래스
/// </summary>
public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public static bool isReady
    {
        get
        {
            return state == StageState.Idle;
        }
    }
    public static bool isLoaded
    {
        get
        {
            return state > StageState.WaitForMapSpanwed;
        }
    }
    public static StageState state;

    private static Test_Player player;
    private static Tracer tracer;
    [SerializeField] GameObject pausePopUp;
    [SerializeField] GameObject clearPopUp;
    [SerializeField] GameObject gameOverPopUp;
    [SerializeField] GameObject controllUI;
    [SerializeField] GameObject loadingUI;

    private StageInfo stageInfo;
    private List<ItemData> earnedItems = new List<ItemData>();
    private List<int> earnedPiecesOfStory = new List<int>();

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public static void Execute()
    {
        state = StageState.StartLoading;
    }

    public static void MoveToNextStage()
    {
        state = StageState.Idle;

        PlayerDataManager.data.SetStageLastPlayed(GameManager.characterSelected, GameManager.currentStage + 1);
        if (PlayerDataManager.data.GetStageSaved(GameManager.characterSelected) < PlayerDataManager.data.GetStageLastPlayed(GameManager.characterSelected))
            PlayerDataManager.data.SetStageSaved(GameManager.characterSelected, GameManager.currentStage + 1);  
        

        PlayerDataManager.SaveData(LoginManager.nickName);
        GameManager.StartStage(GameManager.currentStage + 1);
    }

    public static void MoveToPreviousStage()
    {
        GameManager.StartStage(GameManager.currentStage - 1);
    }

    public static void MoveToStageSelection()
    {
        // todo -> 로비 씬으로 돌아가서 스테이지 선택창을 띄워주기
    }

    public static void EarnItem(Item item)
    {
        ItemData tmpData;
        if (instance.earnedItems.Exists(x => (x.itemName == item.name)))
        {
            tmpData = instance.earnedItems.Find(x => (x.itemName == item.name));
            instance.earnedItems.Remove(tmpData);
            tmpData.num += item.num;
            instance.earnedItems.Add(tmpData);
        }
        else
        {
            tmpData = new ItemData()
            {
                itemName = item.name,
                num = item.num,
            };
            instance.earnedItems.Add(tmpData);
        }
    }

    public static void EarnPieceOfStory(int index)
    {
        instance.earnedPiecesOfStory.Add(index);
    }

    public static void FinishStage()
    {
        if (state < StageState.Finish)
            state = StageState.Finish;
    }

    public static void GameOver()
    {
        if (state < StageState.Finish)
            state = StageState.GameOver;
    }

    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Awake()
    {
        if (instance !=null)
            Destroy(instance);
        instance = this;
        state = StageState.Idle;
    }

    private void Start()
    {
        stageInfo = StageInfoAssets.GetStageInfo(GameManager.currentStage);
    }

    private void OnApplicationPause(bool pause)
    {
        if (state >= StageState.OnStage)
        {
            pausePopUp.SetActive(true);
            PlayStateManager.instance.SetState(PlayState.Paused);
        }
    }

    private void Update()
    {
        Workflow();
    }

    private void Workflow()
    {
        //Debug.Log($"StageManager : {state}");
        switch (state)
        {
            case StageState.Idle:
                break;
            case StageState.StartLoading:
                PlayStateManager.instance.SetState(PlayState.Idle);
                loadingUI.SetActive(true);
                Next();
                break;
            case StageState.SpawnMap:
                SpawnMap();
                Next();
                break;
            case StageState.WaitForMapSpanwed:
                if (MapCreater.instance.isCreated)
                    Next();
                break;
            case StageState.FinishLoading:
                StartCoroutine(E_DeactivateLoadingUI());
                Next();
                break;
            case StageState.WaitForLoadingFinished:
                if (loadingUI.activeSelf == false)
                    Next();
                break;
            case StageState.StoryPlayBeforeStage:
                if (stageInfo.storyBeforeStage != null )
                {
                    StoryPlayer.instance.StartStory(stageInfo.storyBeforeStage);
                    state = StageState.WaitForStoryPlayBeforeStageFinished;
                }
                else
                    state = StageState.StartStage;
                    
                break;

            case StageState.WaitForStoryPlayBeforeStageFinished:
                if (StoryPlayer.instance.isStoryFinished)
                    state = StageState.StartStage;
                break;

            case StageState.StartStage:
                GameObject characterOrigin = CharacterAssets.instance.GetCharacter(GameManager.characterSelected);
                player = Instantiate(characterOrigin,
                                     MapCreater.instance.mapTile_Start.position,
                                     Quaternion.identity)
                                    .GetComponent<Test_Player>();

                GameObject tracerPrefab = MapInfoAssets.instance.GetMapInfo(GameManager.currentStage).tracer;

                if (tracerPrefab != null)
                {
                    // 추격자 시작위치 바로아래 생성
                    tracer = Instantiate(tracerPrefab, MapCreater.instance.tracerPoint + Vector3.down * (tracerPrefab.GetComponent<BoxCollider2D>().size.y / 2 + 2), Quaternion.identity).GetComponent<Tracer>();
                    tracer.StartMove();
                }

                PlayerDataManager.data.SetStageLastPlayed(GameManager.characterSelected, GameManager.currentStage);
                PlayerDataManager.SaveData();
                PlayStateManager.instance.SetState(PlayState.Play);
                Next();
                break;

            case StageState.OnStage:
                break;

            case StageState.Finish:
                Debug.Log("Stage Finished!");
                PlayStateManager.instance.SetState(PlayState.Paused);
                clearPopUp.SetActive(true);
                SaveEarnedItems();
                SaveEarnedPiecesOfStory();
                if (PlayerDataManager.data.GetStageSaved(GameManager.characterSelected) <= GameManager.currentStage)
                {
                    PlayerDataManager.data.SetStageSaved(GameManager.characterSelected, GameManager.currentStage + 1);
                    PlayerDataManager.SaveData();
                }
                Next();
                break;
            case StageState.StoryPlayAfterStage:
                if (stageInfo.storyAfterStage != null)
                {
                    StoryPlayer.instance.StartStory(stageInfo.storyAfterStage);
                    state = StageState.WaitForStoryPlayAfterStageFinished;
                }
                else
                    state = StageState.WaitForUserSelection;
                break;
            case StageState.WaitForStoryPlayAfterStageFinished:
                if (StoryPlayer.instance.isStoryFinished)
                    state = StageState.WaitForUserSelection;
                break;
            case StageState.GameOver:
                Debug.Log("Game Over!");
                gameOverPopUp.SetActive(true);
                controllUI.SetActive(false);
                PlayStateManager.instance.SetState(PlayState.Paused);
                state = StageState.WaitForUserSelection;
                break;
            case StageState.WaitForUserSelection:
            // nothing to do
            default:
                break;
        }
    }

    private void Next()
    {
        state++;
        Debug.Log($"{this.name} : {state}");
    }

    private void SpawnMap()
    {
        MapCreater.instance.CreateMap(GameManager.currentStage);
    }

    private void SaveEarnedItems()
    {
        foreach (var item in earnedItems)
            InventoryDataManager.data.AddData(item);
    }

    private void SaveEarnedPiecesOfStory()
    {
        PlayerData data = PlayerDataManager.data;
        foreach (var item in earnedPiecesOfStory)
            data.piecesOfStory[item] = true;
        PlayerDataManager.data = data;
    }

    private IEnumerator E_DeactivateLoadingUI()
    {
        float elapsedTime = 0;
        while (elapsedTime < 3f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        loadingUI.SetActive(false);
    }

    /// <summary>
    /// 맵 데이터를 저장할 경우 사용할 함수 (미정) 
    /// </summary>
    /*private void CheckSavedDataAndSpawnMap()
    {
        if(PlayerDataManager.data != null &&
           PlayerDataManager.data.stageSaved > 0 &&
           MapDataManager.TryLoadMapData(PlayerDataManager.data.stageSaved, out MapData mapData))
        {
            MapCreater.instance.CreateMap(mapData, false);
        }
        else
        {
            mapData = MapDataManager.LoadDefaultMapData(PlayerDataManager.data.stageSaved);
            MapCreater.instance.CreateMap(mapData, true);
        }
    }*/
} 
//===============================================================================================
//*************************************** types *************************************************
//===============================================================================================
public enum StageState
{
    Idle,
    StartLoading,
    SpawnMap,
    WaitForMapSpanwed,
    FinishLoading,
    WaitForLoadingFinished,
    StoryPlayBeforeStage,
    WaitForStoryPlayBeforeStageFinished,
    StartStage,
    OnStage,
    Finish,
    StoryPlayAfterStage,
    WaitForStoryPlayAfterStageFinished,
    GameOver,
    WaitForUserSelection,
}