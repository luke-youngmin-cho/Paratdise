using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 
/// 설명 : 
/// 
/// 오브젝트 풀. 
/// Start() 에서 생성하니 풀에 등록할 요소들은 Awake 에서 등록 해주어야 함.
/// </summary>

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<ObjectPool>("ObjectPool"));
            }
            return _instance;
        }
    }

    public static bool isReady = false;
    List<PoolElement> poolElements = new List<PoolElement>();
    List<GameObject> spawnedObjects = new List<GameObject>();
    Dictionary<string, Queue<GameObject>> spawnedQueueDictionrary = new Dictionary<string, Queue<GameObject>>();


    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public void AddPoolElement(PoolElement poolElement)
    {
        poolElements.Add(poolElement);
        //Debug.Log($"{poolElement.tag} is added on ObjectPool");
    }

    public void CreatePoolElements()
    {
        foreach (PoolElement poolElement in poolElements)
        {
            //Debug.Log($"pool element registered : {poolElement.tag}");
            spawnedQueueDictionrary.Add(poolElement.tag, new Queue<GameObject>());
            for (int i = 0; i < poolElement.size; i++)
            {
                GameObject obj = CreateNewObject(poolElement.tag, poolElement.prefab);
                ArrangePool(obj);
            }
        }
        isReady = true;
    }

    public static void ReturnToPool(GameObject obj)
    {
        if (!instance.spawnedQueueDictionrary.ContainsKey(obj.name))
            throw new Exception($"Pool doesn't include {obj.name}");

        obj.transform.position = instance.transform.position;
        instance.spawnedQueueDictionrary[obj.name].Enqueue(obj);

        if (obj.TryGetComponent(out MapOptimizableObject mapOptimizableObject))
        {
            MapOptimizer.instance.RemoveMapOptimizableObject(mapOptimizableObject.sector, mapOptimizableObject.id);
        }
    }

    public static void ReturnAllToPool()
    {
        isReady = false;
        foreach (var item in instance.spawnedObjects)
        {
            item.SetActive(false);
            ReturnToPool(item);
        }
        isReady = true;
    }

    public static int GetSpawnedObjectNumber(string tag)
    {
        int count = 0;
        foreach (var go in instance.spawnedObjects)
        {
            if (go.name == tag &&
               go.activeSelf)
                count++;
        }
        return count;
    }

    public static List<GameObject> GetSpawnedObjects(string tag)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var go in instance.spawnedObjects)
        {
            if (go.name == tag)
                list.Add(go);
        }
        return list;
    }

    public static GameObject SpawnFromPool(string tag, Vector2 position) =>
        instance.Spawn(tag, position);


    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Awake()
    {
        if (_instance != null)
        {
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                Destroy(spawnedObjects[i]);
            }
            Destroy(_instance);
            _instance = instance;
        }   
    }

    private GameObject Spawn(string tag, Vector2 position)
    {
        if (!spawnedQueueDictionrary.ContainsKey(tag))
            throw new Exception($"Pool doesn't contains {tag}");

        Queue<GameObject> queue = spawnedQueueDictionrary[tag];
        if (queue.Count == 0)
        {
            PoolElement poolElement = poolElements.Find(x => x.tag == tag);
            var obj = CreateNewObject(poolElement.tag, poolElement.prefab);
            ArrangePool(obj);
        }

        GameObject objectToSpawn = queue.Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = Quaternion.identity;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    private GameObject CreateNewObject(string tag, GameObject prefab)
    {
        //Debug.Log($"Object pool : create new {tag}, {prefab.name}");
        GameObject obj = Instantiate(prefab, transform);
        obj.name = tag;
        obj.SetActive(false);
        ReturnToPool(obj);
        return obj;
    }

    private void ArrangePool(GameObject obj)
    {
        bool isSameNameExist = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == transform.childCount - 1)
            {
                obj.transform.SetSiblingIndex(i);
                spawnedObjects.Insert(i, obj);
                break;
            }
            else if (transform.GetChild(i).name == obj.name)
                isSameNameExist = true;
            else if (isSameNameExist)
            {
                obj.transform.SetSiblingIndex(i);
                spawnedObjects.Insert(i, obj);
                break;
            }
        }
    }
}
[System.Serializable]
public class PoolElement
{
    public string tag;
    public GameObject prefab;
    public int size;
}