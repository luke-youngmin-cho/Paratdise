using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/03
/// ���������� : 
/// ���� : 
/// 
/// �پ��� �þ����ϴ� ����Ʈ
/// </summary>
public class UIScalingEffect : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 scaleAxis;
    [SerializeField] private float scaleMax;
    [SerializeField] private float period;
    private float timer;

    private void OnEnable()
    {
        timer = period;
    }
    // Update is called once per frame
    void Update()
    {
        //if (timer > period / 2 && 
        //    target.localScale.x < scaleMax)
        //    target.localScale += scaleAxis * (scaleMax - 1) * period * timer;
        //else if (timer > 0 &&
        //         target.localScale.x > 1)
        //    target.localScale -= scaleAxis * (scaleMax - 1) * period * timer;
        //else if (timer < 0)
        //{
        //    timer = period;
        //    target.localScale = new Vector3(1, 1, 1);
        //}
        //timer -= Time.deltaTime;
    }
}
