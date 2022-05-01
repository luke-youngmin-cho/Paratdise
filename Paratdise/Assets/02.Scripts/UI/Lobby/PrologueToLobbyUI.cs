using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/29
/// ���������� : 
/// ���� : 
/// 
/// ���ѷα׿��� �κ�ȭ������ �ڿ������� �Ѿ���� ����
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
