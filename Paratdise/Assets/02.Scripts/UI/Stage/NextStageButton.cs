using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.StartStage(GameManager.currentStage + 1);
    }
}
