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
/// 아직 기획 미정으로 추후 수정될 것임
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
