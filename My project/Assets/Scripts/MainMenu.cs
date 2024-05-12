using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGame()
    {
        FindFirstObjectByType<AudioManager>().stopSound("MainThemeMenu");
        FindFirstObjectByType<AudioManager>().playSound("MainThemeGame");
        SceneManager.LoadScene("SampleScene");

    }

    public void loadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");

    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Menu");

    }

    public void loadCredits()
    {
        SceneManager.LoadScene("Credits");

    }

    public void quitGame()
    {
        Application.Quit(); 
    }
}
