using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Eraser : MonoBehaviour
{
   
    [SerializeField] private VFXActivator vfxActivator;
    [SerializeField] private VFXActivator vfxDog;

    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource sfxDog;
    [SerializeField] private AudioSource sfxBox;

    [SerializeField]private float moveSpeedX = 5f; // Velocidad de movimiento horizontal
    [SerializeField]private float moveSpeedY = 5f; // Velocidad de movimiento horizontal
    [SerializeField]private float moveDuration = 1.0f; // Duraci�n del movimiento vertical despu�s de la colisi�n

    private float startY; // Posici�n inicial en el eje Y
    [SerializeField]private float newY;
  
    [SerializeField]private Vector3 leftPosition; // Posici�n a la izquierda
    [SerializeField]private Vector3 rightPosition; // Posici�n a la derecha

    private bool movingHorizontally = true; // Indicador de movimiento horizontal activo
    private bool movingToLeft = true; // Indicador de movimiento hacia la izquierda

    private void Start()
    {
        startY = transform.position.y;
        transform.position = rightPosition; 

    }

    private void Update()
    {
        if (movingHorizontally) { 
        if (movingToLeft)
        {
            // Mover hacia la posici�n a la izquierda
            transform.position = Vector3.MoveTowards(transform.position, leftPosition, moveSpeedX * Time.deltaTime);

            if (transform.position == leftPosition)
            {
                movingToLeft = false; // Cambiar direcci�n hacia la derecha
            }
        }
        else
        {
            // Mover hacia la posici�n a la derecha
            transform.position = Vector3.MoveTowards(transform.position, rightPosition, moveSpeedX * Time.deltaTime);

            if (transform.position == rightPosition)
            {
                movingToLeft = true; // Cambiar direcci�n hacia la izquierda
            }
        }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        movingHorizontally = false; // Desactivar movimiento horizontal
        sfx.Play();
        StartCoroutine(MoveUpAndDown());
       
        if (collision.gameObject.CompareTag("Sid") || collision.gameObject.CompareTag("BlanquiNegro") || collision.gameObject.CompareTag("Terraneitor"))
        {
            vfxDog.ActivateVFX();
            sfxDog.Play();
            Debug.Log("dog");
            Destroy(collision.gameObject);
            GameManager.Instance.DecreseScore(30);

        }
        else
        {
            sfxBox.Play();
            Destroy(collision.gameObject);
            vfxActivator.ActivateVFX(); // Activa el VFX desde el GameObject aparte
        }
    }

    private IEnumerator MoveUpAndDown()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < moveDuration)
        {
            // Movimiento hacia arriba
            float t = elapsedTime / moveDuration;
            transform.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x, startY + newY, startPosition.z), t);
            elapsedTime += Time.deltaTime * moveSpeedY;
            yield return null;
        }

        // Esperar un breve tiempo antes de moverse hacia abajo
        yield return new WaitForSeconds(0.5f);

        elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            // Movimiento hacia abajo
            float t = elapsedTime / moveDuration;
            transform.position = Vector3.Lerp(new Vector3(startPosition.x, startY + newY, startPosition.z), startPosition, t);
            elapsedTime += Time.deltaTime * moveSpeedY;
            yield return null;
        }

        movingHorizontally = true; // Reactivar movimiento horizontal
    }
}


