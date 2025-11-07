using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

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

    private void Start()
    {
        currentTrack = -1;
    }

    private void Update()
    {
        if (currentTrack < 0) return;
        if (bgMusic[currentTrack].isPlaying == false)
        {
            PlayNextBGM();
        }
    }

    public AudioSource titleMusic;
    public AudioSource[] bgMusic;
    private int currentTrack;

    public AudioSource[] sfx;

    public void StopMusic()
    {
        foreach (AudioSource track in bgMusic)
        {
            track.Stop();
        }

        titleMusic.Stop();
    }

    public void PlayTitle()
    {
        StopMusic();
        titleMusic.Play();
    }

    public void PlayNextBGM()
    {
        StopMusic();

        currentTrack++;

        if (currentTrack >= bgMusic.Length)
        {
            currentTrack = 0;
        }

        bgMusic[currentTrack].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void StopSFX(int sfxToStop)
    {
        sfx[sfxToStop].Stop();
    }

    public void PlaySFXPitchAdjusted(int sfxToPlay)
    {
        sfx[sfxToPlay].pitch = Random.Range(0.8f, 1.2f);

        PlaySFX(sfxToPlay);
    }
}
