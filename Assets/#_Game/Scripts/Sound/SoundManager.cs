using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {  get; private set; }

    [SerializeField] AudioClip menuSound;
    [SerializeField] AudioClip playSound;
    [SerializeField] AudioClip clickSFX;

    [SerializeField] AudioSource audioSource;

    private bool isOn;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        isOn = true;
        PlayMenuSound();
    }

    public void TurnOnOffSound()
    {
        isOn = !isOn;
        audioSource.volume = isOn ? 1 : 0;
    }

    public void PlayMenuSound()
    {
        audioSource.clip = menuSound;
        audioSource.Play();
    }

    public void PlayIngameSound()
    {
        audioSource.clip = menuSound;
        audioSource.Play();
    }

    public void PlayClickedSound()
    {
        if (!isOn) return;

        AudioSource.PlayClipAtPoint(clickSFX, transform.position);
    }
}
