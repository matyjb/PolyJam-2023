using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Settings")]
    public AudioClip introMusic;
    public AudioClip gameMusic;

    AudioSource audioSource;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayIntroMusic() {
        audioSource.clip = introMusic;
        audioSource.Play();
    }

    public void PlayGamePlayMusic() {
        audioSource.clip = gameMusic;
        audioSource.Play();
    }
}
