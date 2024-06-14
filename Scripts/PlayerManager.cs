using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Scripts.Arkanoid
{
    public class PlayerManager : MonoBehaviour
    {
        int score;
        [SerializeField] HUDManager hudManager;
        [SerializeField] RectTransform rectTransform;
        int maxLifes = 5;
        LevelManager levelManager;

        //Singleton
        static PlayerManager instance;
        ScoreManager scoreManager;
        public static PlayerManager GetInstance()
        {
            return instance;
        }
        private int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                //actualizo el hud
                hudManager.UpdateScore(score);

            }
        }

        int lifes;
        int Lifes
        {
            get { return lifes; }
            set
            {
                lifes = value;
                UpdateLifes(value);
            }
        }
        float lifeSpriteWidth;

        private void Awake()
        {
            if(instance== null) { instance= this;}
            else { Destroy(gameObject); }
            DontDestroyOnLoad(gameObject);
            lifeSpriteWidth = rectTransform.sizeDelta.x;
            Score = 0;
            Lifes = 3;
        }
        private void Start()
        {
            scoreManager = ScoreManager.GetInstance();
            levelManager = LevelManager.GetInstance();
            hudManager = HUDManager.GetInstance();

            Score = 0;
            Lifes = lifes;
        }
        public void AddScore(int scoreToAdd)
        {
            Score += scoreToAdd;
        }
        internal void UpdateLifes(int lifes)
        {
            rectTransform.sizeDelta = new Vector2(lifeSpriteWidth * lifes, rectTransform.sizeDelta.y);
        }

        internal void SubstractLifes()
        {
            Lifes--;
            if(Lifes < 0) 
            {
                scoreManager.UpdateHighScore(score);
                levelManager.GameOverScene();
            }

        }
        internal void AddLifes()
        {
            if(Lifes <= maxLifes)
            {
                Lifes++;
            }
        }
        internal int GetLifes()
        {
            return lifes;
        }
    }
}

