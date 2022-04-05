using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 로비로 돌아가는 버튼
/// </summary>
namespace YM
{
    public class LobbyButton : MonoBehaviour
    {
        public void OnClick()
        {
            GameManager.GoBackToLobby();
        }
    }
}