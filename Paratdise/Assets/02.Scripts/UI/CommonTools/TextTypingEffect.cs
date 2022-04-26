using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/24
/// 최종수정일 : 
/// 설명 : 
/// 
/// 텍스트 타이핑을 이펙트를 위한 클래스
/// </summary>
/// 
public class TextTypingEffect : MonoBehaviour, IPointerClickHandler
{
    public Text UI_Text; // 유니티 게임 에디터에 있는 Text UI
    public float typingDelay; // 몇초 간격으로 문자를 출력할지
    public List<GameObject> gameObjectsForToggle; // 타이핑 이펙트가 끝나면 활성화/비활성화 시킬 게임오브젝트 리스트
    public bool toggleNormalON; // 이 변수가 true 이면 위의 리스트를 평상시 ON 시키겠다는 뜻.

    private string originText; // 원래 UI 의 텍스트를 보관할 멤버변수
    Coroutine coroutine = null;

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================
    public void OnPointerClick(PointerEventData eventData)
    {
        ShowOriginText(); // 원래 텍스트로 완성시키고
        ToggleGameObjects(); // 리스트의 오브젝트들을 토글시킴
    }


    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Awake()
    {
        originText = UI_Text.text; // UI 에 있는 원래 텍스트를  멤버변수에 보관
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(TypingEffectCoroutine());
    }

    IEnumerator TypingEffectCoroutine()
    {
        string tmpText = originText; // Text UI 가 가지고있는 string 형 텍스트 멤버
        for (int i = 0; i < tmpText.Length; i++)
        {
            UI_Text.text = tmpText.Substring(0, i); // substring 은 string 의 0번째~ i 번째까지 값을 반환하는 함수
            yield return new WaitForSeconds(typingDelay); 
        }
        ToggleGameObjects(); // 타이핑 이펙트 끝났으니 리스트에 담긴 게임오브젝트들 토글 시키기.
        coroutine = null; // 코루틴이 끝났으니 코루틴을 관리하는 변수에 null (아무것도 없음) 이라고 해줌
    }

    /// <summary>
    /// 타이핑이펙트가 끝났을때 원래 텍스트를 보여줄 함수
    /// </summary>
    private void ShowOriginText()
    {
        if (coroutine != null) // 현재 타이핑 이펙트 코루틴이 돌아가고 있으면
            StopCoroutine(coroutine); // 해당 코루틴을 중간에 종료하고
        UI_Text.text = originText; // 바로 UI 텍스트 오브젝트의 텍스트에 원래 텍스트를 집어넣음.
    }

    /// <summary>
    /// 리스트에 담긴 게임 오브젝트들을 토글시킬 함수
    /// </summary>
    private void ToggleGameObjects()
    {
        foreach (GameObject go in gameObjectsForToggle)
        {
            go.SetActive(!toggleNormalON); // 리스트의 게임오브젝트가 평상시 ON 이면 OFF 하고 평상시 OFF 이면 ON 시킴.
        }
    }
}