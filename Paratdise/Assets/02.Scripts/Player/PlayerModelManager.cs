using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// �÷��̾��� 4���� ���� ����Ī�ϰ� �ִϸ����Ϳ� �����ϴ� Ŭ����
/// </summary>
public class PlayerModelManager : MonoBehaviour
{
    public float animationSpeedGain;
    public GameObject frontModel;
    public GameObject backModel;
    public GameObject sideModel;

    public Animator frontAnimator;
    public Animator backAnimator;
    public Animator sideAnimator;
    private Animator currentAnimator;

    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    /// <summary>
    /// �״� �ִϸ��̼��� ��������̹Ƿ� �������� ������ ����
    /// </summary>
    public void Play(string clipName)
    {
        if (clipName == "Die")
        {
            if (currentAnimator == frontAnimator)
                LookRight();
            else if (currentAnimator == backAnimator)
                LookLeft();
        }
        currentAnimator.Play(clipName);
    }

    /// <summary>
    /// �ִϸ��̼� Ŭ���� ������̸� ��ȯ�ϴ� Ŭ����. 
    /// </summary>
    public float GetAnimationTime(string clipName)
    {
        RuntimeAnimatorController ac = sideAnimator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == clipName)
                return ac.animationClips[i].length;
        }
        return -1;
    }

    /// <summary>
    /// ���� �ִϸ����Ϳ� �ش��ϴ� �Ķ���� ����
    /// </summary>
    public void SetFloat(string name, float value) =>
        currentAnimator.SetFloat(name, value);



    public void LookFront()
    {
        frontModel.SetActive(true);
        backModel.SetActive(false);
        sideModel.SetActive(false);
        currentAnimator = frontAnimator;
    }

    public void LookBack()
    {
        frontModel.SetActive(false);
        backModel.SetActive(true);
        sideModel.SetActive(false);
        currentAnimator = backAnimator;
    }

    public void LookLeft()
    {
        frontModel.SetActive(false);
        backModel.SetActive(false);
        sideModel.SetActive(true);
        sideModel.transform.eulerAngles = new Vector3(0, 180, 0);
        currentAnimator = sideAnimator;
    }

    public void LookRight()
    {
        frontModel.SetActive(false);
        backModel.SetActive(false);
        sideModel.SetActive(true);
        sideModel.transform.eulerAngles = Vector2.zero;
        currentAnimator = sideAnimator;
    }
    

    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void Start()
    {
        frontAnimator.speed = animationSpeedGain;
        backAnimator.speed = animationSpeedGain;
        sideAnimator.speed = animationSpeedGain;
        LookFront();
    }


}
