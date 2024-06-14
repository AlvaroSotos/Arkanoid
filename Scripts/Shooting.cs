using Scripts.Arkanoid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Adaptative_pool pool;

    [SerializeField] private float bullet_speed;
    [SerializeField] private float cd_time = 0.2f;
     float Next_bullet;

    PlayerInputActions player_controls;
    bool isShooting;

    
    void Start()
    {
    }

    protected virtual void Awake()
    {
        
        //pool = GetComponent<Adaptative_pool>();

        player_controls = new PlayerInputActions();
        player_controls.Player.Fire.performed += Fire_performed =>
        {
            isShooting = Fire_performed.ReadValueAsButton();
        }; 
        player_controls.Player.Fire.canceled += Fire_performed =>
        {
            isShooting = Fire_performed.ReadValueAsButton();
        };
    }

    void FixedUpdate()
    {
        TryShooting();

    }


     void TryShooting()
     {
        if (isShooting && Time.time > Next_bullet)
        {
            Next_bullet = Time.time + cd_time;

            GameObject bullet = pool.GetPoolBullet();
            bullet.SetActive(true);
            bullet.transform.position = transform.position + new Vector3(1, 1, 0);
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * bullet_speed;
        }
     }

    private void OnEnable()
    {
        player_controls.Enable();
    }

    private void OnDisable()
    {
        player_controls.Disable();

    }
}
