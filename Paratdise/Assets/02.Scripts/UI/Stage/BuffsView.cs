using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 현재 적용된 버프들에 대한 아이콘을 UI 로 띄워줌
/// </summary>
public class BuffsView : MonoBehaviour
{
    public static BuffsView instance;
    public Transform content;

    public GameObject burnIcon;
    public GameObject freezeIcon;
    public GameObject shockIcon;
    public GameObject despairIcon;
    public GameObject poisonIcon;
    public GameObject stunIcon;
    public GameObject blindIcon;
    


    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    private void LateUpdate()
    {
        if (BuffManager.instance == null) return;

        bool burnOn = false;
        bool freezeOn = false;
        bool shockOn = false;
        bool despairOn = false;
        bool poisonOn = false;
        bool stunOn = false;
        bool blindOn = false;

        foreach (var activatedBuff in BuffManager.instance.activatedBuffs)
        {
            switch (activatedBuff.Key.type)
            {
                case BuffType.None:
                    break;
                case BuffType.Burn:
                    burnOn = true;
                    break;
                case BuffType.Freeze:
                    freezeOn = true;
                    break;
                case BuffType.Shock:
                    shockOn = true;
                    break;
                case BuffType.Despair:
                    despairOn = true;
                    break;
                case BuffType.Poison:
                    poisonOn = true;
                    break;
                case BuffType.Stun:
                    stunOn = true;
                    break;
                case BuffType.Blind:
                    blindOn = true;
                    break;
                default:
                    break;
            }
        }
        burnIcon.SetActive(burnOn);
        freezeIcon.SetActive(freezeOn);
        shockIcon.SetActive(shockOn);
        despairIcon.SetActive(despairOn);
        poisonIcon.SetActive(poisonOn);
        stunIcon.SetActive(stunOn);
        blindIcon.SetActive(blindOn);
    }
}