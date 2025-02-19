using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float lastPoint;
    private float currentPoint;
    private float score;
    [SerializeField] private Text scoreText;
    [SerializeField] private float scorePerSecond;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            lastPoint = player.transform.position.y;
        }
    }
    private void Update()
    {
        CalculateScore();
        scoreText.text = "Score " + Mathf.Round(score).ToString();
        CheckHighScore();
    }
    private void CalculateScore()
    {
        currentPoint = player.transform.position.y;
        float distance = currentPoint - lastPoint;  
        if(distance > 0)
        {
            score += scorePerSecond * Time.deltaTime;
        }
        lastPoint = currentPoint;
    }
    private void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (int)score);
        }
    }
    public float GetScore()
    {
        return score;
    }   
    public void AddScore(float score)
    {
        this.score += score;
    }
}
