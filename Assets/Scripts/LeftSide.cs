using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSide : MonoBehaviour
{
    [SerializeField] private GameObject vfxPrefab;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DogClothed"))  // Comprueba si el objeto tiene la etiqueta "Dog"
        {
            // Incrementa los puntajes
            GameManager.Instance.AddScore(10);
        }
        else
        {
            GameManager.Instance.DecreaseHP(1);
        }

        Destroy(collision.gameObject);

        // Instanciar el VFX en la posición deseada
        Instantiate(vfxPrefab, collision.contacts[0].point, Quaternion.identity);
    }
}
