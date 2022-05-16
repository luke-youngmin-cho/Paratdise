using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/11
/// 최종수정일 : 
/// 설명 : 
/// 
/// 구글 애드몹 광고 매니저
/// </summary>
public class AdmobManager : MonoBehaviour
{
    public static AdmobManager instance;
    public static CMDState CMDState = CMDState.BUSY;
    

    public static bool isReady
    {
        get
        {
            return CMDState == CMDState.IDLE ? true : false;
        }
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(instance);
    }

    public bool isTest;

    private void Start()
    {
        var requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
            .SetTestDeviceIds(new List<string>() { "9CAD3917C334B158" }) // test deevice list
            .build();


        MobileAds.SetRequestConfiguration(requestConfiguration);
        MobileAds.Initialize(initStatus => { CMDState = CMDState.IDLE; });
    }
}

