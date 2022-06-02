using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵의 기본 단위 타일용 클래스. 
/// 이벤트 발생시 마다 상하좌우 노드를 업데이트함.
/// 필요시 각 노드 업데이트 후 이벤트 함수호출 해야함.
/// </summary>

public class MapTile : MonoBehaviour
{
    [Header("맵타일의 종류를 선택하세요")]
    public MapTileType type;
    [Header("맵타일 식별용입니다. 수정하지 말 것")]
    public LayerMask layer;
    public MapTile up;
    public MapTile down;
    public MapTile left;
    public MapTile right;

    [HideInInspector] public BoxCollider2D col;

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    /// <summary>
    /// 상하좌우 노드 갱신
    /// </summary>
    public virtual void RefreshNear()
    {
        Collider2D upCol = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + Vector2.up * col.size.y, 0.01f, layer);
        up = upCol != null ? upCol.GetComponent<MapTile>() : null;

        Collider2D downCol = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + Vector2.down * col.size.y, 0.01f, layer);
        down = downCol != null ? downCol.GetComponent<MapTile>() : null;

        Collider2D leftCol = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + Vector2.left * col.size.x, 0.01f, layer);
        left = leftCol != null ? leftCol.GetComponent<MapTile>() : null;

        Collider2D rightCol = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + Vector2.right * col.size.x, 0.01f, layer);
        right = rightCol != null ? rightCol.GetComponent<MapTile>() : null;
    }

    /// <summary>
    /// 오브젝트 풀로 돌아감
    /// </summary>
    public void ReturnToPool()
    {
        ObjectPool.ReturnToPool(gameObject);
    }

    /// <summary>
    /// 오브젝트 풀로 돌아가면서 주위 노드들에대한 상호작용 이벤트 호출
    /// </summary>
    public void ReturnToPoolInteraction()
    {
        RefreshNear();
        gameObject.SetActive(false);
        ObjectPool.ReturnToPool(gameObject);
        if (up != null) up.NodeInteractionEvent();
        if (down != null) down.NodeInteractionEvent();
        if (left != null) left.NodeInteractionEvent();
        if (right != null) right.NodeInteractionEvent();
    }

    /// <summary>
    /// 맵타일에 특별한 이벤트가 필요할경우, 이 함수를 오버라이드.
    /// </summary>
    public virtual void NodeInteractionEvent()
    {
        RefreshNear();
    }

    /// <summary>
    /// 바로위에있는 타일을 오브젝트 풀로 되돌리는 함수
    /// </summary>
    public bool TryRemoveUp()
    {
        if (up != null)
        {
            up.ReturnToPool();
            return true;
        }
        return false;
    }

    public bool TryRemoveDown()
    {
        if (down != null)
        {
            down.ReturnToPool();
            return true;
        }
        return false;
    }

    public bool TryRemoveLeft()
    {
        if (left != null)
        {
            left.ReturnToPool();
            return true;
        }
        return false;
    }

    public bool TryRemoveRight()
    {
        if (right != null)
        {
            right.ReturnToPool();
            return true;
        }
        return false;
    }
    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    public virtual void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public virtual void OnEnable()
    {
        Collider2D[] centers = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 0.01f, layer);
        foreach (var center in centers)
        {
            //Debug.Log($"{gameObject.name} detected {center.name}");
            if (center.gameObject != gameObject)
            {
                center.gameObject.SetActive(false);
                center.gameObject.GetComponent<MapTile>().ReturnToPool();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + Vector3.up * col.size.y, 0.05f);
        Gizmos.DrawSphere(transform.position + Vector3.down * col.size.y, 0.05f);
        Gizmos.DrawSphere(transform.position + Vector3.left * col.size.y, 0.05f);
        Gizmos.DrawSphere(transform.position + Vector3.right * col.size.y, 0.05f);
    }
}
public enum MapTileType
{
    Boundary,
    Basic,
    NaturalDisaster,
    Obstacle,
    Event,
    Start,
    End
}