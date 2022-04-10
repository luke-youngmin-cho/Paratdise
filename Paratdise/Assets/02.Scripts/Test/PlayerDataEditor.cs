using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/31
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어 데이터를 생성하기위한 개발자용 클래스
/// </summary>

public class PlayerDataEditor : MonoBehaviour
{
    public PlayerData data;

    public void CreateMapData()
    {

    }

    public void CreateDefaultPlayerData()
    {
        PlayerDataManager.CreateDefaultData(data);
    }
}