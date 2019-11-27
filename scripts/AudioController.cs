using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source: https://medium.com/@wyattferguson/how-to-fade-out-in-audio-in-unity-8fce422ab1a8
//Call with: StartCoroutine(AudioController.FadeOut(audioSource, 1f));

public static class AudioController
{
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }
    //coroutine to fade the main sound out


    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
    //coroutine to fade the main sound in
}
