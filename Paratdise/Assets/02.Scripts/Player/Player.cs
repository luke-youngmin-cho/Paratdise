using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/20
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어 스텟 / 충돌이벤트
/// </summary>
public class Player : MonoBehaviour
{
    public static Player instance;

    private float _hp;
    public float hp
    {
        set 
        {
            if (value > 0)
                _hp = value;
            else
            {
                _hp = 0;
                machineManager.ChangeState(PlayerState.Die);
            }

            PlayerUI.SetHPBar(_hp/hpMax);
        }

        get 
        { 
            return _hp; 
        }
    }
    public float hpMax;

    private PlayerStateMachineManager machineManager;

    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void Awake()
    {
        instance = this;
        machineManager = GetComponent<PlayerStateMachineManager>();
        hp = hpMax;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            /*if (Input.GetKey(KeyCode.Z))
                collision.gameObject.GetComponent<ItemController>().PickUp(this);*/
            collision.gameObject.GetComponent<ItemController>().PickUp(this);
        }
    }
}
