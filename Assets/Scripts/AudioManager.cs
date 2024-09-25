using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.Audio;
>>>>>>> sound-branch

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

<<<<<<< HEAD
    [Header("------- Audio Clips -------")]
    //Add more where required
    public AudioClip background;
=======
    [Header("------- Audio Mixer -------")]
    public AudioMixer mainMixer;

    [Header("------- Audio Clips -------")]
    // Add more where required
    public AudioClip background;
    public AudioClip button;
>>>>>>> sound-branch
    public AudioClip playerDeath;
    public AudioClip playerAttack;
    public AudioClip playerDamage;
    public AudioClip playerJump;
    public AudioClip playerLand;
    public AudioClip enemyHurt;
    public AudioClip enemyAttack;
    public AudioClip enemySquish;
    public AudioClip foodPickup;
    public AudioClip levelComplete;
    public AudioClip checkpoint;

<<<<<<< HEAD
    //Starts background music on scene start
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    //When given an AudioClip parameter from a different script, plays SFX using the SFX Audio Source
=======
    // Starts background music on scene start
    private void Start()
    {
        // Ensure AudioSources are using the correct mixer groups
        musicSource.outputAudioMixerGroup = mainMixer.FindMatchingGroups("Music")[0];
        SFXSource.outputAudioMixerGroup = mainMixer.FindMatchingGroups("SFX")[0];

        musicSource.clip = background;
        musicSource.Play();

        // Apply saved volume settings
        ApplySavedVolumes();
    }

    // When given an AudioClip parameter from a different script, plays SFX using the SFX Audio Source
>>>>>>> sound-branch
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
<<<<<<< HEAD
}
=======

    private void ApplySavedVolumes()
    {
        if (DataManager.Instance != null)
        {
            float musicVolume = DataManager.Instance.musicVolume;
            float sfxVolume = DataManager.Instance.sfxVolume;

            mainMixer.SetFloat("MusicVolume", musicVolume);
            mainMixer.SetFloat("SFXVolume", sfxVolume);
        }
        else
        {
            Debug.LogWarning("DataManager instance not found!");
        }
    }

    public void SetMusicVolume(float musicVolume)
    {
        Debug.Log(musicVolume);
        mainMixer.SetFloat("MusicVolume", musicVolume);
        DataManager.Instance.musicVolume = musicVolume; // Save to DataManager
        DataManager.Instance.WriteData(); // Save the data to persistent storage
    }

    public void SetSFXVolume(float sfxVolume)
    {
        Debug.Log(sfxVolume);
        mainMixer.SetFloat("SFXVolume", sfxVolume);
        DataManager.Instance.sfxVolume = sfxVolume; // Save to DataManager
        DataManager.Instance.WriteData(); // Save the data to persistent storage
    }
}
>>>>>>> sound-branch
