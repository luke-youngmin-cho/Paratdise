using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/23
/// ���������� : 
/// ���� : 
/// 
/// ��Ÿ�� ������ Fluid �����̸� ������ Ŭ����.
/// </summary>

public class FluidBundle : MonoBehaviour
{
    [SerializeField] private LayerMask mapTileLayer;
    Fluid[] fluids;
    Vector2[] fluidsPos;

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    /// <summary>
    /// fluid bundle ������ fluid ���� �����ֱ����ؼ� �ѹ� ȣ���ؾ���.
    /// </summary>
    public Fluid[] ReleaseAllChildren()
    {
        foreach (var fluid in fluids)
            fluid.gameObject.SetActive(true);
            
        transform.DetachChildren();
        return fluids;
    }

    public void BringChild(Fluid fluid)
    {
        fluid.transform.SetParent(transform);
        fluid.transform.localPosition = Vector3.zero;
        fluid.gameObject.SetActive(false);
        if (transform.childCount == fluids.Length)
        {
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        BringAllChildren();
        ArrangeChildren();
        ObjectPool.ReturnToPool(gameObject);
    }


    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Awake()
    {
        fluids = GetComponentsInChildren<Fluid>();
        fluidsPos = new Vector2[fluids.Length];
        for (int i = 0; i < fluids.Length; i++)
        {
            fluids[i].SetMotherBundle(this);
            fluidsPos[i] = fluids[i].transform.localPosition;
        }
    }
    private void OnEnable()
    {
        Collider2D[] centers = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 0.05f, mapTileLayer);
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

    private void ArrangeChildren()
    {
        for (int i = 0; i < fluids.Length; i++)
            fluids[i].transform.localPosition = fluidsPos[i];
    }

    private void BringAllChildren()
    {
        foreach (Fluid fluid in fluids)
        {
            fluid.transform.SetParent(transform);
            fluid.transform.localPosition = Vector3.zero;
            fluid.gameObject.SetActive(false);
        }
    }
}