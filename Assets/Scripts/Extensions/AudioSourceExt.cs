using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceExt
{
    public static IEnumerator CrossFade(
            this AudioSource audioSource,
            AudioClip newSound,
            float finalVolume,
            float fadeTime)
    {
        // in here we wait until the FadeOut coroutine is done
        yield return FadeOut(audioSource, fadeTime);
        audioSource.clip = newSound;
        yield return FadeIn(audioSource, fadeTime, finalVolume);
    }

    public static IEnumerator FadeOut(this AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = 0;
    }
    public static IEnumerator FadeIn(this AudioSource audioSource, float fadeTime, float finalVolume)
    {
        float startVolume = 0.2f;
        audioSource.volume = 0;
        audioSource.Play();
        while (audioSource.volume < finalVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.volume = finalVolume;
    }
}
