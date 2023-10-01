using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    [SerializeField]
    private PackageData packageData; 

    private void OnMouseDown() 
    {
        Debug.Log(packageData.RussianText); 
        Debug.Log(packageData.EnglishText); 
        Debug.Log(packageData.Icon.name); 
        Debug.Log(packageData.BustSpeed); 
        Debug.Log(packageData.StoryType1);
        Debug.Log(packageData.PlaceNumberInHistory);
    }
}
