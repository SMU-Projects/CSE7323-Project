using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    // Configurable Parameters
    [Header("Sprite")]
    [SerializeField] Sprite[] georgeSprites = null;

    // Setup Variables
    GameSession gameSession;
    Joystick joystick;
    Joybutton joybutton;
    
    bool isRunningOnMobile;


    // Start is called before the first frame update
    private new void Start()
    {
        // Run Base Character Start
        base.Start();

        // Fetch Game Session
        gameSession = FindObjectOfType<GameSession>();
        isRunningOnMobile = gameSession.IsRunningOnMobile();

        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();

        Move();
        if(joybutton.IsPressed() || Input.GetButton("Attack"))
        {
            Attack();
        }
    }

    private void Move()
    {
        float xVelocity;
        float yVelocity;
        float zVelocity;

        if (isRunningOnMobile)
        {
            xVelocity = joystick.Horizontal * moveSpeed;
            yVelocity = myRigidbody.velocity.y;
            zVelocity = joystick.Vertical * moveSpeed;
        }
        else
        {
            xVelocity = Input.GetAxis("Horizontal") * moveSpeed;
            yVelocity = myRigidbody.velocity.y;
            zVelocity = Input.GetAxis("Vertical") * moveSpeed;
        }

        myRigidbody.velocity = new Vector3(xVelocity, yVelocity, zVelocity);
        Vector3 moveDirection = myRigidbody.velocity.normalized;
        if (moveDirection != Vector3.zero)
        {
            attackAoe.transform.localPosition = attackRange * moveDirection + attackAoeSphere.radius * moveDirection;
            ChangeSprite(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (isAttacking)
        {
            Enemy enemy = collision.gameObject.GetComponent(typeof(Enemy)) as Enemy;
            if (enemy)
            {
                enemy.TakeDamage(attackPower, attackKnockback, transform.position);
            }
        }
    }

    public void ChangeSprite(float x, float z)
    {
        if (x == 0 && z < 0)
            mySpriteRenderer.sprite = georgeSprites[0];
        else if (x > 0 && z < 0)
            mySpriteRenderer.sprite = georgeSprites[1];
        else if (x > 0 && z == 0)
            mySpriteRenderer.sprite = georgeSprites[2];
        else if (x > 0 && z > 0)
            mySpriteRenderer.sprite = georgeSprites[3];
        else if (x == 0 && z > 0)
            mySpriteRenderer.sprite = georgeSprites[4];
        else if (x < 0 && z > 0)
            mySpriteRenderer.sprite = georgeSprites[5];
        else if (x < 0 && z == 0)
            mySpriteRenderer.sprite = georgeSprites[6];
        else if (x < 0 && z < 0)
            mySpriteRenderer.sprite = georgeSprites[7];
        mySpriteRenderer.size = new Vector2(2.3f, 1f);
    }

    public void UpdateGameSessionHealth()
    {
        gameSession.SetPlayerHealth(healthCurrent);
    }

    override public void Death()
    {
        SceneLoader sl = new SceneLoader();
        sl.LoadStartScene();
    }
}
