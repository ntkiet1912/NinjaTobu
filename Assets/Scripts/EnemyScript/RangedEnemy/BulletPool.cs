using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    public GameObject bulletPrefab;
    public int poolSize = 10;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            Debug.Log("Lay dan thanh cong");
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            Debug.LogWarning("Pool hết đạn, tạo thêm!");
            GameObject bullet = Instantiate(bulletPrefab);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        Debug.Log("tra dan thanh cong");
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
