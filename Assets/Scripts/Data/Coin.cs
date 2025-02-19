using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinUpdate coin;
    [SerializeField] private GameObject effectCoin;
    [SerializeField] private GetData data;
    private GameObject effect;
    private void Start()
    {
        data = GameObject.FindWithTag("GameManager")?.GetComponent<GetData>();
        coin = GameObject.FindWithTag("GameManager")?.GetComponent<CoinUpdate>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(coin != null)
            {
                coin.UpdateCoin(coin.GetCoinPoint()); 
                AudioManager.instance.PlayCoinCollectedSFX();
                StartCoroutine(StartEffect());
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StartEffect()
    {
        if(effect == null)
        {
            effect = Instantiate(effectCoin, transform.position, Quaternion.identity);
            effect.GetComponentInChildren<ParticleSystem>().Play();
            yield return new WaitForSeconds(1f); 
            Destroy(effect);
        }
    }
}
