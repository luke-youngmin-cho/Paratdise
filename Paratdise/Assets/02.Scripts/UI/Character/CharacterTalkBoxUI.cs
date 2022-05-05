using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/05
/// 최종수정일 : 
/// 설명 : 
/// 
/// 캐릭터 선택창의 캐릭터 말풍선
/// </summary>
public class CharacterTalkBoxUI : MonoBehaviour
{
    private string[,] characterScripts = new string[5, 3]
    {
        {"Dummy", "Dummy", "Dummy" },
        {"나는 평범한 아기 쥐일 뿐이야. ","지상을 보고싶어 ","숟가락만 가지고 될까 " },
        {"작다고 하지 마! 빠르고 멀리 보니까! ", "엄마 미워.. 난 여길 떠날 거야! ", "돌아가고싶어... " },
        { "헷, 난 크고 튼튼하지! 좀 느리지만... ", "몸이 나쁘면 머리가 고생을 하는 법! ", "누구도 날 막을 수 없어! "},
        {"평범한 나라도... ","누군가는 해야할 일이야. ","쉬고 싶어 " }
    };

    [SerializeField] private Text _text;
    public float typingDelay = 0.2f; // 몇초 간격으로 문자를 출력할지
    private Coroutine coroutine;
    private string target;

    public void Refresh()
    {
        int randomIndex = Random.Range(0, 3);
        target = characterScripts[(int)GameManager.characterSelected, randomIndex];
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(TypingEffectCoroutine());
    }

    public void OnClick()
    {
        ShowOriginText();
    }

    private void OnEnable()
    {
        Refresh();
    }
    IEnumerator TypingEffectCoroutine()
    {
        string tmpText = target; // Text UI 가 가지고있는 string 형 텍스트 멤버
        for (int i = 0; i < tmpText.Length; i++)
        {
            _text.text = tmpText.Substring(0, i); // substring 은 string 의 0번째~ i 번째까지 값을 반환하는 함수
            yield return new WaitForSeconds(typingDelay);
        }
        ShowOriginText();
        coroutine = null; // 코루틴이 끝났으니 코루틴을 관리하는 변수에 null (아무것도 없음) 이라고 해줌
    }

    /// <summary>
    /// 타이핑이펙트가 끝났을때 원래 텍스트를 보여줄 함수
    /// </summary>
    private void ShowOriginText()
    {
        if (coroutine != null) // 현재 타이핑 이펙트 코루틴이 돌아가고 있으면
            StopCoroutine(coroutine); // 해당 코루틴을 중간에 종료하고
        _text.text = target; // 바로 UI 텍스트 오브젝트의 텍스트에 원래 텍스트를 집어넣음.
    }

}