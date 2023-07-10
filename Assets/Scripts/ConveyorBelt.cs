using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConveyorBelt : MonoBehaviour
{
    private float BeltMoveInterval = 2; // Intervalo de movimiento
    private float BeltMoveForce = 4;  // Fuerza de movimiento de la cinta transportadora
   
    public GameObject textureObject; // Objeto que tiene la textura

    private bool isMoving = true;
    [SerializeField] private GameObject belt;

    private void OnEnable()
    {
        // Iniciar ambas corutinas
        StartCoroutine(MoveBelt());
        StartCoroutine(MoveTextureOffset());
    }

    private IEnumerator MoveBelt()
    {
        

        while (isMoving)
        {
            // Mover los objetos
            belt.GetComponent<SurfaceEffector2D>().speed = BeltMoveForce;
           

            yield return new WaitForSeconds(1f);

            // Detener la cinta transportadora
            belt.GetComponent<SurfaceEffector2D>().speed = 0f;

            yield return new WaitForSeconds(BeltMoveInterval);
        }
    }

    private IEnumerator MoveTextureOffset()
    {
        float currentOffsetX = 0.0f;
        Renderer textureRenderer = textureObject.GetComponent<Renderer>();

        while (true)
        {
            // Mueve la textura hacia la derecha (aumentando el offset en X)
            while (currentOffsetX < 0.2f)
            {
                    
                currentOffsetX += Time.deltaTime * 0.2f;
                Vector2 offset = new Vector2(currentOffsetX, textureRenderer.material.mainTextureOffset.y);
                textureRenderer.material.SetTextureOffset("_MainTex", offset);

                yield return null;
            }

            // Detener la textura
            

            yield return new WaitForSeconds(BeltMoveInterval);

            // Reinicia el offset a cero para empezar de nuevo el ciclo
            currentOffsetX = 0.0f;
        }
    }
}


