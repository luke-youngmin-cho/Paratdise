using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 배경음 재생
/// </summary>
public class BGMPlayer : MonoBehaviour
{
    [SerializeField] bool playOnStart;
    [SerializeField] GameState playConditionState;

    private void Start()
    {
        if (playOnStart == true)
            StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        yield return new WaitUntil(() => AudioManager.instance != null &&
                                         BGMAssets.instance != null);

        if (playConditionState != GameState.Idle)
            yield return new WaitUntil(() => GameManager.gameState == playConditionState);

        Sound sound = null;
        if (SceneInformation.newSceneName == "Lobby")
            sound = BGMAssets.GetBGM("Lobby");
        else if (SceneInformation.newSceneName == "Stage")
        {
            switch (GameManager.currentStage)
            {
                case 0:
                    break;
                case 1:
                case 2:
                case 3:
                    sound = BGMAssets.GetBGM("Chapter1_Farming");
                    break;
                case 4:
                    sound = BGMAssets.GetBGM("Chapter1_Tracing");
                    break;
                case 5:
                case 6:
                case 7:
                    sound = BGMAssets.GetBGM("Chapter2_Farming");
                    break;
                case 8:
                    sound = BGMAssets.GetBGM("Chapter2_Tracing");
                    break;
                case 9:
                case 10:
                case 11:
                    sound = BGMAssets.GetBGM("Chapter3_Farming");
                    break;
                case 12:
                    sound = BGMAssets.GetBGM("Chapter3_Tracing");
                    break;
                case 13:
                case 14:
                case 15:
                    sound = BGMAssets.GetBGM("Chapter4_Farming");
                    break;
                case 16:
                    sound = BGMAssets.GetBGM("Chapter4_Tracing");
                    break;
                default:
                    break;
            }
        }

        Debug.Log($" BGM {sound.name} 검색됨. 재생 시작");
        if (sound != null)
            AudioManager.instance.PlayBGM(sound);
    }
}