using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/29
/// 최종수정일 : 
/// 설명 : 
/// 
/// 프롤로그에서 로비화면으로 자연스럽게 넘어오는 연출
/// </summary>
public class PrologueToLobbyUI : MonoBehaviour
{
    private static PrologueToLobbyUI _instance;
    public static PrologueToLobbyUI instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<PrologueToLobbyUI>("UI/PrologueToLobbyUI"));
            return _instance;
        }
    }
    [SerializeField] private RectTransform rt;
    [SerializeField] private RectTransform standard;
    [SerializeField] private float delta = 120f;
    
    private Coroutine coroutine = null;

    public void Play()
    {
        if (coroutine == null)
            coroutine = StartCoroutine(E_Scroll());
    }

    private IEnumerator E_Scroll()
    {
        Vector3 originPos = rt.localPosition;
        float height = rt.rect.height - standard.rect.height ;
        Debug.Log(Screen.height);
        float scrollDistance = 0f;
        while (scrollDistance < height)
        {
            scrollDistance += height / delta;
            rt.localPosition = new Vector3 (originPos.x, originPos.y + scrollDistance, originPos.z);
            
            if (scrollDistance >= height)
                rt.localPosition = new Vector3(originPos.x, originPos.y + height, originPos.z);
            yield return null;
        }

        Destroy(gameObject);
    }

}
