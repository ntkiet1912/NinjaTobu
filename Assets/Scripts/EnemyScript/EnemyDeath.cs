using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;
    private GameObject enemy;

    public void Death()
    {
        StartCoroutine(StartEffect());  
    }
    IEnumerator StartEffect()
    {
        if(enemy == null)
        {
            Destroy(gameObject);
            enemy = Instantiate(deathEffect, transform.position, Quaternion.identity);
            enemy.GetComponentInChildren<ParticleSystem>().Play();
            yield return new WaitForSeconds(1f);
            Destroy(enemy);
        }
    }
}
