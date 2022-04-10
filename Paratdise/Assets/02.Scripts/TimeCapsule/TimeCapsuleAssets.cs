using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/08
/// 최종수정일 : 
/// 설명 : 
/// 
/// 타임캡슐을 가져다 쓰기위한 클래스
/// </summary>

public class TimeCapsuleAssets : MonoBehaviour
{
    public static TimeCapsuleAssets _instance;
    public static TimeCapsuleAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<TimeCapsuleAssets>("Assets/TimeCapsuleAssets"));
            return _instance;
        }
    }

    public List<TimeCapsule> timeCapsules = new List<TimeCapsule>();

    public static TimeCapsule GetTimeCapsule(string title) =>
        instance.timeCapsules.Find(x => x.title == title);
}