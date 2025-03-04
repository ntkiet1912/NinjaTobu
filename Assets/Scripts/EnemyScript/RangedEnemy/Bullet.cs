using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator anim;
    public Vector2 direction;
    [SerializeField] private float speed;
  

    private GameManager gameManager;

    private void Start()
    {
        anim = GetComponent<Animator>();

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Initialize(Vector2 fireDirection, float bulletSpeed)
    {
        direction = fireDirection;
        speed = bulletSpeed;
        

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (anim != null)
            {
                anim.SetTrigger("Explode");
            }
            else
            {
                Debug.LogWarning("Animator chưa được gán cho đạn!");
            }

            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.PlayerDie();
            }

            if (gameManager != null)
            {
                gameManager.EndGame();
            }

            StartCoroutine(DisableBullet());
        }
        else if (collision.CompareTag("Wall"))
        {
            if (anim != null)
            {
                anim.SetTrigger("Explode");
            }

            StartCoroutine(DisableBullet());
        }
    }

    private IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(0.2f);
        BulletPool.Instance.ReturnBullet(gameObject);
    }
}
