using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adaptative_pool : MonoBehaviour
{

    [SerializeField] GameObject enemy_prefab;
    [SerializeField] GameObject powerUpEnemy_prefab;

    [SerializeField] GameObject bullet_prefab;
    [SerializeField] private int MaxSize;
    private List<GameObject> pool_list_1;
    private List<GameObject> pool_list_2;

    void Start()
    {
        pool_list_1 = new List<GameObject>();
        for (int i = 0; i < MaxSize; i++)
        {
            GameObject obj = Instantiate(enemy_prefab);
            obj.SetActive(false);
            pool_list_1.Add(obj);
        }
        pool_list_2 = new List<GameObject>();
        for (int i = 0; i < MaxSize; i++)
        {
            GameObject obj = Instantiate(bullet_prefab);
            obj.SetActive(false);
            pool_list_2.Add(obj);
        }

        
    }
   
    void Update()
    {
        
    }
    
    public GameObject GetPoolEnemy()
    {
        for (int i = 0; i < MaxSize; i++)
        {
            if (!pool_list_1[i].activeInHierarchy)
            {
                return pool_list_1[i];
            }           
        }
        GameObject newObj = Instantiate(enemy_prefab);
        pool_list_1.Add(enemy_prefab);
        return newObj;
    }
    public GameObject GetPoolBullet()
    {
        for (int i = 0; i < MaxSize; i++)
        {
            if (!pool_list_2[i].activeInHierarchy)
            {
                return pool_list_2[i];
            }           
        }
        GameObject newObj = Instantiate(bullet_prefab);
        pool_list_2.Add(bullet_prefab);
        return newObj;
    }


}
