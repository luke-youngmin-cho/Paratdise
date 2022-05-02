using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/26
/// 최종수정일 : 
/// 설명 : 
/// 
/// 트랩. 플레이어가 밟으면 데미지가함
/// </summary>


public class Trap : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hurt(damage);
            Destroy(this.gameObject);
        }
    }
}