using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/20
/// 최종수정일 : 
/// 설명 : 
/// 
/// Dig 상태 머신 확장
/// </summary>
public class PlayerStateMachine_Dig : PlayerStateMachine
{
    private float width , height, strength;
    private LayerMask targetLayer;

    private Vector2 center;
    private Vector2 size;
    private Vector2 dir;
    public override void Awake()
    {
        base.Awake();
        
        CharacterData data = PlayerDataManager.data.GetCharacterData(GameManager.characterSelected);
        width = 0.5f;
        height = 0.5f;
        strength = 1 + UpgradeInfoTable.GetTotalAdditionalValue(UpgradeType.DiggingForce, data.toolsLevel.diggingForceLevel);

        targetLayer = LayerMask.NameToLayer("MapTileDestroyable");
    }

    public override PlayerState Workflow()
    {
        PlayerState nextState = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                dir = manager.direction;
                //Debug.Log(dir);
                manager.move = manager.move / 2f;
                modelManager.Play("Dig");
                AudioManager.instance.PlaySFX(SFXAssets.GetSFX("Player_Dig"));
                state++;
                break;
            case State.Casting:
                if (dir != manager.direction)
                    manager.move = Vector2.zero;

                // damage maptile

                center = Vector2.zero;
                size   = Vector2.zero;
                if (dir == Vector2.up)
                {
                    center = new Vector2(transform.position.x + dir.x / 2,
                                         transform.position.y + dir.y / 2 + height / 4);
                    size = new Vector2(width, height) / 2;
                }
                else if (dir == Vector2.down)
                {
                    center = new Vector2(transform.position.x + dir.x / 2,
                                         transform.position.y + dir.y / 2 - height / 4);
                    size = new Vector2(width, height) / 2;
                }
                else if (dir == Vector2.left)
                {
                    center = new Vector2(transform.position.x + dir.x / 2 - height / 4,
                                         transform.position.y + dir.y / 2);
                    size = new Vector2(height, width) / 2;
                }
                else if (dir == Vector2.right)
                {
                    center = new Vector2(transform.position.x + dir.x / 2 + height / 4,
                                         transform.position.y + dir.y / 2);
                    size = new Vector2(height, width) / 2;
                }
                //Debug.Log($"{center}, {size}");
                if (animationTimer < animationTime / 2)
                {
                    //Debug.Log($"Dig spec : {center}, {size}");
                    RaycastHit2D[] hits = Physics2D.BoxCastAll(center , size , 0, Vector2.zero, 0, targetLayer);
                    foreach (var hit in hits)
                    {
                        if (hit.collider.tag == "Destroyable")
                        {
                            hit.collider.GetComponent<MapTile_Destroyable>().Hurt(strength);
                            //Debug.Log($"Dig {hit.collider.name} with {strength}");
                        }

                    }
                    state++;
                }
                else
                    animationTimer -= Time.deltaTime * modelManager.animationSpeedGain;

                break;
            case State.OnAction:
                if (dir != manager.direction)
                    manager.move = Vector2.zero;

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
        if (state != State.Idle)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, size);
        }
    }
}