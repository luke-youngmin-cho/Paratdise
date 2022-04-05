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

namespace YM
{
    public class Story
    {
        public Sprite sprite;
        public Vector2 position;
        public string contents;
        public StoryEffectType effect;
    }

    public enum StoryEffectType
    {
        Idle,
        FadeIn,
        FadeOut,
        Shake,
    }

}
