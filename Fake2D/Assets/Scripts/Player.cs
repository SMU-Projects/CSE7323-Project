using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configurable Parameters
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] bool isRunningOnMobile = true;

    // Setup Variables
    Joystick joystick;
    Joybutton joybutton;
    Rigidbody myRigidbody;
    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
        myRigidbody = GetComponent<Rigidbody>();
        isRunningOnMobile = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunningOnMobile)
        {
            JoyStickMove();
        }
        else
        {
            Move();
        }
        
        Attack();
    }

    private void Move()
    {
        float xVelocity = Input.GetAxis("Horizontal") * moveSpeed;
        float yVelocity = myRigidbody.velocity.y;
        float zVelocity = Input.GetAxis("Vertical") * moveSpeed;

        myRigidbody.velocity = new Vector3(xVelocity, yVelocity, zVelocity);
    }

    private void JoyStickMove()
    {
        float xVelocity = joystick.Horizontal * moveSpeed;
        float yVelocity = myRigidbody.velocity.y;
        float zVelocity = joystick.Vertical * moveSpeed;

        myRigidbody.velocity = new Vector3(xVelocity, yVelocity, zVelocity);
    }

    private void Attack()
    {
        if (!isAttacking && (joybutton.IsPressed() || Input.GetButton("Fire2")))
        {
            isAttacking = true;
            Debug.Log("Attacking!");
        }
        else
        {
            isAttacking = false;
        }
    }
}
