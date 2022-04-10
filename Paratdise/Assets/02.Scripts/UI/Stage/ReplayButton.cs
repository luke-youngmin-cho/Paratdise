using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.StartStage(GameManager.currentStage);
    }
}
