using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인벤토리 창을 토글시키는 클래스
/// </summary>
namespace YM
{
    public class InventoryViewSelector : MonoBehaviour
    {
        public Image equipButtonImage;
        public Image spendButtonImage;
        public Image etcButtonImage;
        public Image cashButtonImage;


        //============================================================================
        //************************* Public  Methods **********************************
        //============================================================================

        public void SelectItemsView(int type)
        {
            switch (type)
            {
                case (int)ItemType.Equip:
                    InventoryView.instance.equipItemsViewInstance.gameObject.SetActive(true);
                    InventoryView.instance.spendItemsViewInstance.gameObject.SetActive(false);
                    InventoryView.instance.etcItemsViewInstance.gameObject.SetActive(false);
                    InventoryView.instance.cashItemsViewInstance.gameObject.SetActive(false);
                    NegativeColor(equipButtonImage);
                    RollBackColor(spendButtonImage);
                    RollBackColor(etcButtonImage);
                    RollBackColor(cashButtonImage);
                    break;
                case (int)ItemType.Spend:
                    InventoryView.instance.equipItemsViewInstance.gameObject.SetActive(false);
                    InventoryView.instance.spendItemsViewInstance.gameObject.SetActive(true);
                    InventoryView.instance.etcItemsViewInstance.gameObject.SetActive(false);
                    InventoryView.instance.cashItemsViewInstance.gameObject.SetActive(false);
                    RollBackColor(equipButtonImage);
                    NegativeColor(spendButtonImage);
                    RollBackColor(etcButtonImage);
                    RollBackColor(cashButtonImage);
                    break;
                case (int)ItemType.ETC:
                    InventoryView.instance.equipItemsViewInstance.gameObject.SetActive(false);
                    InventoryView.instance.spendItemsViewInstance.gameObject.SetActive(false);
                    InventoryView.instance.etcItemsViewInstance.gameObject.SetActive(true);
                    InventoryView.instance.cashItemsViewInstance.gameObject.SetActive(false);
                    RollBackColor(equipButtonImage);
                    RollBackColor(spendButtonImage);
                    NegativeColor(etcButtonImage);
                    RollBackColor(cashButtonImage);
                    break;
                case (int)ItemType.Cash:
                    InventoryView.instance.equipItemsViewInstance.gameObject.SetActive(false);
                    InventoryView.instance.spendItemsViewInstance.gameObject.SetActive(false);
                    InventoryView.instance.etcItemsViewInstance.gameObject.SetActive(false);
                    InventoryView.instance.cashItemsViewInstance.gameObject.SetActive(true);
                    RollBackColor(equipButtonImage);
                    RollBackColor(spendButtonImage);
                    RollBackColor(etcButtonImage);
                    NegativeColor(cashButtonImage);
                    break;
                default:
                    break;
            }
        }


        //============================================================================
        //************************* Private Methods **********************************
        //============================================================================

        private void Start()
        {
            StartCoroutine(E_Start());
        }

        IEnumerator E_Start()
        {
            yield return new WaitUntil(() => InventoryView.instance.isReady);
            SelectItemsView(0);
        }

        private void NegativeColor(Image image)
        {
            Color.RGBToHSV(image.color, out float H, out float S, out float V);
            H = (H + 0.5f) % 1f;
            image.color = Color.HSVToRGB(H, S, V);
        }

        private void RollBackColor(Image image)
        {
            image.color = Color.white;
        }


    }
}