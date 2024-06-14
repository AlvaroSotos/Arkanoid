using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Arkanoid
{
    public class ScoreManager : MonoBehaviour
    {
        static ScoreManager instance;
        [SerializeField] TMPro.TextMeshPro tmp_Highscore;
        HUDManager hudManager;

        int highscore;
        public static ScoreManager GetInstance()
        {
            return instance;
        }
        private void Awake()
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }

        }
        private void Start()
        {
            hudManager = HUDManager.GetInstance();
            highscore = PlayerPrefs.GetInt(Constants.HIGHSCOREARKANOIDKEY, 0);
            hudManager.UpdateHighScore(highscore);
        }
        internal void UpdateHighScore(int score)
        {
            if(score > highscore)
            {
                highscore = score;
                hudManager.UpdateHighScore(highscore);
                PlayerPrefs.SetInt(Constants.HIGHSCOREARKANOIDKEY, highscore);
            }
        }


    }
}

