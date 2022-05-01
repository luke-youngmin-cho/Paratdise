using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/02
/// 최종수정일 : 
/// 설명 : 
/// 
/// 트리거시 특정 버프를 적용
/// </summary>
public class BuffGenerator : MonoBehaviour
{
    [SerializeField] private BuffType buffType;
    [SerializeField] private LayerMask targetLayer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null) return;

        if (1 << collision.gameObject.layer == targetLayer)
        {
            BuffManager.ActiveBuff(buffType, this);
        }
    }
}
