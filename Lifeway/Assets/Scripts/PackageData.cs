using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Package", menuName = "Package Data", order = 51)]
public class PackageData : ScriptableObject

{
    [SerializeField]
    [TextAreaAttribute]
    private string russianText;

    public string RussianText
    {
        get
        {
            return russianText;
        }
    }

    [SerializeField]
    [TextAreaAttribute]
    private string englishText;
    public string EnglishText
    {
        get
        {
            return englishText;
        }
    }

    [SerializeField]
    private Sprite icon;
    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    [SerializeField]
    private float boostSpeed;
    public float BoostSpeed
    {
        get
        {
            return boostSpeed;
        }
    }

    [SerializeField]
    private StoryType storyType;
    public StoryType StoryType
    {
        get
        {
            return storyType;
        }
    }

    [SerializeField]
    private int placeNumberInHistory;
    public int PlaceNumberInHistory
    {
        get
        {
            return placeNumberInHistory;
        }
    }
}

public enum StoryType
{
    Love,
    Parents,
    Emigration,
    Dream,
    BrokenArm,
    Cat,
    Ñhild,
    Job
}