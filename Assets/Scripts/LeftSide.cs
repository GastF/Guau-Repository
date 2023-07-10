using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSide : MonoBehaviour
{
    public List<AudioClip> soundList = new List<AudioClip>(); // Lista de sonidos disponibles

    public float minPitch = 0.8f; // Valor mínimo del pitch
    public float maxPitch = 1.2f; // Valor máximo del pitch
    private float elapsedTime;
    private float currentPitch;
    public float timeToResetPitch = 1f; // Tiempo para volver al pitch normal
    private bool increasingPitch = true;
    private float startTime;

    [SerializeField] private VFXActivator vfxWorst;
    [SerializeField] private VFXActivator vfxGood;
    [SerializeField] private VFXActivator vfxBad;


    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource sfxSad;

    private void Start()
    {
        currentPitch = minPitch;
       
    }
  
    
    public void PlayRandomSound()
    {
        if (soundList.Count > 0)
        {
            // Selecciona un índice aleatorio dentro del rango de la lista
            int randomIndex = Random.Range(0, soundList.Count);

            // Obtén el sonido correspondiente al índice aleatorio
            AudioClip soundToPlay = soundList[randomIndex];

            // Asigna el sonido al AudioSource y reproduce
            sfxSad.clip = soundToPlay;
            sfxSad.Play();
        }
    }
    public void PlayWithRandomPitch()
    {
        // Si ha pasado el tiempo necesario para volver al pitch normal, reseteamos el pitch
        if (Time.time - startTime > timeToResetPitch)
        {
            currentPitch = 1f; // Aquí podrías asignar el valor de pitch normal que desees (usualmente es 1)
            increasingPitch = true;
        }

        // Asigna el valor de pitch actual al AudioSource
        sfx.pitch = currentPitch;

        // Reproduce el sonido
        sfx.Play();

        // Incrementa o decrementa el valor del pitch según la dirección actual
        if (increasingPitch)
        {
            currentPitch += 0.1f;
            if (currentPitch > maxPitch)
            {
                currentPitch = maxPitch;
                increasingPitch = false;
            }
        }
        else
        {
            currentPitch -= 0.1f;
            if (currentPitch <= minPitch)
            {
                currentPitch = minPitch;
                increasingPitch = true;
            }
        }

        // Guardamos el tiempo de inicio de la reproducción del sonido
        startTime = Time.time; ;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DogClothed"))  // Comprueba si el objeto tiene la etiqueta "Dog"
        {
            // Incrementa los puntajes
            GameManager.Instance.AddScore(10);
            PlayWithRandomPitch();
            vfxGood.ActivateVFX();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Hat") || collision.gameObject.layer == LayerMask.NameToLayer("Scarf") || collision.gameObject.layer == LayerMask.NameToLayer("Boot"))
        {
            GameManager.Instance.DecreseScore(10);
            PlayRandomSound();
            vfxBad.ActivateVFX();
        }
        else
        {
            vfxWorst.ActivateVFX();
            GameManager.Instance.DecreaseHP(1);
            
        }

        Destroy(collision.gameObject);

       
    }
}
