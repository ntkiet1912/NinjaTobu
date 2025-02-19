using UnityEngine.SceneManagement;  
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.StartMainMenu();
    }
    public void PlayGame()
    {
        AudioManager.instance.PlayUIButtonSFX();
        SceneManager.LoadScene(1);
        AudioManager.instance.StopMusic();
        AudioManager.instance.SetGamePlayMusic();
        AudioManager.instance.PlayMusic();
    }
}
