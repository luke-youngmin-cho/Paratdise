using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// �ʵ����͸� ��������� �����ڿ� Ŭ����
/// </summary>
namespace YM
{
    public class MapDataEditor : MonoBehaviour
    {
        public int stage;
        public MapData mapData;

        public void CreateMapData()
        {

        }

        public void CreateDefaultMapData()
        {
            MapData tmpMapData = new MapData();
            tmpMapData = mapData;
            MapDataManager.CreateDefaultData(stage, tmpMapData);
        }
    }
}
