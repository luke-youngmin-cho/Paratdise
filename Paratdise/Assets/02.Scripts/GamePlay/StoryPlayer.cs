using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 연출 클래스
/// 기획 미정. 추후 수정될것임
/// </summary>
namespace YM
{
    public class StoryPlayer : MonoBehaviour
    {
        public static StoryPlayer instance;
        private void Awake()
        {
            instance = this;
        }

        private Coroutine coroutine;

        public void PlayStory(int[] stages, float[] durations)
        {
            coroutine = StartCoroutine(E_PlayStory(stages, durations));
        }

        private IEnumerator E_PlayStory(int[] stages, float[] durations)
        {
            for (int i = 0; i < stages.Length; i++)
            {
                // StoryPlayDataManager.LoadStoryData(stages[i]); // do something with this data.
                yield return new WaitForSeconds(durations[i]);
            }
        }
    }

}