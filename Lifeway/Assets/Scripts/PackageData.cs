using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Package", menuName = "Package Data", order = 51)]
public class PackageData : ScriptableObject

{
    [SerializeField]
    private string russianText;

    public string RussianText
    {
        get
        {
            return russianText;
        }
    }

    [SerializeField]
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
    private float bustSpeed;
    public float BustSpeed
    {
        get
        {
            return bustSpeed;
        }
    }

    [SerializeField]
    private StoryType storyType;
    public StoryType StoryType1
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
    SelfTransformation,
    Betrayal,
    DreamAchievement,
    Loyalty,
    OvercomingFear
}