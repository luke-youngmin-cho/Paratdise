using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이 상태 (플레이  및 일시정지 ) 를 위한 유저입력 반응 클래스
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