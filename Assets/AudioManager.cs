using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundSource;
    public AudioSource dialogSource;
    public AudioSource musicSource;
    public AudioClip background;
    public AudioClip dialogWrite;
    public AudioClip music;

    private void Start()
    {
        backgroundSource.clip = background;
        backgroundSource.Play();
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlayDialogSFX(AudioClip clip)
    {
        dialogSource.loop = true;
        dialogSource.clip = clip;
        dialogSource.Play();
    }

    public void StopDialogSFX()
    {
        dialogSource.loop = false;
    }
}
