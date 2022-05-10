using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/11
/// 최종수정일 : 
/// 설명 : 
/// 
/// 스테이지 실패 팝업
/// </summary>
public class GameOverPopUp : MonoBehaviour
{
    [SerializeField] private GameObject respawnWithAdButton;
    [SerializeField] private GameObject halfLosingWithAdButton;

    private void OnEnable()
    {
        MapInfo mapInfo = MapInfoAssets.instance.GetMapInfo(GameManager.currentStage);

        if (mapInfo == null) return;

        if (mapInfo.tracer == null)
        {
            respawnWithAdButton.SetActive(true);
            halfLosingWithAdButton.SetActive(false);
        }
        else
        {
            respawnWithAdButton.SetActive(false);
            halfLosingWithAdButton.SetActive(true);
        }            
    }

    private void Update()
    {
        if (PlayStateManager.instance.currentPlayState != PlayState.Paused)
            PlayStateManager.instance.SetState(PlayState.Paused);
    }
}
