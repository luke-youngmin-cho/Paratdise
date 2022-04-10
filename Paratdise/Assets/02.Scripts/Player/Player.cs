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
public class Player : MonoBehaviour
{
    public static Player instance;
    public float moveSpeed;
    public Vector2 move;
    int _direction; // -1 left 1 right
    public int direction
    {
        set
        {
            if (value < 0)
                rendererPoint.eulerAngles = Vector3.zero;
            else if (value > 0)
                rendererPoint.eulerAngles = new Vector3(0, 180, 0);
        }
        get { return _direction; }
    }
    public int directionInit;
    Transform tr;
    Transform rendererPoint;

    public Transform spoonPoint;
    public LayerMask targetLayer;
    public Animator animator;
    private void Awake()
    {
        instance = this;
        tr = transform;
        rendererPoint = transform.GetChild(0);
        direction = directionInit;
        
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        move = new Vector2(h, v);
        if (move.x < 0) direction = -1;
        else if (move.x > 0) direction = 1;


        Vector2 digDir = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
            digDir = Vector2.left;
        else if (Input.GetKey(KeyCode.RightArrow))
            digDir = Vector2.right;
        else if (Input.GetKey(KeyCode.UpArrow))
            digDir = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow))
            digDir = Vector2.down;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            RaycastHit2D hit = Physics2D.CircleCast((Vector2)tr.position + digDir, 0.1f, digDir , 0, targetLayer);
            if (hit.collider != null)
            {
                animator.SetTrigger("isAttack");
                if (hit.collider.gameObject.TryGetComponent(out MapTile mapTile))
                {
                    if(mapTile.type != MapTileType.Boundary)
                        mapTile.ReturnToPoolInteraction();
                }    
                    
                else if (hit.collider.gameObject.TryGetComponent(out Obstacle obstacle))
                    obstacle.ReturnToPool();
            }
        }
    }

    public void Digging()
    {   
        RaycastHit2D hit = Physics2D.CircleCast(new Vector2(spoonPoint.position.x, spoonPoint.position.y), 0.1f, Vector2.right * direction, 0, targetLayer);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            hit.collider.gameObject.GetComponent<MapTile>().ReturnToPoolInteraction();
        }
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
            if(Input.GetKey(KeyCode.Z))
                collision.gameObject.GetComponent<ItemController>().PickUp(this);
        }
    }
}