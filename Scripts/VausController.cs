using Scripts.Arkanoid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VausController : MonoBehaviour
{
    [SerializeField, Range(5f, 30f)] float vausSpeed = 15f;
    PlayerManager playerManager;
    IUpdateVaus updateVaus;
    PlayerInputActions inputActions;
    Vector2 playerInput;
    Rigidbody2D rb;
    bool ballReleased;
    bool isShooting;
    public event Action OnReleaseBallEvent;
    public event Action OnVausDestroyedEvent;

    VausState vausState;
    VausState VausState //Property associated to vausState var
    {
        get { return vausState; }
        set 
        { 
            if(value != vausState)
            {
                vausState = value;
                updateVaus.UpdateVaus(vausState);
            }
        }
    }     
    void OnEnable()
    {
        inputActions.Enable();
    }
    void OnDisable()
    {
        inputActions.Disable();
    }
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        updateVaus = GetComponent<VausAnimatorUpdater>();

        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += move_performed =>
        {
            playerInput = move_performed.ReadValue<Vector2>();
        };
        inputActions.Player.Move.canceled += move_canceled =>
        {
            playerInput = move_canceled.ReadValue<Vector2>();
        };
        inputActions.Player.Jump.performed += jump_performed =>
        {
            if (!ballReleased)
            {
                OnReleaseBallEvent?.Invoke();
                ballReleased = true;
            }
        };
        inputActions.Player.Fire.performed += fire_performed =>
        {
            isShooting = fire_performed.ReadValueAsButton();
        };

    }
    private void Start()
    {
        playerManager = PlayerManager.GetInstance();
    }
    void FixedUpdate()
    {
        UpdatePosition();

        if (isShooting)
        {
            //shooting
        }
    }

    internal Vector3 GetVausPosition()
    {
        return transform.position;
    }
    void UpdatePosition()
    {
        if (playerInput.magnitude != 0)
        {
            rb.velocity = new Vector2(playerInput.x * vausSpeed, 0f);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Update()
    {


    }

    internal void resetVaus()
    {
        ballReleased = false;
        VausState = VausState.Normal;
    }
    internal void Enlarge()
    {
        VausState = VausState.Enlarged;
    }
    
    internal void Catch()
    {
        VausState = VausState.Catch;
    }

    public void DisableShooting()
    {
        GetComponent<Shooting>().enabled = false;
    }
    internal void Laser()
    {
        if(VausState != VausState.Normal)
        {
            VausState= VausState.Normal;

        }
        VausState = VausState.Laser;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DohBullet"))
        {
            OnVausDestroyedEvent?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            if (collision.gameObject.GetComponent<BallController>().IsCatchy())
            {
                ballReleased = false;
            }
            else
            {
                ballReleased = true;
            }
        }
    }
}
