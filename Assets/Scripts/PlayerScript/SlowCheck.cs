using System.Collections;
using UnityEngine;

public class SlowCheck : MonoBehaviour
{
    [SerializeField] private float slowScale;
    private int slowCount = 1;
    private bool isJump = false;
    [SerializeField] private GroundWallCheck groundCheck;
    [SerializeField] private GameObject slowEffect;
    private GameObject currentSlowEffect;
    private void Update()
    {
        CheckJump();
        ActiveSlow();
    }
    private void ActiveSlow()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (slowCount > 0 && isJump)
            {
                AudioManager.instance.PlaySlowMotionSFX();
                Time.timeScale = slowScale;
                slowCount--;
                if (currentSlowEffect == null) 
                {
                    StartCoroutine(StartEffect());
                }
            }
            else
            {
                return;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1;
            AudioManager.instance.StopSFX();
            if (currentSlowEffect != null) 
            {
                Destroy(currentSlowEffect);
            }
        }
    }
    private void CheckJump()
    {
        if (!groundCheck.isGrounded && !groundCheck.isAgainstWall && !groundCheck.isRooted)
        {
            isJump = true;
        }
        else
        {
            slowCount = 1;
            isJump = false;
        }
    }
    IEnumerator StartEffect()
    {
        currentSlowEffect = Instantiate(slowEffect, transform.position, Quaternion.identity);
        currentSlowEffect.transform.SetParent(transform); // Để hiệu ứng di chuyển theo nhân vật
        ParticleSystem effect = currentSlowEffect.GetComponentInChildren<ParticleSystem>();

        if (effect != null)
        {
            effect.Play();
        }

        while (Time.timeScale < 1) 
        {
            yield return null;
        }

        Destroy(currentSlowEffect);
    }
}