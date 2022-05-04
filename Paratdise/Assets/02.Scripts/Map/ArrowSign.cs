using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSign : MonoBehaviour
{
    private void OnEnable()
    {
        Vector3 dir = MapCreater.instance.mapTile_End.position  - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
