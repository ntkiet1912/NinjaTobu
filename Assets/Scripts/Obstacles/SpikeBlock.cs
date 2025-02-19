
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{
    [Header("WallCheck")]
    public bool isAgainstWall_1;
    public bool isAgainstWall_2;
    public Vector2 wallCheckOffset_1;
    public Vector2 wallCheckOffset_2;
    public float wallCheckRadius;
    public LayerMask whatIsGround;

    public float moveSpeed = 2.0f;
    public int direction = 1; // 1 right , -1 left

    private Vector3 startPosition;
    

    private void Start()
    {
        startPosition = transform.position;
        
    }
    private void Update()
    {
        
        isAgainstWall_1= Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(transform.localScale.x * wallCheckOffset_1.x, wallCheckOffset_1.y), wallCheckRadius, whatIsGround);
        isAgainstWall_2= Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(transform.localScale.x * wallCheckOffset_2.x, wallCheckOffset_2.y), wallCheckRadius, whatIsGround);
        if (isAgainstWall_1 || isAgainstWall_2)
        {
            direction *= -1;
            
        }
        transform.position += Vector3.right * moveSpeed * direction * Time.deltaTime;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(transform.localScale.x * wallCheckOffset_1.x, wallCheckOffset_1.y), wallCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(transform.localScale.x * wallCheckOffset_2.x, wallCheckOffset_2.y), wallCheckRadius);
    }

}
