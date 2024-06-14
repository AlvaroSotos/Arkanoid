using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace Scripts.Arkanoid
{
    public class WorldManagerForBricks : WorldManagerTop
    {

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            playerManager = PlayerManager.GetInstance();
            levelManager = LevelManager.GetInstance();
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
                    ballsManager.Slow();
                    break;

            }
        }


    }
}

