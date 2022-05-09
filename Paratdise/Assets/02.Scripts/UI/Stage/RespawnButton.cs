using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/03
/// ���������� : 
/// ���� : 
/// 
/// ��Ȱ��ư
/// </summary>
public class RespawnButton : MonoBehaviour
{
    private bool wasSpawned = false;

    private void OnEnable()
    {
        MapInfo mapInfo = MapInfoAssets.instance.GetMapInfo(GameManager.currentStage);
        if ((mapInfo != null && 
             mapInfo.tracer != null) ||
            wasSpawned)
            gameObject.SetActive(false);
    }

    public void OnClick()
    {
        StageManager.Respawn();
        wasSpawned = true;
    }
}
