using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private Package[] packages;
    [SerializeField] private float zDelay;
    [SerializeField] private Storage storage;

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

}
