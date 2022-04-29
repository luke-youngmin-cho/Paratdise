using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/29
/// 최종수정일 :
/// 설명 : 
/// 
/// 데미지 팝업 에셋을 가져다쓰기위한 클래스
/// </summary>
public class DamagePopUpAssets : MonoBehaviour
{
    private static DamagePopUpAssets _instance;
    public static DamagePopUpAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<DamagePopUpAssets>("Assets/DamagePopUpAssets"));
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Transform damagePopUp_Enemy_Basic;
    public Transform damagePopUp_Player_Basic;

    public Transform GetDamagePopUpTrasnformByLayer(int layer)
    {
        Transform tmpTransform = damagePopUp_Enemy_Basic;

        if (layer == LayerMask.NameToLayer("Player"))
        {
            tmpTransform = damagePopUp_Player_Basic;
        }

        return tmpTransform;
    }

}