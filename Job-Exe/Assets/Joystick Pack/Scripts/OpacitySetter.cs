using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpacitySetter : MonoBehaviour
{
    // Setup Variables
    bool isRunningOnMobile;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
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

    // Update is called once per frame
    void Update()
    {
        Motion1();
        Motion2();
        Motion3();
    }

    private void Motion1()
    {
        if (Input.GetButton("Motion1"))
        {
            image.color = new Color(1, 0, 0, 0.3f);
        }
    }
    private void Motion2()
    {
        if (Input.GetButton("Motion2"))
        {
            image.color = new Color(0, 1, 0, 0.3f);
        }
    }
    private void Motion3()
    {
        if (Input.GetButton("Motion3"))
        {
            image.color = new Color(0, 0, 1, 0.3f);
        }
    }
}
