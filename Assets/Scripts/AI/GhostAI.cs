using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject ghost;
    [SerializeField] AudioSource ghostScream;
    [SerializeField] AudioSource ghostInRange;
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public float attackDamage = 50f;
    public LayerMask targetLayer;
    Transform target;
    float timeBetweenAttacks = 3;
    Rigidbody2D rb;
    bool shouldRotate = true;
    bool isDying;
    Vector2 movement;
    public Vector3 direction;
    bool isInChaseRange;
    bool isInAttackRange;
    PlayerHeartRate playerHeartRate;
    bool hasAttacked = false;
    private int attackCounter = 0;
    public int MaxHealth = 100;
    int currentHealth;
    bool chaseMusic;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        playerHeartRate = FindObjectOfType<PlayerHeartRate>();
        currentHealth = MaxHealth;

    }

    private void Update()
    {
        GhostMovement();
    }

    private void FixedUpdate()
    {
        if (isInChaseRange && !isInAttackRange)
        {
            rb.MovePosition((Vector2)transform.position + (movement * speed * Time.deltaTime));
        }
        if (isInAttackRange && !isDying)
        {
            Attack();
        }

        // if (ghost comes into contact with light...) { die }
    }

    void GhostMovement()
    {
        anim.SetBool("isMoving", isInChaseRange);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, targetLayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, targetLayer);

        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
        if (shouldRotate)
        {
            anim.SetFloat("X", direction.x);
            anim.SetFloat("Y", direction.y);
        }
        if (isInChaseRange && !chaseMusic && !isDying)
        {
            ghostInRange.Play();
            chaseMusic = true;
        }
        else if (!isInChaseRange || isDying)
        {
            ghostInRange.Stop();
            chaseMusic = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GhostDie();
        }
    }

    private void Attack()
    {
        // Check if the attack has already been called
        if (!hasAttacked)
        {

            ghostScream.Play();
            // Stop the enemy's movement
            rb.velocity = Vector2.zero;

            // Play the attack animation
            anim.SetTrigger("Attack");

            // Increment the attack counter
            attackCounter++;

            // Set the hasAttacked flag to true
            hasAttacked = true;

            // Start checking if the player is in range at regular intervals
            StartCoroutine(CheckPlayerRange());
        }
    }

    IEnumerator CheckPlayerRange()
    {
        // Print a debug message
        Debug.Log("CheckPlayerRange coroutine started");

        // Check if the player is still in range
        while (Physics2D.OverlapCircle(transform.position, attackRadius, targetLayer))
        {
            // If the player is still in range, damage the player and increment the attack counter
            if (Physics2D.OverlapCircle(transform.position, attackRadius, targetLayer))
            {
                playerHeartRate.GhostAttack(attackDamage);
                Debug.Log("player's heart rate is now " + playerHeartRate.currentHeartRate);
                attackCounter++;
            }

            // Wait for the specified interval before checking again
            // You may need to adjust the length of this interval based on the length of your attack animation
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        // Reset the attack counter and the hasAttacked flag
        attackCounter = 0;
        hasAttacked = false;

        // Print a debug message
        Debug.Log("CheckPlayerRange coroutine stopped");
    }

    public void GhostDie()
    {
        isDying = true;
        speed = 0;
        anim.SetTrigger("Die");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 5f);

    }
}
