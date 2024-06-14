using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Arkanoid
{
    [RequireComponent(typeof(Animator))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField, Min(0)] int maxNumberOfEnemies = 2;
        [SerializeField] GameObject enemyPrefab;
        [SerializeField, Range(0.1f, 3f)] float minTime = 1f;
        [SerializeField, Range(2f, 5f)] float maxTime = 3f;
        [SerializeField] Adaptative_pool enemy_pool;
        PlayerManager playerManager;
        LevelManager levelManager;
        List<GameObject> enemyGOs;
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            enemyGOs = new List<GameObject>();
            StartCoroutine(CreateEnemies());
        }
        private void Start()
        {
            playerManager = PlayerManager.GetInstance();
            levelManager = LevelManager.GetInstance();
        }
        IEnumerator CreateEnemies()
        {
            var randomTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(randomTime);
            animator.SetTrigger("OpenDoor");
        }

        public void CreateEnemyFromAnimator()
        {
            var enemyGO = enemy_pool.GetPoolEnemy();
            enemyGO.SetActive(true);
            enemyGO.transform.position = transform.position;
            enemyGOs.Add(enemyGO);
            enemyGO.GetComponentInChildren<EnemyAttackParticles>().OnVausGethitEvent += OnVausGetHitCallback;
            enemyGO.GetComponent<DamageController>().OnDestroyedEvent += OnEnemyDestroyedCallback;

            if (enemyGOs.Count < maxNumberOfEnemies)
            {
                StartCoroutine(CreateEnemies());
            }
        }
        private void OnVausGetHitCallback()
        {
            if (playerManager.GetLifes() == 0)
            {
                levelManager.GameOverScene();
            }
            playerManager.SubstractLifes();
        }
        private void OnEnemyDestroyedCallback(DamageController damageController)
        {
            playerManager.AddScore(damageController.GetScore());
        }

        internal void Reset()
        {
            foreach (var enemyGO in enemyGOs)
            {
                Destroy(enemyGO);
            }
        }
    }
}