using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_AttackButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        PlayerStateMachineManager.instance.ChangeState(PlayerState.Attack);            
    }
}
