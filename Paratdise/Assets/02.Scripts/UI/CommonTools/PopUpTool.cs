using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTool : MonoBehaviour
{
    [SerializeField] PopUpType popUpType;
    [SerializeField] public bool autoClose;
    [SerializeField] public float autoCloseDelayTime;
    [SerializeField] public bool autoDestroy;
    [SerializeField] public float autoDestroyTime;
    private Vector3 awakenPosition;
    private void Awake()
    {
        awakenPosition = transform.position;
    }
    private void OnEnable()
    {
        SetPosition();
        if(autoClose == true)
        {
            StartCoroutine(CloseAfterDelay(autoCloseDelayTime));
        }
        if (autoDestroy == true)
        {
            StartCoroutine(DestroyAfterDelay(autoDestroyTime));
        }
    }

    virtual public void SetPosition()
    {
        if (popUpType.usePopUpPositionType == false) return;

        float thisWidth = 0f ;
        float thisHeight = 0f;
        RectTransform rectTransform;
        
        if (gameObject.TryGetComponent<RectTransform>(out rectTransform) == true)
        {
            thisWidth = rectTransform.rect.width;
            thisHeight = rectTransform.rect.height;
        }

        switch (popUpType.popUpPositionType)
        {
            case e_PopUpPositionType.AwakenPosition:
                gameObject.transform.position = awakenPosition;
                break;
            case e_PopUpPositionType.Middle:
                gameObject.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
                break;
            case e_PopUpPositionType.Top:
                gameObject.transform.position = new Vector2(Screen.width / 2, thisHeight);                
                break;
            case e_PopUpPositionType.Bottm:
                gameObject.transform.position = new Vector2(Screen.width / 2, Screen.height - thisHeight);
                break;
            case e_PopUpPositionType.Left:
                gameObject.transform.position = new Vector2(thisWidth, Screen.height /2);
                break;
            case e_PopUpPositionType.Right:
                gameObject.transform.position = new Vector2(Screen.width - thisWidth, Screen.height / 2);
                break;
            case e_PopUpPositionType.TopLeft:
                gameObject.transform.position = new Vector2(thisWidth, thisHeight);                
                break;
            case e_PopUpPositionType.TopRight:
                gameObject.transform.position = new Vector2(Screen.width - thisWidth, thisHeight);
                break;
            case e_PopUpPositionType.BottomLeft:
                gameObject.transform.position = new Vector2(thisWidth, Screen.height - thisHeight);
                break;
            case e_PopUpPositionType.BottomRight:
                gameObject.transform.position = new Vector2(Screen.width - thisWidth, Screen.height - thisHeight);                
                break;
            default:
                break;
        }
    }

    IEnumerator CloseAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator DestroyAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
        yield return null;
    }
}
[System.Serializable]
class PopUpType
{
    public bool usePopUpPositionType;
    public e_PopUpPositionType popUpPositionType;
}

enum e_PopUpPositionType
{
    AwakenPosition,
    Middle,
    Top,
    Bottm,
    Left,
    Right,
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}