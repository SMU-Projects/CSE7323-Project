using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // Configurable Parameters
    [SerializeField] bool isRunningOnMobile = true;
    [Range(0.5f, 4)] [SerializeField] float gameSpeed = 1f;

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
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void DestroyGameSession()
    {
        Destroy(gameObject);
    }
}
