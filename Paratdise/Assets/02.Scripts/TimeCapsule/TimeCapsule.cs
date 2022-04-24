using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/08
/// 최종수정일 : 
/// 설명 : 
/// 
/// 타임캡슐 
/// </summary>

[CreateAssetMenu(fileName = "New TimeCapsule", menuName = "TimeCapsule/Create New TimeCapsule")]
public class TimeCapsule : ScriptableObject
{
    public TimeCapsuleRarity type;
    public int index;
    public string title;
    public string discription;
    public Sprite icon;
    public int num;
}

[System.Serializable]
public enum TimeCapsuleRarity
{
    Normal,
    Uncommon,
    Rare,
}