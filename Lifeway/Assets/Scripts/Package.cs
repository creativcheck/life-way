using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Package : MonoBehaviour
{
    [SerializeField]
    private PackageData packageData;

    [SerializeField]
    private TextMeshProUGUI speedText;

    [SerializeField]
    private TextMeshProUGUI textLetter;

    [SerializeField]
    private Image icon;


    private void OnMouseDown() 
    {
        Debug.Log(packageData.RussianText); 
        Debug.Log(packageData.EnglishText); 
        Debug.Log(packageData.Icon.name); 
        Debug.Log(packageData.BustSpeed); 
        Debug.Log(packageData.StoryType1);
        Debug.Log(packageData.PlaceNumberInHistory);

    }

    private void Start()
    {
        speedText.text = packageData.BustSpeed.ToString ();
        icon.sprite = packageData.Icon;
        textLetter.text = packageData.RussianText;
   
    }
}
