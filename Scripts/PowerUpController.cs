using Scripts.Arkanoid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float powerUpSpeed = 5f;
    [SerializeField] private PowerUpType powerUpTypes;
    [SerializeField] GameObject destructionPrefab;
    public event Action<PowerUpType> OnPowerUpActivatedEvent;

    internal PowerUpType GetPowerUpType()
    {
        return powerUpTypes;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.down * powerUpSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vaus"))
        {
            OnPowerUpActivatedEvent?.Invoke(powerUpTypes);
            Instantiate(destructionPrefab);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("vacio"))
        {
            Destroy(gameObject);
        }
    }

}
