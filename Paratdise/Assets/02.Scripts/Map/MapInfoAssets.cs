using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 2022/03/29
/// 설명 : 
/// 
/// 스테이지별 맵 정보를 가져다 쓰기위한 클래스.
/// </summary>
namespace YM
{
    public class MapInfoAssets : MonoBehaviour
    {
        public static MapInfoAssets _instance;
        public static MapInfoAssets instance
        {
            get
            {
                if (_instance == null)
                    _instance = Instantiate(Resources.Load<MapInfoAssets>("Assets/MapInfoAssets"));
                return _instance;
            }
        }

        public List<MapInfo> mapInfos = new List<MapInfo>();

        public MapInfo GetMapInfo(int stage) =>
            mapInfos.Find(x => x.name == $"MapInfo_{stage}");
    }
}
