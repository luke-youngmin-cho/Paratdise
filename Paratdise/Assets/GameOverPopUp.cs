using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/11
/// ���������� : 
/// ���� : 
/// 
/// �������� ���� �˾�
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
