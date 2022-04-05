using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵 데이터
/// </summary>
namespace YM
{
    [System.Serializable]
    public class MapData : ScriptableObject
    {
        public List<MapElement> MapElements_Boundary; // 경계 맵타일 리스트
        public List<MapElement> MapElements_Basic; // 기본 맵타일 리스트
        public List<MapElement> MapElements_FluidBundle; // 유체 맵 요소 리스트
        public List<MapElement> MapElements_Event; // 이벤트 요소 리스트
        public MapElement MapElement_Start; // 시작 맵타일 
        public MapElement MapElement_End; // 끝 맵타일 
    }
}

