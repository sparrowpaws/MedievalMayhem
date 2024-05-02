using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    //variables for attack
    public int attackDamage = 20;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    public float attackRange = .5f;
    public LayerMask enemyLayer;
    public Transform attackPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("the player attacked");
            Attack();
            lastAttackTime = Time.time; //update the last attack time to current time
        }
    }

    void Attack()
    {
        Debug.Log("Attack called");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

        }
    }
}