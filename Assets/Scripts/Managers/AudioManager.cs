using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("2D Sources")]
    public AudioSource audioSource;

    [Header("3D Sources")]
    public AudioSource A1SunAudioSource;
    public AudioSource A1MoonAudioSource;
    public AudioSource A1EarthAudioSource;
    public AudioSource A1BigMoonAudioSource;
    public AudioSource A1SpaceAudioSource;
    public AudioSource A1BigEarthAudioSource;
    public AudioSource A1MiddleMoonAudioSource;
    public AudioSource A2SunAudioSource;
    public AudioSource A2MoonAudioSource;
    public AudioSource A2EarthAudioSource;

    [Header("Sound Effects")]
    public AudioClip airZoomVacuum;
    public AudioClip siFiTransition;
    public AudioClip smallSweep;
    public AudioClip transitionBase;
    public AudioClip whoosh;
    public AudioClip trophy;

    // privates
    public static AudioManager Instance;
    [HideInInspector] public bool isFinished = false;

    private void Awake()
    {
        Instance = this;
    }

    public void Play(AudioClip audioClip, float volume = 1f)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.volume = volume;
        audioSource.clip = audioClip;
        audioSource.Play();
        isFinished = false;
    }

    public void Stop()
    {
        audioSource.Stop();
        isFinished = true;
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void Resume()
    {
        audioSource.UnPause();
    }

    public void Update()
    {
        isFinished = !audioSource.isPlaying;
    }

    public void Play(AudioSource source, AudioClip audioClip, bool randomPitch = false, float volume = 0.2f)
    {
        if (source.isPlaying)
            source.Stop();

        if (randomPitch) source.pitch = Random.Range(0.5f, 1.5f);
        else source.pitch = 1;

        source.volume = volume;

        source.clip = audioClip;
        source.Play();
    }

    public void Stop(AudioSource source)
    {
        source.Stop();
    }

    public void Pause(AudioSource source)
    {
        source.Pause();
    }

    public void Resume(AudioSource source)
    {
        source.UnPause();
    }

    public void AdjustVolume(float newVolume)
    {
        AudioListener.volume = newVolume / 100;
    }
}