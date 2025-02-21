using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip mainMenu;
    public AudioClip[] endlessMode;
    public AudioClip wallAgainst;
    public AudioClip jumpSound;
    public AudioClip playerDie;
    public AudioClip uiButtonSound;
    public AudioClip[] slowmotionSound;
    public AudioClip CoinCollected;
    public AudioClip AttackSFX;

    [Header("Save Volume")]
    public bool isMusicOn;
    public bool isSFXOn;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartMainMenu()
    {
        musicSource.clip = mainMenu;
        PlayMusic();
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        isSFXOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        if (isMusicOn)
        {
           musicSource.volume = 1;
        }
        else
        {
            musicSource.volume = 0;
        }
        if (isSFXOn)
        {
            sfxSource.volume = 1;
        }
        else
        {
            sfxSource.volume = 0;
        }

    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void PlayMusic()
    {
        musicSource.Play();
    }
    public void SetGamePlayMusic()
    {
        int index = Random.Range(0, endlessMode.Length);
        musicSource.clip = endlessMode[index];
    }
    public void WallHitSFX()
    {
        sfxSource.PlayOneShot(wallAgainst, 0.3f);

    }

    public void DieSFX()
    {
        sfxSource.PlayOneShot(playerDie);
    }
    public void JumpSFX()
    {
        sfxSource.PlayOneShot(jumpSound , 0.8f);
    }
    public void PlayUIButtonSFX()
    {
        sfxSource.PlayOneShot(uiButtonSound);
    }
    public void PlaySlowMotionSFX()
    {
        int index = Random.Range(0, slowmotionSound.Length);
        sfxSource.PlayOneShot(slowmotionSound[index] , 0.6f);
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }
    public void PlayCoinCollectedSFX()
    {
        sfxSource.PlayOneShot(CoinCollected, 0.8f);
    }
    public void PlayAttackSFX()
    {
        sfxSource.PlayOneShot(AttackSFX);
    }

    public void VolumeMusic(float volume)
    {
        musicSource.volume = volume;
    }
    public void VolumeSFX(float volume)
    {
        sfxSource.volume = volume;
    }
    public void CheckVolume()
    {
        if (musicSource.volume == 0)
        {
            isMusicOn = false;
        }
        else
        {
            isMusicOn = true;
        }

        if (sfxSource.volume == 0)
        {
            isSFXOn = false;
        }
        else
        {
            isSFXOn = true;
        }
    }
}
