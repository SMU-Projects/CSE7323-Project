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
    int playerHealth = 0;
    //Canvas canvas;

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
    }

    public bool IsRunningOnMobile()
    {
        return isRunningOnMobile;
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
