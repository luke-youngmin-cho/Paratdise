using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/23
/// 최종수정일 : 
/// 설명 : 
/// 
/// FluidBundle 의 자식 GameObject 에게 컴포넌트로 추가해야함.
/// </summary>

namespace YM
{
    public class Fluid : MonoBehaviour
    {
        FluidBundle motherBundle;
        LayerMask layer;
        Vector2 overlapSize;
        Vector2 oldPos;
        CircleCollider2D col;
        Transform tr;

        public float evaporationTime = 3f;
        float evaporationTimer;

        //===============================================================================================
        //********************************** Public Methods *********************************************
        //===============================================================================================

        public void ReturnToMother()
        {
            motherBundle.BringChild(this);
        }

        public void SetMotherBundle(FluidBundle bundle)
        {
            motherBundle = bundle;
        }


        //===============================================================================================
        //********************************** Private Methods ********************************************
        //===============================================================================================

        private void Awake()
        {
            tr = transform;
            col = GetComponent<CircleCollider2D>();
            evaporationTimer = evaporationTime;
            layer = 1 << gameObject.layer;
            overlapSize = new Vector2(col.radius * 2, 0.01f);
        }

        private void Update()
        {
            TryEvapration();
        }

        /// <summary>
        /// fluid 의 증발 메커니즘
        /// </summary>
        private void TryEvapration()
        {
            bool upCol   = Physics2D.OverlapBox(new Vector2(tr.position.x, tr.position.y) + Vector2.up * col.radius * 2,
                                                overlapSize,
                                                0,
                                                layer);

            bool downCol = Physics2D.OverlapBox(new Vector2(tr.position.x, tr.position.y) + Vector2.down * col.radius * 2,
                                                overlapSize,
                                                0,
                                                layer);

            // 움직임이 멈추었을때만 증발 가능.
            float delta = new Vector2(oldPos.x - tr.position.x, oldPos.y - tr.position.y).magnitude;

            if (delta < 0.01f)
            {
                // 위/ 아래에 동일한 fluid 가 없으면 증발 시작
                if (!upCol &&
                    !downCol)
                {
                    evaporationTimer -= Time.deltaTime;
                }
                else if (evaporationTimer < evaporationTime)
                {
                    evaporationTimer = evaporationTime;
                }

                // 증발 시간 다되면 번들로 돌아감.
                if (evaporationTimer < 0)
                {
                    ReturnToMother();
                }
            }
            else if (evaporationTimer < evaporationTime)
                evaporationTimer = evaporationTime;

            oldPos = tr.position;
        }

        private void OnDrawGizmosSelected()
        {
            tr = GetComponent<Transform>();
            col = GetComponent<CircleCollider2D>();
            overlapSize = new Vector2(col.radius * 2, 0.01f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(new Vector2(tr.position.x, tr.position.y) + Vector2.up * col.radius * 2, overlapSize);
            Gizmos.DrawWireCube(new Vector2(tr.position.x, tr.position.y) + Vector2.down * col.radius * 2, overlapSize);
        }
    }
}
