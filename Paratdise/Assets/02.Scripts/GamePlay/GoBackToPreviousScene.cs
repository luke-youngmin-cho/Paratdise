using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackToPreviousScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneMover.MoveTo(SceneInformation.oldSceneName);
    }

}
