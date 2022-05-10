using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/20
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어의 하위 상태 머신들을 관리하는 매니저클래스
/// </summary>
public class PlayerStateMachineManager : MonoBehaviour
{
    public static PlayerStateMachineManager instance;

    public bool controllable = true;
    public PlayerState state;
    public float moveSpeed;
    public Vector2 move;
    Vector2 _direction;
    public Vector2 direction
    {
        set
        {
            if (state != PlayerState.Movement ||
                state == PlayerState.Dig ||
                controllable == false) 
                return;

            if (value == Vector2.zero)
            {
                move = Vector2.zero;
                modelManager.SetFloat("h", 0);
                modelManager.SetFloat("v", 0);
            }
            else
            {
                Vector2 normalizedValue = value.normalized;
                Vector2 overlapSize = Vector2.zero;

                if (normalizedValue != _direction)
                {
                    if (normalizedValue == Vector2.up)
                    {
                        modelManager.LookBack();
                        overlapSize = new Vector2(col.size.x, 0.01f);
                    }
                    else if (normalizedValue == Vector2.down)
                    {
                        modelManager.LookFront();
                        overlapSize = new Vector2(col.size.x, 0.01f);
                    }
                    else if (normalizedValue == Vector2.left)
                    {
                        modelManager.LookLeft();
                        overlapSize = new Vector2(0.01f, col.size.x);
                    }
                    else if (normalizedValue == Vector2.right)
                    {
                        modelManager.LookRight();
                        overlapSize = new Vector2(0.01f, col.size.x);
                    }

                    _direction = normalizedValue;
                }

                Collider2D mapTile = Physics2D.OverlapBox(rb.position + _direction * (col.size.y / 2), overlapSize, 0, digTargetLayer);

                //if (mapTile != null)
                //    Debug.Log(mapTile);

                if (mapTile != null &&
                    mapTile.tag == "Destroyable")
                {
                    ChangeState(PlayerState.Dig);
                    //move = Vector2.zero;
                }
                else
                {
                    move = value;
                }

                modelManager.SetFloat("h", move.x);
                modelManager.SetFloat("v", move.y);
            }
            
        }
        get { return _direction; }
    }
    [SerializeField] private LayerMask digTargetLayer;

    Rigidbody2D rb;
    Transform tr;
    CapsuleCollider2D col;

    PlayerStateMachine[] machines;
    PlayerStateMachine currentMachine;
    PlayerModelManager modelManager;


    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void ChangeState(PlayerState newState)
    {
        if (state == newState) return;

        foreach (var sub in machines)
        {
            if (sub.playerState == newState &&
                sub.IsExecuteOK())
            {
                //if (newState != PlayerState.Dig )
                    move = Vector2.zero;
                modelManager.SetFloat("h", move.x);
                modelManager.SetFloat("v", move.y);
                currentMachine.ForceStop();
                currentMachine = sub;
                currentMachine.Execute();
                state = newState;
            }
        }
    }

    public void KnockBack(Vector2 forceVec)
    {
        move = Vector2.zero;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(forceVec.x , forceVec.y), ForceMode2D.Impulse);
        direction = -forceVec.normalized;
    }

    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void Awake()
    {
        instance = this;
        controllable = true;
        machines = GetComponents<PlayerStateMachine>();
        modelManager = GetComponent<PlayerModelManager>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        col = GetComponent<CapsuleCollider2D>();
        currentMachine = GetMachine(PlayerState.Movement);
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

    private void Start()
    {
        direction = Vector2.up;
    }

    private void Update()
    {
        if(controllable)
            UpdateState();
    }

    private void FixedUpdate()
    {
        if (move == Vector2.zero)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = move * moveSpeed;
    }

    private void UpdateState()
    {
        ChangeState(currentMachine.Workflow());
    }

    private PlayerStateMachine GetMachine(PlayerState targetState)
    {
        foreach (var sub in machines)
        {
            if (sub.playerState == targetState)
                return sub;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        Vector2 overlapSize = Vector2.zero;
        if (_direction == Vector2.up)
            overlapSize = new Vector2(col.size.x, 0.01f);
        else if (_direction == Vector2.down)
            overlapSize = new Vector2(col.size.x, 0.01f);
        else if (_direction == Vector2.left)
            overlapSize = new Vector2(0.01f, col.size.x);
        else if (_direction == Vector2.right)
            overlapSize = new Vector2(0.01f, col.size.x);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(rb.position + _direction * (col.size.y / 2), overlapSize);
    }
}

public enum PlayerState
{
    Idle,
    Movement,
    Dig,
    Attack,
    Hurt,
    Die,
}
