using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public static int numberOfCoins;
    public static int numberOfMeters;
    public static int numberOfPoints;
    public Text coinsText;
    public Text metersText;
    public Text pointsText;

    // Start is called before the first frame update
    void Start()
    {

        numberOfCoins = 0;
        numberOfMeters = 0;
        numberOfPoints = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        metersText.text = numberOfMeters + "m";
        coinsText.text = "Coins: " + numberOfCoins;
        pointsText.text = "" + numberOfPoints;
    }
}
