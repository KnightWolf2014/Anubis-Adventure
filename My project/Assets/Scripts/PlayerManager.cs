using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public static bool menu;
    public GameObject menuPanel;
    public GameObject menuPanelGameOvew;

    public static int numberOfCoins;
    public static int numberOfMeters;
    public static int numberOfPoints;
    public Text coinsText;
    public Text metersText;
    public Text pointsText;

    public Text coinsTextGO;
    public Text metersTextGO;
    public Text pointsTextGO;

    public static bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        menu = false;
        gameOver = false;

        numberOfCoins = 0;
        numberOfMeters = 0;
        numberOfPoints = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (menu) {
            Time.timeScale = 0;
            Cursor.visible = true;
            menuPanel.SetActive(true);

        } else if (gameOver) {
            StartCoroutine(GameOver());

        } else {
            Time.timeScale = 1;
            Cursor.visible = false;
            menuPanel.SetActive(false);

            metersText.text = numberOfMeters + "m";
            coinsText.text = numberOfCoins + "$";
            pointsText.text = numberOfPoints + " pts";

            metersTextGO.text = numberOfMeters + "m";
            coinsTextGO.text = numberOfCoins + "$";
            pointsTextGO.text = numberOfPoints + " pts";

        }
    }
    IEnumerator GameOver() {
        yield return new WaitForSecondsRealtime(1.1f);

        Time.timeScale = 0;
        Cursor.visible = true;
        menuPanelGameOvew.SetActive(true);
    }


}
