using UnityEngine;
using UnityEngine.UI;
public class PieceOfStoryPopUp : MonoBehaviour
{
    public static PieceOfStoryPopUp _instance;
    public static PieceOfStoryPopUp instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<PieceOfStoryPopUp>("UI/PieceOfStoryPopUp"));
            return _instance;
        }
    }

    public void PopUp(Sprite icon, string title, string decription, int index, PieceOfStoryRarity rarity)
    {
        _instance.transform.GetChild(0).Find("Image").GetComponent<Image>().sprite = icon;
        _instance.transform.GetChild(0).Find("Title").GetComponent<Text>().text = title;
        _instance.transform.GetChild(0).Find("Description").GetComponent<Text>().text = decription;
        _instance.transform.GetChild(0).Find("Index").GetComponent<Text>().text = index.ToString();
        _instance.transform.GetChild(0).Find("Rarity").GetComponent<Text>().text = rarity.ToString();
        _instance.gameObject.SetActive(true);
    }
}