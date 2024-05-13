using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGame()
    {
        AudioManager.song = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void loadHowToPlay()
    {
        AudioManager.song = 0;
        SceneManager.LoadScene("HowToPlay");
    }

    public void loadMainMenu()
    {
        AudioManager.song = 0;
        SceneManager.LoadScene("Menu");
    }

    public void loadCredits()
    {
        AudioManager.song = 0;
        SceneManager.LoadScene("Credits");
    }

    public void quitGame()
    {
        Application.Quit(); 
    }
}
