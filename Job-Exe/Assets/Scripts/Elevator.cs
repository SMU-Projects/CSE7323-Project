using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    // Configurable Setup Parameters
    [SerializeField] GameObject elevatorDoorObject = null;
    [SerializeField] Material openedElevator = null;

    // Setup Variables
    protected bool isElevatorOpen = false;

    public void OpenElevator()
    {
        isElevatorOpen = true;
        elevatorDoorObject.GetComponent<MeshRenderer>().material = openedElevator;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent(typeof(Player)) as Player;
        if (isElevatorOpen && player)
        {
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            sceneLoader.LoadNextScene();
        }
            
    }
}
