using System;
using UnityEngine;

namespace Scripts.Arkanoid
{
    public abstract class WorldManagerTop : MonoBehaviour
    {
        //protected para poder ser utilizado por los hijos
        //virtual para poder ser modificado por los hijos
        [SerializeField] protected VausController vausController;
        [SerializeField] protected BallsManager ballsManager;
        protected LevelManager levelManager;
        protected PlayerManager playerManager;

        protected virtual void Awake()
        {
            VausSetUp();
        }
        void Start()
        {
            playerManager = PlayerManager.GetInstance();
            levelManager = LevelManager.GetInstance();
        }
        void VausSetUp()
        {
            vausController.OnReleaseBallEvent += OnBallReleaseCallBack;
            vausController.OnVausDestroyedEvent += OnVausDestroyedCallback;
        }

        private void OnVausDestroyedCallback()
        {
            playerManager.SubstractLifes();
        }

        void OnBallReleaseCallBack()
        {
            ballsManager.Release();
        }
        internal void OnVausGetHitCallback()
        {

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

