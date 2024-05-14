using UnityEngine;
using UnityEngine.UI;


public class PointsManager : MonoBehaviour
{
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        float horizontalPercentage = 0.2f;
        float verticalPercentage = 0.1f;
        float percentageFromTop = 0.15f;

        float horizontalOffset = (1f - horizontalPercentage) / 2f;
        float verticalOffset = (1f - verticalPercentage) / 2f;
        float yPos = Screen.height * (1f - percentageFromTop) / 2f;

        rectTransform.anchorMin = new Vector2(horizontalOffset, verticalOffset);
        rectTransform.anchorMax = new Vector2(1f - horizontalOffset, 1f - verticalOffset);
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = new Vector2(0f, yPos);



        Text textComponent = GetComponent<Text>();

        float fontSizePercentage = 0.04f;
        int fontSize = (int)(Screen.height * fontSizePercentage);
        textComponent.fontSize = fontSize;

    }

}