using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵타일 단위의 Fluid 뭉텅이를 관리할 클래스.
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
    /// fluid bundle 생성후 fluid 들을 보여주기위해서 한번 호출해야함.
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