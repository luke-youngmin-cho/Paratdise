using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/22
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵타일 관련 에셋을 가져다 쓰기위한 클래스
/// </summary>
public class MapTileEffectAssets : MonoBehaviour
{
    public static MapTileEffectAssets _instance;
    public static MapTileEffectAssets instance
    {
        get
        {
            if (_instance == null)
               _instance = Instantiate(Resources.Load<MapTileEffectAssets>("Assets/MapTileEffectAssets"));
            return _instance;
        }
    }

    public List<GameObject> effects = new List<GameObject>();

    public GameObject GetEffectPrefab(string name) =>
        effects.Find(x => x.name == name);
}
