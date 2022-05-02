using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/02
/// 최종수정일 : 
/// 설명 : 
/// 
/// 버프 베이스 클래스
/// </summary>

public class Buff : MonoBehaviour
{
    public BuffType type;
    public float duration;
    virtual public void OnActive(Player player, BuffGenerator generator)
    {

    }
    virtual public void OnDeactive(Player player, BuffGenerator generator)
    {

    }
    virtual public void OnDuration(Player player, BuffGenerator generator)
    {

    }
}

public enum BuffType
{
    None = 0,
    Burn,
    Freeze,
    Shock,
    Despair,
    Poison,
    Stun,
    Blind,
}