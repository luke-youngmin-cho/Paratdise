using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/22
/// 최종수정일 : 
/// 설명 : 
/// 
/// 연출 도중에 버튼을 사용해야할때
/// </summary>
public class SelectButtonOnStoryPlay : MonoBehaviour
{
    [SerializeField] private Story storyWantsToPlay;
    
    public virtual void OnClick()
    {
        if (storyWantsToPlay != null)
            StoryPlayer.instance.StartStory(storyWantsToPlay);
        else
            StoryPlayer.instance.EndStory();
    }
}