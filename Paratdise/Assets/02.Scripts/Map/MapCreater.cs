using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 2022/03/29
/// 설명 : 
/// 
/// 맵 생성용 클래스
/// 
/// 순서
/// 1. CelluarAutomata로 맵 경계 타일 생성
/// 2. 나머지 빈공간은 기본 타일 도배
/// 3. 기본타일 위치중 랜덤으로 특수타일/ 맵 관련 객체 배치 
/// </summary>

namespace YM
{
    public class MapCreater : MonoBehaviour
    {
        public static MapCreater instance;
        public static Vector2 sizeUnit;
        public BoxCollider2D mapSizeBoundCol;
        public int algorithmTimes;
        public bool isCreated;
        [HideInInspector] public Transform mapTile_Start;
        [HideInInspector] public Transform mapTile_End;

        private void Awake()
        {
            instance = this;
        }

        /// <summary>
        /// 맵 생성
        /// </summary>
        /// <param name="stage"></param>
        public void CreateMap(int stage)
        {
            UniTask.Create(async () =>
            {
                MapInfo mapInfo = MapInfoAssets.instance.GetMapInfo(stage);

                // 경계 맵타일 오브젝트 풀 등록
                foreach (var item in mapInfo.MapElements_Boundary)
                {
                    ObjectPool.instance.AddPoolElement(new PoolElement()
                    {
                        tag = item.name,
                        prefab = item,
                        size = 1
                    });
                }

                // 기본 맵타일 오브젝트 풀 등록
                foreach (var item in mapInfo.MapElements_Basic)
                {
                    ObjectPool.instance.AddPoolElement(new PoolElement()
                    {
                        tag = item.name,
                        prefab = item,
                        size = 1
                    });
                }

                // 시작 맵타일 오브젝트 풀 등록
                ObjectPool.instance.AddPoolElement(new PoolElement()
                {
                    tag = mapInfo.MapElement_Start.name,
                    prefab = mapInfo.MapElement_Start,
                    size = 1
                });

                // 끝 맵타일 오브젝트 풀 등록
                ObjectPool.instance.AddPoolElement(new PoolElement()
                {
                    tag = mapInfo.MapElement_End.name,
                    prefab = mapInfo.MapElement_End,
                    size = 1
                });

                // 방해물 오브젝트 풀 등록
                foreach (var item in mapInfo.MapElements_Obstacle)
                {
                    ObjectPool.instance.AddPoolElement(new PoolElement()
                    {
                        tag = item.name,
                        prefab = item,
                        size = 1
                    });
                }

                // 유체 번들 오브젝트 풀 등록
                foreach (var item in mapInfo.MapElements_FluidBundle)
                {
                    ObjectPool.instance.AddPoolElement(new PoolElement()
                    {
                        tag = item.name,
                        prefab = item,
                        size = 1
                    });
                }

                // 기본 맵타일 단위 크기
                sizeUnit = mapInfo.MapElements_Boundary[0].GetComponent<BoxCollider2D>().size;

                ObjectPool.instance.CreatePoolElements();
                await UniTask.WaitUntil(() => ObjectPool.isReady);

                int hNum = (int)mapInfo.size.x;
                int vNum = (int)mapInfo.size.y;
                Vector2 tmpTilePos = Vector2.zero;

                List<coordIndex> boundaryCoordList = new List<coordIndex>();
                List<coordIndex> basicCoordList = new List<coordIndex>();
                List<coordIndex> bottomStartCoordList = new List<coordIndex>();
                List<coordIndex> topEndCoordList = new List<coordIndex>();

                int[,] map = new int[hNum, vNum];
                for (int i = 0; i < hNum; i++)
                {
                    for (int j = 0; j < vNum; j++)
                    {
                        map[i, j] = GetRandomBit(30);
                    }
                }

                // 맵 생성 알고리즘
                map = CellularAutomata.Calc(map, mapInfo.algoNum);

                // 경계 맵 타일 생성
                for (int i = 0; i < hNum; i++)
                {
                    for (int j = 0; j < vNum; j++)
                    {
                        if (i == 0 || i == hNum - 1 || j == 0 || j == vNum - 1)
                            map[i, j] = 1;

                        if (map[i, j] > 0)
                        {
                            tmpTilePos.x = (hNum / 2 - i) * sizeUnit.x;
                            tmpTilePos.y = (vNum / 2 - j) * sizeUnit.y;

                            int tmpIndex = Random.Range(0, mapInfo.MapElements_Boundary.Count);
                            ObjectPool.SpawnFromPool(mapInfo.MapElements_Boundary[tmpIndex].name, tmpTilePos);
                            boundaryCoordList.Add(new coordIndex() { x = i, y = j });
                        }
                        else
                        {
                            basicCoordList.Add(new coordIndex() { x = i, y = j });
                        }
                    }
                }


                // 끝 타일 경계 윗쪽 랜덤 배치
                for (int j = 0; j < vNum - 1; j++)
                {
                    for (int i = 1; i < hNum - 1; i++)
                    {
                        if (map[i, j] == 1 &&
                            map[i, j + 1] == 0)
                        {
                            bottomStartCoordList.Add(new coordIndex() { x = i, y = j });
                        }
                    }

                    if (boundaryCoordList.Count > 0)
                        break;
                }

                coordIndex endCoord = GetShuffleList(bottomStartCoordList)[0];
                tmpTilePos = new Vector2()
                {
                    x = (hNum / 2 - endCoord.x) * sizeUnit.x,
                    y = (vNum / 2 - endCoord.y) * sizeUnit.y
                };
                mapTile_End = ObjectPool.SpawnFromPool(mapInfo.MapElement_End.name, tmpTilePos).transform;

                // 시작 타일 경계 아랫쪽 랜덤 배치 
                for (int j = vNum - 1; j > 1; j--)
                {
                    for (int i = 1; i < hNum - 1; i++)
                    {
                        //Debug.Log($"{i}{j}, {map[i,j]}");
                        if (map[i, j] == 1 &&
                            map[i, j - 1] == 0)
                        {
                            topEndCoordList.Add(new coordIndex() { x = i, y = j });
                        }
                    }

                    if (topEndCoordList.Count > 0)
                        break;
                }

                coordIndex startCoord = GetShuffleList(topEndCoordList)[0];
                tmpTilePos = new Vector2()
                {
                    x = (hNum / 2 - startCoord.x) * sizeUnit.x,
                    y = (vNum / 2 - startCoord.y) * sizeUnit.y
                };
                mapTile_Start = ObjectPool.SpawnFromPool(mapInfo.MapElement_Start.name, tmpTilePos).transform;


                // 기본 맵 타일 생성
                Dictionary<coordIndex, GameObject> basicTiles = new Dictionary<coordIndex, GameObject>();
                Queue<coordIndex> coordQueue = new Queue<coordIndex>();

                foreach (var coord in basicCoordList)
                {
                    tmpTilePos = new Vector2()
                    {
                        x = (hNum / 2 - coord.x) * sizeUnit.x,
                        y = (vNum / 2 - coord.y) * sizeUnit.y
                    };
                    int tmpIndex = Random.Range(0, mapInfo.MapElements_Basic.Count);
                    basicTiles.Add(coord, ObjectPool.SpawnFromPool(mapInfo.MapElements_Basic[tmpIndex].name, tmpTilePos));
                }

                // 기본 맵 타일 위치를 랜덤하게 섞고 큐에 등록
                basicCoordList = GetShuffleList(basicCoordList);
                foreach (var item in basicCoordList)
                    coordQueue.Enqueue(item);

                int obstacleCount = (int)(basicCoordList.Count * mapInfo.obstaclePercents / 100.0);
                int fluidCount = (int)(basicCoordList.Count * mapInfo.fluidPercents / 100.0);

                // 방해물 맵 요소 생성
                if (mapInfo.MapElements_Obstacle.Count > 0)
                {
                    for (int i = 0; i < obstacleCount; i++)
                    {
                        coordIndex randomCoord = coordQueue.Dequeue();
                        basicTiles[randomCoord].GetComponent<MapTile>().ReturnToPool();
                        basicTiles.Remove(randomCoord);
                        tmpTilePos = new Vector2()
                        {
                            x = (hNum / 2 - basicCoordList[i].x) * sizeUnit.x,
                            y = (vNum / 2 - basicCoordList[i].y) * sizeUnit.y
                        };
                        int tmpIndex = Random.Range(0, mapInfo.MapElements_Obstacle.Count);
                        ObjectPool.SpawnFromPool(mapInfo.MapElements_Obstacle[tmpIndex].name, tmpTilePos);
                    }
                }

                // 유체 맵 요소 생성
                if (mapInfo.MapElements_FluidBundle.Count > 0)
                {
                    for (int i = 0; i < fluidCount; i++)
                    {
                        coordIndex randomCoord = coordQueue.Dequeue();
                        basicTiles[randomCoord].GetComponent<MapTile>().ReturnToPool();
                        basicTiles.Remove(randomCoord);
                        tmpTilePos = new Vector2()
                        {
                            x = (hNum / 2 - basicCoordList[i].x) * sizeUnit.x,
                            y = (vNum / 2 - basicCoordList[i].y) * sizeUnit.y
                        };
                        int tmpIndex = Random.Range(0, mapInfo.MapElements_FluidBundle.Count);
                        ObjectPool.SpawnFromPool(mapInfo.MapElements_FluidBundle[tmpIndex].name, tmpTilePos).GetComponent<FluidBundle>().ReleaseAllChildren();
                    }
                }

                // 드롭 아이템 배치 
                for (int i = 0; i < mapInfo.itemsOnMapInfo.Count; i++)
                {
                    Debug.Log("Spawned item");

                    for (int j = 0; j < mapInfo.itemsOnMapInfo[i].itemNum; j++)
                    {
                        if (coordQueue.Count > 0)
                        {
                            coordIndex randomCoord = coordQueue.Dequeue();
                            tmpTilePos = new Vector2()
                            {
                                x = (hNum / 2 - randomCoord.x) * sizeUnit.x,
                                y = (vNum / 2 - randomCoord.y) * sizeUnit.y
                            };
                            Instantiate(ItemAssets.instance.GetItemPrefabByName(mapInfo.itemsOnMapInfo[i].itemName),
                                                                                tmpTilePos,
                                                                                Quaternion.identity);
                        }
                    }
                }

                // 기본 맵타일 노드 업데이트
                foreach (var basicTile in basicTiles.Values)
                {
                    if (basicTile.TryGetComponent(out MapTile mapTile))
                        mapTile.RefreshNear();
                }

                isCreated = true;
                
            });
        }

        /// <summary>
        /// 저장된 맵 생성
        /// </summary>
        /// <param name="mapData"></param>
        /// <param name="useAlgorithm"> false 이면 mapData 그대로 배치, true 이면 알고리즘에 따른 랜덤배치 </param>
        public void CreateMap(MapData mapData, bool useAlgorithm)
        {
            UniTask.Create(async () =>
            {
                // 경계 맵타일 오브젝트 풀 등록
                foreach (var item in mapData.MapElements_Boundary)
                {
                    ObjectPool.instance.AddPoolElement(new PoolElement()
                    {
                        tag = item.tag,
                        prefab = MapElementAssets.instance.GetMapElementByName(item.tag),
                        size = 1
                    });
                    //Debug.Log($"{item.tag} registered, {MapElementAssets.instance.GetMapElementByName(item.tag).name}");
                }

                // 기본 맵타일 오브젝트 풀 등록
                foreach (var item in mapData.MapElements_Basic)
                {
                    ObjectPool.instance.AddPoolElement(new PoolElement()
                    {
                        tag = item.tag,
                        prefab = MapElementAssets.instance.GetMapElementByName(item.tag),
                        size = 1
                    });
                    //Debug.Log($"{item.tag} registered, {MapElementAssets.instance.GetMapElementByName(item.tag).name}");
                }
                
                // 시작 맵타일 오브젝트 풀 등록
                ObjectPool.instance.AddPoolElement(new PoolElement()
                {
                    tag = mapData.MapElement_Start.tag,
                    prefab = MapElementAssets.instance.GetMapElementByName(mapData.MapElement_Start.tag),
                    size = 1
                });

                // 끝 맵타일 오브젝트 풀 등록
                ObjectPool.instance.AddPoolElement(new PoolElement()
                {
                    tag = mapData.MapElement_End.tag,
                    prefab = MapElementAssets.instance.GetMapElementByName(mapData.MapElement_End.tag),
                    size = 1
                });

                // 이벤트 타일 오브젝트 풀 등록
                foreach (var item in mapData.MapElements_Event)
                {
                    ObjectPool.instance.AddPoolElement(new PoolElement()
                    {
                        tag = item.tag,
                        prefab = MapElementAssets.instance.GetMapElementByName(item.tag),
                        size = 1
                    });
                    Debug.Log($"{item.tag} registered, {MapElementAssets.instance.GetMapElementByName(item.tag).name}");
                }

                // 유체 번들 오브젝트 풀 등록
                foreach (var item in mapData.MapElements_FluidBundle)
                {
                    ObjectPool.instance.AddPoolElement(new PoolElement()
                    {
                        tag = item.tag,
                        prefab = MapElementAssets.instance.GetMapElementByName(item.tag),
                        size = 1
                    });
                    Debug.Log($"{item.tag} registered, {MapElementAssets.instance.GetMapElementByName(item.tag).name}");
                }
                                
                Debug.Log($"{mapData.MapElement_End.tag} registered, {MapElementAssets.instance.GetMapElementByName(mapData.MapElement_End.tag).name}");
                // 기본 맵타일 단위 크기
                sizeUnit = MapElementAssets.instance.GetMapElementByName("MapTile_Basic").GetComponent<BoxCollider2D>().size;

                ObjectPool.instance.CreatePoolElements();
                await UniTask.WaitUntil(() => ObjectPool.isReady);

                // 알고리즘을 사용한 랜덤 배치
                if (useAlgorithm)
                {
                    int hNum = (int)(mapSizeBoundCol.size.x / sizeUnit.x);
                    int vNum = (int)(mapSizeBoundCol.size.y / sizeUnit.y);
                    Vector2 tmpTilePos = Vector2.zero;
                    
                    List<coordIndex> boundaryCoordList = new List<coordIndex>();
                    List<coordIndex> basicCoordList = new List<coordIndex>();
                    List<coordIndex> bottomStartCoordList = new List<coordIndex>();
                    List<coordIndex> topEndCoordList = new List<coordIndex>();

                    int[,] map = new int[hNum, vNum];
                    for (int i = 0; i < hNum; i++)
                    {
                        for (int j = 0; j < vNum; j++)
                        {
                            map[i, j] = GetRandomBit(30);
                        }
                    }

                    // 맵 생성 알고리즘
                    map = CellularAutomata.Calc(map, algorithmTimes);
                   
                    // 경계 맵 타일 생성
                    for (int i = 0; i < hNum; i++)
                    {
                        for (int j = 0; j < vNum; j++)
                        {
                            if (i == 0 || i == hNum - 1 || j == 0 || j == vNum - 1)
                                map[i, j] = 1;

                            if (map[i, j] > 0)
                            {
                                tmpTilePos.x = (hNum / 2 - i) * sizeUnit.x;
                                tmpTilePos.y = (vNum / 2 - j) * sizeUnit.y;

                                int tmpIndex = Random.Range(0, mapData.MapElements_Boundary.Count);
                                ObjectPool.SpawnFromPool(mapData.MapElements_Boundary[tmpIndex].tag, tmpTilePos);
                                boundaryCoordList.Add(new coordIndex() { x = i, y = j });
                            }
                            else
                            {
                                basicCoordList.Add(new coordIndex() { x = i, y = j });
                            }
                        }
                    }
                                        
                    // 끝 타일 경계 윗쪽 랜덤 배치
                    for (int j = 0; j < vNum - 1; j++)
                    {
                        for (int i = 1; i < hNum - 1; i++)
                        {
                            if (map[i, j] == 1 && 
                                map[i, j + 1] == 0)
                            {
                                bottomStartCoordList.Add(new coordIndex() { x = i, y = j });
                            }
                        }

                        if (boundaryCoordList.Count > 0)
                            break;
                    }

                    coordIndex endCoord = GetShuffleList(bottomStartCoordList)[0];
                    tmpTilePos = new Vector2()
                    {
                        x = (hNum / 2 - endCoord.x) * sizeUnit.x,
                        y = (vNum / 2 - endCoord.y) * sizeUnit.y
                    };
                    mapTile_End = ObjectPool.SpawnFromPool(mapData.MapElement_End.tag, tmpTilePos).transform;

                    // 시작 타일 경계 아랫쪽 랜덤 배치 
                    for (int j = vNum - 1; j > 1; j--)
                    {
                        for (int i = 1; i < hNum - 1; i++)
                        {
                            //Debug.Log($"{i}{j}, {map[i,j]}");
                            if (map[i, j] == 1 &&
                                map[i, j - 1] == 0)
                            {
                                topEndCoordList.Add(new coordIndex() { x = i, y = j });
                            }
                        }

                        if (topEndCoordList.Count > 0)
                            break;
                    }

                    coordIndex startCoord = GetShuffleList(topEndCoordList)[0];
                    tmpTilePos = new Vector2()
                    {
                        x = (hNum / 2 - startCoord.x) * sizeUnit.x,
                        y = (vNum / 2 - startCoord.y) * sizeUnit.y
                    };
                    mapTile_Start = ObjectPool.SpawnFromPool(mapData.MapElement_Start.tag, tmpTilePos).transform;


                    // 기본 맵 타일 생성
                    Dictionary<coordIndex, GameObject> basicTiles = new Dictionary<coordIndex, GameObject>();
                    Queue<coordIndex> coordQueue = new Queue<coordIndex>();
                    
                    foreach (var coord in basicCoordList)
                    {
                        tmpTilePos = new Vector2()
                        {
                            x = (hNum / 2 - coord.x) * sizeUnit.x,
                            y = (vNum / 2 - coord.y) * sizeUnit.y
                        };
                        int tmpIndex = Random.Range(0, mapData.MapElements_Basic.Count);
                        basicTiles.Add(coord, ObjectPool.SpawnFromPool(mapData.MapElements_Basic[tmpIndex].tag, tmpTilePos));
                    }

                    // 기본 맵 타일 위치를 랜덤하게 섞고 큐에 등록
                    basicCoordList = GetShuffleList(basicCoordList);
                    foreach (var item in basicCoordList)
                        coordQueue.Enqueue(item);

                    // 이벤트 맵 요소 생성
                    if(mapData.MapElements_Event.Count > 0)
                    {
                        foreach (var coord in basicCoordList)
                        {
                            coordIndex randomCoord = coordQueue.Dequeue();
                            basicTiles[randomCoord].GetComponent<MapTile>().ReturnToPool();
                            basicTiles.Remove(randomCoord);
                            tmpTilePos = new Vector2()
                            {
                                x = (hNum / 2 - coord.x) * sizeUnit.x,
                                y = (vNum / 2 - coord.y) * sizeUnit.y
                            };
                            int tmpIndex = Random.Range(0, mapData.MapElements_Event.Count);
                            ObjectPool.SpawnFromPool(mapData.MapElements_Event[tmpIndex].tag, tmpTilePos);
                        }
                    }

                    // 유체 맵 요소 생성
                    if(mapData.MapElements_FluidBundle.Count > 0)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            coordIndex randomCoord = coordQueue.Dequeue();
                            basicTiles[randomCoord].GetComponent<MapTile>().ReturnToPool();
                            basicTiles.Remove(randomCoord);
                            tmpTilePos = new Vector2()
                            {
                                x = (hNum / 2 - basicCoordList[i].x) * sizeUnit.x,
                                y = (vNum / 2 - basicCoordList[i].y) * sizeUnit.y
                            };
                            int tmpIndex = Random.Range(0, mapData.MapElements_FluidBundle.Count);
                            ObjectPool.SpawnFromPool(mapData.MapElements_FluidBundle[tmpIndex].tag, tmpTilePos).GetComponent<FluidBundle>().ReleaseAllChildren();
                        }
                    }                    

                    // 기본 맵타일 노드 업데이트
                    foreach (var basicTile in basicTiles.Values)
                    {
                        if (basicTile.TryGetComponent(out MapTile mapTile))
                            mapTile.RefreshNear();
                    }

                    isCreated = true;
                }
                // 맵 데이터를 그대로 생성
                else
                {
                    foreach (var item in mapData.MapElements_Boundary)
                        ObjectPool.SpawnFromPool(item.tag, item.coord);

                    foreach (var item in mapData.MapElements_Basic)
                        ObjectPool.SpawnFromPool(item.tag, item.coord);

                    foreach (var item in mapData.MapElements_FluidBundle)
                        ObjectPool.SpawnFromPool(item.tag, item.coord).GetComponent<FluidBundle>().ReleaseAllChildren();

                    foreach (var item in mapData.MapElements_Event)
                        ObjectPool.SpawnFromPool(item.tag, item.coord);

                    ObjectPool.SpawnFromPool(mapData.MapElement_Start.tag, mapData.MapElement_Start.coord);
                    ObjectPool.SpawnFromPool(mapData.MapElement_End.tag, mapData.MapElement_End.coord);
                }
            });
        }

        private List<T> GetShuffleList<T>(List<T> _list)
        {

            for (int i = _list.Count - 1; i > 0; i--)
            {
                int rnd = UnityEngine.Random.Range(0, i);

                T temp = _list[i];
                _list[i] = _list[rnd];
                _list[rnd] = temp;
            }

            return _list;
        }

        public int GetRandomBit(float percent)
        {
            return Random.Range(0, 100f) < percent ? 1 : 0;
        }

        public void ClearMap()
        {
            if (isCreated)
            {
                isCreated = false;
                ObjectPool.ReturnAllToPool();
            }
        }
    }

    public struct coordIndex
    {
        public int x;
        public int y;
    }
}