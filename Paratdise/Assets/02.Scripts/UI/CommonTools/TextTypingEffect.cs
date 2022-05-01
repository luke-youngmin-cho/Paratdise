using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/24
/// ���������� : 
/// ���� : 
/// 
/// �ؽ�Ʈ Ÿ������ ����Ʈ�� ���� Ŭ����
/// </summary>
/// 
public class TextTypingEffect : MonoBehaviour, IPointerClickHandler
{
    public Text UI_Text; // ����Ƽ ���� �����Ϳ� �ִ� Text UI
    public float typingDelay; // ���� �������� ���ڸ� �������
    public List<GameObject> gameObjectsForToggle; // Ÿ���� ����Ʈ�� ������ Ȱ��ȭ/��Ȱ��ȭ ��ų ���ӿ�����Ʈ ����Ʈ
    public bool toggleNormalON; // �� ������ true �̸� ���� ����Ʈ�� ���� ON ��Ű�ڴٴ� ��.

    private string originText; // ���� UI �� �ؽ�Ʈ�� ������ �������
    Coroutine coroutine = null;

    //===============================================================================================
    //********************************** Public Methods *********************************************
    //===============================================================================================
    public void OnPointerClick(PointerEventData eventData)
    {
        ShowOriginText(); // ���� �ؽ�Ʈ�� �ϼ���Ű��
        ToggleGameObjects(); // ����Ʈ�� ������Ʈ���� ��۽�Ŵ
    }


    //===============================================================================================
    //********************************** Private Methods ********************************************
    //===============================================================================================

    private void Awake()
    {
        originText = UI_Text.text; // UI �� �ִ� ���� �ؽ�Ʈ��  ��������� ����
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(TypingEffectCoroutine());
    }

    IEnumerator TypingEffectCoroutine()
    {
        string tmpText = originText; // Text UI �� �������ִ� string �� �ؽ�Ʈ ���
        for (int i = 0; i < tmpText.Length; i++)
        {
            UI_Text.text = tmpText.Substring(0, i); // substring �� string �� 0��°~ i ��°���� ���� ��ȯ�ϴ� �Լ�
            yield return new WaitForSeconds(typingDelay); 
        }
        ToggleGameObjects(); // Ÿ���� ����Ʈ �������� ����Ʈ�� ��� ���ӿ�����Ʈ�� ��� ��Ű��.
        coroutine = null; // �ڷ�ƾ�� �������� �ڷ�ƾ�� �����ϴ� ������ null (�ƹ��͵� ����) �̶�� ����
    }

    /// <summary>
    /// Ÿ��������Ʈ�� �������� ���� �ؽ�Ʈ�� ������ �Լ�
    /// </summary>
    private void ShowOriginText()
    {
        if (coroutine != null) // ���� Ÿ���� ����Ʈ �ڷ�ƾ�� ���ư��� ������
            StopCoroutine(coroutine); // �ش� �ڷ�ƾ�� �߰��� �����ϰ�
        UI_Text.text = originText; // �ٷ� UI �ؽ�Ʈ ������Ʈ�� �ؽ�Ʈ�� ���� �ؽ�Ʈ�� �������.
    }

    /// <summary>
    /// ����Ʈ�� ��� ���� ������Ʈ���� ��۽�ų �Լ�
    /// </summary>
    private void ToggleGameObjects()
    {
        foreach (GameObject go in gameObjectsForToggle)
        {
            go.SetActive(!toggleNormalON); // ����Ʈ�� ���ӿ�����Ʈ�� ���� ON �̸� OFF �ϰ� ���� OFF �̸� ON ��Ŵ.
        }
    }
}