using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// �÷��� ���� (�÷���  �� �Ͻ����� ) �� ���� �����Է� ���� Ŭ����
/// </summary>

public class PauseController : MonoBehaviour
{
    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public void SetState(PlayState playState)
    {
        PlayStateManager.instance.SetState(playState);
    }

    public void Pause()
    {
        SetState(PlayState.Paused);
    }

    public void Play()
    {
        SetState(PlayState.Play);
    }

    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayState currentGameState = PlayStateManager.instance.currentPlayState;
            PlayState newGameState = currentGameState == PlayState.Play
                ? PlayState.Paused
                : PlayState.Play;

            PlayStateManager.instance.SetState(newGameState);
        }
    }    
}