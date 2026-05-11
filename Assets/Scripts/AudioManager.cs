using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip menuMusic;
    public AudioClip levelMusic;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip jumpSound;
    public AudioClip buttonClickSound;

    void Awake()
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

    void Start()
    {
        ApplyVolume();
    }

    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(buttonClickSound);
    }

    public void PlayMenuMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayLevelMusic()
    {
        musicSource.clip = levelMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayWinSound()
    {
        sfxSource.PlayOneShot(winSound);
    }

    public void PlayLoseSound()
    {
        sfxSource.PlayOneShot(loseSound);
    }

    public void PlayJumpSound()
    {
        sfxSource.PlayOneShot(jumpSound);
    }

    public void ApplyVolume()
    {
        bool soundOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        float vol = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = soundOn ? vol : 0f;
    }
}