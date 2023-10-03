using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shelf : MonoBehaviour
{
    public Package[] packages;
    public float zDelay;
    [SerializeField] private Storage storage;
    [SerializeField] private InputAction mouseOver;
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private GameObject panelTip;
    [SerializeField] private Transform button;
    [SerializeField] private string RusTip, EngTip;

    public Package[] Packages { get { return packages; } }

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        panelTip.SetActive(false);

        if (GameStats.Instance.language == Lang.Rus)
        {
            panelTip.GetComponentInChildren<TextMeshProUGUI>().text = RusTip;
        }
        else if (GameStats.Instance.language == Lang.Eng)
        {
            panelTip.GetComponentInChildren<TextMeshProUGUI>().text = EngTip;
        }
    }

    private void OnDisable()
    {
        mouseOver.performed -= OnOver;
        mouseOver.Disable();
        mouseClick.performed -= OnClick;
        mouseClick.canceled -= OnUp;
        mouseClick.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector3 pos = button.localPosition;
        pos.x = -0.85f;
        button.localPosition = pos;
        BurnPackages();
    }

    private void OnUp(InputAction.CallbackContext context)
    {
        Vector3 pos = button.localPosition;
        pos.x = -0.92f;
        button.localPosition = pos;
        panelTip.SetActive(false);

        mouseClick.performed -= OnClick;
        mouseClick.canceled -= OnUp;
        mouseClick.Disable();
        mouseOver.performed -= OnOver;
        mouseOver.Disable();
    }

    private void OnOver(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == gameObject.GetComponentInChildren<Collider>())
            {
                panelTip.SetActive(true);
                mouseClick.performed += OnClick;
                mouseClick.canceled += OnUp;
                mouseClick.Enable();
            }
            else
            {
                panelTip.SetActive(false);
                mouseClick.performed -= OnClick;
                mouseClick.canceled -= OnUp;
                mouseClick.Disable();
            }
        }
        else
        {
            panelTip.SetActive(false);
            mouseClick.performed -= OnClick;
            mouseClick.canceled -= OnUp;
            mouseClick.Disable();
        }

    }


    public void PlacePackage(Package package)
    {
        for (int i = 0; i < packages.Length; i++)
        {
            Vector3 pos = transform.position;
            pos.z -= zDelay * i;

            if (packages[i] == null)
            {
                packages[i] = package;
                package.transform.parent = null;
                package.transform.position = pos;
                package.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                storage.PlacePackage();

                mouseOver.Enable();
                mouseOver.performed += OnOver;
                break;
            }
        }
    }

    public void PrePlacePackege()
    {
        storage.OpenPackage();
    }

    public void BurnPackages()
    {
        for (int i = 0; i < packages.Length; i++)
        {
            if (packages[i] != null)
            {
                Destroy(packages[i].gameObject);
                packages[i] = null;
            }
        }
    }

}
