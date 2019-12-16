using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    // Configurable Parameters
    [SerializeField] bool isRunningOnMobile = true;
    [Range(0.5f, 4)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] Image[] hearts = null;

    // Setup Variables
    int playerHealthMax = 6;
    int playerHealthCurrent = 6;
    Vector3 accelerationDirection;
    bool motion1 = false;
    bool motion2 = false;
    bool motion3 = false;
    float motionCooldownTime = 0.3f;
    float timeSinceLastDetectedMotion = 0;

    private void Awake()
    {
        //gameSpeed
        //pointsPerBlock
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            DestroyGameSession();
            GameSession gameSession = FindObjectOfType<GameSession>();
            gameSession.gameSpeed = gameSpeed;
            gameSession.isRunningOnMobile = isRunningOnMobile;
            gameSession.playerHealthMax = playerHealthMax;
            gameSession.playerHealthCurrent = playerHealthCurrent;
        }
        else
        {
            Player player = FindObjectOfType<Player>();
            playerHealthMax = player.healthMax;
            playerHealthCurrent = player.healthCurrent;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        
        if (isRunningOnMobile)
        {
            timeSinceLastDetectedMotion += Time.deltaTime;

            accelerationDirection = Input.acceleration;
        }
    }

    public bool IsRunningOnMobile()
    {
        return isRunningOnMobile;
    }

    public bool IsMotion1Detected()
    {
        if (!(timeSinceLastDetectedMotion >= motionCooldownTime))
        {
            return motion1;
        }
        else
        {
            if (accelerationDirection.sqrMagnitude >= 5f)
            {
                timeSinceLastDetectedMotion = 0;
                motion1 = true;
            }
            else
            {
                motion1 = false;
            }
        }
        return motion1;
    }

    public bool IsMotion2Detected()
    {
        if (!(timeSinceLastDetectedMotion >= motionCooldownTime))
        {
            return motion2;
        }
        else
        {
            if (accelerationDirection.x <= -0.6 )
            {
                timeSinceLastDetectedMotion = 0;
                motion2 = true;
            }
            else
            {
                motion2 = false;
            }
        }
        return motion2;
    }

    public bool IsMotion3Detected()
    {
        if (!(timeSinceLastDetectedMotion >= motionCooldownTime))
        {
            return motion3;
        }
        else
        {
            if (accelerationDirection.x >= 0.6)
            {
                timeSinceLastDetectedMotion = 0;
                motion3 = true;
            }
            else
            {
                motion3 = false;
            }
        }
        return motion3;
    }

    public void UpdatePlayerHealth(int healthMax, int healthCurrent)
    {
        playerHealthMax = healthMax;
        playerHealthCurrent = healthCurrent;
        for (int i = 0; i < healthMax; i++)
        {
            if (i < healthCurrent)
            {
                hearts[i].color = new Color(hearts[i].color.r, hearts[i].color.g, hearts[i].color.b, 1f);
            }
            else
            {
                hearts[i].color = new Color(hearts[i].color.r, hearts[i].color.g, hearts[i].color.b, 0.3f);
            }
        }
    }

    public void DestroyGameSession()
    {
        Destroy(gameObject);
    }
}
