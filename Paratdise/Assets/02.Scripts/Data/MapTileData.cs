using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵 요소 ( 맵 프리팹 이름과 좌표 )
/// </summary>
namespace YM
{
    [System.Serializable]
    public struct MapElement
    {
        public string tag;
        public Vector2 coord;
    }
}

