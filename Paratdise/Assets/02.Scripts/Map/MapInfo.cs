using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵의 정보를 가지고있는 클래스. 
/// 스테이지를 시작할때 해당 클래스의 멤버들을 가지고 MapCreater 가 맵을 구성.
/// </summary>
namespace YM
{
    [CreateAssetMenu(fileName = "New MapInfo", menuName = "MapInfo/Create New MapInfo")]
    public class MapInfo : ScriptableObject
    {
        public Vector2 size;
        public int algoNum;
        public int fluidPercents;
        public int obstaclePercents;
        public List<GameObject> MapElements_Boundary; // 경계 맵타일 리스트
        public List<GameObject> MapElements_Basic; // 기본 맵타일 리스트
        public List<GameObject> MapElements_FluidBundle; // 유체 맵 요소 리스트
        public List<GameObject> MapElements_Obstacle; // 이벤트 요소 리스트
        public GameObject MapElement_Start; // 시작 맵타일 
        public GameObject MapElement_End; // 끝 맵타일 
        public GameObject tracer;
        public List<ItemOnMapInfo> itemsOnMapInfo;
    }

    [System.Serializable]
    public struct ItemOnMapInfo
    {
        public string itemName;
        public int itemNum;
    }
}
