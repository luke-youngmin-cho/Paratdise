using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// ���� ���� ������. 
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

// ���丮 ����ȿ��
[System.Serializable]
public enum StoryEffectType
{
    None,
    FadeIn,
    FadeOut,
    Dissolve,
}

// ���� ������
[System.Serializable]
public class StoryPage
{
    public Sprite sprite;
    public PageEffectType effectType;
}

// ���� ������ ����ȿ��
[System.Serializable]
public enum PageEffectType
{
    None,
    Shake,
    AutoNext
}