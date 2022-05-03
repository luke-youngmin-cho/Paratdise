using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 부활버튼
/// </summary>
public class RespawnButton : MonoBehaviour
{
    public void OnClick()
    {
        StageManager.Respawn();
    }
}
