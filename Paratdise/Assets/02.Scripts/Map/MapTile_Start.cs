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


public class MapTile_Start : MapTile
{
    [SerializeField] LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Debug.Log("Reached to start");
        if (1 << collision.gameObject.layer == playerLayer)
        {
            if (collision.transform.position.y + 0.2f < transform.position.y)
            {
                Debug.Log("Go back to previous stage!");
                StageManager.MoveToPreviousStage();
            }

        }*/
    }
}