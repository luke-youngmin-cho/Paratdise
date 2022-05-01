using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/29
/// 최종수정일 :
/// 설명 : 
/// 
/// 데미지를 띄워주기위한 TextMeshPro 
/// </summary>
public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disapperTimer = 0.5f;
    private float moveSpeedY = 0.5f;
    private float disappearSpeed = 3f;
    private Color textColor;
    private Transform tr;

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    public static DamagePopUp Create(Vector3 pos, float damage, int layer)
    {
        Transform damagePopUpTransform = Instantiate(DamagePopUpAssets.instance.GetDamagePopUpTrasnformByLayer(layer),
                                                     pos, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.SetUp(damage);
        return damagePopUp;
    }

    public void SetUp(float damage)
    {
        int tmpDamage = (int)damage;
        textMesh.SetText(tmpDamage.ToString());
    }

    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Awake()
    {
        tr = GetComponent<Transform>();
        textMesh = tr.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        tr.position += new Vector3(0f, moveSpeedY * Time.deltaTime, 0f);

        if (disapperTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
                Destroy(gameObject);
        }
        else
        {
            disapperTimer -= Time.deltaTime;
        }
    }
    
}