using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPlayer : MonoBehaviour
{
    public static TutorialPlayer instance;
    [SerializeField] private GameObject tutorialPage;
    [SerializeField] private Text title;
    [SerializeField] private Text discription;
    public List<GameObject> gameObjectsForToggle; // Ÿ���� ����Ʈ�� ������ Ȱ��ȭ/��Ȱ��ȭ ��ų ���ӿ�����Ʈ ����Ʈ
    public bool toggleNormalON; // �� ������ true �̸� ���� ����Ʈ�� ���� ON ��Ű�ڴٴ� ��.

    [System.Serializable]
    public class Tutorial
    {
        public string title;
        public string description;
    }
    private Tutorial[] tutorials =
        new Tutorial[13]
        {
            new Tutorial()
            {
                title = "���۹��",
                description = "�̵� �� ����\n���� �ϴܿ� �ִ� ���̽�ƽ���� ĳ���͸� ������ �� �ֽ��ϴ�.\n���� �������� ĳ���͸� ������ �� �ش� ������ �ν� ĳ���Ͱ� �̵� �� �� �ֽ��ϴ�.\n��, �ʿ��� �ı��Ұ��� ����(��)�� �����մϴ�."
            },
            new Tutorial()
            {
                title = "���۹��",
                description = "����\n���� �ϴܿ� �ִ� ���ݹ�ư���� ũ��ó�� ������ �� �ֽ��ϴ�."
            },
            new Tutorial()
            {
                title = "�������� Ŭ����",
                description = "�������� Ŭ����\n��� ���������� �ְ� ���̱��� �ö� ��, �� ������ ����ִ� �� ĭ�� �����ϸ� Ŭ����˴ϴ�."
            },
            new Tutorial()
            {
                title = "���� ����",
                description = "��ġ 0\n���� �ð����� ĳ���Ͱ� Ż������ ���ϰų� ü���� 0�̵Ǹ� ���� ������ �˴ϴ�.\n�߰� �������� ����, 1ȸ�� �����Ͽ� ���� ��û �� ��Ȱ�� �� �ֽ��ϴ�."
            },
            new Tutorial()
            {
                title = "���� ����",
                description = "�г�Ƽ\nĳ���� ����� ������ �ִ� �ڿ����� ��� ������� �˴ϴ�.\n(���丮 ����, ���� ī��, �ó����� ������, ���� ��ȭ ������ �����˴ϴ�)"
            },
            new Tutorial()
            {
                title = "������",
                description = "Ÿ��ĸ��\n�ʿ� �������� �����ϴ� �������Դϴ�.\nŸ��ĸ�� ȹ�� �� ü���� ������ ȸ���Ǹ� �ִ� ü�·��� �����մϴ�."
            },
            new Tutorial()
            {
                title = "������",
                description = "�ó����� ������\n���� ���� �ó����� �б��� ���ÿ� ���� ���� �� �ִ� �������Դϴ�.\nƯ�� é�Ϳ��� �ڵ����� Ȱ��ȭ �˴ϴ�."
            },
            new Tutorial()
            {
                title = "���� �ѵ�",
                description = "�������� é���� �������������� ���� �ѵ� �ý����� �����մϴ�.\n��ġ�� ���������� ���ҵǸ�, 0�� �Ǹ� ���� �ð����� ���ٽ��ϴ�.\n������ �� �ִ� ��ġ�� �ٽ� �������ϴ�.\nƯ�� ���ϰ� ������ ��, ��ġ�� �� ������ ���ҵ� �� �ֽ��ϴ�."
            },
            new Tutorial()
            {
                title = "���� �ѵ�",
                description = "�Ҳ� ����\n�������� é���� 4F �������������� �����ϴ� �����Դϴ�.\n���� �� �̵� �ӵ��� ��������, ���� �ѵ� ��ġ�� �����մϴ�."
            },
            new Tutorial()
            {
                title = "���ŷ�",
                description = "���ش��� é���� �������������� ���ŷ� �ý����� �����մϴ�.\n��ġ�� ���������� ���ҵǸ�, ������ ���� ������ ���� ����մϴ�.\n���ŷ� ��ġ�� 0�� �Ǹ� �����ϰ� �ǰ�, ���� �ð����� ������ �ݴ�� �Էµ˴ϴ�.\n���� �� ���ŷ��� �ִ� ��ġ�� �ٽ� �������ϴ�.\nƯ�� ������ �ı��� ��, ��ġ�� �� ������ ���ҵ� �� �ֽ��ϴ�."
            },
            new Tutorial()
            {
                title = "������ȭ",
                description = "���ش��� é���� �������������� ���ŷ� �ý����� �����մϴ�.\n��ġ�� ���������� ���ҵǸ�, ������ ���� ������ ���� ����մϴ�.\n���ŷ� ��ġ�� 0�� �Ǹ� �����ϰ� �ǰ�, ���� �ð����� ������ �ݴ�� �Էµ˴ϴ�.\n���� �� ���ŷ��� �ִ� ��ġ�� �ٽ� �������ϴ�.\nƯ�� ������ �ı��� ��, ��ġ�� �� ������ ���ҵ� �� �ֽ��ϴ�."
            },
            new Tutorial()
            {
                title = "����",
                description = "���丮 ����\n�ʿ� �������� �����ϴ� Ÿ��ĸ���� ȹ���� �� ���� �� �ִ� �������Դϴ�.\n���丮 ������ ���, ����, ����, �۱�, ���������� �������� �ֽ��ϴ�."
            },
            new Tutorial()
            {
                title = "����",
                description = "����ī��\n���� Ŭ���� �� �ó����� �б����� ���ÿ� ���� ���� ī�带 ������ �� �ֽ��ϴ�."
            }
        };


    public void PlayTutorial(int index)
    {
        if (index < 0 || 
            index >= tutorials.Length)
            return;

        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);

        
    }
   /* IEnumerator TypingEffectCoroutine()
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
    }*/
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
