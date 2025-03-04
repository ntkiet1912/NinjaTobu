using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    private Animator anim;
    public float fireRate = 1.5f; // Delay giữa các lần bắn
    public float bulletSpeed = 8f;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask playerLayer;

    [Header("Fire Point Settings")]
    [SerializeField] private Vector2 firePointOffset;

    private bool canShoot = true; 

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        DetectPlayer();
        if (canShoot) anim.SetTrigger("Idle");
    }

    private void DetectPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (player != null && canShoot)
        {
            canShoot = false; 
            anim.SetTrigger("Attack");
            StartCoroutine(ShootAfterAnimation(player.transform.position));
        }
    }

    private IEnumerator ShootAfterAnimation(Vector2 targetPosition)
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length); 

        FireBullet(targetPosition);
        yield return new WaitForSeconds(fireRate); 

        canShoot = true; 
    }

    private void FireBullet(Vector2 targetPosition)
    {
        if (BulletPool.Instance == null)
        {
            Debug.LogError("BulletPool.Instance is null!");
            return;
        }

        GameObject bullet = BulletPool.Instance.GetBullet();
        if (bullet != null)
        {
            Vector2 firePointPosition = (Vector2)transform.position + new Vector2(Mathf.Sign(transform.localScale.x) * firePointOffset.x, firePointOffset.y);
            Vector2 fireDirection = (targetPosition - firePointPosition).normalized;

            bullet.transform.position = firePointPosition;
            bullet.GetComponent<Bullet>().Initialize(fireDirection, bulletSpeed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Vector3 firePointPosition = transform.position + new Vector3(Mathf.Sign(transform.localScale.x) * firePointOffset.x, firePointOffset.y, 0);
        Gizmos.DrawSphere(firePointPosition, 0.1f);
    }
}
