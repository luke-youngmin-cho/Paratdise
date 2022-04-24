using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/22
/// 최종수정일 : 
/// 설명 : 
/// 
/// 다시 스테이지 시작하는 버튼
/// </summary>
public class ReplayButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.StartStage(GameManager.currentStage);
    }
}
