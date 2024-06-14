using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayCallback);
        quitButton.onClick.AddListener(QuitCallback);
    }

    void PlayCallback()
    {
        Debug.Log("PlayCallback");
        SceneManager.LoadScene("Intro");
    }
    void QuitCallback()
    {
        quitButton.onClick.RemoveListener(QuitCallback);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
