using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GetData data;
    [SerializeField] private CoinUpdate coinUpdate;
    [SerializeField] private GameScore gameScore;
    [SerializeField] private PlayerAttack killedCount; 
    private int totalScore;
    [SerializeField] private Text scoreText;
    private int totalCoin;
    [SerializeField] private Text coinText;
    private int killed;
    [SerializeField] private Text killedText;

    private void Start()
    {
        data = GameObject.FindWithTag("GameManager").GetComponent<GetData>();
        gameScore = GameObject.FindWithTag("GameManager").GetComponent<GameScore>();
        coinUpdate = GameObject.FindWithTag("GameManager").GetComponent<CoinUpdate>();
        killedCount = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        totalScore = (int)gameScore.GetScore();
        totalCoin = coinUpdate.GetTotalCoin();
        scoreText.text = "score: " + totalScore.ToString();
        coinText.text = totalCoin.ToString();
        killed = killedCount.GetKilled();
        killedText.text = "killed: "+ killed.ToString();
    }
}
