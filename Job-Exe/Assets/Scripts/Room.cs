using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Configurable Setup Parameters
    [Header("Architecture")]
    [SerializeField] GameObject ceiling = null;
    [SerializeField] Door bottomDoor = null;
    [SerializeField] Door rightDoor = null;
    [SerializeField] Door topDoor = null;
    [SerializeField] Door leftDoor = null;

    [Header("Other")]
    [SerializeField] Spawner[] spawners = null;

    // Setup Variables
    public bool isTopBorder = false;
    public bool isBottomBorder = false;
    public bool isRightBorder = false;
    public bool isLeftBorder = false;
    protected bool hasPlayerEntered = false;
    List<Enemy> enemies;

    private void Awake()
    {
        enemies = new List<Enemy>();
    }

    // Start is called before the first frame update
    void Start()
    {
        topDoor.isActive = !isTopBorder;
        bottomDoor.isActive = !isBottomBorder;
        rightDoor.isActive = !isRightBorder;
        leftDoor.isActive = !isLeftBorder;
    }

    // Update is called once per frame
    void Update()
    {
        RoomCheck();
    }

    private void RoomCheck()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].IsDead())
            {
                enemies.RemoveAt(i);
            }
        }
        if (enemies.Count == 0)
        {
            bottomDoor.OpenDoor();
            topDoor.OpenDoor();
            leftDoor.OpenDoor();
            rightDoor.OpenDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayerEntered)
        {
            Player player = other.gameObject.GetComponent(typeof(Player)) as Player;
            if (player)
            {
                Destroy(ceiling);

                // close doors
                topDoor.CloseDoor();
                rightDoor.CloseDoor();
                bottomDoor.CloseDoor();
                leftDoor.CloseDoor();

                // Spawn enemies
                for(int i = 0; i < spawners.Length; i++)
                {
                    GameObject enemyPrefab = spawners[i].Spawn();
                    GameObject enemyGameObject = Instantiate(enemyPrefab, spawners[i].transform.position, spawners[i].transform.rotation) as GameObject;
                    Enemy enemy = enemyGameObject.GetComponent<Enemy>();
                    enemies.Add(enemy);
                }
            }
        }
        hasPlayerEntered = true;
    }
}
