using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpacitySetter : MonoBehaviour
{
    // Setup Variables
    bool isRunningOnMobile;
    Image image;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        isRunningOnMobile = gameSession.IsRunningOnMobile();

        image = GetComponent<Image>();
        if (isRunningOnMobile)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.3f);
        }
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
    }
}
