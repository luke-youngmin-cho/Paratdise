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