using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public static bool menu;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private  GameObject menuPanelGameOvew;

    public static int numberOfCoins;
    public static int numberOfMeters;
    public static int numberOfPoints;

    [SerializeField] private Text coinsText;
    [SerializeField] private Text metersText;
    [SerializeField] private Text pointsText;

    [SerializeField] private Text coinsTextGO;
    [SerializeField] private Text metersTextGO;
    [SerializeField] private Text pointsTextGO;

    [SerializeField] private Text godModeText;

    public static bool gameOver;
    public static bool godmode; 

    // Start is called before the first frame update
    void Awake()
    {
        menu = false;
        gameOver = false;
        godmode = false;

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

        if (Input.GetKeyDown(KeyCode.G)) {
            if (!godmode) godmode = true;
            else godmode = false;    
        }

        if (godmode) godModeText.enabled = true;
        else godModeText.enabled = false;   

    }
    IEnumerator GameOver() {
        yield return new WaitForSecondsRealtime(1.1f);

        Time.timeScale = 0;
        Cursor.visible = true;
        menuPanelGameOvew.SetActive(true);
    }


}
