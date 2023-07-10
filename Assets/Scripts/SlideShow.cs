using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlideShow : MonoBehaviour
{
    public Image image; // Referencia al componente Image donde se muestra la imagen
    public Sprite[] images; // Arreglo de sprites de las im�genes
    public Image imageSecond; // Referencia al componente Image donde se muestra la imagen
    public Sprite[] imagesSecond; // Arreglo de sprites de las im�genes

    private int currentIndex = 0; // �ndice de la imagen actual

    private void Start()
    {
        // Mostrar la primera imagen al iniciar
        ShowImage(currentIndex);
    }

    public void NextImage(string sceneName)
    {
        // Avanzar al siguiente �ndice de imagen
        currentIndex++;
        if (currentIndex >= images.Length)
        {
            currentIndex = images.Length; // Volver al primer �ndice si se alcanza el final del arreglo
            SceneManager.LoadScene(sceneName);
        }

        // Mostrar la nueva imagen
        ShowImage(currentIndex);
    }

    public void PreviousImage()
    {
        // Retroceder al �ndice anterior de imagen
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex= 0;
        }

        // Mostrar la nueva imagen
        ShowImage(currentIndex);
    }

    private void ShowImage(int index)
    {
        // Mostrar la imagen correspondiente al �ndice dado
        if (index >= 0 && index < images.Length)
        {
            image.sprite = images[index];
            imageSecond.sprite = imagesSecond[index];
        }
      
    }

    public void SkipTutorial(string sceneName) 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
