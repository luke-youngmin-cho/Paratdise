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
/// 구글 애드몹 보상형광고 - 타임캡슐 한번 더 뽑기
/// </summary>
public class Admob_RewardButton_DrowOneMoreTimeCapsule : Admob_RewardButton
{
    public override void Awake()
    {
        rewardID = AdsData.AD_Reward_DrowOneMoreTimeCapsule;
    }
    public override void Start()
    {
        SetRewardEvent(RewardEventToUpgrade);
        base.Start();
    }
    private void RewardEventToUpgrade()
    {
        PieceOfStoryPopUp.instance.DrowAgain();
    }
}
