using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/22
/// ���������� : 
/// ���� : 
/// 
/// ��Ÿ�� ���� ������ ������ �������� Ŭ����
/// </summary>
public class MapTileEffectAssets : MonoBehaviour
{
    public static MapTileEffectAssets _instance;
    public static MapTileEffectAssets instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<MapTileEffectAssets>("Assets/MapTileEffectAssets"));
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

    public List<GameObject> effects = new List<GameObject>();

    public GameObject GetEffectPrefab(string name) =>
        effects.Find(x => x.name == name);
}
