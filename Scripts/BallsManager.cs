using AimClicker.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Arkanoid
{
    [RequireComponent(typeof(WorldManagerTop))]
    public class BallsManager : MonoBehaviour
    {
        //[SerializeField] GameObject ballPrefab;
        [SerializeField] BallController ballController;
        public List<BallController> ballControllerList = new List<BallController>();
        WorldManagerTop worldManager;
        PlayerManager playerManager;
        LevelManager levelManager;
        public int ballCounter = 1;
        Rigidbody2D ballRB;


        private void Awake()
        {
            worldManager = GetComponent<WorldManagerTop>();
            BallSetUp();
            ballRB = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            playerManager = PlayerManager.GetInstance();
            levelManager = LevelManager.GetInstance();  
        }

        void BallSetUp()
        {
            ballControllerList.Add(ballController);
            ballController.OnBallDestroyedEvent += OnBallDestroyedEventCallBack;
        }
        void OnBallDestroyedEventCallBack(BallController ballController)
        {
            DestroyBall(ballController.gameObject);
        }

        internal void SetCatch(bool value)
        {
            foreach (var ballController in ballControllerList)
            {
                ballController.SetCatch(value);
            }
        }

        internal void Disruption()
        {
            if (ballCounter == 1)
            {
                SpawnBall();
                SpawnBall();
            }
            else if (ballCounter == 2)
            {
                SpawnBall();
            }
        }
        void SpawnBall()
        {
            GameObject ballGO = Instantiate(ballController.gameObject);
            ballGO.SetActive(true);
            BallController newBallController = ballGO.GetComponent<BallController>();

            ballGO.transform.position = ballController.transform.position;
            ballGO.transform.SetParent(ballController.transform.parent);
            //newBallController.OnBallDestroyedEvent += OnBallDestroyedEventCallBack;
            ballControllerList.Add(newBallController);
            bool isCatchy = ballController.IsCatchy();
            ballController.SetCatch(isCatchy);
            //newBallController.SetDirection(Random.insideUnitCircle);
            newBallController.isFirstTime = false;
            if (!isCatchy) newBallController.Release();
            ballCounter++;
        }

        internal void DestroyBall(GameObject go)
        {
            if (ballCounter == 1)
            {
                if (playerManager.GetLifes() < 0)
                {
                    levelManager.GameOverScene();
                }
                worldManager.SubstractLife();
                ballController.gameObject.SetActive(false);
                ballController.resetBall(ballControllerList[0]);
                ballController.SetCatch(false);

            }
            else
            {
                ballController.gameObject.SetActive(false);
                ballCounter--;
                /*ballControllerList.Remove(ballController);
                ballController = ballControllerList[0];*/
            }
        }

        internal void Release()
        {
            foreach (var ballController in ballControllerList)
            {
                ballController.Release();
            }
        }
        internal void Freeze()
        {
            ballController.FreezeBall();
        }

        internal void Slow()
        {
            foreach (var ballController in ballControllerList)
            {
                ballController.Slow();
            }
        }
    }
}

