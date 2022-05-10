using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/11
/// 최종수정일 : 
/// 설명 : 
/// 
/// 게임이 일시정지 상태여야 하는데, 광고 종료 후 등의 특수 이벤트로 인해 
/// TimeScale 이 해제되어 버리는 경우 다시 되돌리기 위해 체크하는 클래스
/// </summary>
public class TimeScaleDoubleChecker : MonoBehaviour
{
    private void Update()
    {
        if (PlayStateManager.instance.currentPlayState == PlayState.Paused &&
            Time.timeScale > 0.5f)
            PlayStateManager.instance.SetState(PlayState.Paused);
    }
}