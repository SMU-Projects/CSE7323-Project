using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Configurable Parameters
    [Header("Powerup Sprites")]
    [SerializeField] Sprite bat = null;
    [SerializeField] Sprite coffee = null;
    [SerializeField] Sprite donut = null;
    [SerializeField] Sprite gloves = null;
    [SerializeField] Sprite heart = null;
    [SerializeField] Sprite shoe = null;
    [SerializeField] Sprite spinach = null;
    [SerializeField] Sprite stapler = null;
    [SerializeField] Sprite watch = null;

    // Setup Variables
    GameSession gameSession;
    protected SpriteRenderer mySpriteRenderer;
    int powerupType;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        powerupType = Random.Range(1, 10);
        switch (powerupType)
        {
            case 1: // Bat
                mySpriteRenderer.sprite = bat;
                break;
            case 2: // Coffee
                mySpriteRenderer.sprite = coffee;
                break;
            case 3: // Donut of Rage
                mySpriteRenderer.sprite = donut;
                break;
            case 4: // Gloves
                mySpriteRenderer.sprite = gloves;
                break;
            case 5: // Heart
                mySpriteRenderer.sprite = heart;
                break;
            case 6: // Shoe
                mySpriteRenderer.sprite = shoe;
                break;
            case 7: // Can O'Spinach
                mySpriteRenderer.sprite = spinach;
                break;
            case 8: // Stapler
                mySpriteRenderer.sprite = stapler;
                break;
            case 9: // Watch
                mySpriteRenderer.sprite = watch;
                break;
            default:
                Debug.Log("Error: Index Out of Bounds");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent(typeof(Player)) as Player;
        if (player)
        {
            switch (powerupType)
            {
                case 1: // Bat
                    player.PickedUpBat();
                    break;
                case 2: // Coffee
                    if (player.healthCurrent < player.healthMax)
                    {
                        player.healthCurrent += 1;
                        gameSession.UpdatePlayerHealth(player.healthMax, player.healthCurrent);
                    }
                    break;
                case 3: // Donut of Rage
                    player.attackKnockback += 3;
                    break;
                case 4: // Gloves
                    player.attackRange += 0.2f;
                    player.attackAoe.GetComponent<SphereCollider>().radius += 0.2f;
                    break;
                case 5: // Heart
                    if (player.healthCurrent < player.healthMax)
                    {
                        player.healthCurrent += 1;
                        gameSession.UpdatePlayerHealth(player.healthMax, player.healthCurrent);
                    }
                    break;
                case 6: // Shoe
                    player.moveSpeed += 2; player.attackCooldownTime -= 0.1f;
                    break;
                case 7: // Can O'Spinach
                    player.attackPower += 1;
                    break;
                case 8: // Stapler
                    player.PickedUpStapler();
                    break;
                case 9: // Watch
                    player.attackCooldownTime -= 0.1f;
                    break;
                default:
                    Debug.Log("Error: Index Out of Bounds");
                    break;
            }
        }

        Destroy(gameObject);
    }
}
