using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ToolData
{
    public ToolType type;

}

[System.Serializable]
public enum ToolType
{
    None,
    Spoon,
    Shovel,
    Drill,
    PickAxe
}
