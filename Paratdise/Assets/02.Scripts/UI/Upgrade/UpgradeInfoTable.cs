using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/06
/// ���������� :
/// ���� : 
/// 
/// ��ȭ ���� ���̺�
/// </summary>
public class UpgradeInfoTable : MonoBehaviour
{
    private static UpgradeInfoTable _instance;
    public static UpgradeInfoTable instance
    {
        get
        {
            if (_instance == null &&
                instantiateDisabled == false)
            {
                _instance = new UpgradeInfoTable();
                instantiateDisabled = true;
            }
            return _instance;
        }
    }
    public static bool instantiateDisabled;
    public bool isReady = false;

    [System.Serializable]
    public class UpgradeResourceInfo
    {
        public Item item;
        public int num;
    }
    public Dictionary<UpgradeType, List<UpgradeResourceInfo>[]> resourceDictionary;
    public Dictionary<UpgradeType, float> upgradeValueDictionaray;


    UpgradeInfoTable()
    {
        SetUp();
    }

    public bool SetUp()
    {
        if (!isReady)
        {
            resourceDictionary = new Dictionary<UpgradeType, List<UpgradeResourceInfo>[]>()
            {
                // ������
                [UpgradeType.DiggingForce] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("����������"),
                                                 num = 50
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("����������"),
                                                 num = 50
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("����������"),
                                                 num = 50
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�������ʲ�"),
                                                 num = 75
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�������ʲ�"),
                                                 num = 75
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�������ʲ�"),
                                                 num = 75
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�����Ǻ�"),
                                                 num = 100
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�����Ǻ�"),
                                                 num = 100
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��������"),
                                                 num = 125
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��������"),
                                                 num = 125
                                             },
                                         },

                                     },
                // ���ݷ�
                [UpgradeType.Attack] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ΰ���"),
                                                 num = 20
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ΰ���"),
                                                 num = 20
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ΰ���"),
                                                 num = 20
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��Ƽ����"),
                                                 num = 30
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��Ƽ����"),
                                                 num = 30
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��Ƽ����"),
                                                 num = 30
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("������"),
                                                 num = 40
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("������"),
                                                 num = 40
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��Ʈ���Ѳ�"),
                                                 num = 100
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��Ʈ���Ѳ�"),
                                                 num = 100
                                             },
                                         },
                                     },
                // ������
                [UpgradeType.Speed] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�����찢��"),
                                                 num = 20
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�����찢��"),
                                                 num = 20
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�����찢��"),
                                                 num = 20
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("���㳯��"),
                                                 num = 30
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("���㳯��"),
                                                 num = 30
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("���㳯��"),
                                                 num = 30
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�����̴¾�ü"),
                                                 num = 40
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�����̴¾�ü"),
                                                 num = 40
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("õ�ɰ���"),
                                                 num = 100
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("õ�ɰ���"),
                                                 num = 100
                                             },
                                         },
                                     },
                // ���
                [UpgradeType.Luck] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("���ھ�"),
                                                 num = 20
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("���ھ�"),
                                                 num = 20
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("���ھ�"),
                                                 num = 20
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�����"),
                                                 num = 30
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�����"),
                                                 num = 30
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�����"),
                                                 num = 30
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ڳɳ������"),
                                                 num = 40
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ڳɳ������"),
                                                 num = 40
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("����ħ"),
                                                 num = 100
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("��ź"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("�ν˵�"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("����ħ"),
                                                 num = 100
                                             },
                                         },
                                     },

            };
            upgradeValueDictionaray = new Dictionary<UpgradeType, float>()
            {
                [UpgradeType.DiggingForce] = 1.5f,
                [UpgradeType.Attack] = 11f,
                [UpgradeType.Speed] = 0.05f,
                [UpgradeType.Luck] = 2.5f
            };
        }
        isReady = true;
        return isReady;
    }

    public static List<UpgradeResourceInfo> GetUpgradeResourcesInfo(UpgradeType type, int level)
    {
        if (type <= UpgradeType.None)
            return null;

        if (level > instance.resourceDictionary[type].Length - 1)
            return null;

        return instance.resourceDictionary[type][level];
    }

    public static float GetUpgradeValue(UpgradeType type) =>
        instance.upgradeValueDictionaray[type];

    public static float GetTotalAdditionalValue(UpgradeType type, int level)
    {
        if (level > instance.resourceDictionary.Values.Count - 1)
            level = instance.resourceDictionary.Values.Count - 1;

        return GetUpgradeValue(type) * level;
    }

}

public enum UpgradeType
{
    None,
    DiggingForce,
    Attack,
    Speed,
    Luck,
}
