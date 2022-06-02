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
/// 섹터단위로 나누고 최적화 할 대상을 각 섹터에 등록함.
/// 플레이어 기준 반경 내의 섹터만 활성화 하고 그외 비활성화.
/// </summary>

public class MapOptimizer : MonoBehaviour
{
    public static MapOptimizer instance;
    [SerializeField] private LayerMask targetsLayer;
    private float _activateRange = 10f;
    private bool _doOptimize;
    private Vector2 _mapCenter;
    private Vector2 _mapSize;
    private Vector2 _sectorSize;
    private Vector2 _padding = new Vector2(0.01f, 0.01f);
    public struct Sector
    {
        public int id;
        public Vector3 center;
    }
    private Dictionary<Sector, List<GameObject>> _sectors = new Dictionary<Sector, List<GameObject>>();

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    /// <summary>
    /// 전체 맵을 섹터 단위로 나누고 등록하는 함수. 
    /// </summary>
    /// <param name="mapCenter"> 전체 맵의 중간</param>
    /// <param name="mapSize"> 전체 맵의 크기</param>
    /// <param name="sectorSize"> 각 섹터 크기</param>
    public void DivideSectors(Vector2 mapCenter, Vector2 mapSize, Vector2 sectorSize)
    {
        _mapCenter = mapCenter;
        _mapSize = mapSize;
        _sectorSize = sectorSize;

        int horizontalNum = (int)(_mapSize.x / _sectorSize.x) + 1;
        int verticalNum = (int)(_mapSize.y / _sectorSize.y) + 1;
        int sectorID = 0;
        for (int i = 0; i < verticalNum; i++)
        {
            for (int j = 0; j < horizontalNum; j++)
            {
                Vector2 sectorCenter = _mapCenter + new Vector2(_sectorSize.x * ((-horizontalNum) + 1 + 2 * j) / 2f,
                                                                _sectorSize.y * ((-verticalNum)   + 1 + 2 * i) / 2f);
                Sector sector = new Sector()
                {
                    id = sectorID++,
                    center = sectorCenter,
                };
                // 섹터 내 게임 오브젝트 찾기
                Collider2D[] cols = Physics2D.OverlapBoxAll(sectorCenter, _sectorSize - _padding, 0, targetsLayer);
                List<GameObject> sectorObjects = new List<GameObject>();

                int objectID = 0;
                MapOptimizableObject mapOptimizableObject = null;
                foreach (var col in cols)
                {
                    // 최적화 가능 컴포넌트 추가
                    mapOptimizableObject = col.gameObject.AddComponent<MapOptimizableObject>();
                    mapOptimizableObject.id = objectID++;
                    mapOptimizableObject.sector = sector;
                    sectorObjects.Add(col.gameObject);
                }
                _sectors.Add(sector, sectorObjects);
            }
        }        
    }

    public void RemoveMapOptimizableObject(Sector sector, int id)
    {
        GameObject tmp = _sectors[sector].Find(x => x != null &&
                                                x.TryGetComponent(out MapOptimizableObject mapOptimizableObject) &&
                                                mapOptimizableObject.id == id);
        if (tmp != null)
        {
            _sectors[sector].Remove(tmp);
            Destroy(tmp.GetComponent<MapOptimizableObject>());
        }   
    }

    public void DoOptimization()
    {
        _doOptimize = true;
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
        if (_doOptimize &&
            Player.instance)
        {
            Vector2 playerPos = Player.instance.transform.position;

            // 모든 섹터 순회
            foreach (var pair in _sectors)
            {
                // 반경 벗어난 최적화 오브젝트 비활성화
                if (Vector2.Distance(pair.Key.center, playerPos) > _activateRange)
                {
                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        if (pair.Value[i] != null)
                            pair.Value[i].SetActive(false);
                    }
                }
                // 반경 내 최적화 오브젝트 활성화
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
    }

    private void OnDrawGizmosSelected()
    {
        int horizontalNum = (int)(_mapSize.x / _sectorSize.x) + 1;
        int verticalNum = (int)(_mapSize.y / _sectorSize.y) + 1;

        Vector2 sectorCenter = Vector2.zero;

        for (int i = 0; i < verticalNum; i++)
        {
            for (int j = 0; j < horizontalNum; j++)
            {
                sectorCenter = _mapCenter + new Vector2(_sectorSize.x * ((-horizontalNum) + 1 + 2 * j) / 2f,
                                                        _sectorSize.y * ((-verticalNum) + 1 + 2 * i) / 2f);

                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(sectorCenter, _sectorSize);
            }
        }
    }
}