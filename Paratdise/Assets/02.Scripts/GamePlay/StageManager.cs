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
namespace YM
{
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
        private static StageState state;

        private static Player player;
        private static Tracer tracer;
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
            PlayerDataManager.data.stageSaved = GameManager.currentStage + 1;
            PlayerDataManager.SaveData(LoginManager.nickName);
            GameManager.StartStage(GameManager.currentStage + 1);
        }

        public static void MoveToPreviousStage()
        {
            GameManager.StartStage(GameManager.currentStage - 1);
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
                                        .GetComponent<Player>();

                    GameObject tracerPrefab = MapInfoAssets.instance.GetMapInfo(GameManager.currentStage).tracer;
                    
                    if(tracerPrefab != null)
                    {
                        // 추격자 시작위치 바로아래 생성
                        tracer = Instantiate(tracerPrefab, MapCreater.instance.mapTile_Start.position + Vector3.down * tracerPrefab.transform.lossyScale.y / 2, Quaternion.identity).GetComponent<Tracer>();
                        tracer.StartMove();
                    }                        
                    Next();
                    break;

                case StageState.OnStage:
                    break;

                case StageState.Finish:
                    break;

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
    }
}
