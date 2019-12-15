using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Configurable Parameters
    [SerializeField] float distanceToStopBetweenPlayer = 0f;

    // Setup Variables
    Player player;

    // Start is called before the first frame update
    private new void Start()
    {
        // Run Base Character Start
        base.Start();

        myRigidbody = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>(); // This needs to be changed in multi player
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        Move();
    }

    private void Move()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer = directionToPlayer.normalized;
        if (Vector3.Distance(player.transform.position, transform.position) > distanceToStopBetweenPlayer)
        {
            myRigidbody.velocity = directionToPlayer * moveSpeed;
        }

        attackAoe.transform.localPosition = attackRange * directionToPlayer + attackAoeSphere.radius * directionToPlayer;
    }

    private void OnTriggerStay(Collider collision)
    {
        Attack();
        if (isAttacking)
        {
            Player player = collision.gameObject.GetComponent(typeof(Player)) as Player;
            if (player)
            {
                player.TakeDamage(attackPower, attackKnockback, transform.position);
            }
        }
    }
}
