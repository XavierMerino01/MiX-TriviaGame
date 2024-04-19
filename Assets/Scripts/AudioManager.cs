using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Musica")]
    public AudioClip[] soundTracks;


    [HideInInspector] public AudioSource mainAudioSource;

    private float volumeValue;
    private Coroutine volumeFadeCoroutine;

    private void Awake()
    {
        mainAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        volumeValue = mainAudioSource.volume;
        mainAudioSource.volume = 0;
        PlayRandomTrack();
        FadeInCurrentTrack(2);
    }

    private void Update()
    {
        if(!mainAudioSource.isPlaying)
        {
            PlayRandomTrack();
        }
    }

    private void PlayRandomTrack()
    {
        int randomIndex = Random.Range(0, soundTracks.Length);
        mainAudioSource.clip = soundTracks[randomIndex];
        mainAudioSource.Play();
    }

    public void FadeOutCurrentTrack(float fadeDuration)
    {
        if (volumeFadeCoroutine != null)
        {
            StopCoroutine(volumeFadeCoroutine);
        }
        volumeFadeCoroutine = StartCoroutine(FadeCoroutine(0, fadeDuration));
    }

    public void FadeInCurrentTrack(float fadeDuration)
    {
        if (volumeFadeCoroutine != null)
        {
            StopCoroutine(volumeFadeCoroutine);
        }
        volumeFadeCoroutine = StartCoroutine(FadeCoroutine(volumeValue, fadeDuration));
    }

    private IEnumerator FadeCoroutine(float targetVolume, float fadeDuration)
    {
        float startVolume = mainAudioSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            float normalizedTime = (Time.time - startTime) / fadeDuration;
            mainAudioSource.volume = Mathf.Lerp(startVolume, targetVolume, normalizedTime);
            yield return null;
        }

        mainAudioSource.volume = targetVolume;
    }
}