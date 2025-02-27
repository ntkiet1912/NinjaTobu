using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isKnockedBack = false;
    
    
    [Header("Knockback Settings")]
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;
    public float slowMotionTime = 0.1f;
    public float slowMotionScale = 0.3f;

    [Header("Player Die")]
    public GameObject dieEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void ApplyKnockBack(Vector2 direction)
    {
        if (isKnockedBack)
        {
            return;
        }
        isKnockedBack = true;
        transform.localScale *= -1;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(ApplySlowMotion());

        Invoke(nameof(ResetKnockBack), knockbackDuration);
    }
    IEnumerator ApplySlowMotion()
    {
        Time.timeScale = slowMotionScale;
        yield return new WaitForSeconds(slowMotionTime);
        Time.timeScale = 1f;
    }
    private void ResetKnockBack()
    {
        isKnockedBack = false;
    } 
    public void PlayerDie()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DieEffect());
        player.SetActive(false);
        AudioManager.instance.DieSFX();
    }
    IEnumerator DieEffect()
    {
        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        effect.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(2f);
    }
}
