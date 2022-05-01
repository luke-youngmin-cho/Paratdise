using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵의 요소에 대한 프리팹들을 참조할 수 있는 클래스
/// </summary>

public class MapElementAssets : MonoBehaviour
{
    public static MapElementAssets _instance;
    public static MapElementAssets instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<MapElementAssets>("Assets/MapElementAssets"));
                DontDestroyOnLoad(_instance.gameObject);
            }   
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    [SerializeField] List<GameObject> mapElementPrefabs = new List<GameObject>();

    public GameObject GetMapElementByName(string tag) =>
        mapElementPrefabs.Find(x => x.name == tag);

}