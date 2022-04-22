using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/22
/// 최종수정일 : 
/// 설명 : 
/// 
/// 다음스테이지 진행하는 버튼
/// </summary>
public class NextStageButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.StartStage(GameManager.currentStage + 1);
    }
}
