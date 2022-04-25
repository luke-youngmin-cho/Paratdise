/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [Header("삽화 이미지를 렌더링 할 대상 UI")]
    public Image imageRender;

    [Header("페이드 인&아웃에 걸리는 시간"), Range(0.01f, 5f)]
    public float fadeInOrOutTime = 0.8f;

    [Header("삽화 이미지")]
    public Sprite[] images;

    [HideInInspector] public int stageCount;

    private int imageCount = 0;
    private bool isEffectStarted = false;

    private void Start()
    {
        imageCount = 0;
        //ShowCurrentPage();
    }

    //Stage 이미지 로드
    public void LoadImages(int _stage = -1)
    {
        int targetStage = _stage;
        if (_stage == -1)
            targetStage = stageCount;

        int stageLength = StoryPlayDataManager.LoadStoryData(targetStage).stories.Count;
        images = new Sprite[stageLength];
        for (int i = 0; i < stageLength; i++)
        {
            images[i] = StoryPlayDataManager.LoadStoryData(targetStage).stories[i].sprite;
        }
        imageCount = 0;
    }

    //페이지 하나씩 넘기며 Fade In & Out
    public void FadeInAndOut()
    {
        if (imageCount == images.Length - 1)
            StartCoroutine(CoFadeOut_Black());
        else if (imageCount == 0)
        {
            StartCoroutine(CoFadeIn_Black());
            imageCount++;
        }
        else if (!isEffectStarted)
            StartCoroutine(AllFadeInOut());
    }
    
    public void FadeOut()
    {
        StartCoroutine(CoFadeOut());
    }

    public void FadeIn()
    {
        StartCoroutine(CoFadeIn());
    }

    public void ShowCurrentPage()
    {
        imageRender.sprite = images[imageCount];
    }

    IEnumerator AllFadeInOut()
    {
        isEffectStarted = true;
        yield return CoFadeOut();
        imageCount++;
        ShowCurrentPage();
        yield return CoFadeIn();
        isEffectStarted = false;
    }

    IEnumerator CoFadeOut_Black()
    {
		Color tempColor = imageRender.color;
		while(tempColor.r > 0f){
			tempColor.r -= Time.deltaTime / fadeInOrOutTime;
			tempColor.g -= Time.deltaTime / fadeInOrOutTime;
			tempColor.b -= Time.deltaTime / fadeInOrOutTime;
			imageRender.color = tempColor;

			if(tempColor.r <= 0f) tempColor = Color.black;

			yield return null;
		}
		imageRender.color = tempColor;
    }

    IEnumerator CoFadeIn_Black()
    {
        ShowCurrentPage();
        imageRender.color = new Color(0f, 0f, 0f, 1f);
		Color tempColor = imageRender.color;
		while(tempColor.r < 1f){
			tempColor.r += Time.deltaTime / fadeInOrOutTime;
			tempColor.g += Time.deltaTime / fadeInOrOutTime;
			tempColor.b += Time.deltaTime / fadeInOrOutTime;
			imageRender.color = tempColor;

			if(tempColor.r >= 1f) tempColor = Color.white;

			yield return null;
		}
		imageRender.color = tempColor;
    }


    IEnumerator CoFadeOut()
    {
		Color tempColor = imageRender.color;
		while(tempColor.a > 0f){
			tempColor.a -= Time.deltaTime / fadeInOrOutTime;
			imageRender.color = tempColor;

			if(tempColor.a <= 0f) tempColor.a = 0f;

			yield return null;
		}
		imageRender.color = tempColor;
    }

    IEnumerator CoFadeIn()
    {
		Color tempColor = imageRender.color;
		while(tempColor.a < 1f){
			tempColor.a += Time.deltaTime / fadeInOrOutTime;
			imageRender.color = tempColor;

			if(tempColor.a >= 1f) tempColor.a = 1f;

			yield return null;
		}
		imageRender.color = tempColor;
    }
}
*/