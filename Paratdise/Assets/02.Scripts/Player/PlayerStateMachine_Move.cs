using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/20
/// 최종수정일 : 
/// 설명 : 
/// 
/// Move 상태 머신 확장
/// </summary>
public class PlayerStateMachine_Move : PlayerStateMachine
{
    public override PlayerState Workflow()
    {
        PlayerState nextState = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                modelManager.Play("Movement");
                state++;
                break;
            case State.Casting:
                state++;
                break;
            case State.OnAction:
                state++;
                break;
            case State.Finish:
                nextState = PlayerState.Movement;
                break;
            default:
                break;
        }
        return nextState;
    }    
}