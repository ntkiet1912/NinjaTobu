using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUpdate : MonoBehaviour
{
    [SerializeField] private Text coinCount;
    [SerializeField] private int coinPoint;
    private int totalCoin;

    private void Start()
    {
        totalCoin = 0;
        coinPoint = 1;
    }
    private void Update()
    {
        coinCount.text = totalCoin.ToString();
    }
    public void UpdateCoin(int point)
    {
        totalCoin += point * coinPoint;
        int currentCoin = PlayerPrefs.GetInt("currentCoin", 0);
        currentCoin += totalCoin;
        PlayerPrefs.SetInt("currentCoin", currentCoin);
        PlayerPrefs.Save();
    }
    public int GetCoinPoint()
    {
        return coinPoint;
    }

    public void SetCoinPoint(int point)
    {
        coinPoint = point;
    }

    public int GetTotalCoin()
    {
        return totalCoin;
    }

    
}
