using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    // Configurable Parameters
    [Header("Sprite")]
    [SerializeField] Sprite[] georgeSprites = null;
    [SerializeField] Sprite bat = null;
    [SerializeField] Sprite stapler = null;

    // Setup Variables
    GameSession gameSession;
    Joystick joystick;
    Joybutton joybutton;

    protected int weaponIndex = 0; 
    protected bool isHoldingBat = false;
    protected bool isHoldingStapler = false;
    protected int ammoPunch = 10;
    protected int ammoBat = 10;
    protected int ammoStapler = 10;
    protected bool isReloading = false;
    protected bool isSwitchingLeft = false;
    protected bool isSwitchingRight = false;

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

        Reload();
        SwitchWeaponLeft();
        SwitchWeaponRight();
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
            float xPos = transform.position.x + attackRange * moveDirection.x;
            float yPos = attackAoeSphere.radius;
            float zPos = transform.position.z + attackRange * moveDirection.z;
            attackAoe.transform.position = new Vector3(xPos, yPos, zPos); 

            ChangeSprite(xVelocity / (moveSpeed + 0.00001f), zVelocity / (moveSpeed + 0.00001f));
        }
    }

    override protected void Attack()
    {
        if (timeSinceLastAttack >= attackCooldownTime)
        {
            switch (weaponIndex)
            {
                case 0: // Punch
                    if (!(ammoPunch > 0))
                        return;
                    ammoPunch--;
                    gameSession.UpdatePlayerAmmo(ammoPunch);
                    break;
                case 1: // Bat
                    ammoBat--;
                    if (ammoBat == 0)
                    {
                        SwitchWeaponToPunch();
                        weaponIndex = 0;
                        isHoldingBat = false;
                        gameSession.UpdatePlayerAmmo(ammoPunch);
                        return;
                    }
                    gameSession.UpdatePlayerAmmo(ammoBat);
                    break;
                case 2: // Stapler
                    ammoStapler--;
                    if (ammoStapler == 0)
                    {
                        SwitchWeaponToPunch();
                        weaponIndex = 0;
                        isHoldingStapler = false;
                        gameSession.UpdatePlayerAmmo(ammoPunch);
                        return;
                    }
                    gameSession.UpdatePlayerAmmo(ammoStapler);
                    break;
            }
        }
        base.Attack(); // needs to be editted
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
        x = Mathf.Round(x);
        z = Mathf.Round(z);

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

    public void PickedUpBat()
    {
        weaponIndex = 1;
        isHoldingBat = true;
        ammoBat = 10;
        attackAoe.GetComponent<SpriteRenderer>().sprite = bat;
        attackAoe.GetComponent<SpriteRenderer>().size = new Vector2(2f, 0.7f);
        gameSession.UpdatePlayerAmmo(ammoBat);
    }

    public void PickedUpStapler()
    {
        weaponIndex = 2;
        isHoldingStapler = true;
        ammoStapler = 10;
        attackAoe.GetComponent<SpriteRenderer>().sprite = stapler;
        attackAoe.GetComponent<SpriteRenderer>().size = new Vector2(2.4f, 0.2f);
        gameSession.UpdatePlayerAmmo(ammoStapler);
    }

    public void UpdateGameSessionHealth()
    {
        gameSession.UpdatePlayerHealth(healthMax, healthCurrent);
    }

    override public void Death()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void Reload()
    {
        if (weaponIndex == 0 && gameSession.IsMotion1Detected())
        {
            if (!isReloading)
            {
                isReloading = true;
                if(ammoPunch < 10)
                {
                    ammoPunch += 1;
                    gameSession.UpdatePlayerAmmo(ammoPunch);
                }
            }
        }
        else
        {
            isReloading = false;
        }
    }
    private void SwitchWeaponLeft()
    {
        if (gameSession.IsMotion2Detected())
        {
            if (!isSwitchingLeft)
            {
                isSwitchingLeft = true;
                if (weaponIndex == 0)
                {
                    if (isHoldingStapler)
                        SwitchWeaponToStapler();
                    else if (isHoldingBat)
                        SwitchWeaponToBat();
                }
                else if (weaponIndex == 1)
                {
                    SwitchWeaponToPunch();
                }
                else
                {
                    if (isHoldingBat)
                        SwitchWeaponToBat();
                    else
                        SwitchWeaponToPunch();
                }
            }
        }
        else
        {
            isSwitchingLeft = false;
        }
    }
    private void SwitchWeaponRight()
    {
        if (gameSession.IsMotion3Detected())
        {
            if (!isSwitchingRight)
            {
                isSwitchingRight = true;
                if (weaponIndex == 0)
                {
                    if (isHoldingBat)
                        SwitchWeaponToBat();
                    else if (isHoldingStapler)
                        SwitchWeaponToStapler();
                }
                else if (weaponIndex == 1)
                {
                    if (isHoldingStapler)
                        SwitchWeaponToStapler();
                    else
                        SwitchWeaponToPunch();
                }
                else
                {
                    SwitchWeaponToPunch();
                }
            }
        }
        else
        {
            isSwitchingRight = false;
        }
    }

    private void SwitchWeaponToPunch()
    {
        weaponIndex = 0;
        attackAoe.GetComponent<SpriteRenderer>().sprite = null;
        gameSession.UpdatePlayerAmmo(ammoPunch);
    }

    private void SwitchWeaponToBat()
    {
        weaponIndex= 1;
        attackAoe.GetComponent<SpriteRenderer>().sprite = bat;
        attackAoe.GetComponent<SpriteRenderer>().size = new Vector2(2f, 0.7f);
        gameSession.UpdatePlayerAmmo(ammoBat);
    }

    private void SwitchWeaponToStapler()
    {
        weaponIndex = 2;
        attackAoe.GetComponent<SpriteRenderer>().sprite = stapler;
        attackAoe.GetComponent<SpriteRenderer>().size = new Vector2(2.4f, 0.2f);
        gameSession.UpdatePlayerAmmo(ammoStapler);
    }
}
