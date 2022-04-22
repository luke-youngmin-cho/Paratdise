using UnityEngine;
using System.Collections.Generic;


public class StoryAssets : MonoBehaviour
{
    static public StoryAssets _instance;
    static public StoryAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<StoryAssets>("Assets/StoryAssets"));
            return _instance;
        }
    }

    public List<Story> stories_Mice = new List<Story> ();
    public List<Story> stories_Laila = new List<Story>();
    public List<Story> stories_DrillGgabijo = new List<Story>();
    public List<Story> stories_Eily = new List<Story>();

    public Story GetStory(CharacterType type, int index)
    {
        switch (type)
        {
            case CharacterType.None:
                break;
            case CharacterType.Mice:
                return stories_Mice.Find(x => x.characterType == type && x.index == index);
            case CharacterType.Laila:
                return stories_Laila.Find(x => x.characterType == type && x.index == index);
            case CharacterType.DrillGgabijo:
                return stories_DrillGgabijo.Find(x => x.characterType == type && x.index == index);
            case CharacterType.Eily:
                return stories_Eily.Find(x => x.characterType == type && x.index == index);
            default:
                break;
        }
        return null;
    }
}