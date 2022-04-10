using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 
/// 설명 : 
/// 
/// 테스트용 패널
/// </summary>


public class TestPanel : MonoBehaviour
{
    public InputField algoNumInputField;
    public BoxCollider2D mapSizeCol;
    public InputField h;
    public InputField v;


    public void UpdateAlgoNum()
    {
        int inputValue = Convert.ToInt32(algoNumInputField.text);
        if (inputValue > 0 && inputValue < 1000)
            MapCreater.instance.algorithmTimes = inputValue;
    }

    public void UpdateMapSizeH()
    {
        int inputValue = Convert.ToInt32(h.text);
        if (inputValue > 0 && inputValue < 1000)
            mapSizeCol.size = new Vector2(inputValue, mapSizeCol.size.y);
    }
    public void UpdateMapSizeV()
    {
        int inputValue = Convert.ToInt32(v.text);
        if (inputValue > 0 && inputValue < 1000)
            mapSizeCol.size = new Vector2(mapSizeCol.size.x, inputValue);
    }

    public void ChangeFOV(float value)
    {
        Camera.main.orthographicSize = value;
    }
}