using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/31
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어 시작시 플레이어를 위치시킬 맵 타일. 플레이어가 아래로 가면 이전 스테이지로 돌아감.
/// </summary>


public class MapTile_End : MapTile
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask destroyTargetLayer;
    public override void OnEnable()
    {
        Collider2D[] centers = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 0.01f, destroyTargetLayer);
        foreach (var center in centers)
        {
            if (center.gameObject != gameObject)
            {
                Destroy(center.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Reached to end");
        if (1 << collision.gameObject.layer == playerLayer)
        {
            if (collision.transform.position.y + 0.2f > transform.position.y)
            {
                Debug.Log("Go to next stage!");
                StageManager.FinishStage();
            }

        }
    }
}