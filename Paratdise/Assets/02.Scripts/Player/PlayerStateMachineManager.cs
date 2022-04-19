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

    public PlayerState state;
    public float moveSpeed;
    public Vector2 move;
    Vector2 _direction;
    public Vector2 direction
    {
        set
        {
            if (state != PlayerState.Movement) return;

            if (value != _direction)
            {
                if (value == Vector2.up)
                    modelManager.LookBack();
                else if (value == Vector2.down)
                    modelManager.LookFront();
                else if (value == Vector2.left)
                    modelManager.LookLeft();
                else if (value == Vector2.right)
                    modelManager.LookRight();

                _direction = value;
            }

            Collider2D mapTile = Physics2D.OverlapCircle(rb.position + _direction / 2, 0.01f, digTargetLayer);

            if (mapTile != null)
                Debug.Log(mapTile);

            if (mapTile != null &&
                mapTile.tag == "Destroyable")
            {
                ChangeState(PlayerState.Dig);
                move = Vector2.zero;
            }
            else
            {   
                move = value;
            }

            modelManager.SetFloat("h", move.x);
            modelManager.SetFloat("v", move.y);
        }
        get { return _direction; }
    }
    [SerializeField] private LayerMask digTargetLayer;

    Rigidbody2D rb;
    Transform tr;

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
                currentMachine.ForceStop();
                currentMachine = sub;
                currentMachine.Execute();
                state = newState;
            }
        }
    }


    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void Awake()
    {
        instance = this;
        machines = GetComponents<PlayerStateMachine>();
        modelManager = GetComponent<PlayerModelManager>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        currentMachine = GetMachine(PlayerState.Movement);
        
    }

    private void Start()
    {
        direction = Vector2.up;
    }

    private void Update()
    {
        

        UpdateState();
    }

    private void FixedUpdate()
    {        
        tr.Translate(move * moveSpeed * Time.fixedDeltaTime);
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
