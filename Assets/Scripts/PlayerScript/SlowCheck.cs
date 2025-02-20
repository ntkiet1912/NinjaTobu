using System.Collections;
using UnityEngine;

public class SlowCheck : MonoBehaviour
{
    [SerializeField] private float slowScale;
    private int slowCount = 1;
    private bool isJump = false;
    [SerializeField] private GroundWallCheck groundCheck;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    private void Update()
    {
        CheckJump();
        ActiveSlow();
    }
    private void ActiveSlow()
    {
        if (Input.GetMouseButton(0))
        {
            if (slowCount > 0 && isJump)
            {
                AudioManager.instance.PlaySlowMotionSFX();
                Time.timeScale = slowScale;
                slowCount--;
                anim.SetBool("isSlow", true);
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
            anim.SetBool("isSlow", false);
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
   
}