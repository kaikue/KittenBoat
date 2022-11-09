using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource boatMusicSrc;
    public AudioSource mainIslandMusicSrc;
    public AudioSource puzzleIslandMusicSrc;
    public AudioSource shopMusicSrc;
    public AudioSource deepIslandMusicSrc;
    public AudioSource bossMusicSrc;

    private Dictionary<AudioSource, float> baseVolumes;

    private const float musicFadeTime = 1;

    private Coroutine crtFadeMusic;
    private AudioSource currentMusicSrc;

    private void Start()
    {
        currentMusicSrc = mainIslandMusicSrc;
        currentMusicSrc.Play();
        baseVolumes = new Dictionary<AudioSource, float>();
        AudioSource[] audioSources = {
            boatMusicSrc,
            mainIslandMusicSrc,
            puzzleIslandMusicSrc,
            shopMusicSrc,
            deepIslandMusicSrc,
            bossMusicSrc,
        };
        foreach (AudioSource audioSource in audioSources)
        {
            baseVolumes.Add(audioSource, audioSource.volume);
        }
    }

    public void SetMusic(AudioSource newMusic)
    {
        if (crtFadeMusic != null)
        {
            StopCoroutine(crtFadeMusic);
        }
        StartCoroutine(FadeMusic(newMusic));
    }

    private IEnumerator FadeMusic(AudioSource newSrc)
    {
        float oldVolume = currentMusicSrc.volume;
        for (float t = 0; t < musicFadeTime; t += Time.deltaTime)
        {
            currentMusicSrc.volume = Mathf.Lerp(oldVolume, 0, t / musicFadeTime);
            yield return null;
        }
        currentMusicSrc.volume = 0;
        currentMusicSrc.Pause();
        currentMusicSrc = newSrc;
        currentMusicSrc.volume = 0;
        currentMusicSrc.Play();
        float newVolume = baseVolumes[currentMusicSrc];
        for (float t = 0; t < musicFadeTime; t += Time.deltaTime)
        {
            currentMusicSrc.volume = Mathf.Lerp(0, newVolume, t / musicFadeTime);
            yield return null;
        }
        currentMusicSrc.volume = newVolume;
    }
}
