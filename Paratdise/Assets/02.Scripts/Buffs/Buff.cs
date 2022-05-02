using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/02
/// ���������� : 
/// ���� : 
/// 
/// ���� ���̽� Ŭ����
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