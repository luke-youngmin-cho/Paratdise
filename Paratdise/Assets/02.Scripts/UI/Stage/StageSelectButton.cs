using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 2022/04/10
/// ���� : 
/// 
/// ���� �������� UI Ŭ����
/// </summary>

public class StageSelectButton : MonoBehaviour
{
    public int stage;
    [SerializeField] private Image image;
    [SerializeField] private Sprite spriteActivated;
    [SerializeField] private Sprite spriteDeactivated;
    public Transform playDoubleCheckPanel;
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
            if(StageSelectionView.instance.stageSelected != stage)
            {
                StageSelectionView.instance.stageSelected = stage;
            }
            else
            {
                playDoubleCheckPanel.gameObject.SetActive(true);
                playDoubleCheckPanel.Find("StageText").GetComponent<Text>().text = stage.ToString() + " ��";
                playDoubleCheckPanel.Find("OKButton").GetComponent<Button>().onClick.AddListener(() => PlayWithSelectedStage());
                
            }
        }

    }

    public void PlayWithSelectedStage() =>
        GameManager.StartStage(stage);
}