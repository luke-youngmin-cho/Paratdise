using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 
/// 설명 : 
/// 
/// FPS 측정용 테스트 클래스
/// </summary>

public class FPSCounter : MonoBehaviour
{
    private float deltaTime = 0.0f;

    public Text FPStext;

    void Update()
    {
        // 10 프레임 평균 시간
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // FPS
        float fps = 1.0f / deltaTime;

        // 반올림
        string text = Mathf.Ceil(fps).ToString();

        // 텍스트 갱신
        FPStext.text = text;
    }
}