using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    private Animator anim;
    public GameManager gameManager;

    [Header("ColliderCheck")]
    public float radius;
    public bool isHit;
    public Vector2 colliderCheckOffset;
    public LayerMask targetPlayer;

    [Header("FlipEnemy")]
    public float timeToFlip = 5f;
    private Coroutine corountineFlip;

    [Header("Attack")]
    [SerializeField] private float fireDuration;
    [SerializeField] private float delaytimeAtk;
    private bool isAttack;
    public bool isEndAtk = false;
    private float timer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isAttack = false;
        timer = 0;
        // Chạy coroutine auto flip khi enemy được kích hoạt
        corountineFlip = StartCoroutine(AutoFlip());
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        fireDuration = anim.runtimeAnimatorController.animationClips
            .FirstOrDefault(clip => clip.name == "Enemy3_Attack").length;
    }
    private void Update()
    {
        CheckFireHit();
        if (!isAttack)
        {
            timer += Time.deltaTime;
            if (timer >= delaytimeAtk)
            {
                isAttack = false;
                StartAttack();
                timer = 0;
            }
        }                       
    }
    private void StartAttack()
    {
        if (!isAttack)
        {
            isAttack = true;
            isEndAtk = false;
            anim.SetTrigger("Attack");
            Invoke(nameof(EndAtk), fireDuration);
        }
    }
    private void EndAtk()
    {
        isAttack = false;
        isEndAtk = true;
        anim.SetTrigger("Idle");
    }
    private void CheckFireHit()
    {
        Collider2D player = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(transform.localScale.x * colliderCheckOffset.x, colliderCheckOffset.y), radius, targetPlayer);
        isHit = (player != null);
        if (isHit)
        {
            if (isAttack)
            {
                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.PlayerDie();
                gameManager.EndGame();
            }
            
        }
    }
    public void FireHitEvent()
    {
        CheckFireHit();
        Debug.Log("Fire hit");
    }
    IEnumerator AutoFlip()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToFlip);
            if (!isAttack)
            {
                Flip();
            }
        }
    }
    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void OnDisable()
    {
        if(corountineFlip != null)
        {
            StopCoroutine(corountineFlip);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(transform.localScale.x * colliderCheckOffset.x, colliderCheckOffset.y), radius);
    }
}
