using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 배틀필드에 떨어져있을 아이템을 위한클래스. 
/// </summary>

public class ItemController : MonoBehaviour
{
    public Item item;
    public int num = 1;
    public bool pickUpEnable = false;
    public bool isPickedUp = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float pickUpTimer = 1f;
    //============================================================================
    //*************************** Public Methods *********************************
    //============================================================================

    /// <summary>
    /// 스테이지 내에서 해당 아이템 획득하기위해 호출해야하는 함수
    /// </summary>    
    public void PickUp(Player player)
    {
        if (pickUpEnable == false || isPickedUp) return;
        isPickedUp = true;
        InventoryData data = InventoryDataManager.data;
        StageManager.EarnItem(item);
        StartCoroutine(E_PickUpEffect(player));
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    IEnumerator E_PickUpEffect(Player player)
    {
        // pop 
        rb.velocity = Vector2.zero;

        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        bool isReachedToPlayer = false;
        float fadeAlpha = 1f;
        Color fadeColor = spriteRenderer.color;

        while (pickUpTimer > 0 && isReachedToPlayer == false)
        {
            if ((Mathf.Abs(player.transform.position.x - rb.position.x) < 0.1f) &&
                ((player.transform.position.y - rb.position.y) < 0))
            {
                isReachedToPlayer = true;
            }
            Vector2 distance = (Vector2)player.transform.position - rb.position;
            Vector2 moveVelocity = distance;
            if (distance.magnitude < distance.normalized.magnitude)
                moveVelocity = distance.normalized;

            rb.position += moveVelocity * Time.deltaTime;

            fadeAlpha -= Time.deltaTime;
            fadeColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeAlpha);
            spriteRenderer.color = fadeColor;

            pickUpTimer -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
