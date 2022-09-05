using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources References")]
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource playerAudioSource;
    [Header("Audio Clip References")]
    [SerializeField] private AudioClip laserAudioClip;
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip powerUpAudioClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayLaserSound() => playerAudioSource.PlayOneShot(laserAudioClip);
    public void PlayPowerUpSound() => playerAudioSource.PlayOneShot(powerUpAudioClip);
    public void PlayExplosionSound() => playerAudioSource.PlayOneShot(explosionAudioClip);
   
}
