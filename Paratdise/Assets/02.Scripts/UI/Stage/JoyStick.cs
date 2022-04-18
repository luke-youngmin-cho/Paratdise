using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
   

    [SerializeField,Range(10,250)]
    private float leverRange;

    private Vector2 inputDirection;
    private bool isInput;

    [SerializeField]
    //private PlayerController2 controller;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        isInput = true;
    }
    // Start is called before the first frame update

    public void OnDrag(PointerEventData eventData)
    {
        lever.position = eventData.position;
        lever.localPosition = Vector2.ClampMagnitude(lever.localPosition, leverRange);
        Test_Player.instance.direction = GetJoyStickDirection(lever.localPosition.normalized);
        Debug.Log($"{inputDirection}, {Test_Player.instance.direction}");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.localPosition = Vector2.zero;
        Test_Player.instance.direction = Vector2.zero;
        isInput = false;
    }


    private Vector2 GetJoyStickDirection(Vector2 dir)
    {
        Vector2 moveDirection = Vector2.zero;
        if ((dir.x < 0.5f || dir.x >= -0.5f) && dir.y >= 0.5f)
        {
            moveDirection = Vector2.up;
        }
        else if ((dir.x < 0.5f || dir.x >= -0.5f) && dir.y < -0.5f)
        {
            moveDirection = Vector2.down;
        }
        else if ((dir.y < 0.5f || dir.y >= -0.5f) && dir.x >= 0.5f)
        {
            moveDirection = Vector2.right;
        }
        else if ((dir.y < 0.5f || dir.y >= -0.5f) && dir.x < -0.5f)
        {
            moveDirection = Vector2.left;
        }

        return moveDirection;
    }

}