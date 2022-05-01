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

    PointerEventData dragEventData;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    private void Update()
    {
        if (GameManager.gameState != GameState.StageLoaded ||
            PlayerStateMachineManager.instance == null) 
            return;

        if (isInput &&
            dragEventData != null)
        {
            UpdateLeverPosition(dragEventData.position);
        }
        else
        {
            lever.localPosition = Vector2.zero;
            PlayerStateMachineManager.instance.direction = Vector2.zero;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isInput = true;
    }
    // Start is called before the first frame update

    // 2022.04.20 조영민 : 드래그가 되고있지 않아도 조이스틱을 당기고있다면 동작하도록 수정
    public void OnDrag(PointerEventData eventData)
    {
        dragEventData = eventData;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.localPosition = Vector2.zero;
        PlayerStateMachineManager.instance.direction = Vector2.zero;
        isInput = false;
    }

    private void UpdateLeverPosition(Vector3 position)
    {
        lever.position = position;
        lever.localPosition = Vector2.ClampMagnitude(lever.localPosition, leverRange);
        PlayerStateMachineManager.instance.direction = GetJoyStickDirection(lever.localPosition.normalized);
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