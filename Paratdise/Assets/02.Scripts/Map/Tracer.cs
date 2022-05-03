using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 2022/04/29
/// 설명 : 
/// 
/// 추격스테이지의 추격자
/// 가로 움직이는 효과 추가
/// </summary>

public class Tracer : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float delayTime = 4f;
    [SerializeField] private float animationSpeed = 1f;
    Transform tr;
    BoxCollider2D col;
    Transform renderer1;
    Transform renderer2;
    Transform renderer3;
    Vector2 pos1;
    Vector2 pos2;
    Vector2 pos3;
    float width;
    float scale;


    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public void StartMove()
    {
        StartCoroutine(E_Move());
    }


    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Awake()
    {
        tr = transform;
        col = GetComponent<BoxCollider2D>();
        renderer1 = tr.GetChild(0);
        renderer2 = tr.GetChild(1);
        renderer3 = tr.GetChild(2);
        pos1 = renderer1.position;
        pos2 = renderer2.position;
        pos3 = renderer3.position;
        width = col.size.x / 3;
        scale = renderer1.localScale.x;
        PlayStateManager.instance.OnPlayStateChanged += OnPlayStateChanged;
    }

    private void OnDestroy()
    {
        PlayStateManager.instance.OnPlayStateChanged -= OnPlayStateChanged;
    }

    private void OnPlayStateChanged(PlayState newPlayState)
    {
        enabled = newPlayState == PlayState.Play;
    }

    private void FixedUpdate()
    {
        if (renderer1.position.x - pos1.x >= width)
            renderer1.position = new Vector2(pos3.x, renderer1.position.y);

        if (renderer2.position.x - pos1.x >= width)
            renderer2.position = new Vector2(pos3.x, renderer2.position.y);

        if (renderer3.position.x - pos1.x >= width)
            renderer3.position = new Vector2(pos3.x, renderer3.position.y);
        renderer1.localPosition += Vector3.right * animationSpeed * Time.fixedDeltaTime;
        renderer2.localPosition += Vector3.right * animationSpeed * Time.fixedDeltaTime;
        renderer3.localPosition += Vector3.right * animationSpeed * Time.fixedDeltaTime;
    }


    IEnumerator E_Move()
    {
        yield return new WaitForSeconds(delayTime);
        float elapsedTime = 0f;
        while (true)
        {
            if (elapsedTime / 5 >= 1)
            {
                speed = speed * (1.2f);
                elapsedTime = 0;
            }
            tr.Translate(Vector3.up * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player.instance.Hurt(10);
        }
    }
}