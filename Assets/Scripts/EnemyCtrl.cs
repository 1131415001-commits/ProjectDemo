

using UnityEngine;
using System.Collections;

public class EnemyCtrl : MonoBehaviour
{
    // The distance the enemy can see the player
    public float searchRange = 10f;
    // The distance the enemy can attack the player
    public float attackRange = 2f;
    public float attackCooldown = 2f; // Time between attacks

    private CharacterController controller;
    private Animator animator;
    private Transform player;
    private float nextAttackTime = 0f; // Timer for attack cooldown


    // Use this for initialization
    void Start()
    {
        // Get the CharacterController on this object
        controller = GetComponent<CharacterController>();

        // Get the Animator component from any child object
        animator = GetComponentInChildren<Animator>();

        // Find the player object by its tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Please make sure the player has the 'Player' tag.");
        }

        if (animator == null)
        {
            Debug.LogError("Animator not found on any child object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || controller == null)
        {
            // Stop execution if essential components are missing
            return;
        }

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Player is in attack range
            Attack();
        }
        else if (distanceToPlayer <= searchRange)
        {
            // Player is in search range
            Chase();
        }
        else
        {
            // Player is out of range
            Idle();
        }
    }

    void Idle()
    {
        if (animator != null)
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttacking", false);
        }
    }

    void Chase()
    {
        // Look at the player
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Keep the enemy upright
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        // Move towards the player
        if (direction.magnitude > attackRange)
        {
            controller.Move(direction.normalized * 3.0f * Time.deltaTime); // Using a sample speed of 3.0f
            if (animator != null)
            {
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsAttacking", false);
            }
        }
    }

    void Attack()
    {
        // Look at the player
        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);

        // Trigger attack animation
        if (animator != null)
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttacking", true);
        }

        // Check if it's time to attack again
        if (Time.time >= nextAttackTime)
        {
            // Calculate damage (10% of max HP)
            int damage = (int)(GameData.hpMax * 0.1f);
            GameData.hp -= damage;

            Debug.Log($"Enemy attacked Player, dealing {damage} damage. Player HP is now {GameData.hp}");

            // Set the next attack time
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
