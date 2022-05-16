using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/11
/// 최종수정일 : 
/// 설명 : 
/// 
/// 구글 애드몹 보상형 광고 버튼
/// </summary>
public class Admob_RewardButton : MonoBehaviour
{
    private const string rewardTestID = AdsData.AD_rewardTestID;
    [HideInInspector] protected string rewardID;
    private string ID;

    RewardedAd rewardAD;
    public delegate void RewardEvent();
    public RewardEvent _rewardEvent;
    public delegate void FailedToLoadEvent();
    public FailedToLoadEvent _failedToLoadEvent;
    private Button button;

    public GameObject failedToLoadWarningPopUp;
    public virtual void Awake()
    {
        rewardID = AdsData.AD_reward_Android_ID;
    }
    public virtual void Start()
    {
        // Call SetRewardEvent() at the top
        button = GetComponent<Button>();
        StartCoroutine(InitCoroutine());
    }
    private void OnEnable()
    {
        if (button != null)
            button.interactable = true;
        //LoadAD_Reward();
    }
    IEnumerator InitCoroutine()
    {
        yield return new WaitUntil(() => AdmobManager.CMDState == CMDState.IDLE);
        //LoadAD_Reward();
    }
    public virtual void LoadAD_Reward()
    {
        Debug.Log($"Loading reward ad... {ID}");
        ID = AdmobManager.instance.isTest ? rewardTestID : rewardID;
        rewardAD = new RewardedAd(ID);
        rewardAD.LoadAd(GetAdRequest());
        
        rewardAD.OnUserEarnedReward += (sender, e) =>
        {
            Debug.Log("Reward Ads succeed");
            _rewardEvent();
        };

        rewardAD.OnAdFailedToLoad += (sender, e) =>
        {
            Debug.Log("Rewards Ads Failed To Load");
            _failedToLoadEvent();
        };
    }
   

    public virtual void SetRewardEvent(RewardEvent rewardEvent)
    {
        _rewardEvent = rewardEvent;
    }

    public virtual void SetFailedToLoadEvent(FailedToLoadEvent failedToLoadEvent)
    {
        _failedToLoadEvent = failedToLoadEvent;
    }
    public virtual void ShowRewardAd()
    {
        if (button != null)
            button.interactable = false;

        if (InternetConnection.IsGoogleWebsiteReachable() == false)
        {
            if (failedToLoadWarningPopUp != null)
                failedToLoadWarningPopUp.SetActive(true);
        }   
        else
        {
            LoadAD_Reward();
            UniTask.Create(async () =>
            {
                await UniTask.WaitUntil(() => rewardAD.IsLoaded());
                rewardAD.Show();
            });

        }        
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

}
