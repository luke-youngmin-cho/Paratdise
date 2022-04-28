using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_AttackButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        if (Player.instance.hp > 0)
            PlayerStateMachineManager.instance.ChangeState(PlayerState.Attack);            
    }
}
