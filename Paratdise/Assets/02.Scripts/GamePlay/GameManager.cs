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

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static GameState _gameState;
    public static GameState gameState
    {
        set
        {
            if (value < GameState.StartStage)
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
        if (StageInfoAssets.GetStageInfo(stage) != null)
        {
            currentStage = stage;
            gameState = GameState.StartStage;
        }
        else
        {
            GoBackToLobby();
        }
        
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
        yield return new WaitForSeconds(5f);

        gameState = GameState.WaitForAssetsLoaded;
    }

    private void Update()
    {
        Workflow();
        //Debug.Log($"Current game state : {PlayStateManager.instance.CurrentPlayState}, {currentStage}");
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
            case GameState.WaitForAssetsLoaded:
                if (AssetsLoader.isLoaded)
                {
                    SceneMover.MoveTo("Login");
                    Next();
                }
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
                    PlayerDataManager.data.SetStageSaved(characterSelected, 1);
                    PlayerDataManager.SaveData(LoginManager.nickName);
                }

                if (InventoryDataManager.instance == null)
                    DisplayGameState.SetDiscription("Inventory data manager instance is null");

                InventoryDataManager.LoadAll();

                Next();
                break;
            case GameState.GoLobby:
                SceneMover.MoveTo("Lobby");
                Next();
                break;
            case GameState.OnLobby:
                break;
            case GameState.StartStage:
                PlayStateManager.instance.SetState(PlayState.Play);
                SceneMover.MoveTo("Stage");
                Debug.LogWarning("GameManager : Start Stage");
                Next();
                break;
            case GameState.LoadStage:
                Debug.LogWarning("GameManager : Execute StageManager");
                if (StageManager.isReady)
                {
                    StageManager.Execute();
                    Next();
                }
                break;
            case GameState.WaitForStageLoaded:
                Debug.LogWarning("GameManager : Wait for stage manager");
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
    WaitForAssetsLoaded,
    WaitForLogin,
    LoadPlayerData,
    GoLobby,
    OnLobby,
    StartStage,
    LoadStage,
    WaitForStageLoaded,
    StageLoaded,
}