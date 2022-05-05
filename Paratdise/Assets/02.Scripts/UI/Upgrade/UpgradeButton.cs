using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/06
/// 최종수정일 :
/// 설명 : 
/// 
/// 강화를 시도하는 버튼
/// </summary>
public class UpgradeButton : MonoBehaviour
{
    public UpgradeType type;

    public void OnClick()
    {
        UpgradeManager.instance.TryUpgrade(type);
    }
}