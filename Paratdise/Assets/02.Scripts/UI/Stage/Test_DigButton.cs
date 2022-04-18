using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DigButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        Test_Player.instance.Dig();
    }
}
