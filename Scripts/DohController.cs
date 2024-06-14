using Scripts.Arkanoid;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using Unity.VisualScripting;

public class DohController : MonoBehaviour
{
    [SerializeField] Transform target;
    Animator animator;
    DamageController damageController;
    public event Action OnDohDestroyedEvent;
    public event Action OnDohDestroyedBeforeEvent;
    [SerializeField] VausController vausController;
    //ShooterController shooterController;

    [SerializeField] Shooting shooting;
    DohShooting dohShooting;
    [SerializeField] BallController ballController;
    [SerializeField, Range(2f, 4f)] float maxCDShoots = 3f;
    [SerializeField, Range(0.5f, 2f)] float minCDShoots = 1f;

    private void Awake()
    {
        animator= GetComponent<Animator>();
        damageController= GetComponent<DamageController>();
        damageController.OnDestroyedEvent += OnDestroyedCallback;
        damageController.OnDamageReceivedEvent += OnDamageReceivedCallback;
        dohShooting = GetComponentInChildren<DohShooting>();
        //shooting = GetComponentInChildren<Shooting>();
        //InvokeRepeating("Shoot", 1f, 3f);
    }

    void ShootFromAnimation()
    {
        StartCoroutine(DohShooting(shooting.transform.position, target));
    }
    private void Start()
    {
        StartCoroutine(StartAttack());
    }
    IEnumerator StartAttack()
    {
        float randomTime = Random.Range(minCDShoots, maxCDShoots);
        yield return new WaitForSeconds(randomTime);
        animator.SetTrigger("Attack");
    }
    IEnumerator DohShooting(Vector3 position, Transform target)
    {
        for (int i = 0; i < 3; i++)
        {
            dohShooting.GetBullet(position, target);
            yield return new WaitForSeconds(0.5f);

        }
        float randomTime = Random.Range(minCDShoots, maxCDShoots);
        yield return new WaitForSeconds(randomTime);
        animator.SetTrigger("Attack");


    }

    private void OnDestroyedCallback(DamageController obj)
    {
        //ballController.FreezeBall();
        animator.SetTrigger("Destroy");
        OnDohDestroyedBeforeEvent?.Invoke();
    }

    private void OnDamageReceivedCallback(DamageController obj)
    {
        animator.SetTrigger("Hurt");
    }

    void DestroyFromAnimation()
    {
        OnDohDestroyedEvent?.Invoke();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            animator.SetTrigger("Attack");
        }
        
    }
}
