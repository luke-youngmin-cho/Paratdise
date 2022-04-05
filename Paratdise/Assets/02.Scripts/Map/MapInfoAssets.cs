using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/23
/// ���������� : 2022/03/29
/// ���� : 
/// 
/// ���������� �� ������ ������ �������� Ŭ����.
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
