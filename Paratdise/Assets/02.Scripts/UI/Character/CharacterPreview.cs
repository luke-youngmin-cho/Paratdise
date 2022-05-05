using UnityEngine;


public class CharacterPreview : MonoBehaviour
{
    [SerializeField] private GameObject micePreviewModel;
    [SerializeField] private GameObject lailaPreviewModel;
    [SerializeField] private GameObject joePreviewModel;
    [SerializeField] private GameObject aileyPreviewModel;
    [SerializeField] private GameObject micePreviewModelNotAvailable;
    [SerializeField] private GameObject lailaPreviewModelNotAvailable;
    [SerializeField] private GameObject joePreviewModelNotAvailable;
    [SerializeField] private GameObject aileyPreviewModelNotAvailable;

    

    public void SwitchPreviewModel()
    {
        if (GameManager.characterSelected <= CharacterType.Mice)
            GameManager.SelectCharacter(CharacterType.Mice);

        DeactivateAllModels();
        switch (GameManager.characterSelected)
        {
            case CharacterType.None:
                break;
            case CharacterType.Mice:
                if (PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).isAvailable)
                    micePreviewModel.SetActive(true);
                else
                    micePreviewModelNotAvailable.SetActive(true);
                break;
            case CharacterType.Laila:
                if (PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).isAvailable)
                    lailaPreviewModel.SetActive(true);
                else
                {
                    GameManager.SelectCharacter(GameManager.characterSelected - 1);
                    SwitchPreviewModel();
                }
                break;
            case CharacterType.DrillGgabijo:
                if (PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).isAvailable)
                    joePreviewModel.SetActive(true);
                else
                {
                    GameManager.SelectCharacter(GameManager.characterSelected - 1);
                    SwitchPreviewModel();
                }
                break;
            case CharacterType.Ailey:
                if (PlayerDataManager.data.GetCharacterData(GameManager.characterSelected).isAvailable)
                    aileyPreviewModel.SetActive(true);
                else
                {
                    GameManager.SelectCharacter(GameManager.characterSelected - 1);
                    SwitchPreviewModel();
                }   
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        SwitchPreviewModel();
    }

    private void DeactivateAllModels()
    {
        micePreviewModel.SetActive(false);
        lailaPreviewModel.SetActive(false);
        joePreviewModel.SetActive(false);
        aileyPreviewModel.SetActive(false);
        micePreviewModelNotAvailable.SetActive(false);
        lailaPreviewModelNotAvailable.SetActive(false);
        joePreviewModelNotAvailable.SetActive(false);
        aileyPreviewModelNotAvailable.SetActive(false);
    }
}