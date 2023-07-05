using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void PlayButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
