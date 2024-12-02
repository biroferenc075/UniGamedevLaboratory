using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    private List<GameObject> pool;
    public GameObject poolItem;
    public int poolSize;
    public static BulletPoolManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
            
        pool = new List<GameObject>(poolSize);

        for(int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(poolItem);
            go.SetActive(false);

            pool.Add(go);
        }
    }

    public GameObject getItemFromPool()
    {
        GameObject res = pool.Find(go => !go.activeInHierarchy);
        if(res == null)
        {
            res = Instantiate(poolItem);
            pool.Add(res);
        }

        return res;
    }

    public void freeItem(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.parent = this.transform;
    }
}

