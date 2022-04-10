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


    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayState currentGameState = PlayStateManager.instance.CurrentPlayState;
            PlayState newGameState = currentGameState == PlayState.Play
                ? PlayState.Paused
                : PlayState.Play;

            PlayStateManager.instance.SetState(newGameState);
        }
    }
}