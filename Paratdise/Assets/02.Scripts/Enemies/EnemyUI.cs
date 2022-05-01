using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/29
/// 최종수정일 :
/// 설명 : 
/// 
/// 에너미 UI
/// </summary>
public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider hpBar;

    public void SetHPBar(float value)
    {
        hpBar.value = value;
    }

    
}