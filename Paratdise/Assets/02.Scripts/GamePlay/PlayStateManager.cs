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
public class PlayStateManager
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
    private PlayState _currentPlayState;
    public PlayState currentPlayState
    {
        get
        {
            return _currentPlayState;
        }
        set
        {
            _currentPlayState = value;
        }
    }
    public delegate void PlayStateChangeHandler(PlayState newPlayState);
    public event PlayStateChangeHandler OnPlayStateChanged;


    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public void SetState(PlayState newPlayState)
    {
        //if (newPlayState == currentPlayState) return;

        currentPlayState = newPlayState;
        OnPlayStateChanged?.Invoke(newPlayState);

        if (currentPlayState == PlayState.Paused)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
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