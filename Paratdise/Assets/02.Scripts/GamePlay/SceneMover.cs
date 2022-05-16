using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/31
/// 최종수정일 : 
/// 설명 : 
/// 
/// 씬을 이동시키는 클래스
/// </summary>

public class SceneMover
{
    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public static void MoveTo(string sceneName)
    {
        if(SceneManager.GetActiveScene().name == sceneName)
            SceneManager.LoadScene("Temp");
        else
            SceneManager.LoadScene(sceneName);

        System.GC.Collect();
    }
}