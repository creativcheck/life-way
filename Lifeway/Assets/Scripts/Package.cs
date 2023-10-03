using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Package : MonoBehaviour
{
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private PackageData packageData;

    [SerializeField]
    private TextMeshProUGUI speedText;

    [SerializeField]
    private TextMeshProUGUI textLetter;

    [SerializeField]
    private Image icon, letterIcon;

    private Camera mainCamera;
    private Animator animator;

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
        animator = GetComponent<Animator>();
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
                mouseClick.performed -= GetPackage;
                mouseClick.Disable();
                OpenPackage();
            }
        }

    }

    private void OpenPackage()
    {
        animator.SetBool("Open", true);
        gameObject.transform.parent = mainCamera.transform.GetChild(0);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
        shelf.PrePlacePackege();
    }

    public void AddEventToPlacePackage()
    {
        mouseClick.performed += PlacePackage;
        mouseClick.Enable();
    }

    private void PlacePackage(InputAction.CallbackContext context)
    {
        shelf.PlacePackage(this);
        animator.SetBool("Open", false);
        mouseClick.performed -= PlacePackage;
        mouseClick.Disable();
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
        letterIcon.sprite = packageData.Icon;

        if(GameStats.Instance.language == Lang.Rus)
            textLetter.text = packageData.RussianText;
        else
            textLetter.text = packageData.EnglishText;
    }
}
