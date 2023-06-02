using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConveyorBelt : MonoBehaviour
{
    public float BeltMoveInterval = 1f; // Intervalo de movimiento
    public float BeltMoveForce = 5f; // Fuerza de movimiento de la cinta transportadora

    private bool isMoving = false;
    [SerializeField] private GameObject belt;
    private void Start()
    {
        StartCoroutine(MoveBelt());
        
    }

    private IEnumerator MoveBelt()
    {
        isMoving = true;

        while (isMoving)
        {
            // Mover los objetos
            belt.GetComponent<SurfaceEffector2D>().speed = -BeltMoveForce;

            yield return new WaitForSeconds(BeltMoveInterval);

            // Detener la cinta transportadora
            belt.GetComponent<SurfaceEffector2D>().speed = 0f;

            yield return new WaitForSeconds(BeltMoveInterval);
        }
    }
}


