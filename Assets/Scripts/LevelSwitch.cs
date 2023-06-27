using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSwitch : MonoBehaviour
{
    public void ChangeLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ResumeGame(GameObject pauseMenu)
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }
    public void restartLevel(GameObject pauseMenu)
    {
        Time.timeScale = 1.0f;
       
        pauseMenu.SetActive(false);
        
    }
    public void restartLevelP(GameObject pauseMenu)
    {
        Time.timeScale = 1.0f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        pauseMenu.SetActive(false);

    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1.0f;
        
    }
    public void ReturnToMainMenuP()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
    


    public void RestartLevelWithSound(GameObject pauseMenu)
    {
        StartCoroutine(RestartLevelCoroutine(pauseMenu));
    }

    IEnumerator RestartLevelCoroutine(GameObject pauseMenu)
    {
        // Reproducir sonido
       

        // Esperar un breve momento
        yield return new WaitForSeconds(0.5f);  // Ajusta el tiempo según sea necesario

        // Reiniciar el nivel
        Time.timeScale = 1.0f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        pauseMenu.SetActive(false);
    }

    public void ReturnToMainMenuWithSound()
    {
        StartCoroutine(ReturnToMainMenuCoroutine());
    }

    IEnumerator ReturnToMainMenuCoroutine()
    {
        // Reproducir sonido
       

        // Esperar un breve momento
        yield return new WaitForSeconds(0.5f);  // Ajusta el tiempo según sea necesario

        // Regresar al menú principal
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("MainMenu");
        }

}
