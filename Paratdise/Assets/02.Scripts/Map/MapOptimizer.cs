using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/01
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵 최적화
/// </summary>
public class MapOptimizer : MonoBehaviour
{
    public static MapOptimizer instance;
    [SerializeField] LayerMask targetsLayer;
    private float activateRange = 10f;
    private bool optimize;
    //private List<GameObject> list = new List<GameObject>();

    private Vector2 _mapCenter;
    private Vector2 _mapSize;
    private Vector2 _sectorSize;
    Vector2 padding = new Vector2(0.01f, 0.01f);
    private Dictionary<Vector2, List<GameObject>> _dic = new Dictionary<Vector2, List<GameObject>>();

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    /// <summary>
    /// 전체 맵을 섹터 단위로 나누어서 Dictionary 에 등록하는 함수. 
    /// </summary>
    /// <param name="mapCenter"> 전체 맵의 중간</param>
    /// <param name="mapSize"> 전체 맵의 크기</param>
    /// <param name="sectorSize"> 각 섹터 크기</param>
    public void DivideSectors(Vector2 mapCenter, Vector2 mapSize, Vector2 sectorSize)
    {
        _mapCenter = mapCenter;
        _mapSize = mapSize;
        _sectorSize = sectorSize;

        int hNum = (int)(_mapSize.x / _sectorSize.x) + 1;
        int vNum = (int)(_mapSize.y / _sectorSize.y) + 1;

        Vector2 sectorCenter = Vector2.zero;

        Debug.Log($"Start optimization setting ... {hNum},{vNum} - {_mapCenter}, {_mapSize} ");
        
        for (int i = 0; i < vNum; i++)
        {
            for (int j = 0; j < hNum; j++)
            {
                sectorCenter = _mapCenter + new Vector2(_sectorSize.x * ((-hNum) + 1 + 2 * j) / 2f,
                                                        _sectorSize.y * ((-vNum) + 1 + 2 * i) / 2f);
                Collider2D[] cols = Physics2D.OverlapBoxAll(sectorCenter, _sectorSize - padding, 0, targetsLayer);
                List<GameObject> tmpList = new List<GameObject>();

                int tmpID = 0;
                MapOptimizableObject mapOptimizableObject = null;
                foreach (var col in cols)
                {   
                    if (col.gameObject != null){
                        mapOptimizableObject = col.gameObject.AddComponent<MapOptimizableObject>();
                        mapOptimizableObject.id = tmpID++;
                        mapOptimizableObject.sector = sectorCenter;
                        tmpList.Add(col.gameObject);
                    }
                }
                Debug.Log($"{sectorCenter} will be optimized");
                _dic.Add(sectorCenter, tmpList);
            }
        }
        
    }

    public void RemoveMapOptimizableObject(Vector2 sector, int id)
    {
        GameObject tmp = _dic[sector].Find(x => x != null &&
                                                x.TryGetComponent(out MapOptimizableObject mapOptimizableObject) &&
                                                mapOptimizableObject.id == id);
        if (tmp != null)
        {
            _dic[sector].Remove(tmp);
            Destroy(tmp.GetComponent<MapOptimizableObject>());
        }   
    }

    //public void Add(GameObject go)
    //{
    //    list.Add(go);
    //}

    public void DoOptimization()
    {
        optimize = true;
    }

    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    private void Update()
    {
        if (optimize &&
            Player.instance)
        {
            Vector2 playerPos = Player.instance.transform.position;
            foreach (var pair in _dic)
            {
                if (Vector2.Distance(pair.Key, playerPos) > activateRange)
                {
                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        if (pair.Value[i] != null)
                            pair.Value[i].SetActive(false);
                    }
                }
                else
                {
                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        if (pair.Value[i] != null)
                            pair.Value[i].SetActive(true);
                    }
                }
            }
        }

        /*if (optimize && Player.instance != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                    list.Remove(list[i]);
                else
                {
                    if (Vector2.Distance(Player.instance.transform.position, list[i].transform.position) > activateRange)
                    {
                        if (list[i].activeSelf)
                            list[i].SetActive(false);
                    }
                    else
                    {
                        if (list[i].activeSelf == false)
                            list[i].SetActive(true);
                    }
                }
            }
        }*/
    }

    private void OnDrawGizmosSelected()
    {
        int hNum = (int)(_mapSize.x / _sectorSize.x) + 1;
        int vNum = (int)(_mapSize.y / _sectorSize.y) + 1;

        Vector2 sectorCenter = Vector2.zero;

        for (int i = 0; i < vNum; i++)
        {
            for (int j = 0; j < hNum; j++)
            {
                sectorCenter = _mapCenter + new Vector2(_sectorSize.x * ((-hNum) + 1 + 2 * j) / 2f,
                                                        _sectorSize.y * ((-vNum) + 1 + 2 * i) / 2f);

                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(sectorCenter, _sectorSize);
            }
        }
    }
}