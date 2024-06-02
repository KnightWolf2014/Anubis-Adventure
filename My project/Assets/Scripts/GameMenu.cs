using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void continueGame()
    {

        PlayerManager.menu = false;
        CynoController.menuBool = false;
        FindFirstObjectByType<AudioManager>().playSound("Page");
        FindFirstObjectByType<AudioManager>().playSound("Running");

    }

    public void replay()
    {
        FindFirstObjectByType<AudioManager>().playSound("Page");
        FindFirstObjectByType<AudioManager>().stopSound("MainThemeGame");
        FindFirstObjectByType<AudioManager>().playSound("MainThemeGame");
        SceneManager.LoadScene("SampleScene");
    }

    public void quit()
    {
        FindFirstObjectByType<AudioManager>().playSound("Page");
        AudioManager.song = 0;
        SceneManager.LoadScene("Menu");
    }
}
