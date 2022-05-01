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

    public bool invincible;
    private Coroutine invincibleCoroutin = null;
    private float invincibleTime = 1;
    private float _hp;
    public float hp
    {
        set 
        {
            if (value > 0)
            {
                if (value < _hp)
                    machineManager.ChangeState(PlayerState.Hurt);
            }   
            else
            {
                value = 0;
                machineManager.ChangeState(PlayerState.Die);
                Invoke("GameOver", 1f);
            }

            if (value > hpMax)
                value = hpMax;
            _hp = value;
            PlayerUI.SetHPBar(_hp/hpMax);
        }

        get 
        { 
            return _hp; 
        }
    }
    public float hpMax;

    private PlayerStateMachineManager machineManager;
    private CapsuleCollider2D col;

    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void Hurt(float damage)
    {
        if (invincible == false &&
            invincibleCoroutin == null)
        {
            hp -= damage;
            invincibleCoroutin = StartCoroutine(E_Invincible());
            DamagePopUp.Create(transform.position + new Vector3(0f, col.size.y / 2, 0f), damage, gameObject.layer);
        }   
    }

    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void Awake()
    {
        instance = this;
        col = GetComponent<CapsuleCollider2D>();
        machineManager = GetComponent<PlayerStateMachineManager>();
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
        else if (collision.gameObject.layer == LayerMask.NameToLayer("TimeCapsule"))
        {
            collision.gameObject.GetComponent<TimeCapsuleController>().PickUp();
        }


    }

    IEnumerator E_Invincible()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
        invincibleCoroutin = null;
    }

    private void GameOver()
    {
        StageManager.GameOver();
    }
}
