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
namespace YM
{
    public class LoginButton : MonoBehaviour
    {
        public InputField inputField;
        public void OnClick()
        {
            if (!string.IsNullOrEmpty(inputField.text))
            {
                LoginManager.nickName = inputField.text;
                LoginManager.loggedIn = true;
            }   
        }
    }
}