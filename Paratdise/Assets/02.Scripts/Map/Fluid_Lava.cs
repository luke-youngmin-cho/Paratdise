using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 
/// 설명 : 
/// 
/// 용암을 위한 Fluid 확장. 믈과 닿으면 돌로 변함
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
        /// 물과 만나면 돌로 변하는 이벤트.
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