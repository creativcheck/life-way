using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.ReloadAttribute;

public class Shelf : MonoBehaviour
{
    public Package[] packages;
    public float zDelay;
    [SerializeField] private Storage storage;

    public Package[] Packages { get { return packages; } }

    private void Start()
    {
        /*for (int i = 0; i < packages.Length; i++)
        {
            Vector3 pos = transform.position;
            pos.z -= zDelay * i;

            //Package package = Instantiate(packagePrefab, pos, Quaternion.AngleAxis(90, Vector3.up), transform).GetComponent<Package>();
            //packages[i] = package;
        }*/
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
