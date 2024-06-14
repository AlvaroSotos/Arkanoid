using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Arkanoid
{
    public class LevelManager : MonoBehaviour
    {
        int roundId;
        int roundCounter;
        //Singleton
        static LevelManager instance;
        AudioSource audioSource;
        HUDManager hudManager;
        public static LevelManager GetInstance()
        {
            return instance;
        }
        private void Awake()
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }

            audioSource= GetComponent<AudioSource>();
            CalculateRoundId();
            CalculateRoundNumber();
        }

        private void CalculateRoundNumber()
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string sceneName = SceneUtility.GetScenePathByBuildIndex(i);
                if (sceneName.Contains("Round"))
                {
                    roundCounter++;
                }
            }
        }
        private void Start()
        {
            hudManager = HUDManager.GetInstance();
            StartCoroutine(StartRoundCoroutine());
        }
        IEnumerator StartRoundCoroutine()
        {
            audioSource.Play();
            hudManager.ShowText(roundId);
            yield return new WaitForSeconds(audioSource.clip.length);
            hudManager.HideText();
        }
        private void CalculateRoundId()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            string[] splits = sceneName.Split("_");

            if(splits.Length == 2)
            {
                int.TryParse(splits[1], out roundId);
               
            }
        }
        public void NextLevel()
        {
            if(roundId < roundCounter)
            {
                SceneManager.LoadScene("Round_" + (roundId + 1));
            }
            else
            {
                VictoryScene();
            }
        }
        public void VictoryScene()
        {
            SceneManager.LoadScene("Victory");
        }
        public void GameOverScene()
        {
            SceneManager.LoadScene("GameOver");

        }




    }
}

