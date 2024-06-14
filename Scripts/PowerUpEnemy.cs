using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PowerUpEnemy : MonoBehaviour
{
    public event Action OnVausGethitEvent;
    int hitCounter = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Vaus"))
        {
            OnVausGethitEvent?.Invoke();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("ball"))
        {

                Destroy(gameObject);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            Destroy(gameObject);

        }
    }

}
