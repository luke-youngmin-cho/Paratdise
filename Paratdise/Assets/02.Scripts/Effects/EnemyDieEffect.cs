using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/29
/// 최종수정일 : 
/// 설명 : 
/// 
/// 에너미가 죽을때 발생하는 이펙트
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
