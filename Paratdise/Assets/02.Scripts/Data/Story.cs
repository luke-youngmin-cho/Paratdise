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
/// ���� ��ȹ �������� ���� ������ ����
/// </summary>

public class Story
{
    public StoryExecuteType executeType;
    public StoryEffectType effect;
    public int stage;
    public string contents;
    public Sprite sprite;

}

public enum StoryExecuteType
{
    None,
    StartOfStage,
    EndOfStage,
}

public enum StoryEffectType
{
    Idle,
    FadeIn,
    FadeOut,
    Shake,
}
