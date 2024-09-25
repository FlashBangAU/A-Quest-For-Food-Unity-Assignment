using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager not found in the scene!");
        }
    }

    public void SetMusicVolume(float musicVolume)
    {
        Debug.Log(musicVolume);
        if (audioManager != null)
        {
            audioManager.SetMusicVolume(musicVolume);
        }
    }

    public void SetSFXVolume(float sfxVolume)
    {
        Debug.Log(sfxVolume);
        if (audioManager != null)
        {
            audioManager.SetSFXVolume(sfxVolume);
        }
    }
}