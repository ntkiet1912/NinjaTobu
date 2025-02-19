using UnityEngine;

public class GroundWallCheck : MonoBehaviour
{
    [Header("GroundCheck")]
    public bool isGrounded;
    public Vector2  groundCheckOffset;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    [Header("RootCheck")]
    public bool isRooted;
    public Vector2 rootCheckOffset;
    public float rootCheckRadius;

    [Header("WallCheck")]
    public bool isAgainstWall;
    public Vector2 wallCheckOffset;
    public float wallCheckRadius;

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2) transform.position + groundCheckOffset, groundCheckRadius, whatIsGround);

        isRooted = Physics2D.OverlapCircle((Vector2)transform.position + rootCheckOffset, rootCheckRadius, whatIsGround);

        isAgainstWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(transform.localScale.x * wallCheckOffset.x , wallCheckOffset.y), wallCheckRadius, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + groundCheckOffset, groundCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rootCheckOffset, rootCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(transform.localScale.x * wallCheckOffset.x, wallCheckOffset.y), wallCheckRadius);
    }
}
