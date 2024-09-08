using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clips -------")]
    //Add more where required
    public AudioClip background;
    public AudioClip playerDeath;
    public AudioClip playerAttack;
    public AudioClip playerDamage;
    public AudioClip playerJump;
    public AudioClip playerLand;
    public AudioClip enemyHurt;
    public AudioClip enemyAttack;
    public AudioClip foodPickup;
    public AudioClip levelComplete;
    public AudioClip checkpoint;

    //Starts background music on scene start
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    //When given an AudioClip parameter from a different script, plays SFX using the SFX Audio Source
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
