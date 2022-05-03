using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 2022/04/26
/// 설명 : 
/// 
/// 연출 클래스
/// </summary>

public class StoryPlayer : MonoBehaviour
{
    public static StoryPlayer _instance;
    public static StoryPlayer instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<StoryPlayer>("UI/StoryPlayerUI"));
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    public Image image;

    public bool isStoryFinished
    {
        get
        {
            return  _story == null ? true : false;
        }
    }
    public Story _story;
    [SerializeField] private int _page;
    public GameObject _popUp;
    public GameObject nextButton;
    public GameObject skipButton;
    private Coroutine fadeCoroutine = null;
    private Coroutine nextButtonCoroutine = null;


    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================

    /// <summary>
    /// 다음 클릭시
    /// </summary>
    public void OnNextButton()
    {
        if (fadeCoroutine != null)
        {
            Debug.Log("Fade coroutine is not null");
            return;
        }
            

        if (nextButtonCoroutine != null)
        {
            Debug.Log("Next Button coroutine is not null");
            return;
        }
            
                   
        // 마지막페이지면
        if (_page >= _story.pages.Length - 1)
        {
            skipButton.SetActive(false);
            Debug.Log("이거 마지막 페이지 였음");
            EndStory();
        }
        // 다음페이지 넘기기
        else
        {
            ++_page;
            Debug.Log($"다음 페이지, {_story.pages[_page].sprite.name}");

            if (_story.pages[_page].effectType == PageEffectType.AutoNext)
            {
                if (nextButtonCoroutine != null)
                {
                    StopCoroutine(nextButtonCoroutine);
                    nextButtonCoroutine = null;
                }   
                nextButton.SetActive(false);
                StartCoroutine(E_OnNextButton(3f));
            }
            else
            {
                nextButtonCoroutine = StartCoroutine(E_ShowNextButtonLater());
            }
            
            if (_story.pages[_page].effectType == PageEffectType.Shake)
            {
                fadeCoroutine = StartCoroutine(E_Shake());
            }

            image.sprite = _story.pages[_page].sprite;
            skipButton.SetActive(true);
        }
    }

    private IEnumerator E_OnNextButton(float delay)
    {
        float timer = delay;
        while (timer > 0)
        {
            timer -= 0.0167f;
            yield return null;
        }
        OnNextButton();
    }

    /// <summary>
    /// 연출 시작 함수
    /// </summary>
    public void StartStory(Story story)
    {
        if (_popUp != null)
            Destroy(_popUp);

        if (story == null) 
            return;

        Debug.Log($"연출 시작 {story.name}");

        PlayStateManager.instance.SetState(PlayState.Paused);
        transform.GetChild(0).GetComponent<Image>().color = Color.black; // BG
        transform.GetChild(1).GetComponent<Image>().color = Color.white; 
        _story = story;
        _page = 0;
        image.sprite = _story.pages[0].sprite;

        gameObject.SetActive(true);
        if (nextButtonCoroutine != null)
            StopCoroutine(nextButtonCoroutine);
        nextButtonCoroutine = StartCoroutine(E_ShowNextButtonLater());

        switch (_story.showEffect)
        {
            case StoryEffectType.None:
                break;
            case StoryEffectType.FadeIn:
                fadeCoroutine = StartCoroutine(E_FadeIn());
                break;
            case StoryEffectType.FadeOut:
                fadeCoroutine = StartCoroutine(E_FadeOut());
                break;
            case StoryEffectType.Dissolve:
                break;
            default:
                break;
        }
        skipButton.SetActive(true);
    }

    /// <summary>
    /// 연출 종료 함수
    /// </summary>
    public void EndStory()
    {
        if(_story.popUp != null &&
            _popUp == null)
        {
            skipButton.SetActive(false);
            _page = _story.pages.Length - 1;
            image.sprite = _story.pages[_page].sprite;
            _popUp = Instantiate(_story.popUp, transform);
            Debug.Log($"Pop up {_popUp.name}");
        }
            
        // 마지막 이벤트가 있으면 이벤트재생
        /*if ((_page < _story.pages.Length - 1) &&
            (_story.popUp != null))
        {
            _page = _story.pages.Length - 1;
            OnNextButton();
        }*/
        
        // 없으면 연출 끝냄
        else
        {
            switch (_story.hideEffect)
            {
                case StoryEffectType.None:
                    gameObject.SetActive(false);
                    break;
                case StoryEffectType.FadeIn:
                    fadeCoroutine = StartCoroutine(E_FadeIn());
                    break;
                case StoryEffectType.FadeOut:
                    fadeCoroutine = StartCoroutine(E_FadeOut());
                    break;
                case StoryEffectType.Dissolve:
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
            _story = null;
            _page = 0;
            PlayStateManager.instance.SetState(PlayState.Play);
        }
    }


    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================


    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator E_ShowNextButtonLater()
    {
        nextButton.SetActive(false);
        nextButton.GetComponent<Button>().interactable = false;
        Image buttonImage = nextButton.GetComponent<Image>();

        float elapsedTime = 0;

        // wait for 1 sec
        while (elapsedTime < 0.5f)
        {
            elapsedTime += 0.0167f;
            yield return null;
        }

        nextButton.SetActive(true);
        Color c = buttonImage.color;
        c.a = 0;
        buttonImage.color = c;
        elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            Debug.Log($"Showing next button... {c.a}");
            elapsedTime += 0.0167f * 2;
            c.a += elapsedTime;
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
        gameObject.SetActive(true);
        fadeCoroutine = null;
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
        gameObject.SetActive(false);
        fadeCoroutine = null;
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
        fadeCoroutine = null;
    }
}