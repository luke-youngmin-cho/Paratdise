using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/20
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어의 상태별로 동작하는 상태머신 베이스
/// </summary>
public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState playerState;
    public SkillType skillType;
    public State state;
    [HideInInspector] public PlayerStateMachineManager manager;
    [HideInInspector] public PlayerModelManager modelManager;
    [HideInInspector] public float animationTime;
    [HideInInspector] public float animationTimer;

    public virtual void Awake()
    {
        manager = GetComponent<PlayerStateMachineManager>();
        modelManager = GetComponent<PlayerModelManager>();
        animationTime = modelManager.GetAnimationTime(playerState.ToString());
    }

    public virtual bool IsExecuteOK()
    {
        return true;
    }

    public virtual void Execute()
    {
        state = State.Prepare;
        animationTimer = animationTime;
    }

    public virtual PlayerState Workflow()
    {
        PlayerState nextState = playerState;

        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                state++;
                break;
            case State.Casting:
                state++;
                break;
            case State.OnAction:
                state++;
                break;
            case State.Finish:
                nextState = PlayerState.Idle;
                state = State.Idle;
                break;
            default:
                break;
        }

        return nextState;
    }

    public virtual void ForceStop()
    {
        state = State.Idle;
    }

    public enum State
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish,
    }

    public enum SkillType
    {
        None,
        Basic,
        Active,
        Passive
    }
}