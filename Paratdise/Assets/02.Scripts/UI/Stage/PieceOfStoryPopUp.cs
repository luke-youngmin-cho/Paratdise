using UnityEngine;

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

    public void PopUp()
    {
        _instance.gameObject.SetActive(true);
    }
}