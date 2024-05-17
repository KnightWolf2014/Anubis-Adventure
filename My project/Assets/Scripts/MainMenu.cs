using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGame()
    {
        AudioManager.song = 1;
        FindFirstObjectByType<AudioManager>().playSound("Page");
        SceneManager.LoadScene("SampleScene");
    }

    public void loadHowToPlay()
    {
        FindFirstObjectByType<AudioManager>().playSound("Page");
        AudioManager.song = 0;
        SceneManager.LoadScene("HowToPlay");
    }

    public void loadMainMenu()
    {
        FindFirstObjectByType<AudioManager>().playSound("Page");
        AudioManager.song = 0;
        SceneManager.LoadScene("Menu");
    }

    public void loadCredits()
    {
        FindFirstObjectByType<AudioManager>().playSound("Page");
        AudioManager.song = 0;
        SceneManager.LoadScene("Credits");
    }

    public void quitGame()
    {
        FindFirstObjectByType<AudioManager>().playSound("Page");
        Application.Quit(); 
    }
}
