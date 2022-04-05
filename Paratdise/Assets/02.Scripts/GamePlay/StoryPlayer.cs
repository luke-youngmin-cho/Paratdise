using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// ���� Ŭ����
/// ��ȹ ����. ���� �����ɰ���
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