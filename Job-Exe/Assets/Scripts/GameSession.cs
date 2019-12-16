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
    [SerializeField] Text xText, yText, zText;

    // Setup Variables
    int playerHealth = 0;
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
            gameSession.playerHealth = playerHealth;
        }
        else
        {
            Player player = FindObjectOfType<Player>();
            playerHealth = player.GetCurrentHealth();
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
            xText.text = "X: " + accelerationDirection.x.ToString();
            yText.text = "Y: " + accelerationDirection.y.ToString();
            zText.text = "Z: " + accelerationDirection.z.ToString();
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

    public void SetPlayerHealth(int health)
    {
        playerHealth = health;
        int numberOfHeartsToDelete = hearts.Length - playerHealth;
        for(int i = 1; i <= numberOfHeartsToDelete; i++)
        {
            Destroy(hearts[hearts.Length-i]);
        }
    }

    public void DestroyGameSession()
    {
        Destroy(gameObject);
    }
}
