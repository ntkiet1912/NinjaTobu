using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private Text currentCointText;
    [SerializeField] private GetData Coin;
    [SerializeField] private int currentCoin;

    private void Awake()
    {
        Coin = GameObject.FindWithTag("GameManager").GetComponent<GetData>();
    }
    private void Update()
    {
        SetTextCoin();
    }
    private void SetTextCoin()
    {
        currentCoin = Coin.GetIntData("currentCoin", 0);
        currentCointText.text = currentCoin.ToString();
    }
}
