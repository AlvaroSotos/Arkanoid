using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
