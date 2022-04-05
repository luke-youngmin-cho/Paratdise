using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵데이터를 만들기위한 개발자용 클래스
/// </summary>
namespace YM
{
    public class MapDataEditor : MonoBehaviour
    {
        public int stage;
        public MapData mapData;

        public void CreateMapData()
        {

        }

        public void CreateDefaultMapData()
        {
            MapData tmpMapData = new MapData();
            tmpMapData = mapData;
            MapDataManager.CreateDefaultData(stage, tmpMapData);
        }
    }
}
