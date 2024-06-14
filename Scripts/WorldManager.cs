using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace Scripts.Arkanoid
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] VausController vausController;
        PlayerManager playerManager;
        [SerializeField] Adaptative_pool enemyPool;
        [SerializeField] BallsManager ballsManager;
        LevelManager levelManager;
        


        void Awake()
        {
            VausSetUp();
        }



        private void Start()
        {
            playerManager = PlayerManager.GetInstance();
            levelManager = LevelManager.GetInstance();
        }


        void VausSetUp()
        {
            vausController.OnReleaseBallEvent += OnBallReleaseCallBack;
        }

        internal void OnPowerUpActivatedCallBack(PowerUpType powerUpTypes)
        {

            switch (powerUpTypes)
            {
                case PowerUpType.Player:
                    playerManager.AddLifes();
                    break;
                case PowerUpType.Enlarged:
                    vausController.Enlarge();
                    break;
                case PowerUpType.Laser:
                    vausController.Laser();
                    Shooting shootingScript = vausController.GetComponent<Shooting>();
                    shootingScript.enabled = true;
                    ballsManager.SetCatch(false);

                    break;
                case PowerUpType.Catch:
                    vausController.Catch();
                    ballsManager.SetCatch(true);
                    break;
                case PowerUpType.Disruption:
                    ballsManager.Disruption();

                    break;
                case PowerUpType.Break:
                    break;
                case PowerUpType.Slow:
                    break;

            }
        }




        void OnBallReleaseCallBack()
        {
            ballsManager.Release();

        }
        
        


     
        internal void OnVausGetHitCallback()
        {
            if (playerManager.GetLifes() == 0)
            {
                //levelManager.GameOverScene();
            }
            playerManager.SubstractLifes();
        }

        internal void SubstractLife()
        {

            playerManager.SubstractLifes();
            vausController.resetVaus();
        }

        internal void AddScore(int value)
        {
            playerManager.AddScore(value);
        }

        internal void NextLevel()
        {
            levelManager.NextLevel();
        }
    }
}

