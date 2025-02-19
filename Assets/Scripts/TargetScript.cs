using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetScript : MonoBehaviour
{
    public GameObject gameOverCanvas;
    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EndGame();
        }
    }
    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    private void EndGame()
    {
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
        Debug.Log("Game Over");
    }
}
