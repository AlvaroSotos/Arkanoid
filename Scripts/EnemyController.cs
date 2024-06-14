using Pong.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Arkanoid
{
    public class EnemyController : MonoBehaviour
    {
        enum EnemyStateEnum
        {
            Stopped, Chasing, Down
        }

        [SerializeField, Range(1f, 10f)] float enemySpeed = 5f;
        [SerializeField, Range(0.1f, 0.5f)] float stopTime = 0.5f;
        [SerializeField, Range(0.5f, 3f)] float chaseTime = 2f;
        ParticleSystem enemyAttack;
        Rigidbody2D EnemyRb;
        [SerializeField] float radius;
        [SerializeField, Range(1, 5)] int resistance = 3;
        Vector3 enemyDir;
        Vector3[] points;
        DamageController damageController;
        [SerializeField] EnemyStateEnum enemyState;
        EnemyStateEnum EnemyState
        {
            get 
            { 
                return enemyState;
            }
            set 
            { 
                enemyState = value;
                switch (enemyState)
                {
                    case EnemyStateEnum.Stopped:
                        StartCoroutine(StopTime());
                        break;
                    case EnemyStateEnum.Chasing:
                        StartCoroutine(ChaseTime());
                        break;
                    case EnemyStateEnum.Down:
                        EnemyRb.velocity = Vector2.down * enemySpeed;
                        break;
                }

            }
        }
        IEnumerator StopTime()
        {
            EnemyRb.velocity = Vector2.zero;
           
            yield return new WaitForSeconds(stopTime);
            EnemyState = EnemyStateEnum.Chasing;
        }
        IEnumerator ChaseTime()
        {
            EnemyRb.velocity = new Vector3(enemyDir.x, 0f, 0f).normalized   /*enemyDir.normalized */* enemySpeed;
            yield return new WaitForSeconds(chaseTime);
            EnemyRb.velocity = Vector2.zero;
            enemyAttack.transform.position = transform.position;
            enemyAttack.Play();
            yield return new WaitForSeconds(2f);
            EnemyState = EnemyStateEnum.Down;
        }
        private void OnEnable()
        {
            enemyAttack = GetComponentInChildren<ParticleSystem>();
            EnemyRb = GetComponent<Rigidbody2D>();
            var collider = GetComponent<CircleCollider2D>();
            EnemyState = EnemyStateEnum.Down;
            points = new Vector3[3];
        }
        private void Awake()
        {
            damageController = GetComponent<DamageController>();
            damageController.OnDestroyedEvent += OnDestroyedEnemyCallback;
        }
        private void OnDestroyedEnemyCallback(DamageController obj)
        {
            gameObject.SetActive(false);
        }
        void FixedUpdate()
        {
            
            Vector3 point = transform.position + Vector3.down * 1.1f;
            Collider2D circleHit = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));
            /*RaycastHit2D rayHit = Physics2D.Raycast(point, Vector2.down, 10f);
            Debug.DrawRay(point, Vector2.down * 10f);*/
            if (circleHit != null && EnemyState.Equals(EnemyStateEnum.Down))
            {
                //Debug.Log(circleHit.transform);
                EnemyState = EnemyStateEnum.Stopped;
                enemyDir = circleHit.transform.position - transform.position;
            }
            
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == ("ball"))
            {
                resistance = damageController.MakeDamageBall(resistance);

            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                resistance = damageController.MakeDamageBullet(resistance);

            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
            
        }
    }
}

