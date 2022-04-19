using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어의 4방향 모델을 스위칭하고 애니메이터에 접근하는 클래스
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
    /// 죽는 애니메이션은 옆모습뿐이므로 죽을때는 옆으로 죽음
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
    /// 애니메이션 클립의 재생길이를 반환하는 클래스. 
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
    /// 현재 애니메이터에 해당하는 파라미터 수정
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
