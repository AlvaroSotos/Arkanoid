using System;
using UnityEngine;

namespace Scripts.Arkanoid
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshPro tmp_Score;
        [SerializeField] TMPro.TextMeshPro tmp_Highscore;
        [SerializeField] TMPro.TextMeshPro tmp_Round;
        static HUDManager instance;
        public static HUDManager GetInstance()
        {
            return instance;
        }


        private void Awake()
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }
            DontDestroyOnLoad(gameObject);
        }
        public void UpdateScore(int score)
        {
            tmp_Score.text = score.ToString();
        }

        internal void UpdateHighScore(int highscore)
        {
            tmp_Highscore.text = highscore.ToString();
        }

        internal void ShowText(int roundId)
        {
            tmp_Round.text = "Round " + roundId.ToString();
        }

        internal void HideText()
        {
            tmp_Round.gameObject.SetActive(false);
        }
    }
}

