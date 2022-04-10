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
    Fluid[] fluids;
    Vector2[] fluidsPos;

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    /// <summary>
    /// fluid bundle ������ fluid ���� �����ֱ����ؼ� �ѹ� ȣ���ؾ���.
    /// </summary>
    public void ReleaseAllChildren()
    {
        foreach (var fluid in fluids)
            fluid.gameObject.SetActive(true);
        transform.DetachChildren();
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