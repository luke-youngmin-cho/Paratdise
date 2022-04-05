using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/23
/// ���������� : 
/// ���� : 
/// 
/// ����� ���� Fluid Ȯ��. �ɰ� ������ ���� ����
/// </summary>

namespace YM
{
    public class Fluid_Lava : Fluid
    {
        LayerMask waterLayer;
        private void Start()
        {
            waterLayer = LayerMask.NameToLayer("Fluids_Water");
        }

        /// <summary>
        /// ���� ������ ���� ���ϴ� �̺�Ʈ.
        /// </summary>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == waterLayer)
            {
                ObjectPool.SpawnFromPool("Obstacle_LavaRock", transform.position);
                ReturnToMother();
                collision.gameObject.GetComponent<Fluid>().ReturnToMother();
            }
        }
    }
}