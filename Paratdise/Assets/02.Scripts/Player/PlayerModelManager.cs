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

    [SerializeField] private SpriteRenderer[] frontSpriteRenderers;
    [SerializeField] private SpriteRenderer[] backSpriteRenderers;
    [SerializeField] private SpriteRenderer[] sideSpriteRenderers;
    [SerializeField] private SpriteOutline[] frontSpriteOutLines;
    [SerializeField] private SpriteOutline[] backSpriteOutLines;
    [SerializeField] private SpriteOutline[] sideSpriteOutLines;
    private Color tmpColor = Color.white;

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
    
    public void StartBlink(Color blinkColor, float time, float period, bool onlyOutline)
    {
        StartCoroutine(E_Blink(blinkColor, time, period, onlyOutline));
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

   

    private IEnumerator E_Blink(Color blinkColor, float time, float period, bool onlyOutline)
    {
        tmpColor = blinkColor;
        float timer = time;
        float blinkTimer = period;
        foreach (var spriteRenderer in frontSpriteRenderers)
            spriteRenderer.color = tmpColor + Color.gray;

        foreach (var spriteRenderer in backSpriteRenderers)
            spriteRenderer.color = tmpColor + Color.gray;

        foreach (var spriteRenderer in sideSpriteRenderers)
            spriteRenderer.color = tmpColor + Color.gray;

        foreach (var spriteOutline in frontSpriteOutLines)
        {
            spriteOutline.color = tmpColor;
            spriteOutline.outlineSize = 16;
        }

        foreach (var spriteOutline in backSpriteOutLines)
        {
            spriteOutline.color = tmpColor;
            spriteOutline.outlineSize = 16;
        }

        foreach (var spriteOutline in sideSpriteOutLines)
        {
            spriteOutline.color = tmpColor;
            spriteOutline.outlineSize = 16;
        }
            

        while (timer > 0)
        {
            if (blinkTimer > period / 2f)
                tmpColor.a -= Time.deltaTime * period * 2f;
            else if (blinkTimer > 0)
                tmpColor.a += Time.deltaTime * period * 2f;
            else
                blinkTimer = period;

            blinkTimer -= Time.deltaTime;

            Debug.Log(tmpColor);
            if (!onlyOutline)
            {
                foreach (var spriteRenderer in frontSpriteRenderers)
                    spriteRenderer.color = new Color(tmpColor.r + Color.gray.r,
                                                     tmpColor.g + Color.gray.g,
                                                     tmpColor.b + Color.gray.b,
                                                     tmpColor.a);

                foreach (var spriteRenderer in backSpriteRenderers)
                    spriteRenderer.color = new Color(tmpColor.r + Color.gray.r,
                                                     tmpColor.g + Color.gray.g,
                                                     tmpColor.b + Color.gray.b,
                                                     tmpColor.a);

                foreach (var spriteRenderer in sideSpriteRenderers)
                    spriteRenderer.color = new Color(tmpColor.r + Color.gray.r,
                                                     tmpColor.g + Color.gray.g,
                                                     tmpColor.b + Color.gray.b,
                                                     tmpColor.a);
            }

            foreach (var spriteOutline in frontSpriteOutLines)
                spriteOutline.color = tmpColor;

            foreach (var spriteOutline in backSpriteOutLines)
                spriteOutline.color = tmpColor;

            foreach (var spriteOutline in sideSpriteOutLines)
                spriteOutline.color = tmpColor;

            timer -= Time.deltaTime;
            yield return null;
        }

        tmpColor = Color.white;

        foreach (var spriteRenderer in frontSpriteRenderers)
            spriteRenderer.color = tmpColor;

        foreach (var spriteRenderer in backSpriteRenderers)
            spriteRenderer.color = tmpColor;

        foreach (var spriteRenderer in sideSpriteRenderers)
            spriteRenderer.color = tmpColor;

        foreach (var spriteOutline in frontSpriteOutLines)
        {
            spriteOutline.color = tmpColor;
            spriteOutline.outlineSize = 0;
        }

        foreach (var spriteOutline in backSpriteOutLines)
        {
            spriteOutline.color = tmpColor;
            spriteOutline.outlineSize = 0;
        }

        foreach (var spriteOutline in sideSpriteOutLines)
        {
            spriteOutline.color = tmpColor;
            spriteOutline.outlineSize = 0;
        }
    }
}
