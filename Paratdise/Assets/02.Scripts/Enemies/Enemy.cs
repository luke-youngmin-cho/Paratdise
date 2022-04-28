using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float hpMax;
    public float damage;
    public float moveSpeed;
    public float knockBackForce;

    [Header("Target")]
    public LayerMask targetLayer;
    public float sightRange;
    public float releaseTargetTime;
    private Coroutine releaseTargetCoroutine;

    [Header("Digging Option")]
    public bool isDigger;
    public float diggingDamage;
    public float diggingDelay = 1f;
    private Coroutine digCoroutine = null;

    [HideInInspector] public bool isDead;
    private float _hp;
    public float hp
    {
        set
        {
            if (value > 0)
            {
                isDead = false;
                col.enabled = true;
                if (value < _hp)
                    controller.ChangeEnemyState(EnemyController.EnemyState.Hurt);
            }
            else
            {
                DropRandomItem();
                isDead = true;
                col.enabled = false;
                value = 0;
                controller.ChangeEnemyState(EnemyController.EnemyState.Die);
            }

            _hp = value;
            hpBar.value = (_hp / hpMax);
        }
        get
        {
            return _hp;
        }
    }
    [SerializeField] Slider hpBar;

    [Header("Animation")]
    public float hpBarShowTime;
    Coroutine hpBarShowCoroutine;

    // components
    Transform tr;
    EnemyController controller;
    CapsuleCollider2D col;
    SpriteRenderer spriteRenderer;
    Color originColor;

    [System.Serializable]
    public class DropItemInfo
    {
        public GameObject itemPrefab;
        public float dropRatio; // 0~100;
    }
    [SerializeField] private List<DropItemInfo> dropItems = new List<DropItemInfo>();

    //============================================================================
    //*************************** Public Methods *********************************
    //============================================================================

    /// <summary>
    /// Get single damage.
    /// Show HP bar for few seconds & Damage pop up
    /// </summary>
    public void Hurt(float damage)
    {
        ShowHPBarForSeconds();
        DamagePopUp.Create(tr.position + new Vector3(0f, col.size.y / 2, 0f), damage, gameObject.layer);

        FindTarget();
        hp -= damage;
        controller.TryHurt();
        if (_hp <= 0)
        {
            controller.TryDie();
            StartCoroutine(E_FadeOutWhenDead());
        }
    }

    //============================================================================
    //*************************** Private Methods ********************************
    //============================================================================

    private void Awake()
    {
        tr = GetComponent<Transform>();
        controller = GetComponent<EnemyController>();
        col = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        spriteRenderer.color = originColor;
        hp = hpMax;
    }

    private void OnDisable()
    {
        //ObjectPool.ReturnToPool(gameObject);
    }

    /// <summary>
    /// Detect player in range and set target
    /// </summary>
    private void FindTarget()
    {
        controller.autoFollowPlayer = true;
        if(releaseTargetCoroutine != null)
            StopCoroutine(releaseTargetCoroutine);
        releaseTargetCoroutine = StartCoroutine(E_ReleaseTarget());
    }

    /// <summary>
    /// Stop targeting / following player.
    /// </summary>
    IEnumerator E_ReleaseTarget()
    {
        yield return new WaitForSeconds(releaseTargetTime);
        releaseTargetCoroutine = null;
        if (controller.moveAIState == EnemyController.MoveAIState.FollowTarget)
            FindTarget();
    }

    /// <summary>
    /// Fade out for few seconds
    /// </summary>
    IEnumerator E_FadeOutWhenDead()
    {
        float elapsedTime = 0;
        Color originColor = spriteRenderer.color;
        while (elapsedTime < 1f)
        {
            spriteRenderer.color = new Color(originColor.r, originColor.g, originColor.b, 1f - elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    void ShowHPBarForSeconds()
    {
        if (hpBarShowCoroutine != null)
            StopCoroutine(E_ShowHPBarForSeconds());
        hpBarShowCoroutine = StartCoroutine(E_ShowHPBarForSeconds());
    }

    IEnumerator E_ShowHPBarForSeconds()
    {
        float elapsedTime = 0;
        while (elapsedTime < hpBarShowTime)
        {
            hpBar.gameObject.SetActive(true);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        hpBar.gameObject.SetActive(false);
        hpBarShowCoroutine = null;
    }

    IEnumerator E_WaitForDigDelay()
    {
        yield return new WaitForSeconds(diggingDelay);
        digCoroutine = null;
    }

    private void DropRandomItem()
    {
        foreach (var item in dropItems)
        {
            float rand = Random.Range(0f, 100.0f);
            if (rand < item.dropRatio)
            {
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
                break;
            }
        }
    }

    /// <summary>
    /// Hurt player when touch
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go == null) return;

        if (go.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = go.GetComponent<Player>();
            if (isDead == false &&
                player.hp > 0)
            {
                Vector2 forceVec = Vector2.zero;
                if (Mathf.Abs(controller.direction.x) > Mathf.Abs(controller.direction.y))
                    forceVec = new Vector2(controller.direction.x, 0).normalized;
                else
                    forceVec = new Vector2(0, controller.direction.y).normalized;

                forceVec *= knockBackForce;
                go.GetComponent<PlayerStateMachineManager>().KnockBack(forceVec);
                player.Hurt(damage);
            }
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        
        if (go == null ||
            isDigger == false)
        {
            Debug.Log($"Enemy : detected {go}");
            return;
        }
            
        if (digCoroutine != null)
        {
            Debug.Log($"Enemy : is digging...");
            return;
        }


        if (go.layer == LayerMask.NameToLayer("MapTile") &&
            go.tag == "Destroyable")
        {
            Debug.Log($"Enemy : detected destroyable maptile");
            if (go.TryGetComponent(out MapTile_Destroyable maptile))
            {
                maptile.hp -= diggingDamage;
                digCoroutine = StartCoroutine(E_WaitForDigDelay());
            }
        }
    }
}