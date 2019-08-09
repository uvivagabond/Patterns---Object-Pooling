using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // przechowujemy referencje do puli kul armatnich
    PoolingManager cannonBallsPool;

    private void Start()
    {
        // pobieramy referencje do puli kul armatnich, która przechowywana jest w skrypcie CannoBallPool
        cannonBallsPool = CannoBallPool.SharedInstance.CannonBalls;
    }
    
    // ta funkcja zostanie wywołana gdy kula armatnia zderzy się ze ścianą
    // parametr cannonBall będzie przechowywać informacje o zaistniałem kolizji ze ścianą
    // a tym samy informacje o obiekcie, który w ścianę uderzył
    void OnCollisionEnter2D(Collision2D cannonBall)
    {
        // gdy kula trafi w ścianę zostaję umieszczona z powrotem w puli obiektów
        cannonBallsPool.PutToPool(cannonBall.gameObject);
    }   
}
