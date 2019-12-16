using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Configurable Parameters
    [SerializeField] GameObject[] rooms = null;
    [SerializeField] GameObject player = null;

    // Setup Variables
    AudioSource myAudioSource;
    int numberOfRows = 3;
    int numberOfCols = 4;

    private void Awake()
    {
        int elevatorRoomIndex = Random.Range(0, (numberOfRows * numberOfCols) - 1);

        for (int r = 0; r < numberOfRows; r++)
        {
            for (int c = 0; c < numberOfCols; c++)
            {
                int index = Random.Range(0, rooms.Length);
                int chanceForPowerup = Random.Range(0, 3);

                Vector3 roomPosition = new Vector3(25 * c, 0, -25 * r);
                GameObject roomGameObject = Instantiate(rooms[index], roomPosition, Quaternion.identity) as GameObject;
                Room room = roomGameObject.GetComponent<Room>();

                if ((r * numberOfCols + c) == elevatorRoomIndex)
                    room.isElevatorRoom = true;
                //if (chanceForPowerup == 0)
                    room.isPowerupRoom = true;
                if (c == 0)
                    room.isLeftBorder = true;
                if (c == numberOfCols - 1)
                    room.isRightBorder = true;
                if (r == 0)
                    room.isTopBorder = true;
                if (r == numberOfRows - 1)
                    room.isBottomBorder = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        int startRow = (int)Mathf.Round(Random.Range(0f, numberOfRows - 1));
        int startCol = (int)Mathf.Round(Random.Range(0f, numberOfCols - 1));
        player.transform.position = new Vector3(25 * startCol, 2, -25 * startRow);
        PlayMusic();
    }

    void PlayMusic()
    {
        myAudioSource.PlayOneShot(myAudioSource.clip);
    }
}  
