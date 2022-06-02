using System;
using System.Collections;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.Android;
using GooglePlayGames.BasicApi;
using Cysharp.Threading.Tasks;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 추후 로그인 시스템 개발을 위한 사전테스트 클래스
/// </summary>
public class LoginManager
{
    private static LoginManager _instance;
    public static LoginManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LoginManager();
                _instance.Login();
            }   
            return _instance;
        }
    }

    public static string nickName;
    public bool loggedIn;

    public bool triedGPGS = false;
    
    private void Login()
    {
        PlayGamesPlatform.Activate();

        //UniTask.Void(async () =>
        //{
        //    float timeMark = Time.time;
        //    float timer = 10f;
        //
        //    await UniTask.WaitUntil(() => Try() || (Time.time - timeMark > timer));
        //});

        Try();

        UniTask.Void(async () =>
        {
            await UniTask.WaitUntil(() => triedGPGS);
            if (loggedIn == false &&
            Settings.instance.NickName_LatestGuest != "")
            {
                nickName = Settings.instance.NickName_LatestGuest;
                loggedIn = true;
            }
        });       

        

        //UniTask.Void(async () =>
        //{
        //    float timeMark = Time.time;
        //    float timer = 5f;
        //    await UniTask.WaitUntil(() => Try() || (Time.time - timeMark > timer));
        //
        //    PlayGamesPlatform.Instance.Authenticate(delegate (SignInStatus status)
        //    {
        //        Debug.Log($"로그인 상태 : {status} , 아이디 : {PlayGamesPlatform.Instance.localUser.id}");
        //
        //        // 구글 로그인 성공
        //        if (status == SignInStatus.Success)
        //        {
        //            nickName = PlayGamesPlatform.Instance.localUser.id.ToString();
        //            Debug.Log($"Logged in with {nickName}");
        //            loggedIn = true;
        //        }
        //        // 게스트 로그인 성공
        //        else if (Settings.instance.NickName_LatestGuest != "")
        //        {
        //            nickName = Settings.instance.NickName_LatestGuest;
        //            loggedIn = true;
        //        }
        //    });
        //});
    }

    private bool Try()
    {
        bool success = false;

        GPGSBinder.Inst.Login((success, localUser) =>
        {
            Debug.Log($"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}");
            if (success)
            {
                nickName = localUser.userName;
                loggedIn = success;
            }
            triedGPGS = true;
        });
        //PlayGamesPlatform.Instance.Authenticate(delegate (SignInStatus status)
        //{
        //    if (status == SignInStatus.Success)
        //        result = true;
        //});
        //Debug.Log("Trying login...");
        return success;
    }
}