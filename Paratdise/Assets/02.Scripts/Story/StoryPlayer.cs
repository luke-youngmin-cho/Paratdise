using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 연출 클래스
/// </summary>

public class StoryPlayer : MonoBehaviour
{
    public static StoryPlayer instance;
    public Image image;

    public bool isStoryFinished
    {
        get
        {
            return  _story == null ? true : false;
        }
    }
    public Story _story;
    private int _page;
    public GameObject _popUp;
    public GameObject nextButton;
    private Coroutine coroutine = null;
    private Coroutine nextButtonCoroutine = null;


    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    /// <summary>
    /// 다음 클릭시
    /// </summary>
    public void OnNextButton()
    {
        if (_story == null ||
            _page == _story.pages.Length - 1) 
            return;

        // 다음페이지 넘기기
        if (_page < _story.pages.Length - 1)
        {
            ++_page;
            Debug.Log($"다음 페이지, {_story.pages[_page].sprite.name}");

            if (_story.pages[_page].effectType == PageEffectType.AutoNext)
            {
                nextButton.SetActive(false);
                Invoke("OnNextButton", 1f);
            }
            else
            {
                if (nextButtonCoroutine != null)
                    StopCoroutine(nextButtonCoroutine);
                nextButtonCoroutine = StartCoroutine(E_ShowNextButtonLater());
            }
            
            if (_story.pages[_page].effectType == PageEffectType.Shake)
            {
                StartCoroutine(E_Shake());
            }

            image.sprite = _story.pages[_page].sprite;
        }
        // 마지막페이지면
        if (_page == _story.pages.Length - 1)
        {
            Debug.Log("이거 마지막 페이지 였음");
            // 선택창 없으면 연출종료
            if (_story.popUp == null)
                EndStory();
            // 선택창 있으면 선택창 띄우기
            else
                _popUp = Instantiate(_story.popUp, transform);
        }
    }

    /// <summary>
    /// 연출 시작 함수
    /// </summary>
    public void StartStory(Story story)
    {
        StopAllCoroutines();
        Debug.Log($"연출 시작 {story.name}");

        if (_popUp != null)
            Destroy(_popUp);

        transform.GetChild(0).GetComponent<Image>().color = Color.black; // BG
        transform.GetChild(1).GetComponent<Image>().color = Color.white; 
        _story = story;
        _page = 0;
        image.sprite = _story.pages[0].sprite;
        nextButton.GetComponent<Button>().interactable = true;
        nextButton.SetActive(true);

        gameObject.SetActive(true);

        switch (_story.showEffect)
        {
            case StoryEffectType.None:
                break;
            case StoryEffectType.FadeIn:
                coroutine = StartCoroutine(E_FadeIn());
                break;
            case StoryEffectType.FadeOut:
                coroutine = StartCoroutine(E_FadeOut());
                break;
            case StoryEffectType.Dissolve:
                break;
            default:
                break;
        }        
    }

    /// <summary>
    /// 연출 종료 함수
    /// </summary>
    public void EndStory()
    {
        switch (_story.hideEffect)
        {
            case StoryEffectType.None:
                break;
            case StoryEffectType.FadeIn:
                coroutine = StartCoroutine(E_FadeIn());
                break;
            case StoryEffectType.FadeOut:
                coroutine = StartCoroutine(E_FadeOut());
                break;
            case StoryEffectType.Dissolve:
                break;
            default:
                break;
        }
        _story = null;
        _page = 0;
        gameObject.SetActive(false);
        StopAllCoroutines();
    }


    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator E_ShowNextButtonLater()
    {
        nextButton.SetActive(false);
        nextButton.GetComponent<Button>().interactable = false;
        Image buttonImage = nextButton.GetComponent<Image>();
        yield return new WaitForSeconds(1f);

        nextButton.SetActive(true);
        Color c = buttonImage.color;
        c.a = 0;
        buttonImage.color = c;
        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            elapsedTime += 0.167f;
            buttonImage.color = c;
            yield return null;
        }
        nextButton.GetComponent<Button>().interactable = true;
        nextButtonCoroutine = null;
    }

    private IEnumerator E_FadeIn()
    {
        Color c = image.color;
        c.a = 0;
        image.color = c;
        while (c.a < 1)
        {   
            c.a += 0.0167f;
            image.color = c;
            yield return null;
        }
        coroutine = null;
    }

    private IEnumerator E_FadeOut()
    {
        Color c = image.color;
        c.a = 1;
        image.color = c;
        while (c.a > 0)
        {
            c.a -= 0.0167f;
            image.color = c;
            yield return null;
        }
        coroutine = null;
    }

    private IEnumerator E_Shake()
    {
        Vector3 startPos = transform.GetChild(1).position;
        float amplitude = transform.GetChild(1).GetComponent<RectTransform>().rect.width / 40;
        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            elapsedTime += 0.0167f;
            transform.GetChild(1).position = startPos + Random.insideUnitSphere * amplitude;
            yield return null;
        }
        transform.GetChild(1).position = startPos;
    }
}