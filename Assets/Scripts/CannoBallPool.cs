using System;
using UnityEngine;

// ta klasa będzie przechowywać wszystkie pule obiektów
// w tym przykładzie mamy tylko jedną pule
public class CannoBallPool : Singleton<CannoBallPool>
{
    // z pomocą tej instancji definiujemy zachowanie naszej puli obiektów
    // ten parametr zostanie wykorzystany w konstruktorze klasy PoolingManager
    public PoolInfo poolInfo;
    // ta instancja reprezentuję jedną pule objektów
    PoolingManager cannonBalls;

    // korzystamy z property i lazy initialization aby mieć pewność, że konstruktor zostanie wywołany
    // przed pobraniem elementu z puli
    public PoolingManager CannonBalls
    {
        get
        {
            if (cannonBalls == null)
            {
                cannonBalls = new PoolingManager(poolInfo);
            }
            return cannonBalls;
        }
    }
}


//[SerializeField] GameObject cannonBallPrefab;

//[SerializeField] PoolingManager cannonBallsPool;

//public PoolingManager CannonBallsPool
//{
//    get
//    {
//        if (cannonBallsPool == null)
//        {
//            cannonBallsPool = new PoolingManager(cannonBallPrefab, this.gameObject, 10);
//        }
//        return cannonBallsPool;
//    }
//}