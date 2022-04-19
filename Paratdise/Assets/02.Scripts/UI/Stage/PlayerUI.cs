using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/20
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어의 UI 연결 클래스
/// </summary>
public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    [SerializeField] private Slider hpBar;

    
    public static void SetHPBar(float value)
    {
        if (instance != null)
            instance.hpBar.value = value;    
    }

    private void Awake()
    {
        instance = this;
    }
}