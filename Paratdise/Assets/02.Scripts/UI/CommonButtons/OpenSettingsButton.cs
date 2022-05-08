using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettingsButton : MonoBehaviour
{
    public void OnClick()
    {
        if (Settings.instance != null)
            Settings.instance.gameObject.SetActive(true);
    }
}
