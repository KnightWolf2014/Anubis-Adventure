using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoManager : MonoBehaviour
{
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        float horizontalPercentage = 0.7f;
        float verticalPercentage = 0.3f;
        float percentageFromTop = 0.4f;

        float horizontalOffset = (1f - horizontalPercentage) / 2f;
        float verticalOffset = (1f - verticalPercentage) / 2f;
        float yPos = Screen.height * (1f - percentageFromTop) / 2f;

        rectTransform.anchorMin = new Vector2(horizontalOffset, verticalOffset);
        rectTransform.anchorMax = new Vector2(1f - horizontalOffset, 1f - verticalOffset);
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = new Vector2(0f, yPos);

    }
}
