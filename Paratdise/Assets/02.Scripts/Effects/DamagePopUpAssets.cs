using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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