using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/04
/// ���������� : 
/// ���� : 
/// 
/// ��Ʋ�ʵ忡 ���������� �������� ����Ŭ����. 
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
    /// �������� ������ �ش� ������ ȹ���ϱ����� ȣ���ؾ��ϴ� �Լ�
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
