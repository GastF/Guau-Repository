using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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


    [SerializeField] private Spawner DogSpawner;
    [SerializeField] private Spawner TrashSpawner1;
    [SerializeField] private Spawner TrashSpawner2;
    [SerializeField] private Spawner TrashSpawner3;

    private FirstObject elementDetector;
    [SerializeField] private float ObjectMoveForceLeft;
    [SerializeField] private float ObjectMoveForceRight;
    [SerializeField] private float ObjectMoveForceUp;

    [SerializeField] private GameObject SidPrefab;
    [SerializeField] private GameObject TerraneitorPrefab;
    [SerializeField] private GameObject BlanquiNegroPrefab;
    [SerializeField] private GameObject Bulb1;
    [SerializeField] private GameObject Bulb2;
    [SerializeField] private GameObject Bulb3;
  
    private Color offColor = new Color(164f / 255f, 126f / 255f, 4f / 255f, 1f);
    public Dictionary<string, GameObject> dogPrefabs;


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
        TrashSpawner3.enabled = false;
        time = 0;
        
        
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeUI.text = time.ToString("0");
        if (score <= 0) { scoreUI.text = $"Score:0"; score = 0; }
        else if (score > 0) { scoreUI.text = $"Score:{score.ToString()}"; }
        
        switch (hp)
        {
            case 0:
                Bulb3.GetComponent<SpriteRenderer>().color = offColor;
                break;
            case 1:
                Bulb2.GetComponent<SpriteRenderer>().color = offColor;
                break;
            case 2:
                Bulb1.GetComponent<SpriteRenderer>().color = offColor;
                break;
        }
        #region Difficulty Over Time
        switch (time)
        {
            case > 25 when time <= 50:
                DogSpawner.spawnInterval = 2f;
                Debug.Log("Dog Spawn Interval = 2");
                break;
            case > 50 when time <= 75:
                TrashSpawner1.enabled = true;
               
                break;
            case > 75 when time <= 100:

                DogSpawner.spawnInterval = 1.9f;
                Debug.Log("Dog Spawn Interval = 1.9");
                break;
            case > 100 when time <= 125:
                
                TrashSpawner2.enabled = true;
                DogSpawner.spawnInterval = 1.8f;
                Debug.Log("Dog Spawn Interval = 1.8");
                break;
            case > 100 when time <= 150:

               
                break;
            case > 200:
                Debug.Log("Dog Spawn Interval = 1.7");
                DogSpawner.spawnInterval = 1.7f;
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
                string objectTag = firstElement.tag;

                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    // Aplicar una fuerza hacia la derecha al objeto detectado
                    rb2d.AddForce(Vector2.right * ObjectMoveForceRight, ForceMode2D.Impulse);
                    
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    // Aplicar una fuerza hacia arriba al objeto detectado
                    
                    rb2d.AddForce(Vector2.up * ObjectMoveForceUp, ForceMode2D.Impulse);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    // Aplicar una fuerza hacia la izquierda al objeto detectado
                    rb2d.AddForce(Vector2.left * ObjectMoveForceLeft, ForceMode2D.Impulse);
                  
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    // Aplicar una fuerza hacia arriba a la izquierda al objeto detectado
                    rb2d.AddForce(Vector2.left * ObjectMoveForceLeft, ForceMode2D.Impulse);
                    rb2d.AddForce(Vector2.up * ObjectMoveForceUp, ForceMode2D.Impulse);

                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    // Aplicar una fuerza hacia arriba a la derecha al objeto detectado
                    rb2d.AddForce(Vector2.right * ObjectMoveForceRight, ForceMode2D.Impulse);
                    rb2d.AddForce(Vector2.up * ObjectMoveForceUp, ForceMode2D.Impulse); 

                }

                int dogLayer = firstElement.gameObject.layer;

                if (dogLayer == LayerMask.NameToLayer("Hat"))
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        string dogName = GetDogName(firstElement);

                        if (dogName == "Terraneitor")
                        {
                            Instantiate(TerraneitorPrefab, firstElement.transform.position, firstElement.transform.rotation);
                            Destroy(firstElement);
                        }
                        else if (dogName == "Sid")
                        {
                            Instantiate(SidPrefab, firstElement.transform.position, firstElement.transform.rotation);
                            Destroy(firstElement);
                        }
                        else if (dogName == "BlanquiNegro")
                        {
                            Instantiate(BlanquiNegroPrefab, firstElement.transform.position, firstElement.transform.rotation);
                            Destroy(firstElement);
                        }

                    }
                }
                else if (dogLayer == LayerMask.NameToLayer("Scarf"))
                {
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        string dogName = GetDogName(firstElement);

                        if (dogName == "Terraneitor")
                        {
                            Instantiate(TerraneitorPrefab, firstElement.transform.position, firstElement.transform.rotation);
                            Destroy(firstElement);
                        }
                        else if (dogName == "Sid")
                        {
                            Instantiate(SidPrefab, firstElement.transform.position, firstElement.transform.rotation);
                            Destroy(firstElement);
                        }
                        else if (dogName == "BlanquiNegro")
                        {
                            Instantiate(BlanquiNegroPrefab, firstElement.transform.position, firstElement.transform.rotation);
                            Destroy(firstElement);
                        }

                    }
                }
                else if (dogLayer == LayerMask.NameToLayer("Boot"))
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        string dogName = GetDogName(firstElement);

                        if (dogName == "Terraneitor")
                        {
                            Instantiate(TerraneitorPrefab, firstElement.transform.position, firstElement.transform.rotation);
                            Destroy(firstElement);
                        }
                        else if (dogName == "Sid")
                        {
                            Instantiate(SidPrefab, firstElement.transform.position, firstElement.transform.rotation);
                            Destroy(firstElement);
                        }
                        else if (dogName == "BlanquiNegro")
                        {
                            Instantiate(BlanquiNegroPrefab, firstElement.transform.position, firstElement.transform.rotation);
                            Destroy(firstElement);
                        }

                    }
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

    string GetDogName(GameObject dogObject)
    {
        // Obtener el nombre del GameObject
        string dogName = dogObject.tag;

        // Devolver el nombre del perro
        return dogName;
    }

}
