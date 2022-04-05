using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 개별 스테이지 UI 클래스
/// </summary>
namespace YM
{
    public class StageView : MonoBehaviour
    {
        public int stage;
        [SerializeField] private Image image;
        [SerializeField] private Sprite spriteActivated;
        [SerializeField] private Sprite spriteDeactivated;
        [HideInInspector] public Transform playDoubleCheckPanel;
        private bool _isActivated;

        //===============================================================================================
        //********************************** Public Methods *********************************************
        //===============================================================================================

        public bool isActivated
        {
            set
            {
                _isActivated = value;
                if (_isActivated)
                {
                    image.sprite = spriteActivated;
                    GetComponent<Button>().interactable = true;
                }
                else
                {
                    image.sprite = spriteDeactivated;
                    GetComponent<Button>().interactable = false;
                }
            }
        }

        public void OnButtonClick()
        {
            if (_isActivated)
            {
                playDoubleCheckPanel.gameObject.SetActive(true);
                playDoubleCheckPanel.Find("OKButton").GetComponent<Button>().onClick.AddListener(() => PlayWithSelectedStage());
            }
                
        }

        public void PlayWithSelectedStage() =>
            GameManager.StartStage(stage);
    }

}