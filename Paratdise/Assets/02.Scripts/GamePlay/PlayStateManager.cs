using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 게임을 일시정지/ 해제 하기 위한 클래스
/// 일시정지 되어야하는 클래스들에게 모두 아래 함수들을 추가해야함.
/// 
/// private void Awake()
/// {
///    PlayStateManager.instance.OnPlayStateChanged += OnPlayStateChanged;
/// }
/// private void OnDestroy()
/// {
///    PlayStateManager.instance.OnPlayStateChanged -= OnPlayStateChanged;
/// }
/// private void OnPlayStateChanged(PlayState newPlayState)
/// {
///    enabled = newPlayState == PlayState.Play;
/// }
///
/// </summary>

public class PlayStateManager : MonoBehaviour
{
    private static PlayStateManager _instance;
    public static PlayStateManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayStateManager();
            return _instance;
        }
    }
    public PlayState CurrentPlayState { get; private set; }
    public delegate void GameStateChangeHandler(PlayState newPlayState);
    public event GameStateChangeHandler OnGameStateChanged;


    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public void SetState(PlayState newPlayState)
    {
        if (newPlayState == CurrentPlayState) return;

        CurrentPlayState = newPlayState;
        OnGameStateChanged?.Invoke(newPlayState);
    }
}
//===============================================================================================
//************************************** types **************************************************
//===============================================================================================

public enum PlayState
{
    Idle,
    Play,
    Paused,
}