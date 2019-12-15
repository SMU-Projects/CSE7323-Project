using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Configurable Parameters
    [SerializeField] protected GameObject attackAoe;
    [SerializeField] protected float moveSpeed = 7f;
    [SerializeField] protected float attackCooldownTime = 1f;
    [SerializeField] protected float attackDuration = 0.1f;
    [SerializeField] protected float attackPower = 2f;
    [SerializeField] protected float attackKnockback = 2f;
    [SerializeField] protected float attackRange = 0.8f;
    [SerializeField] protected float healthMax = 6f;
    [SerializeField] protected float healthCurrent = 6f;

    // Setup Variables
    protected bool isAttacking;
    protected Rigidbody myRigidbody;
    protected SphereCollider attackAoeSphere;
    protected float timeSinceLastAttack = 0;

    // Start is called before the first frame update
    protected void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        // Setup Attack Range
        attackAoeSphere = attackAoe.GetComponent<SphereCollider>();
        attackAoeSphere.radius = attackRange;

    }

    protected void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if(isAttacking && timeSinceLastAttack >= attackDuration)
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

    public void TakeDamage(float damageAmount, float knockback, Vector3 fromPosition)
    {
        Debug.Log("ouch! " + knockback.ToString());

        healthCurrent -= damageAmount;

        Vector3 knockbackDirection = transform.position - fromPosition;
        //myRigidbody.velocity = knockbackDirection.normalized * knockback;

        myRigidbody.AddForce(knockbackDirection.normalized * knockback * 1000f);

        //if (currentHealth)
    }
}
