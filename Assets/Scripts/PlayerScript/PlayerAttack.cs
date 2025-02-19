using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Check Attack Horizontal")]
    public bool isAttack;
    public Vector2 attackCheckOffset;
    public float attackRadius;
    public LayerMask enemyLayer;

    [Header("Check Attack Up")]
    public Vector2 attackCheckUpOffset;

    [Header("Check Attack Down")]
    public Vector2 attackCheckDownOffset;

    [Header("Attack Effect")]
    [SerializeField] private Vector2 trailOffset;
    [SerializeField] private ParticleSystem slashEffect;

    [Header("Attack Cooldown")]
    [SerializeField] private float attackCooldown;
    private float lastAttackTime;

    private int killed;

    private void Start()
    {
        killed = 0;
    }
    private void Update()
    {
        EnemyDitect();
    }
    private void EnemyDitect()
    {
        List<Collider2D> hitEnemies = new List<Collider2D>();

        // Kiểm tra theo 3 hướng và thêm enemy vào danh sách
        hitEnemies.AddRange(Physics2D.OverlapCircleAll(
            (Vector2)transform.position + new Vector2(transform.localScale.x * attackCheckOffset.x, attackCheckOffset.y),
            attackRadius,
            enemyLayer
        ));

        hitEnemies.AddRange(Physics2D.OverlapCircleAll(
            (Vector2)transform.position + attackCheckUpOffset,
            attackRadius,
            enemyLayer
        ));

        hitEnemies.AddRange(Physics2D.OverlapCircleAll(
            (Vector2)transform.position + attackCheckDownOffset,
            attackRadius,
            enemyLayer
        ));

        isAttack = hitEnemies.Count > 0;

        if (isAttack && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack(hitEnemies.ToArray());
            lastAttackTime = Time.time;
        }
    }
    private void Attack(Collider2D[] hitEnemies)
    {
        Vector2 spawnPos = (Vector2)transform.position + new Vector2(transform.localScale.x * trailOffset.x, trailOffset.y);
        if (slashEffect != null)
        {
            ParticleSystem slash = Instantiate(slashEffect, spawnPos, Quaternion.identity);
            slash.transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 1);
            AudioManager.instance.PlayAttackSFX();
            slash.Play();
            
        }
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyDeath enemyScript = enemy.GetComponent<EnemyDeath>();
            if (enemyScript != null)
            {
                enemyScript.Death();
                killed++;// Gọi hàm Death của enemy
            }
        }


    }
    public int GetKilled()
    {
        return killed;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(transform.localScale.x * attackCheckOffset.x, attackCheckOffset.y), attackRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + attackCheckUpOffset, attackRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + attackCheckDownOffset, attackRadius);
    }
    
}
