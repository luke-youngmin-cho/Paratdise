using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/31
/// 최종수정일 : 
/// 설명 : 
/// 
/// 씬의 정보, 이전씬과 현재씬을 비교할수 있는 클래스
/// </summary>
namespace YM
{
    public class SceneInformation : MonoBehaviour
    {
        static public SceneInformation instance;
        static public bool sceneLoaded;
        static public string oldSceneName;
        static public string newSceneName;

        //===============================================================================================
        //********************************** Private Methods ********************************************
        //===============================================================================================

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
        }

        private void Start()
        {
            SceneManager.sceneUnloaded += delegate
            {
                sceneLoaded = false;
            };
            SceneManager.sceneLoaded += delegate
            {
                oldSceneName = newSceneName;
                Scene scene = SceneManager.GetActiveScene();
                newSceneName = scene.name;
                sceneLoaded = true;
            };
        }
    }
}
