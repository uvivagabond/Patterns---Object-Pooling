using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolingManager
{
    // ta wielkość przechowuję informacje o ustawieniach puli
    PoolInfo poolInfo; 
    // tu przechowywane są referencje do dostępnych obiektów w puli
    Stack<GameObject> pool = new Stack<GameObject>();
    // tu przechowujemy metodę resetująca obiekty puli przed każdym użyciem
    Action<GameObject> initializationFunction;

    // informacje o puli obiektów możece przekazać z pomocą konstruktora z parametrem typu PoolInfo
    public PoolingManager(PoolInfo poolInfo)
    {
        this.poolInfo = poolInfo;
        InitializePoolItems();
    }
    // ... lub z pomocą konstruktora 4 parametrowego
    public PoolingManager(GameObject poolItem, GameObject container, int poolSize = 1, bool isPoolExpandable = true)
    {
        this.poolInfo.poolItem = poolItem;
        this.poolInfo.isPoolExpandable = isPoolExpandable;
        this.poolInfo.basePoolSize = poolSize;
        this.poolInfo.container = container;
        this.pool = new Stack<GameObject>();
        InitializePoolItems();
    }    
    // z pomocą tej metody przypisujęmy funkcje resetującą obiekty puli
    public void SetInitializationFunction(Action<GameObject> initializationFunction)
    {
        this.initializationFunction = initializationFunction;
    }
  
    // metoda do pobierania obiektów z puli
    // metoda 
    public GameObject GetFromPool()
    {
        if (this.pool.Count > 0)
        {
            GameObject itemFromPool = pool.Pop();
            itemFromPool.SetActive(true);
            ResetPoolItem(itemFromPool);
            return itemFromPool;
        }
        else if (poolInfo.isPoolExpandable)
        {      
            return InstantiateAndResetPoolItem(); 
        }
        else
        {
            return null;
        }
    }
    // metoda do umieszczania obiektów spowrotem w puli
    // metoda 
    public void PutToPool(GameObject poolItem)
    {
        this.pool.Push(poolItem);
        poolItem.transform.parent = poolInfo.container.transform;
        poolItem.SetActive(false);
    }
    // metoda do tworzenia nowych obiektów puli na bazie gameObjektu wzorcowego
    private GameObject InstantiateAndResetPoolItem()
    {
        GameObject newItem = UnityEngine.Object.Instantiate<GameObject>(this.poolInfo.poolItem, Vector3.zero, Quaternion.identity);
        ResetPoolItem(newItem);
        return newItem;
    }
    // wywołujemy metodę resetują obiekty puli
    void ResetPoolItem(GameObject poolItem)
    {
        initializationFunction?.Invoke(poolItem);
    }
    // inicjalizujemy początkową pulę obiektów znajdujących się w puli
    void InitializePoolItems()
    {
        for (int i = 0; i < poolInfo.basePoolSize; i++)
        {
            GameObject newItem = InstantiateAndResetPoolItem();
            PutToPool(newItem);
        }
    }
}

[System.Serializable]
public class PoolInfo
{
    public GameObject poolItem;
    public GameObject container;

    public int basePoolSize = 1;
    public bool isPoolExpandable = true;
}
