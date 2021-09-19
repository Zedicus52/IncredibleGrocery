using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    public void PlayEffect(AudioClip clip)
    {
        _audioSource.pitch = Random.Range(1.0f, 2.5f);
        _audioSource.PlayOneShot(clip);
    }
}
