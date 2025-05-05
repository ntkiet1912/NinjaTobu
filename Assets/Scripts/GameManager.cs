using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    

  
    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }
    public void ToHome()
    {
        AudioManager.instance.PlayUIButtonSFX();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        AudioManager.instance.StopMusic();
        AudioManager.instance.StartMainMenu();
    }
    public void Replay()
    {
        AudioManager.instance.PlayUIButtonSFX();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        AudioManager.instance.StopMusic();
        AudioManager.instance.SetGamePlayMusic();
        AudioManager.instance.PlayMusic();
    }
    public void EndGame()
    {
        if (gameOverCanvas == null)
        {
            Debug.LogError("gameOverCanvas bị null! Kiểm tra lại trong Inspector.");
            return;
        }
        //Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
        Debug.Log("Game Over");
    }
}
