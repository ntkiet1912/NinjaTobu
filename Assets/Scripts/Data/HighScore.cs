using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    [SerializeField] private GetData highScore;

    private void Start()
    {
        highScoreText.text = "best\n" + highScore.GetIntData("HighScore", 0).ToString();
    }
}
