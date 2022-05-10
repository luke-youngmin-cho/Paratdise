using System;
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

    private void Login()
    {
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(delegate (SignInStatus status)
        {
            Debug.Log($"로그인 상태 : {status} , 아이디 : {PlayGamesPlatform.Instance.localUser.id}");
        
            if (status == SignInStatus.Success)
            {
                nickName = PlayGamesPlatform.Instance.localUser.id.ToString();
                Debug.Log($"Logged in with {nickName}");
                loggedIn = true;
            }
        });

    }
}