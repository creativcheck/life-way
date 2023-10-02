using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private GameObject packagePrefab;
    [SerializeField] private PackageData[] packagesData;
    [SerializeField] private float delayGivePackage, randomBetweenDelay;

    private float timer;
    private ObjectPool _packagesPool;

    public Package currentPackage;

    private void Start()
    {
        _packagesPool = new ObjectPool(packagesData.Length, packagePrefab);

        for (int i = 0; i < packagesData.Length; i++)
        {
            _packagesPool.GetPool()[i].GetComponent<Package>().UpdateData(packagesData[i]);
        }

        timer = delayGivePackage + (Random.value * randomBetweenDelay);
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {

        }
    }


}

public class ObjectPool
{
    private List<GameObject> PooledObjects;
    public ObjectPool(int amount, GameObject prefabObjectToPool)
    {
        PooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amount; i++)
        {
            tmp = GameObject.Instantiate(prefabObjectToPool);
            //tmp.SetActive(false);
            PooledObjects.Add(tmp);
        }
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy)
            {
                return PooledObjects[i];
            }
        }

        return null;
    }

    public List<GameObject> GetPool()
    {
        return PooledObjects;
    }
}