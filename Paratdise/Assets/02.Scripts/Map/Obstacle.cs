using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 
/// 설명 : 
/// 
/// 장애물을 위한 클래스
/// </summary>

namespace YM
{
    public class Obstacle : MonoBehaviour
    {
        public void ReturnToPool()
        {
            ObjectPool.ReturnToPool(gameObject);
        }        
    }
}