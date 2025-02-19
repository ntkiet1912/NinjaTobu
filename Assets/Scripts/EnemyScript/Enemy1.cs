using System.Collections;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private Animator anim;

    [Header("ColliderCheck")]
    public float radius;
    private bool isHit;
    public Vector2 colliderCheckOffset;
    public LayerMask targetPlayer;

    [Header("FlipEnemy")]
    public float timeToFlip = 5f;
    private Coroutine corountineFlip;

    [Header("Attack")]
    [SerializeField] private int delaytimeAtk;
    [SerializeField] private int maxAttackCount;
    private bool isAttack;
    private float timer;
    private int attackCount;

    private void Start()
    {
        attackCount = maxAttackCount;
        anim = GetComponent<Animator>();

        // Chạy coroutine auto flip khi enemy được kích hoạt
        corountineFlip = StartCoroutine(AutoFlip());
    }

    private void Update()
    {
        Detection();
        EndAtk();
        if(isAttack)
        {
            timer += Time.deltaTime;
            if (timer >= delaytimeAtk)
            {
                isAttack = false;
                timer = 0;
            }
        }
    }

    IEnumerator AutoFlip()
    {
        while (true) // Vòng lặp vô hạn cho auto flip
        {
            yield return new WaitForSeconds(timeToFlip);
            Flip();
        }
    }

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1; // Đổi hướng enemy
        transform.localScale = scale;
    }

    private void OnDisable()
    {
        // Kiểm tra nếu coroutine đang chạy thì mới dừng
        if (corountineFlip != null)
        {
            StopCoroutine(corountineFlip);
        }
    }

    private void Detection()
    {
        isHit = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(transform.localScale.x * colliderCheckOffset.x, colliderCheckOffset.y), radius, targetPlayer);
        if (isHit)
        {
            if (attackCount > 0 && !isAttack)
            {
                anim.SetTrigger("Attack");
                attackCount -= 1;
                isAttack = true;
            }
        }
    }

    private void EndAtk()
    {
        if (attackCount <= 0)
        {
            anim.SetBool("EndAtk", true);

            // Dừng Auto Flip khi hết lượt tấn công
            if (corountineFlip != null)
            {
                StopCoroutine(corountineFlip);
                corountineFlip = null;
            }

            Invoke(nameof(ResetAtk), 5.0f);
        }
    }

    private void ResetAtk()
    {
        anim.SetBool("EndAtk", false);
        attackCount = maxAttackCount;

        // Nếu chưa có coroutine auto flip, thì khởi động lại
        if (corountineFlip == null)
        {
            corountineFlip = StartCoroutine(AutoFlip());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(transform.localScale.x * colliderCheckOffset.x, colliderCheckOffset.y), radius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<PlayerController>()?.ApplyKnockBack(knockbackDirection);
        }
    }
}
