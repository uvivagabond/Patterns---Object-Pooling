using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // przechowujemy referencje do puli kul armatnich
    PoolingManager cannonBalls;

    private void Start()
    {
        // pobieramy referencje do puli kul armatnich, która przechowywana jest w skrypcie CannoBallPool
        cannonBalls = CannoBallPool.SharedInstance.CannonBalls;
        // przypisujemy puli kul armatnich metodę resetującą
        // robimy to po to aby zawsze wystrzeliwane kule miały właściwe ustawienia gdy wyciągamy je z puli
        // nie musimy przypisywać metody resetującej w metodzie SetInitializationFunction()
        // możemy także resetować objekty zaraz po wyjęciu ich z puli, ale będzie to mnie eleganckie rozwiązanie
        cannonBalls.SetInitializationFunction(ResetPoolItem);
    }

    // metoda resetująca
    // ja w tym przykładzie resetuję kule poprzez podpięcie ich do gameObjectu Cannon, zresetowanie ich pozycji
    // i ustawienie im odpowiedniej prędkości z pomocą komponentu Rigidbody
    private void ResetPoolItem(GameObject cannonBall)
    {
        cannonBall.transform.parent = transform;
        cannonBall.transform.localPosition = Vector3.zero;
        cannonBall.GetComponent<Rigidbody2D>().velocity = new Vector3(10, 4, 0);
    }

    void Update()
    {
        // gdy wciśniemy spacje bądź LPM to zostanie wystrzelona kula
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // uzyskujemy kule armatnią z puli, która jest już zresetowana i gotowa to użycia
            GameObject cannonBall = cannonBalls.GetFromPool();
            // oczywiście możecie zmodyfikować zachowanie kuli także dopiero po wydobyciu jej z puli
        }
    }

}
