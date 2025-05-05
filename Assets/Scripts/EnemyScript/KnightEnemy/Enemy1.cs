    using System.Collections;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private Animator anim;

    [Header("ColliderCheck")]
    public float radius;
    public bool isHit;
    public Vector2 colliderCheckOffset;
    public LayerMask targetPlayer;

    [Header("FlipEnemy")]
    public float timeToFlip = 5f;
    private Coroutine corountineFlip;

    [Header("Attack")]
    [SerializeField] private int delaytimeAtk;
    [SerializeField] private int maxAttackCount;
    public bool isEndAtk = false;
    private int attackCount; //so luot tan cong
    private bool isAttack;
    private float timer;
    [SerializeField] private GameObject vision;
    private void Start()
    {
        attackCount = maxAttackCount;
        isAttack = false;
        anim = GetComponent<Animator>();
        vision.SetActive(true);
        // Chạy coroutine auto flip khi enemy được kích hoạt
        Debug.Log("Coroutine has started");
        corountineFlip = StartCoroutine(AutoFlip());
    }

    private void Update()
    {
        Detection();
        EndAtk();
        if (isAttack)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
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
        Collider2D player = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(transform.localScale.x * colliderCheckOffset.x, colliderCheckOffset.y), radius, targetPlayer);
        isHit = (player != null);
        if (isHit)
        {
            if (attackCount > 0 && !isAttack)
            {
                anim.SetTrigger("Attack");
                //AudioManager.instance.PlayEnemyAttack();
                attackCount -= 1; 
                isAttack = true;
                Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
                player.GetComponent<PlayerController>()?.ApplyKnockBack(knockbackDirection);
            }
        }
    }

    private void EndAtk()
    {
        if (attackCount <= 0)
        {
            anim.SetBool("EndAtk", true);
            isEndAtk = true;
            vision.SetActive(false);
            // Dừng Auto Flip khi hết lượt tấn công
            if (corountineFlip != null)
            {
                StopCoroutine(corountineFlip);
                corountineFlip = null;
            }

            Invoke(nameof(ResetAtk), delaytimeAtk);
        }
    }

    private void ResetAtk()
    {
        anim.SetBool("EndAtk", false);
        isEndAtk = false;
        attackCount = maxAttackCount;
        vision.SetActive(true);
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

}
