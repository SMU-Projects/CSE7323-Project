using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Configurable Parameters
    [Header("Door GameObjects")]
    [SerializeField] GameObject topWallObject = null;
    [SerializeField] GameObject leftWallObject = null;
    [SerializeField] GameObject rightWallObject = null;
    [SerializeField] GameObject lefDoorObject = null;
    [SerializeField] GameObject rightDoorObject = null;

    [Header("Door Materials")]
    [SerializeField] Material inactiveWallMaterial = null;
    [SerializeField] Material inactiveDoorMaterial = null;

    // Setup Variables
    public bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        if(!isActive)
        {
            topWallObject.GetComponent<MeshRenderer>().material = inactiveWallMaterial;
            leftWallObject.GetComponent<MeshRenderer>().material = inactiveWallMaterial;
            rightWallObject.GetComponent<MeshRenderer>().material = inactiveWallMaterial;

            lefDoorObject.GetComponent<MeshRenderer>().material = inactiveDoorMaterial;
            rightDoorObject.GetComponent<MeshRenderer>().material = inactiveDoorMaterial;
        }
        else
        {
            OpenDoor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        if(isActive)
        {
            lefDoorObject.transform.localPosition = new Vector3(-1.25f, -0.25f, 0f);
            rightDoorObject.transform.localPosition = new Vector3(1.25f, -0.25f, 0f);
        }
    }

    public void CloseDoor()
    {
        if (isActive)
        {
            lefDoorObject.transform.localPosition = new Vector3(-0.4166f, -0.25f, 0f);
            rightDoorObject.transform.localPosition = new Vector3(0.4166f, -0.25f, 0f);
        }
    }
}
