using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Package : MonoBehaviour
{
    [SerializeField] private InputAction mouseClick;
    [SerializeField]
    private PackageData packageData;

    [SerializeField]
    private TextMeshProUGUI speedText;

    [SerializeField]
    private TextMeshProUGUI textLetter;

    [SerializeField]
    private Image icon;

    private Camera mainCamera;

    [HideInInspector] public PackageData PackageData { get { return packageData; } }
    [HideInInspector] public Vector3 place;
    [HideInInspector] public Shelf shelf;

    private void OnMouseDown() 
    {
        /*Debug.Log(packageData.RussianText); 
        Debug.Log(packageData.EnglishText); 
        Debug.Log(packageData.Icon.name); 
        Debug.Log(packageData.BoostSpeed); 
        Debug.Log(packageData.StoryType);
        Debug.Log(packageData.PlaceNumberInHistory);*/
    }


    private void Start()
    {
        mainCamera = Camera.main;
        RedrawData();
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += GetPackage;
    }

    private void OnDisable()
    {
        mouseClick.performed -= GetPackage;
        mouseClick.Disable();
    }

    private void GetPackage(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == gameObject.GetComponent<Collider>())
            {
                PlacePackage();
            }
        }

        mouseClick.performed -= GetPackage;
        mouseClick.Disable();
    }

    private void PlacePackage()
    {
        shelf.PlacePackage(this);
    }

    public void UpdateData(PackageData newPackageData)
    {
        packageData = newPackageData;
        RedrawData();
    }

    private void RedrawData()
    {
        speedText.text = packageData.BoostSpeed.ToString();
        icon.sprite = packageData.Icon;
        textLetter.text = packageData.RussianText;
    }
}
