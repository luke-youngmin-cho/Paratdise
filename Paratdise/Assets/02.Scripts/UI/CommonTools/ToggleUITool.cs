using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// UI 토글을 위한 클래스.
/// 선택시 색을 반전시키고싶은 이미지나 활성화 / 비활성화 시키고싶은 게임오브젝트를 등록해놓고 사용함.
/// </summary>
public class ToggleUITool : MonoBehaviour
{
    public Image[] images;
    public GameObject[] gameObjects1;
    public GameObject[] gameObjects2;


    //============================================================================
    //************************* Public  Methods **********************************
    //============================================================================

    public void Select(int index)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if(images[i] != null)
            {
                if (i == index)
                    NegativeColor(images[i]);
                else
                    RollBackColor(images[i]);
            }
        }

        for (int i = 0; i < gameObjects1.Length; i++)
        {
            if (gameObjects1[i] != null)
            {
                if (i == index)
                    gameObjects1[i].SetActive(true);
                else
                    gameObjects1[i].SetActive(false);
            }
        }

        for (int i = 0; i < gameObjects2.Length; i++)
        {
            if (gameObjects2[i] != null)
            {
                if (i == index)
                    gameObjects2[i].SetActive(true);
                else
                    gameObjects2[i].SetActive(false);
            }
        }
    }


    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void Start()
    {
        Select(0);
    }


    private void NegativeColor(Image image)
    {
        //Color.RGBToHSV(image.color, out float H, out float S, out float V);
        //H = (H + 0.5f) % 1f;
        //image.color = Color.HSVToRGB(H, S, V);
        image.color = Color.gray;
    }

    private void RollBackColor(Image image)
    {
        image.color = Color.white;
    }
}
