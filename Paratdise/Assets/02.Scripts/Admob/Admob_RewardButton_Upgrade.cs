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
/// 구글 애드몹 보상형광고 - 강화
/// </summary>
public class Admob_RewardButton_Upgrade : Admob_RewardButton
{
    public UpgradeType upgradeType;
    protected override void Awake()
    {
        rewardID = AdsData.AD_Reward_Upgrade;
    }
    protected override void Start()
    {
        SetRewardEvent(RewardEventToUpgrade);
        base.Start();
    }
    private void RewardEventToUpgrade()
    {
        UpgradeManager.instance.TryUpgradeWithAd(upgradeType);
    }
}
