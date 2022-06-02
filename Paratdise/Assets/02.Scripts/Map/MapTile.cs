using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/23
/// ���������� : 
/// ���� : 
/// 
/// ���� �⺻ ���� Ÿ�Ͽ� Ŭ����. 
/// �̺�Ʈ �߻��� ���� �����¿� ��带 ������Ʈ��.
/// �ʿ�� �� ��� ������Ʈ �� �̺�Ʈ �Լ�ȣ�� �ؾ���.
/// </summary>

public class MapTile : MonoBehaviour
{
    [Header("��Ÿ���� ������ �����ϼ���")]
    public MapTileType type;
    [Header("��Ÿ�� �ĺ����Դϴ�. �������� �� ��")]
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
    /// �����¿� ��� ����
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
    /// ������Ʈ Ǯ�� ���ư�
    /// </summary>
    public void ReturnToPool()
    {
        ObjectPool.ReturnToPool(gameObject);
    }

    /// <summary>
    /// ������Ʈ Ǯ�� ���ư��鼭 ���� ���鿡���� ��ȣ�ۿ� �̺�Ʈ ȣ��
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
    /// ��Ÿ�Ͽ� Ư���� �̺�Ʈ�� �ʿ��Ұ��, �� �Լ��� �������̵�.
    /// </summary>
    public virtual void NodeInteractionEvent()
    {
        RefreshNear();
    }

    /// <summary>
    /// �ٷ������ִ� Ÿ���� ������Ʈ Ǯ�� �ǵ����� �Լ�
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