using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/23
/// ���������� : 
/// ���� : 
/// 
/// ��ֹ��� ���� Ŭ����
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