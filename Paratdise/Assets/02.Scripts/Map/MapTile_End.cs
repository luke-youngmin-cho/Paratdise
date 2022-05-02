using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/31
/// ���������� : 
/// ���� : 
/// 
/// �÷��̾� ���۽� �÷��̾ ��ġ��ų �� Ÿ��. �÷��̾ �Ʒ��� ���� ���� ���������� ���ư�.
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