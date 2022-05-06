using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/04
/// ���������� : 2022/04/29
/// ���� : 
/// 
/// �߰ݽ��������� �߰���
/// ���� �����̴� ȿ�� �߰�
/// </summary>

public class Tracer_Lasor : Tracer
{
    [SerializeField] private GameObject lasorPrefab;
    [SerializeField] private float shotDelay = 4f;
    [SerializeField] private float shotPeriod = 4f;
    [SerializeField] private float shotAccel = 1.2f;
    private float mapHeight;

    public override void StartMove()
    {
        mapHeight = MapCreater.mapHeight;
        StartCoroutine(E_Shot());
    }

    IEnumerator E_Shot()
    {
        yield return new WaitUntil(() => Player.instance != null);
        yield return new WaitForSeconds(shotDelay);
        float elapsedTime = 0f;
        while (true)
        {
            if (elapsedTime / 5 >= 1)
            {
                float x = Random.Range(-mapHeight, mapHeight);
                float y = Mathf.Sqrt((mapHeight * mapHeight) / (x * x));
                Vector3 startPos = Player.instance.transform.position + new Vector3(x, y, 0);
                GameObject lasor = Instantiate(lasorPrefab, startPos, Quaternion.identity);
                lasor.GetComponent<Lasor>().speed *= shotAccel;
                shotPeriod = shotPeriod / (shotAccel);
                elapsedTime = 0;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    protected override void Awake()
    {
        tr = transform;
        PlayStateManager.instance.OnPlayStateChanged += OnPlayStateChanged;
    }
    protected override void FixedUpdate()
    {
        // donothing
    }
}