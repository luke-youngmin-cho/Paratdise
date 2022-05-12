using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 로그인 버튼
/// </summary>

public class LogOutButton : MonoBehaviour
{
    public void OnClick()
    {
        Settings.instance.NickName_LatestGuest = "";
        LoginManager.nickName = "";
        LoginManager.instance.loggedIn = false;
        SceneMover.MoveTo("Login");
        GameManager.gameState = GameState.WaitForInternetConnection;
    }
}