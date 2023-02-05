using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Settings")]
    public AudioClip introMusic;
    public AudioClip gameMusic;
    public AudioClip[] sounds;

    [Header("AudioSource")]
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSounds;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        audioSourceMusic = GetComponent<AudioSource>();
    }

    public void PlayIntroMusic() {
        audioSourceMusic.clip = introMusic;
        audioSourceMusic.Play();
    }

    public void PlayGamePlayMusic() {
        audioSourceMusic.Stop();
        audioSourceMusic.clip = gameMusic;
        audioSourceMusic.Play();
    }

    public void PlaySound(int id) {
        audioSourceSounds.PlayOneShot(sounds[id]);
    }
}
