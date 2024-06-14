using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Arkanoid
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class BulletController : MonoBehaviour
    {
        [SerializeField, Min(3f)] float speed = 20f;
        new Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = Vector2.up * speed;
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Brick"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}