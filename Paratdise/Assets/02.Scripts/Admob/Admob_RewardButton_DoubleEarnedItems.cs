using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/11
/// 최종수정일 : 
/// 설명 : 
/// 
/// 구글 애드몹 보상형광고 - 아이템 두배 획득
/// </summary>
public class Admob_RewardButton_DoubleEarnedItems : Admob_RewardButton
{
    public override void Awake()
    {
        rewardID = AdsData.AD_reward_Android_ID_PlayPro;
    }
    public override void Start()
    {
        SetRewardEvent(RewardEventToDouble);
        base.Start();
    }
    private void RewardEventToDouble()
    {
        StageManager.DoubleEarnedItems();
    }
}
