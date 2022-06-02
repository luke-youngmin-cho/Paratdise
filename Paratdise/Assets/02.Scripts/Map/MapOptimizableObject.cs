using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/01
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵 최적화 대상들에게 적용해야하는 Monobehavior
/// </summary>
public class MapOptimizableObject : MonoBehaviour
{
    public MapOptimizer.Sector sector;
    public int id;

    private void OnDestroy()
    {
        if (MapOptimizer.instance != null)
            MapOptimizer.instance.RemoveMapOptimizableObject(sector, id);
    }
}