using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorOffset : MonoBehaviour
{
    public Renderer targetRenderer;
    public float moveSpeed = 0.1f;
    public float pauseTime = 1.0f;

    private Vector2 originalOffset;
    

    private void Start()
    {
        // Guarda el offset original de la textura
        originalOffset = targetRenderer.sharedMaterial.GetTextureOffset("_MainTex");

        // Inicia la corutina para mover la textura
        StartCoroutine(MoveTextureOffset());
    }

    private IEnumerator MoveTextureOffset()
    {
        float currentOffsetX = 0.0f;

        while (true)
        {
            // Mueve la textura hacia la derecha (aumentando el offset en X)
            while (currentOffsetX < 1.0f)
            {
             
                currentOffsetX += Time.deltaTime * moveSpeed;
                Vector2 offset = new Vector2(currentOffsetX, originalOffset.y);
                targetRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
                yield return null;
            }

            // Detiene la textura por un breve tiempo
          
            yield return new WaitForSeconds(pauseTime);

            // Reinicia el offset a cero para empezar de nuevo el ciclo
            currentOffsetX = 0.0f;
        }
    }
}
