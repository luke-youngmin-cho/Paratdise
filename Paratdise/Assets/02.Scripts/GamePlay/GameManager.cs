using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/31
/// 최종수정일 : 
/// 설명 : 
/// 
/// 게임의 시작부터 끝까지 상태를 관리하는 클래스
/// </summary>

namespace YM
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private static GameState _gameState;
        public static GameState gameState
        {
            set
            {
                if(value < GameState.StartStage)
                {
                    characterSelected = CharacterType.None;
                    currentStage = StageTable.NONE;
                }
                _gameState = value;
            }
            get
            {
                return _gameState;
            }
        }
        public static CharacterType characterSelected;
        public static int currentStage;

        public static string stateDiscription = ""; // debug


        //===============================================================================================
        //********************************** Public Methods *********************************************
        //===============================================================================================

        public static void SelectCharacter(CharacterType characterType)
        {
            characterSelected = characterType;
        }

        public static void StartStage(int stage)
        {
            currentStage = stage;
            gameState = GameState.StartStage;
        }

        public static void GoBackToLobby()
        {
            gameState = GameState.GoLobby;
        }


        //===============================================================================================
        //********************************** Private Methods ********************************************
        //===============================================================================================

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            StartCoroutine(E_CheckDataManagersAndMoveToLoginScene());
        }

        IEnumerator E_CheckDataManagersAndMoveToLoginScene()
        {
            yield return new WaitUntil(() => PlayerDataManager.instance != null);
            yield return new WaitUntil(() => MapDataManager.instance != null);
            yield return new WaitUntil(() => StoryPlayDataManager.instance != null);
            SceneMover.MoveTo("Login");
            Next();
        }

        private void Update()
        {
            Workflow();
        }

        private void Next()
        {
            gameState++;
            Debug.Log($"{this.name} : {gameState}");
            DisplayGameState.SetDiscription("");
        }

        private void Workflow()
        {
            switch (_gameState)
            {
                case GameState.Idle:
                    break;
                case GameState.WaitForLogin:
                    if (LoginManager.loggedIn)
                    {
                        SceneMover.MoveTo("Loading");
                        Next();
                    }   
                    break;
                case GameState.LoadPlayerData:

                    if (PlayerDataManager.instance == null)
                        DisplayGameState.SetDiscription("player data manager instance is null");

                    if (!PlayerDataManager.TryLoadData(LoginManager.nickName, out PlayerData data))
                    {
                        stateDiscription = "Creating Data...";
                        PlayerDataManager.CreateData(LoginManager.nickName);
                        stateDiscription = "Saving Data...";
                        PlayerDataManager.data.stageSaved = 1;
                        PlayerDataManager.SaveData(LoginManager.nickName);
                    }

                    if (InventoryDataManager.instance == null)
                        DisplayGameState.SetDiscription("Inventory data manager instance is null");

                    if(!InventoryDataManager.TryLoadData(LoginManager.nickName, out InventoryData inventoryData))
                    {
                        stateDiscription = "Creating inventory Data...";
                        InventoryDataManager.CreateData(LoginManager.nickName);
                        stateDiscription = "Saving inventory Data...";                        
                    }                    
                    Next();
                    break;
                case GameState.GoLobby:
                    if (PreloadedUIManager.isReady)
                    {
                        InventoryDataManager.ApplyData();
                        SceneMover.MoveTo("Lobby");
                        Next();
                    }   
                    break;
                case GameState.OnLobby:
                    break;
                case GameState.StartStage:
                    SceneMover.MoveTo("Stage");
                    Next();
                    break;
                case GameState.LoadStage:
                    StageManager.Execute();
                    Next();
                    break;
                case GameState.WaitForStageLoaded:
                    if (StageManager.isLoaded)
                        Next();
                    break;
                case GameState.StageLoaded:
                    break;
                default:
                    break;
            }
        }
    }


    //===============================================================================================
    //*************************************** types *************************************************
    //===============================================================================================

    public enum GameState
    {
        Idle,
        WaitForLogin,
        LoadPlayerData,
        GoLobby,
        OnLobby,
        StartStage,
        LoadStage,
        WaitForStageLoaded,
        StageLoaded,
    }
}
