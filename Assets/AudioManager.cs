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
        if (!dialogSource.loop)
        {
            dialogSource.loop = true;
            dialogSource.clip = clip;
            dialogSource.Play();
        }
    }

    public void PlayDialogSFX(AudioClip clip, float pitch)
    {
        if (!dialogSource.loop)
        {
            dialogSource.pitch = pitch;
            this.PlayDialogSFX(clip);
        }
    }

    public void StopDialogSFX()
    {
        dialogSource.loop = false;
        this.ResetPitch();
    }

    public void ResetPitch()
    {
        dialogSource.pitch = 1f;
    }
}
