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
/// 스테이지 선택창
/// </summary>
namespace YM
{
    public class StageSelectionView : MonoBehaviour
    {
        [SerializeField] private StageView stageViewPrefab;
        [SerializeField] private RectTransform contents;
        [SerializeField] private Transform doubleCheckPanel;
        private List<StageView> stageViews = new List<StageView>();

        //===============================================================================================
        //********************************** Private Methods ********************************************
        //===============================================================================================

        private void Start()
        {
            Vector2 stageViewSize = stageViewPrefab.GetComponent<RectTransform>().rect.size;
            contents.GetComponent<RectTransform>().rect.Set(0, 1000, 300, StageTable.TOTAL * 100);
            int dir = 1;
            Vector2 pos = new Vector2(contents.rect.width / 4, stageViewSize.y);
            int xIndex = 0;
            for (int i = 1; i <= MapInfoAssets.instance.mapInfos.Count; i++)
            {
                pos.x += (xIndex * dir * stageViewSize.x);
                pos.y += stageViewSize.y;

                if (i % 3 == 0)
                {
                    dir *= -1;
                    xIndex = 0;
                }

                xIndex++;

                StageView stageView = Instantiate(stageViewPrefab, contents).GetComponent<StageView>();
                stageView.stage = i;
                stageView.playDoubleCheckPanel = doubleCheckPanel;
                stageView.transform.position = pos;
                stageView.transform.GetComponentInChildren<Text>().text = i.ToString();
                stageViews.Add(stageView);

            }
            

            ActiveStageViewsOpened();
        }

        private void ActiveStageViewsOpened()
        {
            for (int i = 0; i < stageViews.Count ; i++)
            {
                if(i > PlayerDataManager.data.stageSaved - 1)
                    stageViews[i].isActivated = false;
                else
                    stageViews[i].isActivated = true;
            }
        }
    }
}
