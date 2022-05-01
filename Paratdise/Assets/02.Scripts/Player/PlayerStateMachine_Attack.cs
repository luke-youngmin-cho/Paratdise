using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/20
/// 최종수정일 : 
/// 설명 : 
/// 
/// Attack 상태 머신 확장
/// </summary>
public class PlayerStateMachine_Attack : PlayerStateMachine
{
    [SerializeField] private Vector2 castingSize;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float damage;
    [SerializeField] private float knockBackForce = 1f;

    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }
    public override PlayerState Workflow()
    {
        PlayerState nextState = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                manager.move = Vector2.zero;
                modelManager.Play("Attack");
                state++;
                break;
            case State.Casting:
                if (animationTimer < animationTime * 0.3)
                {
                    // cast enemies
                    Collider2D[] enemies = Physics2D.OverlapBoxAll(rb.position + manager.direction * (col.size.y / 2), castingSize, 0, targetLayer);
                    foreach (var enemy in enemies)
                    {
                        Vector2 forceVec = Vector2.zero;
                        if (Mathf.Abs(manager.direction.x) > Mathf.Abs(manager.direction.y))
                            forceVec = new Vector2(manager.direction.x, 0).normalized;
                        else
                            forceVec = new Vector2(0, manager.direction.y).normalized;

                        forceVec *= knockBackForce;
                        enemy.GetComponent<EnemyController>().KnockBack(forceVec);
                        enemy.GetComponent<Enemy>().Hurt(damage);
                    }
                    state++;
                }
                animationTimer -= Time.deltaTime * modelManager.animationSpeedGain;
                break;
            case State.OnAction:
                if (animationTimer < 0)
                    state++;
                else
                    animationTimer -= Time.deltaTime * modelManager.animationSpeedGain;
                break;
            case State.Finish:
                nextState = PlayerState.Movement;
                break;
            default:
                break;
        }
        return nextState;
    }

    private void OnDrawGizmos()
    {
        if (state > State.Idle)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(rb.position + manager.direction * (col.size.y / 2), castingSize);
        }
    }
}