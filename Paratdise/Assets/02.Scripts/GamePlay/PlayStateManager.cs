using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// ������ �Ͻ�����/ ���� �ϱ� ���� Ŭ����
/// �Ͻ����� �Ǿ���ϴ� Ŭ�����鿡�� ��� �Ʒ� �Լ����� �߰��ؾ���.
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