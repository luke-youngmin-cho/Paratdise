using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 연출 단위 데이터. 
/// </summary>

[CreateAssetMenu(fileName = "New Stroy", menuName = "Story/Create New Story")]
public class Story : ScriptableObject
{
    public CharacterType characterType;
    public StoryEffectType showEffect;
    public StoryEffectType hideEffect;
    public int index;
    public StoryPage[] pages;
    public GameObject popUp;
}

// 스토리 연출효과
[System.Serializable]
public enum StoryEffectType
{
    None,
    FadeIn,
    FadeOut,
    Dissolve,
}

// 개별 페이지
[System.Serializable]
public class StoryPage
{
    public Sprite sprite;
    public PageEffectType effectType;
}

// 개별 페이지 연출효과
[System.Serializable]
public enum PageEffectType
{
    None,
    Shake,
    AutoNext
}