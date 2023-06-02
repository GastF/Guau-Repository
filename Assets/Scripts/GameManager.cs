using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     private int score = 0;
     private int hp = 3;
     private float time = 0;

    [SerializeField] private Text hpUI;
    [SerializeField] private Text scoreUI;
    [SerializeField] private Text gameoverUI;
    [SerializeField] private Text timeUI;

    [SerializeField] private Spawner TrashSpawner;
    [SerializeField] private Spawner TrashSpawner1;
    [SerializeField] private Spawner TrashSpawner2;

    private FirstObject elementDetector;
    [SerializeField] private float ObjectMoveForceLeft;
    [SerializeField] private float ObjectMoveForceRight;
    [SerializeField] private float ObjectMoveForceUp;
    // Patrón Singleton para acceder al GameManager desde otros scripts
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Evita la creación de múltiples instancias del GameManager
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Conserva el GameManager entre escenas
        }
        
    }

    private void Start()
    {
        elementDetector = FindObjectOfType<FirstObject>();
        gameoverUI.enabled = false; 
        TrashSpawner1.enabled = false;
        TrashSpawner2.enabled = false; 
        time = 0;
        
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeUI.text = time.ToString("0");
        if (score <= 0) { scoreUI.text = $"Score:0"; score = 0; }
        else if (score > 0) { scoreUI.text = $"Score:{score.ToString()}"; }
        
        #region Spawners
        switch (time)
        {
            case > 50 when time <= 100:
                TrashSpawner1.enabled = true;
                break;

            case > 100:
                TrashSpawner2.enabled = true;
                break;
        }
        #endregion
        #region RayCast
        if (elementDetector != null)
        {
            GameObject firstElement = elementDetector.GetFirstElement();

            if (firstElement != null)
            {
                Rigidbody2D rb2d = firstElement.GetComponent<Rigidbody2D>();


                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    // Aplicar una fuerza hacia la izquierda al objeto detectado
                    rb2d.AddForce(Vector2.left * ObjectMoveForceLeft, ForceMode2D.Impulse);
                    
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    // Aplicar una fuerza hacia la derecha al objeto detectado
                    rb2d.AddForce(Vector2.right * ObjectMoveForceRight, ForceMode2D.Impulse);
                    rb2d.AddForce(Vector2.up * ObjectMoveForceUp, ForceMode2D.Impulse);
                }
            }
        }
        #endregion

    }

    public void AddScore(int points)
    {
        
        score += points;
        Debug.Log("Puntuación actual: " + score);
        scoreUI.text = $"Score:{score.ToString()}";
        
    }
    public void DecreseScore(int points)
    {
        
            score -= points;
            Debug.Log("Puntuación actual: " + score);

    }

    public void DecreaseHP(int amount)
    {
        hp -= amount;
        Debug.Log("Puntos de vida restantes: " + hp);
        hpUI.text = $"HP:{hp.ToString()}";

        if (hp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameoverUI.enabled = true;
      
        Debug.Log("¡Game Over!");
        gameoverUI.text = "¡Game Over!";
        Time.timeScale = 0;
    }

   
}
