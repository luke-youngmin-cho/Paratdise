using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/06
/// ���������� : 
/// ���� : 
/// 
/// Ʃ�丮�� ����
/// </summary>
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
                description = "�̵� �� ����\n���� �ϴܿ� �ִ� ���̽�ƽ���� ĳ���͸� ������ �� �ֽ��ϴ�.\n��� �������� ĳ���͸� ������ �� �ش� ����� �ν� ĳ���Ͱ� �̵� �� �� �ֽ��ϴ�.\n��, �ʿ��� �ı��Ұ��� ���(��)�� �����մϴ�."
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
                description = "�������� é���� �������������� ���� �ѵ� �ý����� �����մϴ�.\n��ġ�� ���������� ���ҵǸ�, 0�� �Ǹ� ���� �ð����� ���ٽ��ϴ�.\n������ �� �ִ� ��ġ�� �ٽ� �������ϴ�.\nƯ�� ��ϰ� ������ ��, ��ġ�� �� ������ ���ҵ� �� �ֽ��ϴ�."
            },
            new Tutorial()
            {
                title = "���� �ѵ�",
                description = "�Ҳ� ����\n�������� é���� 4F �������������� �����ϴ� ����Դϴ�.\n���� �� �̵� �ӵ��� ��������, ���� �ѵ� ��ġ�� �����մϴ�."
            },
            new Tutorial()
            {
                title = "���ŷ�",
                description = "���ش��� é���� �������������� ���ŷ� �ý����� �����մϴ�.\n��ġ�� ���������� ���ҵǸ�, ������ ���� ������ ���� ����մϴ�.\n���ŷ� ��ġ�� 0�� �Ǹ� �����ϰ� �ǰ�, ���� �ð����� ������ �ݴ�� �Էµ˴ϴ�.\n���� �� ���ŷ��� �ִ� ��ġ�� �ٽ� �������ϴ�.\nƯ�� ����� �ı��� ��, ��ġ�� �� ������ ���ҵ� �� �ֽ��ϴ�."
            },
            new Tutorial()
            {
                title = "������ȭ",
                description = "���ش��� é���� �������������� ���ŷ� �ý����� �����մϴ�.\n��ġ�� ���������� ���ҵǸ�, ������ ���� ������ ���� ����մϴ�.\n���ŷ� ��ġ�� 0�� �Ǹ� �����ϰ� �ǰ�, ���� �ð����� ������ �ݴ�� �Էµ˴ϴ�.\n���� �� ���ŷ��� �ִ� ��ġ�� �ٽ� �������ϴ�.\nƯ�� ����� �ı��� ��, ��ġ�� �� ������ ���ҵ� �� �ֽ��ϴ�."
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

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}

