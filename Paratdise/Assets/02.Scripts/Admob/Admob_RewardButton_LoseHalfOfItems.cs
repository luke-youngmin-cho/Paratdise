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
/// 구글 애드몹 보상형광고 - 실패시 아이템을 절반만 사라지게 해줌
/// </summary>
public class Admob_RewardButton_LoseHalfOfItems : Admob_RewardButton
{
    protected override void Awake()
    {
        rewardID = AdsData.AD_Reward_HalfLosing;
    }
    protected override void Start()
    {
        SetRewardEvent(RewardEventToHalfLose);
        base.Start();
    }
    private void RewardEventToHalfLose()
    {
        StageManager.instance.dohalfPenalty = true;
    }
}
