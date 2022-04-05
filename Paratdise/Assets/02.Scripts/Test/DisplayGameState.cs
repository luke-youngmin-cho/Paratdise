using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YM;

/// <summary>
/// just for test
/// </summary>
public class DisplayGameState : MonoBehaviour
{
    public static DisplayGameState instance;
    [SerializeField] Text gamestateText;
    [SerializeField] Text discriptionText;
    // Update is called once per frame
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        gamestateText.text = GameManager.gameState.ToString();
    }

    static public void SetDiscription(string contents)
    {
        instance.discriptionText.text = contents;
    }
}
