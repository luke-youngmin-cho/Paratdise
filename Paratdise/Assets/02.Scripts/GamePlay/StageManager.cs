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
    [SerializeField] GameObject clearPopUp;
    private List<ItemData> earnedItems = new List<ItemData>();
    

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public static void Execute()
    {
        if (state == StageState.Idle)
            instance.Next();
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

    public static void FinishStage()
    {
        if (state < StageState.Finish)
            state = StageState.Finish;
    }


    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Awake()
    {
        instance = this;
        state = StageState.Idle;
    }

    private void Update()
    {
        Workflow();
    }

    private void Workflow()
    {
        switch (state)
        {
            case StageState.Idle:
                break;

            case StageState.SpawnMap:
                SpawnMap();
                Next();
                break;

            case StageState.WaitForMapSpanwed:
                if (MapCreater.instance.isCreated)
                    Next();
                break;

            case StageState.StoryPlay:
                Next();
                break;

            case StageState.WaitForStoryPlayFinished:
                Next();
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
                    tracer = Instantiate(tracerPrefab, MapCreater.instance.mapTile_Start.position + Vector3.down * tracerPrefab.transform.lossyScale.y / 2, Quaternion.identity).GetComponent<Tracer>();
                    tracer.StartMove();
                }

                PlayerDataManager.data.SetStageLastPlayed(GameManager.characterSelected, GameManager.currentStage);
                PlayerDataManager.SaveData();
                Next();
                break;

            case StageState.OnStage:
                break;

            case StageState.Finish:
                Debug.Log("Stage Finished!");
                clearPopUp.SetActive(true);
                SaveEarnedItems();
                if(PlayerDataManager.data.GetStageSaved(GameManager.characterSelected) < GameManager.currentStage)
                {
                    PlayerDataManager.data.SetStageSaved(GameManager.characterSelected, GameManager.currentStage);
                    PlayerDataManager.SaveData();
                }
                Next();
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
    SpawnMap,
    WaitForMapSpanwed,
    StoryPlay,
    WaitForStoryPlayFinished,
    StartStage,
    OnStage,
    Finish,
    WaitForUserSelection,
}