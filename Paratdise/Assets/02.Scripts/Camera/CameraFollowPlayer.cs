using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어를 따라다닐 카메라 조작.
/// smoothness 로 따라다니는 속도 조절가능.
/// boundingShape에 범위 한정해서 따라다님.
/// </summary>

namespace YM
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        public Vector3 offset;
        [Range(1, 10)]
        public float smoothness;
        public BoxCollider2D boundingShape;
        private float boundingShapeXMin;
        private float boundingShapeXMax;
        private float boundingShapeYMin;
        private float boundingShapeYMax;
        private Transform tr;
        private Camera cam;
        

        //===============================================================================================
        //********************************** Private Methods ********************************************
        //===============================================================================================

        private void Awake()
        {
            cam = GetComponent<Camera>();
            tr = GetComponent<Transform>();
            boundingShapeXMin = boundingShape.transform.position.x + boundingShape.offset.x - boundingShape.size.x / 2;
            boundingShapeXMax = boundingShape.transform.position.x + boundingShape.offset.x + boundingShape.size.x / 2;
            boundingShapeYMin = boundingShape.transform.position.y + boundingShape.offset.y - boundingShape.size.y / 2;
            boundingShapeYMax = boundingShape.transform.position.y + boundingShape.offset.y + boundingShape.size.y / 2;
        }

        private void FixedUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            if (Test_Player.instance == null) return;

            Transform target = Test_Player.instance.transform;

            Vector3 targetPos = new Vector3(target.position.x, target.position.y, tr.position.z) + offset;
            Vector3 smoothPos = Vector3.Lerp(tr.position, targetPos, smoothness * Time.fixedDeltaTime);

            Vector2 camWorldPosLeftBottom = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector2 camWorldPosRightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            Vector2 camWorldPosSize = new Vector2(camWorldPosRightTop.x - camWorldPosLeftBottom.x,
                                                  camWorldPosRightTop.y - camWorldPosLeftBottom.y);

            if (smoothPos.x - camWorldPosSize.x / 2 < boundingShapeXMin)
                smoothPos.x = boundingShapeXMin + camWorldPosSize.x / 2;
            else if (smoothPos.x + camWorldPosSize.x / 2 > boundingShapeXMax)
                smoothPos.x = boundingShapeXMax - camWorldPosSize.x / 2;

            if (smoothPos.y - camWorldPosSize.y / 2 < boundingShapeYMin)
                smoothPos.y = boundingShapeYMin + camWorldPosSize.y / 2;
            else if (smoothPos.y + camWorldPosSize.y / 2 > boundingShapeYMax)
                smoothPos.y = boundingShapeYMax - camWorldPosSize.y / 2;

            tr.position = smoothPos;
        }

        private void OnDrawGizmosSelected()
        {
            Camera cam = GetComponent<Camera>();
            Vector3 p = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector3 q = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            Vector3 center = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane));

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, new Vector2(q.x - p.x, q.y - p.y));

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(boundingShape.transform.position + (Vector3)boundingShape.offset, (boundingShape.size));
        }
    }

}
