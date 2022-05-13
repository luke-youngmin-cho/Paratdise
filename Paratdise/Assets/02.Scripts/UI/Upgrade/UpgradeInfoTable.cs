using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÀÛ¼ºÀÚ : Á¶¿µ¹Î
/// ÃÖÃÊÀÛ¼ºÀÏ : 2022/05/06
/// ÃÖÁ¾¼öÁ¤ÀÏ :
/// ¼³¸í : 
/// 
/// °­È­ Á¤º¸ Å×ÀÌºí
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
                // ±¼Âø·Â
                [UpgradeType.DiggingForce] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Çö¹«¾ÏÁ¶°¢"),
                                                 num = 50
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Çö¹«¾ÏÁ¶°¢"),
                                                 num = 50
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Çö¹«¾ÏÁ¶°¢"),
                                                 num = 50
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Áú±äÇØÃÊ²ö"),
                                                 num = 75
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Áú±äÇØÃÊ²ö"),
                                                 num = 75
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Áú±äÇØÃÊ²ö"),
                                                 num = 75
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Àü¼±ÇÇº¹"),
                                                 num = 100
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Àü¼±ÇÇº¹"),
                                                 num = 100
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("±úÁøº®µ¹"),
                                                 num = 125
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("±úÁøº®µ¹"),
                                                 num = 125
                                             },
                                         },

                                     },
                // °ø°Ý·Â
                [UpgradeType.Attack] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("µÎ°³°ñ"),
                                                 num = 20
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("µÎ°³°ñ"),
                                                 num = 20
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("µÎ°³°ñ"),
                                                 num = 20
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¿¹Æ¼°¡Á×"),
                                                 num = 30
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¿¹Æ¼°¡Á×"),
                                                 num = 30
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¿¹Æ¼°¡Á×"),
                                                 num = 30
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("³³Á¶°¢"),
                                                 num = 40
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("³³Á¶°¢"),
                                                 num = 40
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ÆäÆ®º´¶Ñ²±"),
                                                 num = 100
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ÆäÆ®º´¶Ñ²±"),
                                                 num = 100
                                             },
                                         },
                                     },
                // ºü¸£±â
                [UpgradeType.Speed] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("µµ¸¶¹ì°¢¸·"),
                                                 num = 20
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("µµ¸¶¹ì°¢¸·"),
                                                 num = 20
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("µµ¸¶¹ì°¢¸·"),
                                                 num = 20
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¹ÚÁã³¯°³"),
                                                 num = 30
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¹ÚÁã³¯°³"),
                                                 num = 30
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¹ÚÁã³¯°³"),
                                                 num = 30
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("²öÀûÀÌ´Â¾×Ã¼"),
                                                 num = 40
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("²öÀûÀÌ´Â¾×Ã¼"),
                                                 num = 40
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ÃµÂÉ°¡¸®"),
                                                 num = 100
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ÃµÂÉ°¡¸®"),
                                                 num = 100
                                             },
                                         },
                                     },
                // Çà¿î
                [UpgradeType.Luck] = new List<UpgradeResourceInfo>[10]
                                     {
                                         // level 1
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("°ñ·½ÄÚ¾î"),
                                                 num = 20
                                             },
                                         },
                                         // level 2
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("°ñ·½ÄÚ¾î"),
                                                 num = 20
                                             },
                                         },
                                         // level 3
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 50
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("°ñ·½ÄÚ¾î"),
                                                 num = 20
                                             },
                                         },
                                         // level 4
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Æó¾îºñ´Ã"),
                                                 num = 30
                                             },
                                         },
                                         // level 5
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Æó¾îºñ´Ã"),
                                                 num = 30
                                             },
                                         },
                                         // level 6
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 75
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("Æó¾îºñ´Ã"),
                                                 num = 30
                                             },
                                         },
                                         // level 7
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ÄÚ³É³¢¸®»ó¾Æ"),
                                                 num = 40
                                             },
                                         },
                                         // level 8
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 100
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ÄÚ³É³¢¸®»ó¾Æ"),
                                                 num = 40
                                             },
                                         },
                                         // level 9
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¶¥¹úÄ§"),
                                                 num = 100
                                             },
                                         },
                                         // level 10
                                         new List<UpgradeResourceInfo>()
                                         {
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¼®Åº"),
                                                 num = 125
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("ºÎ½Ëµ¹"),
                                                 num = 15
                                             },
                                             new UpgradeResourceInfo()
                                             {
                                                 item = ItemAssets.instance.GetItemByName("¶¥¹úÄ§"),
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
