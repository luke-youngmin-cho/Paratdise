using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/26
/// 최종수정일 : 
/// 설명 : 
/// 
/// SceneInformation 을 통해 사용자가 이전에 머물렀던 씬으로돌아가게하는 클래스
/// </summary>
public class GoBackToPreviousScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneMover.MoveTo(SceneInformation.oldSceneName);
    }

}
