using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> soundList = new List<AudioClip>(); // Lista de sonidos disponibles
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        if (soundList.Count > 0)
        {
            // Selecciona un �ndice aleatorio dentro del rango de la lista
            int randomIndex = Random.Range(0, soundList.Count);

            // Obt�n el sonido correspondiente al �ndice aleatorio
            AudioClip soundToPlay = soundList[randomIndex];

            // Asigna el sonido al AudioSource y reproduce
            audioSource.clip = soundToPlay;
            audioSource.Play();
        }
    }
}
