using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-Audio Sources-")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;

    [Header("-Audio Clips-")]
    [SerializeField] private AudioClip[] audioClips;
    
    [Header("-Audio Clips-")]
    [SerializeField] private AudioClip[] musicClips;

    private void Start()
    {
        if (musicClips.Length > 0)
        {
            musicSource.clip = musicClips[0];
            //musicSource.Play();
        }

        if (musicClips.Length > 1)
        {
            effectsSource.clip = musicClips[1];
            //effectsSource.Play();
        }
    }

    public void SetMusicClip(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Invalid music clip index: {index}");
        }
    }

    public void SetEffectsClip(int index)
    {
        if (index >= 0 && index  < audioClips.Length)
        {
            effectsSource.clip = audioClips[index];
            effectsSource.Play();
        }
        else
        {
            Debug.LogWarning($"Invalid effects clip index: {index}");
        }
    }

    public AudioClip GetCurrentMusicClip()
    {
        return musicSource.clip;
    }

    public AudioClip GetCurrentEffectsClip()
    {
        return effectsSource.clip;
    }
}


/*
public class OtherClass : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
        if (audioManager != null)
        {
            audioManager.SetMusicClip(1); // Ustaw nowy klip
        }
    }
}

 LUB

                if (audioManager != null)
        {
            audioManager.SetEffectsClip(1); // Ustaw nowy klip
        }
*/