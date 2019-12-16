using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Configurable Parameters
    [Header("Attack")]
    [SerializeField] public GameObject attackAoe;
    [SerializeField] public float attackCooldownTime = 1f;
    [SerializeField] public float attackDuration = 0.1f;
    [SerializeField] public int attackPower = 2;
    [SerializeField] public float attackKnockback = 2f;
    [SerializeField] public float attackRange = 0.8f;
    
    [Header("Health")]
    [SerializeField] public int healthMax = 6;
    [SerializeField] public int healthCurrent = 6;
    [SerializeField] public float invincibleCooldownTime = 1f;

    [Header("Other")]
    [SerializeField] public float moveSpeed = 7f;

    // Setup Variables
    protected SpriteRenderer mySpriteRenderer;
    protected Rigidbody myRigidbody;
    protected SphereCollider attackAoeSphere;
    protected bool isAttacking;
    protected bool isDead = false;
    protected float timeSinceLastAttack = 0;
    protected float timeSinceDamaged = 0;

    // Start is called before the first frame update
    protected void Start()
    {
        // Get Core Components
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody>();

        // Setup Attack Range
        attackAoeSphere = attackAoe.GetComponent<SphereCollider>();
        attackAoeSphere.radius = attackRange;
    }

    protected void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        timeSinceDamaged += Time.deltaTime;
        if (isAttacking && timeSinceLastAttack >= attackDuration)
        {
            isAttacking = false;
        }
    }

    protected void Attack()
    {
        if (timeSinceLastAttack >= attackCooldownTime)
        {
            isAttacking = true;
            timeSinceLastAttack = 0;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(int damageAmount, float knockback, Vector3 fromPosition)
    {
        if(timeSinceDamaged >= invincibleCooldownTime)
        {
            healthCurrent -= damageAmount;
            if (healthCurrent <= 0)
            {
                Death();
                knockback /= 2;
            }
            Player player = this as Player;
            if(this as Player)
            {
                player.UpdateGameSessionHealth();
            }
            timeSinceDamaged = 0;
            Vector3 knockbackDirection = transform.position - fromPosition;
            knockbackDirection.y = 0f;
            myRigidbody.AddForce(knockbackDirection.normalized * knockback * 1000f);
        }
    }

    virtual public void Death()
    {
        isDead = true;
        Destroy(attackAoe);
        enabled = false;

        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;

        transform.Rotate(90, 0, 0);

        myRigidbody.constraints = RigidbodyConstraints.None;
        myRigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        myRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;
        

    }
}
