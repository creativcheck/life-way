using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private GameObject packagePrefab;
    [SerializeField] private PackageData[] packagesData;
    [SerializeField] private float delayGivePackage, randomBetweenDelay, timeToGive;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Shelf[] shelfs;

    private float timer;
    private bool giving, timerTick;
    private ObjectPool _packagesPool;
    private int ShelfToPlace, indexOnShelfToPlace;

    public Package currentPackage;

    private void Start()
    {
        _packagesPool = new ObjectPool(packagesData.Length, packagePrefab, packagesData);

        timerTick = true;
        timer = delayGivePackage + (Random.value * randomBetweenDelay);
    }

    private void Update()
    {
        if (timer > 0 && timerTick)
        {
            timer -= Time.deltaTime;
        }
        else if(timerTick && !GameStats.Instance.ending)
        {
            if (giving)
            {
                _packagesPool.DestroyPackage();
                timer = delayGivePackage + (Random.value * randomBetweenDelay);
                giving = false;
                currentPackage = null;
            }
            else
            {
                GivePackage();
            }
        }
    }

    public void PlacePackage()
    {
        _packagesPool.DestroyPackage(false);
        timer = delayGivePackage + (Random.value * randomBetweenDelay);
        giving = false;
        timerTick = true;
        currentPackage = null;
    }

    public void OpenPackage()
    {
        timerTick = false;
    }

    private void GivePackage()
    {
        float rand = Random.value * 3;

        if(rand < 1)
        {
            for (int i = 0; i < shelfs.Length; i++)
            {
                if (shelfs[i].Packages[0] == null)
                {
                    if (_packagesPool.GetNewStoryPackage() != null)
                    {
                        currentPackage = _packagesPool.GetNewStoryPackage().GetComponent<Package>();
                        ShelfToPlace = i;
                        indexOnShelfToPlace = 0;
                        break;
                    }
                }
                else if (shelfs[i].Packages[1] == null)
                {
                    if (_packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 2) != null)
                    {
                        currentPackage = _packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 2).GetComponent<Package>();
                        ShelfToPlace = i;
                        indexOnShelfToPlace = 1;
                        break;
                    }
                }
                else if (shelfs[i].Packages[2] == null)
                {
                    if (_packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 3) != null)
                    {
                        currentPackage = _packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 3).GetComponent<Package>();
                        ShelfToPlace = i;
                        indexOnShelfToPlace = 2;
                        break;
                    }
                }
            }

        }
        else if(rand < 2)
        {
            for (int i = 1; i < shelfs.Length; i++)
            {
                if (shelfs[i].Packages[0] == null)
                {
                    if (_packagesPool.GetNewStoryPackage() != null)
                    {
                        currentPackage = _packagesPool.GetNewStoryPackage().GetComponent<Package>();
                        ShelfToPlace = i;
                        indexOnShelfToPlace = 0;
                        break;
                    }
                }
                else if (shelfs[i].Packages[1] == null)
                {
                    if (_packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 2) != null)
                    {
                        currentPackage = _packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 2).GetComponent<Package>();
                        ShelfToPlace = i;
                        indexOnShelfToPlace = 1;
                        break;
                    }
                }
                else if (shelfs[i].Packages[2] == null)
                {
                    if (_packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 3) != null)
                    {
                        currentPackage = _packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 3).GetComponent<Package>();
                        ShelfToPlace = i;
                        indexOnShelfToPlace = 2;
                        break;
                    }
                }
            }

        }
        else
        {
            for (int i = 2; i < shelfs.Length; i++)
            {
                if (shelfs[i].Packages[0] == null)
                {
                    if (_packagesPool.GetNewStoryPackage() != null)
                    {
                        currentPackage = _packagesPool.GetNewStoryPackage().GetComponent<Package>();
                        ShelfToPlace = i;
                        indexOnShelfToPlace = 0;
                        break;
                    }
                }
                else if (shelfs[i].Packages[1] == null)
                {
                    if (_packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 2) != null)
                    {
                        currentPackage = _packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 2).GetComponent<Package>();
                        ShelfToPlace = i;
                        indexOnShelfToPlace = 1;
                        break;
                    }
                }
                else if (shelfs[i].Packages[2] == null)
                {
                    if (_packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 3) != null)
                    {
                        currentPackage = _packagesPool.GetPoolObject(shelfs[i].Packages[0].PackageData.StoryType, 3).GetComponent<Package>();
                        ShelfToPlace = i;
                        indexOnShelfToPlace = 2;
                        break;
                    }
                }
            }

        }

        if (currentPackage != null)
        {
            currentPackage.gameObject.transform.position = spawnPoint.position;
            currentPackage.gameObject.transform.rotation = spawnPoint.localRotation;
            currentPackage.gameObject.SetActive(true);
            currentPackage.shelf = shelfs[ShelfToPlace];

            giving = true;
        }

        timer = timeToGive;
    }


}

public class ObjectPool
{
    private MyList<GameObject> PooledObjects;
    private int currentPackage;
    public ObjectPool(int amount, GameObject prefabObjectToPool, PackageData[] packData)
    {
        PooledObjects = new MyList<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amount; i++)
        {
            tmp = GameObject.Instantiate(prefabObjectToPool);
            tmp.GetComponent<Package>().UpdateData(packData[i]);
            tmp.SetActive(false);
            
            PooledObjects.Add(tmp);
        }

        PooledObjects.Shuffle();
    }

    public void DestroyPackage(bool destroy = true)
    {
        //PooledObjects[currentPackage].SetActive(false);
        if(destroy)
            Object.Destroy(PooledObjects[currentPackage]);

        PooledObjects.RemoveAt(currentPackage);
    }

    public GameObject GetPoolObject(StoryType storyType, int placeInHistory)
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy)
            {
                PackageData data = PooledObjects[i].GetComponent<Package>().PackageData;
                if (data.StoryType == storyType && data.PlaceNumberInHistory == placeInHistory)
                {
                    currentPackage = i;
                    return PooledObjects[i];
                }
            }
        }

        return null;
    }

    public GameObject GetNewStoryPackage()
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy)
            {
                PackageData data = PooledObjects[i].GetComponent<Package>().PackageData;

                if (data.PlaceNumberInHistory == 1)
                {
                    currentPackage = i;
                    return PooledObjects[i];
                }
            }
        }

        return null;
    }

}

class MyList<T> : List<T>
{
    public void Shuffle()
    {
        System.Random rand = new System.Random();

        for (int i = this.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            T tmp = this[j];
            this[j] = this[i];
            this[i] = tmp;
        }
    }
}