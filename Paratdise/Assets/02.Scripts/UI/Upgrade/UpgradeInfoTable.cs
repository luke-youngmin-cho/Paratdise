using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/06
/// 최종수정일 :
/// 설명 : 
/// 
/// 강화 정보 테이블
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
                // 굴착력
                [UpgradeType.DiggingForce] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("현무암조각"),
                                                 num = 50
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("현무암조각"),
                                                 num = 50
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("현무암조각"),
                                                 num = 50
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("질긴해초끈"),
                                                 num = 75
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("질긴해초끈"),
                                                 num = 75
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("질긴해초끈"),
                                                 num = 75
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 40
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("전선피복"),
                                                 num = 100
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 40
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("전선피복"),
                                                 num = 100
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("깨진벽돌"),
                                                 num = 125
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("깨진벽돌"),
                                                 num = 125
                                             },
                                         },

                                     },
                // 공격력
                [UpgradeType.Attack] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("두개골"),
                                                 num = 20
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("두개골"),
                                                 num = 20
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("두개골"),
                                                 num = 20
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("예티가죽"),
                                                 num = 30
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("예티가죽"),
                                                 num = 30
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("예티가죽"),
                                                 num = 30
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 40
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("납조각"),
                                                 num = 40
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 40
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("납조각"),
                                                 num = 40
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("페트병뚜껑"),
                                                 num = 100
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("페트병뚜껑"),
                                                 num = 100
                                             },
                                         },
                                     },
                // 빠르기
                [UpgradeType.Speed] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("도마뱀각막"),
                                                 num = 20
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("도마뱀각막"),
                                                 num = 20
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("도마뱀각막"),
                                                 num = 20
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("박쥐날개"),
                                                 num = 30
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("박쥐날개"),
                                                 num = 30
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("박쥐날개"),
                                                 num = 30
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 40
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("끈적이는액체"),
                                                 num = 40
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 40
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("끈적이는액체"),
                                                 num = 40
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("천쪼가리"),
                                                 num = 100
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("천쪼가리"),
                                                 num = 100
                                             },
                                         },
                                     },
                // 행운
                [UpgradeType.Luck] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("골렘코어"),
                                                 num = 20
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("골렘코어"),
                                                 num = 20
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 20
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("골렘코어"),
                                                 num = 20
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("폐어비늘"),
                                                 num = 30
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("폐어비늘"),
                                                 num = 30
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 30
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("폐어비늘"),
                                                 num = 30
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 40
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("코냥끼리상아"),
                                                 num = 40
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 40
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("코냥끼리상아"),
                                                 num = 40
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("땅벌침"),
                                                 num = 100
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("석탄"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("부싯돌"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("땅벌침"),
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
