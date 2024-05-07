using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    //for the attack sound effect
    public AudioSource attackSound;

    //variables for attack
    public int attackDamage = 20;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    public float attackRange = .5f;
    public LayerMask enemyLayer;
    public Transform attackPoint;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (attackSound == null)
        {
            attackSound = GetComponent<AudioSource>(); // Get the AudioSource component if not assigned
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            animator.SetBool("isAttacking", true);
            lastAttackTime = Time.time; //update the last attack time to current time
        }
    }

    void Attack()
    {
        // Play the attack sound effect
        if (attackSound != null)
        {
            attackSound.Play();
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }
}