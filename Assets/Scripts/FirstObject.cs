using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstObject : MonoBehaviour
{
    private GameObject firstElement;
    [SerializeField] private Transform rayCastPlace;

    private LayerMask objectsInBeltLayer;
    public float detectionRange = 14f;
    private bool isDetectingObject = false;
    private RaycastHit2D hit;

    // Referencia al material para resaltar el objeto seleccionado
    [SerializeField] private Material highlightedMaterialDressed;
    [SerializeField] private Material highlightedMaterialHat;
    [SerializeField] private Material highlightedMaterialScarf;
    [SerializeField] private Material highlightedMaterialBoot;
    private Material originalMaterial; // Almacenar el material original del objeto

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector2 rayDirection = Vector2.left;
        

        if (rayCastPlace != null) 
        {  
            hit = Physics2D.Raycast(rayCastPlace.position, rayDirection, detectionRange); 
            Debug.DrawRay(rayCastPlace.position, rayDirection * detectionRange, Color.red);
        }
        

       

        if (hit.collider != null)
        {
            GameObject detectedObject = hit.collider.gameObject;

            if (detectedObject != firstElement)
            {
                // Restaurar el material original del objeto anterior
                RestoreOriginalMaterial();

                firstElement = detectedObject;
                isDetectingObject = true;

                // Almacenar el material original del nuevo objeto
                Renderer objectRenderer = firstElement.GetComponent<Renderer>();
                originalMaterial = objectRenderer.material;

                // Resaltar el nuevo objeto seleccionado
                int dogLayer = firstElement.gameObject.layer;

                if (dogLayer == LayerMask.NameToLayer("Hat")) 
                {
                    objectRenderer.material = highlightedMaterialHat;
                }
                else if (dogLayer == LayerMask.NameToLayer("Scarf")) 
                {
                    objectRenderer.material = highlightedMaterialBoot;
                }
                else if (dogLayer == LayerMask.NameToLayer("Boot")) 
                {
                    objectRenderer.material = highlightedMaterialScarf;
                   
                }
                else if (dogLayer == LayerMask.NameToLayer("DogClothed"))
                {
                    objectRenderer.material = highlightedMaterialDressed;

                }
                else 
                {
                    objectRenderer.material = highlightedMaterialDressed;
                }

            }
        }
        else
        {
            if (firstElement != null)
            {
                // Restaurar el material original cuando no se detecta ningún objeto
                RestoreOriginalMaterial();

                firstElement = null;
                isDetectingObject = false;
            }
        }

        // Verificar si el objeto aún existe antes de acceder a su posición
        if (firstElement != null)
        {
            Transform elementTransform = firstElement.transform;
            if (elementTransform != null)
            {
                transform.position = elementTransform.position;
            }
        }

      
    }

    public GameObject GetFirstElement()
    {
        if (isDetectingObject)
            return firstElement;

        return null;
    }

    public void ClearFirstElement()
    {
        // Restaurar el material original al limpiar el objeto seleccionado
        RestoreOriginalMaterial();

        firstElement = null;
        isDetectingObject = false;
    }

    private void RestoreOriginalMaterial()
    {
        // Verificar si hay un objeto seleccionado y si tiene un Renderer y un material original
        if (firstElement != null && firstElement.TryGetComponent(out Renderer objectRenderer) && originalMaterial != null)
        {
            // Restaurar el material original del objeto
            objectRenderer.material = originalMaterial;
            originalMaterial = null;
        }
    }


}
