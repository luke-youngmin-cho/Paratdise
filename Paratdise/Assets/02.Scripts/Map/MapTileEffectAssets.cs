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
               _instance = Instantiate(Resources.Load<MapTileEffectAssets>("Assets/MapTileEffectAssets"));
            return _instance;
        }
    }

    public List<GameObject> effects = new List<GameObject>();

    public GameObject GetEffectPrefab(string name) =>
        effects.Find(x => x.name == name);
}
