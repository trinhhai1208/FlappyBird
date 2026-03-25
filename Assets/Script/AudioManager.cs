using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header ("Audio Clips")]
    public AudioClip flapClip;
    public AudioClip hitClip;
    public AudioClip scoreClip;
    public AudioClip dieClip;
    public AudioClip swooshClip;

    private AudioSource audioSource;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayFlap() => audioSource.PlayOneShot(flapClip);
    public void PlayHit() => audioSource.PlayOneShot(hitClip);
    public void PlayScore() => audioSource.PlayOneShot(scoreClip);
    public void PlayDie() => audioSource.PlayOneShot(dieClip);
    public void PlaySwoosh() => audioSource.PlayOneShot(swooshClip);
}
