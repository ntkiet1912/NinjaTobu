using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Swipe Settings")]
    public float swipeThreshold = 50f; 
    public float dashForce = 20f;     

    [Header("Wall Interaction")]
    public float stickDuration = 1f; 
    private bool isStickingToWall = false;

    [Header("Sound Wall Settings")]
    private bool isPlayWallSFX = false;
    private bool isWallImpact = false;
    private bool isRootImpact = false;

    [Header("Sound Jump Settings")]
    [SerializeField] private bool isPlayJumpSFX = false;
    private bool isJump = false;

    [Header("Swipe Line Settings")]
    public LineRenderer lineRenderer;
    public float lineDuration = 0.3f;
    public float maxLineLength = 2f;
    public float minLineWidth = 0.05f;
    public float maxLineWidth = 0.2f;

    [Header("Jump Settings")]
    public int maxJump = 1;
    [SerializeField] private int jumpCount = 0;

    private Vector2 startMousePosition; 
    private Vector2 endMousePosition;   
    private Rigidbody2D rb;
    private bool isJumpDirect = true; // 0: left, 1: right  
    private bool isClimbing = false;

    private GroundWallCheck groundWallCheck;
    private Animator anim;

   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundWallCheck = GetComponent<GroundWallCheck>();
        anim = GetComponent<Animator>();

        //line renderer
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        CheckCollision();
        PlayAnimation();
        HandleMouseSwipe();
        ResetJumpCount();
        FlipPlayer();
        CheckWallSFX();
        CheckJumpSFX();
    }
    private void PlayAnimation()
    {
        //jump
        if(!groundWallCheck.isGrounded)
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }

        //climb
        if (isClimbing)
        {
            anim.SetBool("Climbing", true);
        }
        else
        {
            anim.SetBool("Climbing", false);
        }

        //wall stick
        if (isStickingToWall)
        {
            anim.SetBool("sticking", true);

        }
        else
        {
            anim.SetBool("sticking", false);
        }

        
    }
    private void CheckCollision()
    {
        if (groundWallCheck.isRooted)
        {
            if (!groundWallCheck.isGrounded)
            {
                isClimbing = true;
                WallClimbing();
            }
        }
        else
        {
            isClimbing = false;
            if (!groundWallCheck.isGrounded && groundWallCheck.isAgainstWall)
            {
                StickToWall();
            }
        }
    }
    private void WallClimbing()
    {
        
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.gravityScale = 0;

        //Invoke(nameof(ReleaseFromWall), stickDuration);

    }
    
    private void HandleMouseSwipe()
    {

        
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Input.mousePosition;
            //line renderer
            lineRenderer.enabled = true;
            

        }
        if(Input.GetMouseButton(0))
        {
            Vector2 currentMousePosition = Input.mousePosition;
            lineRenderer.SetPosition(0, transform.position);

            
            Vector2 swipeVector = currentMousePosition - startMousePosition;

           
            Vector2 direction = swipeVector.normalized;
            float distance = swipeVector.magnitude;

            
            distance = Mathf.Min(distance, maxLineLength * 100);

            
            Vector2 endPosition = (Vector2)transform.position + direction * (distance / 100); 

            
            float normalizedDistance = distance / (maxLineLength * 100);
            float currentWidth = Mathf.Lerp(maxLineWidth, minLineWidth, normalizedDistance);

            lineRenderer.startWidth = currentWidth;
            lineRenderer.endWidth = currentWidth;
            lineRenderer.startWidth = currentWidth;
            lineRenderer.endWidth = currentWidth;

            // Sử dụng offset hoặc điểm cụ thể
            Vector3 lineStart = transform.position + new Vector3(0, 0.5f, 0); // offset example
                                                                              // hoặc: Vector3 lineStart = lineStartPoint.position;

            lineRenderer.SetPosition(0, lineStart);
            lineRenderer.SetPosition(1, endPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            endMousePosition = Input.mousePosition;
            Vector2 swipeVector = endMousePosition - startMousePosition;


            if (swipeVector.magnitude >= swipeThreshold)
            {
                isJumpDirect = swipeVector.x > 0;
                bool canJump = groundWallCheck.isGrounded || groundWallCheck.isAgainstWall || groundWallCheck.isRooted;
                Debug.Log("Swipe direction: " + isJumpDirect);
                if(canJump && jumpCount < maxJump)
                {
                    PerformDash(swipeVector.normalized);
                    jumpCount++;
                }
                else if(jumpCount < maxJump && !canJump)
                {
                    PerformDash(swipeVector.normalized);
                    jumpCount++;
                }
                else if (jumpCount >= maxJump)
                {
                    return;
                }

            }

            lineRenderer.enabled = false;
        }
    }

    private void ResetJumpCount()
    {
        if (groundWallCheck.isGrounded || groundWallCheck.isRooted || groundWallCheck.isAgainstWall)
        {
            jumpCount = 0;
        }
    }
    private void FlipPlayer()
    {
        if (!isJumpDirect)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void PerformDash(Vector2 direction)
    {
        if (isStickingToWall || isClimbing)
        {
            ReleaseFromWall();
        }
       
        Vector2 dashForceVector = direction.normalized * dashForce;
        rb.AddForce(dashForceVector, ForceMode2D.Impulse);

        
        Debug.Log("Lướt với lực: " + dashForceVector);
    }

    private void StickToWall()
    {

        if (!isStickingToWall)
        {
            isStickingToWall = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
            Debug.Log("Nhân vật đang bám tường");
            //Invoke(nameof(ReleaseFromWall), stickDuration);
        }
    }

    private void ReleaseFromWall()
    {
        isStickingToWall = false;
        isClimbing = false;
        rb.gravityScale = 1f;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        
        Debug.Log("Rời khỏi tường!");
    }
    private void CheckWallSFX()
    {
        isWallImpact = groundWallCheck.isAgainstWall;
        isRootImpact = groundWallCheck.isRooted;
        if (isWallImpact == false && isRootImpact == false) isPlayWallSFX = false;
        if ((isWallImpact || isRootImpact) && !isPlayWallSFX)
        {
            PlaySoundWallSFX();
            isPlayWallSFX = true;
        }

    }
    private void PlaySoundWallSFX()
    {
        AudioManager.instance.WallHitSFX();

    }
    private void CheckJumpSFX()
    {
        isJump = !groundWallCheck.isGrounded && !groundWallCheck.isRooted && !groundWallCheck.isAgainstWall;
        if (isJump == false) isPlayJumpSFX = false;
        if (isJump && !isPlayJumpSFX)
        {
            PlaySoundJumpSFX();
            isPlayJumpSFX = true;
        }
    }
    private void PlaySoundJumpSFX()
    {
        AudioManager.instance.JumpSFX();
    }
}
