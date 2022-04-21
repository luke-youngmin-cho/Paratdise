using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolInfo : MonoBehaviour
{
    public int toolIndex;
    public string toolName;
    public float toolHeight;
    public float toolWidth;
    public float luck;
    public float power;
    public int reinforceCount;
    public ToolType toolType;
    // Start is called before the first frame update
    public ToolInfo(int _toolIndex, string _toolName, float _toolHeight,
        float _toolWidth, float _luck, float _power, int _reinforceCount, ToolType _toolType)
    {
        toolIndex = _toolIndex;
        toolName = _toolName;
        toolHeight = _toolHeight;
        toolWidth = _toolWidth;
        luck = _luck;
        power = _power;
        reinforceCount = _reinforceCount;
        toolType = _toolType;
    }
}
