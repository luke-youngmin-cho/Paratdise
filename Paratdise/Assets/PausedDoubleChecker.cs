using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedDoubleChecker : MonoBehaviour
{
    void Update()
    {
        if (PlayStateManager.instance.currentPlayState != PlayState.Paused)
            PlayStateManager.instance.SetState(PlayState.Paused);
    }
}
