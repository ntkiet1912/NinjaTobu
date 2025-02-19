using UnityEngine.SceneManagement;  
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{


    public GameObject settingsPanel;
    public GameObject startButton;
    public GameObject settingsButton;
    


    [SerializeField] private Sprite[] musicToggle; // 0 : On , 1 : Off
    [SerializeField] private Sprite[] sfxToggle; // 0 : On , 1 : Off


    private void Start()
    {
        AudioManager.instance.StartMainMenu();
        settingsPanel.SetActive(false);
        AudioManager.instance.CheckVolume();
        
        AudioManager.instance.isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        AudioManager.instance.isSFXOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        
    }
    private void Update()
    {
        UpdateSoundIcons();
    }
    public void PlayGame()
    {
        AudioManager.instance.PlayUIButtonSFX();
        SceneManager.LoadScene(1);
        AudioManager.instance.StopMusic();
        AudioManager.instance.SetGamePlayMusic();
        AudioManager.instance.PlayMusic();
    }
    public void ToSettings()
    {
        AudioManager.instance.PlayUIButtonSFX();
        settingsPanel.SetActive(true);
        startButton.SetActive(false);
        settingsButton.SetActive(false);
    }
    public void CloseSettings()
    {
        AudioManager.instance.PlayUIButtonSFX();
        settingsPanel.SetActive(false);
        startButton.SetActive(true);
        settingsButton.SetActive(true);
    }
    public void MusicButton()
    {
        AudioManager.instance.PlayUIButtonSFX();
        AudioManager.instance.isMusicOn = !AudioManager.instance.isMusicOn;

        
        AudioManager.instance.VolumeMusic(AudioManager.instance.isMusicOn ? 1 : 0);
        PlayerPrefs.SetInt("MusicOn", AudioManager.instance.isMusicOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateSoundIcons();
    }

    public void SFXButton()
    {
        AudioManager.instance.PlayUIButtonSFX();
        AudioManager.instance.isSFXOn = !AudioManager.instance.isSFXOn;

       
        AudioManager.instance.VolumeSFX(AudioManager.instance.isSFXOn ? 1 : 0);
        PlayerPrefs.SetInt("SFXOn", AudioManager.instance.isSFXOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateSoundIcons();
    }

    private void UpdateSoundIcons()
    {
       
        GameObject musicObj = GameObject.FindGameObjectWithTag("Music");
        if (musicObj != null)
        {
            Image imageMusic = musicObj.GetComponent<Image>();
            imageMusic.sprite = musicToggle[AudioManager.instance.isMusicOn ? 0 : 1];
        }

        GameObject sfxObj = GameObject.FindGameObjectWithTag("SFX");
        if (sfxObj != null)
        {
            Image imageSFX = sfxObj.GetComponent<Image>();
            imageSFX.sprite = sfxToggle[AudioManager.instance.isSFXOn ? 0 : 1];
        }
    }
}
