using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/20
/// 최종수정일 : 
/// 설명 : 
/// 
/// Die 상태 머신 확장
/// </summary>
public class PlayerStateMachine_Die : PlayerStateMachine
{
    public override PlayerState Workflow()
    {
        PlayerState nextState = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                manager.move = Vector2.zero;
                modelManager.Play("Die");
                state++;
                break;
            case State.Casting:
                // damage maptile
                state++;
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
    
}