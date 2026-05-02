using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 2.0f;      // how far you can hit
    public float attackRate = 1f;         // how fast you can attack
    public float attackMoveSpeed = 5f;    // how fast you move toward target when attacking

    [Header("References")]
    public Animator animator;
    public Transform attackPoint;         // empty GameObject in front of player
    public LayerMask enemyLayers;         // what counts as an enemy
    public Transform lockOnTarget;        // the current enemy target

    private float nextAttackTime = 0f;

    void Update()
    {
        HandleLockOn();
        HandleAttack();
    }

    void HandleLockOn()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // toggle lock-on to nearest enemy
            if (lockOnTarget == null)
                lockOnTarget = FindNearestEnemy();
            else
                lockOnTarget = null;
        }

        if (lockOnTarget)
        {
            // face toward locked target
            Vector3 dir = lockOnTarget.position - transform.position;
            dir.y = 0; // ignore vertical difference
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 10f);
        }
    }

    void HandleAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(1)) // Right-click
            {
                animator.SetTrigger("LightAttack");

                // Move slightly toward target if locked on
                if (lockOnTarget != null)
                {
                    StartCoroutine(MoveTowardTarget(lockOnTarget.position));
                }

                // Detect enemies in range
                Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider enemy in hitEnemies)
                {
                    enemy.GetComponent<EnemyHealth>()?.TakeDamage(10);
                }

                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    IEnumerator MoveTowardTarget(Vector3 targetPos)
    {
        float elapsed = 0f;
        float duration = 0.2f;

        Vector3 startPos = transform.position;
        Vector3 dir = (targetPos - transform.position).normalized;

        while (elapsed < duration)
        {
            transform.position += dir * attackMoveSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    Transform FindNearestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 15f, enemyLayers);
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy.transform;
            }
        }

        return nearest;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
