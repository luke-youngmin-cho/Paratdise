using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어 테스트. 삭제 후 새 플레이어 추가 예정
/// </summary>
public class Test_Player : MonoBehaviour
{
    public static Test_Player instance;
    public float moveSpeed;
    public Vector2 move;
    Vector2 _direction;
    public Vector2 direction
    {
        set
        {
            if(value != _direction)
            {
                if (value == Vector2.up)
                    spriteRenderer.sprite = upSprite;
                else if (value == Vector2.down)
                    spriteRenderer.sprite = downSprite;
                else if (value == Vector2.left)
                    spriteRenderer.sprite = leftSprite;
                else if (value == Vector2.right)
                    spriteRenderer.sprite = rightSprite;

                _direction = value;
            }
            move = value;
        }
        get { return _direction; }
    }

    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;

    Transform tr;
    public Transform spoonPoint;
    public LayerMask targetLayer;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        instance = this;
        tr = transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftArrow))
            direction = Vector2.left;
        else if (Input.GetKey(KeyCode.RightArrow))
            direction = Vector2.right;
        else if (Input.GetKey(KeyCode.UpArrow))
            direction = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow))
            direction = Vector2.down;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Dig();
        }
#endif
    }

    public void Dig()
    {
        RaycastHit2D hit = Physics2D.CircleCast((Vector2)tr.position + _direction, 0.1f, _direction, 0, targetLayer);
        if (hit.collider != null)
        {
            //animator.SetTrigger("isAttack");
            if (hit.collider.gameObject.TryGetComponent(out MapTile mapTile))
            {
                if (mapTile.type == MapTileType.Basic)
                    mapTile.ReturnToPoolInteraction();
            }

            else if (hit.collider.gameObject.TryGetComponent(out Obstacle obstacle))
                obstacle.ReturnToPool();
        }
    }

    public void Attack()
    {

    }

    private void FixedUpdate()
    {
        tr.Translate(move * moveSpeed* Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(spoonPoint.position, 0.1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {   
            /*if (Input.GetKey(KeyCode.Z))
                collision.gameObject.GetComponent<ItemController>().PickUp(this);*/
            collision.gameObject.GetComponent<ItemController>().PickUp(this);
        }
    }
}