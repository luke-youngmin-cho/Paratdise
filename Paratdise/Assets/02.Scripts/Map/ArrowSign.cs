using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/05
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵에 등장하는 방향 표지판
/// </summary>
public class ArrowSign : MonoBehaviour
{
    private void OnEnable()
    {
        Vector3 dir = MapCreater.instance.mapTile_End.position  - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
