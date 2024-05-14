using UnityEngine;

public class ResizeAndFollowCamera : MonoBehaviour
{
    private RectTransform rectTransform;
    private Camera mainCamera;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;

        ResizeFrame();
    }

    void Update()
    {
        FollowCamera();
    }

    void ResizeFrame()
    {
        // Configurar el RectTransform para que ocupe toda la pantalla y est� centrado
        //rectTransform.anchorMin = Vector2.zero;
        //rectTransform.anchorMax = Vector2.one;
        //rectTransform.offsetMin = Vector2.zero;
        //rectTransform.offsetMax = Vector2.zero;

        // Centrar el marco en la posici�n de la c�mara
        rectTransform.position = mainCamera.transform.position;
    }

    void FollowCamera()
    {
        // Mover el marco junto con la c�mara
        rectTransform.position = mainCamera.transform.position;
    }
}
