using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/29
/// ���������� : 
/// ���� : 
/// 
/// ���ʹ̰� ������ �߻��ϴ� ����Ʈ
/// </summary>
public class EnemyDieEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float scaleUpSpeed = 1f;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float fadeOutSpeed = 1f;
    [SerializeField] private float lifeTime = 2f;
    private float elapsedTime = 0f;

    private void Update()
    {
        if (elapsedTime > lifeTime)
            gameObject.SetActive(false);
        else
        {
            Color c = sr.color;
            c = new Color(c.r, c.g, c.b, c.a - fadeOutSpeed * Time.deltaTime);
            sr.color = c;
            transform.localScale += new Vector3(1f, 1f, 1f) * scaleUpSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
    }
}
