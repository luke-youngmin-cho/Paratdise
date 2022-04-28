using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Enemy actions & AI
/// </summary>
public class EnemyController : MonoBehaviour
{
    public bool limitMovement = true;

    [Header("Parameters")]
    public bool attackEnabled;

    public Vector2 attackRangeCenter;
    public Vector2 attackRangeSize;
    public bool moveEnabled;

    public float hurtTimeOffset;

    [Header("Components")]
    public Enemy enemy;

    [Header("States")]
    private EnemyState newState;
    private EnemyState oldState;
    private IdleState idleState;
    private MoveState moveState;
    private AttackState attackState;
    private HurtState hurtState;
    private DieState dieState;

    [Header("AI")]
    public bool normalOn = false;
    public MoveAIState moveAIState;
    public float thinkingTimeMin = 0.3f;
    public float thinkingTimeMax = 3f;
    float moveAIStateTime;
    float moveAIStateTimeElapsed;
    public bool autoFollowPlayer;
    public float autoFollowRange;
    public float followingDelay = 1f;
    public Vector2 followVec;
    public LayerMask targetLayer;
    private Player target;

    [Header("Kinematics")]
    Rigidbody2D rb;
    CapsuleCollider2D col;
    Vector2 move;
    Vector2 targetVelocity;
    Vector2 _direction; // +1 : right -1 : left

    public Vector2 direction
    {
        set
        {
            _direction = value.normalized;
            if (_direction.x < 0)
                transform.eulerAngles = Vector3.zero;
            else if (_direction.x > 0)
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        get
        {
            return _direction;
        }
    }
    public Vector2 directionInit = Vector2.left;

    [Header("Animations")]
    Animator animator;
    string currentAnimationName;
    float animationTimer;
    float attackTime;
    float hurtTime;
    float dieTime;


    //============================================================================
    //*************************** Public Methods *********************************
    //============================================================================
    public void TryHurt()
    {
        if (oldState == EnemyState.Hurt)
            animationTimer = 0; // keep hurting animation
        else if (IsHurtPossible())
            ChangeEnemyState(EnemyState.Hurt);
    }

    public void TryDie()
    {
        ChangeEnemyState(EnemyState.Die);
    }

    public void KnockBack(Vector2 forceVec)
    {
        if (moveEnabled == false) return;
        move = Vector2.zero;
        rb.velocity = Vector2.zero;
        rb.AddForce(forceVec, ForceMode2D.Impulse);
    }

    virtual public void AttackBehavior(List<GameObject> castedTargets)
    {
        
        if (target.TryGetComponent(out Player player))
        {
            if (player.hp > 0)
            {
                PlayerStateMachineManager playerController = target.GetComponent<PlayerStateMachineManager>();

                Vector2 forceVec = Vector2.zero;
                if (Mathf.Abs(_direction.x) > Mathf.Abs(_direction.y))
                    forceVec = new Vector2(_direction.x, 0).normalized;
                else
                    forceVec = new Vector2(0, _direction.y).normalized;

                forceVec *= enemy.knockBackForce;
                playerController.KnockBack(forceVec);
                player.Hurt(enemy.damage);
            }
        }
    }


    //============================================================================
    //*************************** Private Methods ********************************
    //============================================================================

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();

        direction = directionInit;
        attackTime = GetAnimationTime("Attack");
        hurtTime = GetAnimationTime("Hurt");
        dieTime = GetAnimationTime("Die");
    }

    private void Start()
    {
        if (normalOn)
            moveAIState = MoveAIState.DecideRandomBehavior;
        else
            StartCoroutine(E_ExecuteAIWhenPlayerDetected());
    }

    private void Update()
    {

        AIWorkflow();

        if (moveEnabled &&
            moveAIState > MoveAIState.Idle)
        {
            if (limitMovement)
            {
                if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
                    move = new Vector2(move.x, 0).normalized;
                else
                    move = new Vector2(0, move.y).normalized;
            }

            direction = move;

            if (Mathf.Abs(_direction.magnitude) > 0.1f)
                ChangeEnemyState(EnemyState.Move);
            else
                ChangeEnemyState(EnemyState.Idle);
        }

        UpdateEnemyState();
    }

    private void FixedUpdate()
    {
        FixedUpdateMovement();
    }

    void AIWorkflow()
    {
        if (oldState == EnemyState.Hurt && ((int)moveAIState < (int)MoveAIState.FollowTarget))
            moveAIState = MoveAIState.FollowTarget;

        // autofollow
        if (autoFollowPlayer)
        {
            RaycastHit2D hit = Physics2D.CircleCast(rb.position,
                                                    autoFollowRange,
                                                    Vector2.zero,
                                                    0,
                                                    targetLayer);

            if (hit.collider != null)
            {
                if (moveAIState < MoveAIState.FollowTarget)
                {
                    moveAIStateTimeElapsed = 0;
                    moveAIState = MoveAIState.FollowTarget;
                }
                target = hit.collider.GetComponent<Player>();
            }
            else
                target = null;
            
        }

        switch (moveAIState)
        {
            case MoveAIState.Idle:
                break;
            case MoveAIState.DecideRandomBehavior:
                move = Vector2.zero;
                followVec = Vector2.zero;
                moveAIStateTime = Random.Range(thinkingTimeMin, thinkingTimeMax);
                moveAIStateTimeElapsed = 0;
                moveAIState = (MoveAIState)Random.Range(2, 7);
                break;
            case MoveAIState.TakeARest:
                if (moveAIStateTimeElapsed > moveAIStateTime)
                {
                    moveAIState = MoveAIState.DecideRandomBehavior;
                }
                else
                    move = Vector2.zero;
                moveAIStateTimeElapsed += Time.deltaTime;
                break;
            case MoveAIState.MoveUp:
                if (moveAIStateTimeElapsed > moveAIStateTime)
                    moveAIState = MoveAIState.DecideRandomBehavior;
                else
                    move = Vector2.up;
                moveAIStateTimeElapsed += Time.deltaTime;
                break;
            case MoveAIState.MoveDown:
                if (moveAIStateTimeElapsed > moveAIStateTime)
                    moveAIState = MoveAIState.DecideRandomBehavior;
                else
                    move = Vector2.down;
                moveAIStateTimeElapsed += Time.deltaTime;
                break;
            case MoveAIState.MoveRight:
                if (moveAIStateTimeElapsed > moveAIStateTime)
                    moveAIState = MoveAIState.DecideRandomBehavior;
                else
                    move = Vector2.right;
                moveAIStateTimeElapsed += Time.deltaTime;
                break;
            case MoveAIState.MoveLeft:
                if (moveAIStateTimeElapsed > moveAIStateTime)
                    moveAIState = MoveAIState.DecideRandomBehavior;
                else
                    move = Vector2.left;
                moveAIStateTimeElapsed += Time.deltaTime;
                break;
            case MoveAIState.FollowTarget:
                if (target != null)
                {   
                    if (moveAIStateTimeElapsed > followingDelay)
                    {
                        followVec = target.transform.position - transform.position;
                        moveAIStateTimeElapsed = 0;
                    }
                    moveAIStateTimeElapsed += Time.deltaTime;

                    move = followVec;

                    //if (attackEnabled && oldState != EnemyState.Attack)
                    //{
                    //    // try attack if the target in range
                    //}
                }
                else
                {
                    moveAIState = MoveAIState.DecideRandomBehavior;
                }
                break;
            case MoveAIState.AttackingTarget:
                if (oldState == EnemyState.Idle)
                {
                    ChangeEnemyState(EnemyState.Idle);
                    moveAIState = MoveAIState.FollowTarget;
                }

                break;
            default:
                break;
        }
    }

    void UpdateEnemyState()
    {
        switch (newState)
        {
            case EnemyState.Idle:
                UpdateIdleState();
                break;
            case EnemyState.Move:
                UpdateMoveState();
                break;
            case EnemyState.Attack:
                UpdateAttackState();
                break;
            case EnemyState.Hurt:
                UpdateHurtState();
                break;
            case EnemyState.Die:
                UpdateDieState();
                break;
        }
    }

    void UpdateIdleState()
    {
        switch (idleState)
        {
            case IdleState.Idle:
                break;
            case IdleState.Prepare:
                rb.velocity = Vector2.zero;
                ChangeAnimationState("Idle");
                moveState++;
                break;
            case IdleState.Casting:
                moveState++;
                break;
            case IdleState.OnAction:
                break;
            case IdleState.Finish:
                break;
            default:
                break;
        }
    }

    void UpdateMoveState()
    {
        switch (moveState)
        {
            case MoveState.Idle:
                break;
            case MoveState.Prepare:
                rb.velocity = Vector2.zero;
                ChangeAnimationState("Move");
                moveState++;
                break;
            case MoveState.Casting:
                moveState++;
                break;
            case MoveState.OnAction:
                break;
            case MoveState.Finish:
                break;
            default:
                break;
        }
    }

    void UpdateAttackState()
    {
        switch (attackState)
        {
            case AttackState.PrepareToAttack:
                ChangeAnimationState("Attack");
                attackState = AttackState.AttackCasting;
                break;
            case AttackState.AttackCasting:
                if (animationTimer > (attackTime * animator.speed) / 2)
                {
                    // todo -> cast target

                    attackState = AttackState.Attacking;
                }
                animationTimer += Time.deltaTime;
                break;
            case AttackState.Attacking:
                if (animationTimer > (attackTime * animator.speed))
                {
                    attackState = AttackState.Attacked;
                }
                animationTimer += Time.deltaTime;
                break;
            case AttackState.Attacked:
                ChangeEnemyState(EnemyState.Idle);
                break;
        }
    }

    void UpdateHurtState()
    {
        switch (hurtState)
        {
            case HurtState.PrepareToHurt:
                ChangeAnimationState("Hurt");
                hurtState = HurtState.Hurting;
                break;
            case HurtState.Hurting:
                if (animationTimer > (hurtTime * animator.speed + hurtTimeOffset))
                {
                    ChangeEnemyState(EnemyState.Idle);
                }
                animationTimer += Time.deltaTime;
                break;
        }
    }

    void UpdateDieState()
    {
        switch (dieState)
        {
            case DieState.PrepareToDie:
                ChangeAnimationState("Die");
                move = Vector2.zero;
                dieState = DieState.Dying;
                break;
            case DieState.Dying:
                if (animationTimer > dieTime * animator.speed)
                {
                    dieState = DieState.Dead;
                }
                animationTimer += Time.deltaTime;
                break;
            case DieState.Dead:
                // todo -> dead event
                break;
        }
    }

    public void ChangeEnemyState(EnemyState stateToChange)
    {
        if (oldState == stateToChange) return;
        newState = stateToChange;
        ResetAllMotion();
        switch (newState)
        {
            case EnemyState.Idle:
                idleState = IdleState.Prepare;
                break;
            case EnemyState.Move:
                moveState = MoveState.Prepare;
                break;
            case EnemyState.Attack:
                if (attackEnabled)
                    attackState = AttackState.PrepareToAttack;
                else
                    ChangeEnemyState(EnemyState.Idle);
                break;
            case EnemyState.Hurt:
                hurtState = HurtState.PrepareToHurt;
                break;
            case EnemyState.Die:
                dieState = DieState.PrepareToDie;
                break;
        }
        oldState = newState;
    }

    void ResetAllMotion()
    {
        move = Vector2.zero;
        animationTimer = 0;
        attackState = AttackState.Idle;
        hurtState = HurtState.Idle;
        dieState = DieState.Idle;

        if (rb.bodyType == RigidbodyType2D.Kinematic)
            rb.bodyType = RigidbodyType2D.Dynamic;
        if (animator.speed < 1f)
            animator.speed = 1f;
    }

    void ChangeAnimationState(string newAnimationName)
    {
        if (currentAnimationName == newAnimationName) return;

        animator.Play(newAnimationName);
        currentAnimationName = newAnimationName;
    }

    bool IsHurtPossible()
    {
        bool isOK = false;
        if (oldState != EnemyState.Attack)
            isOK = true;
        return isOK;
    }

    void ComputeVelocity()
    {
        Vector2 velocity = move * enemy.moveSpeed;
        targetVelocity = velocity;
    }

    void FixedUpdateMovement()
    {
        if (moveEnabled == false) return;
        ComputeVelocity();
        rb.position += targetVelocity * Time.fixedDeltaTime;
    }

    float GetAnimationTime(string name)
    {
        float time = 0;
        //RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        //for (int i = 0; i < ac.animationClips.Length; i++)
        //{
        //    if (ac.animationClips[i].name == name)
        //    {
        //        time = ac.animationClips[i].length;
        //    }
        //}
        return time;
    }

    IEnumerator E_ExecuteAIWhenPlayerDetected()
    {
        yield return new WaitUntil(() =>
        {
            RaycastHit2D hit = Physics2D.CircleCast(rb.position,
                                                autoFollowRange,
                                                Vector2.zero,
                                                0,
                                                targetLayer);

            if (hit.collider != null &&
                hit.collider.TryGetComponent(out Player player))
            {
                Debug.Log("AI on");
                moveAIState = MoveAIState.FollowTarget;
                return true;
            }
            else
                return false;
        });
    }
     
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, autoFollowRange);
    }


    //============================================================================
    //******************************* Enums **************************************
    //============================================================================

    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Hurt,
        Die,
    }

    public enum IdleState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish,
    }

    public enum MoveState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish,
    }

    public enum AttackState
    {
        Idle,
        PrepareToAttack,
        AttackCasting,
        Attacking,
        Attacked
    }
    public enum HurtState
    {
        Idle,
        PrepareToHurt,
        Hurting,
        Hurted,
    }
    public enum DieState
    {
        Idle,
        PrepareToDie,
        Dying,
        Dead,
    }
    public enum MoveAIState
    {
        Idle,
        DecideRandomBehavior,
        TakeARest,
        MoveUp,
        MoveDown,
        MoveRight,
        MoveLeft,
        FollowTarget,
        AttackingTarget,
    }

}