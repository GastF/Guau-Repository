using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     private int score = 0;
     private int hp = 3;
     private float time = 0;
     private bool paused = false;
     private bool gameover = false;

    [SerializeField] private Text hpUI;
    [SerializeField] private Text scoreUI;
    [SerializeField] private Text highscoreUI;
    [SerializeField] private Text timeUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameoverUI;

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

    [SerializeField] private  Animator bulb;
    
    [SerializeField] private AudioClip[] sfxClip;
    [SerializeField] private AudioSource sfx;
 
    [SerializeField] private AudioSource bgSfx;
    [SerializeField] private AudioSource endSfx;
    [SerializeField] private AudioSource machineSfx;


    private Color offColor = new Color(164f / 255f, 126f / 255f, 4f / 255f, 1f);
    public Dictionary<string, GameObject> dogPrefabs;


    // Patrón Singleton para acceder al GameManager desde otros scripts
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        Time.timeScale = 1.0f;
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Evita la creación de múltiples instancias del GameManager
        }
        else
        {
            instance = this;
          
        }
        highscoreUI.text = "High Score:" + PlayerPrefs.GetInt("highscore");
    }

    private void Start()
    {
        elementDetector = FindObjectOfType<FirstObject>();
        gameoverUI.SetActive(false);
        TrashSpawner1.enabled = false;
        TrashSpawner2.enabled = false; 
        TrashSpawner3.enabled = false;
        scoreUI.enabled = false;
        highscoreUI.enabled = false;
        bgSfx.enabled = true;
        endSfx.enabled = false;
        pauseMenu.SetActive(false);
        time = 0;
        score = 0;
        DogSpawner.SpawnObject();
        

    }

    private void Update()
    {
        time += Time.deltaTime;
        timeUI.text = time.ToString("0");
        if (score <= 0) { scoreUI.text = $"Puntuacion:0"; score = 0; }
        else if (score > 0) { scoreUI.text = $"Puntuacion:{score.ToString()}"; }
        
        switch (hp)
        {
            case 0:
                bulb.SetTrigger("bublHP3");
                break;
            case 1:
                bulb.SetTrigger("bublHP2");
                break;
            case 2:
                bulb.SetTrigger("bublHP1");
                break;
        }
        #region Difficulty Over Time
        switch (time)
        {
            case > 25 when time <= 50:
                DogSpawner.spawnInterval = 4f;
                Debug.Log("Dog Spawn Interval = 4");
                break;
            case > 50 when time <= 75:
                TrashSpawner1.enabled = true;
               
                break;
            case > 75 when time <= 100:

                DogSpawner.spawnInterval = 3f;
                Debug.Log("3f");
                break;
            case > 100 when time <= 125:
                
                
             //   DogSpawner.spawnInterval = 2.2f;
                Debug.Log("Segundo trash");
                break;
            case > 125 when time <= 150:
                DogSpawner.spawnInterval = 2.5f;
                Debug.Log("Dog Spawn Interval = 2.5");
                break;
            case > 150 when time <= 250:
                DogSpawner.spawnInterval = 2.3f;
                Debug.Log("Dog Spawn Interval = 2.3");
                break;
            case > 300:
                TrashSpawner2.enabled = true;
                Debug.Log("Dog Spawn Interval = 2f");
                DogSpawner.spawnInterval = 2f;
                break;
        }
        #endregion
        #region RayCast
        if (elementDetector != null)
        {
            GameObject firstElement = elementDetector.GetFirstElement();
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Z))
            {
                PlayAudioClip(4);
                sfx.Play();
            }
            if (firstElement != null)
            {
                Rigidbody2D rb2d = firstElement.GetComponent<Rigidbody2D>();    
                string objectTag = firstElement.tag;

                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    // Aplicar una fuerza hacia la derecha al objeto detectado
                    rb2d.AddForce(Vector2.right * ObjectMoveForceRight, ForceMode2D.Impulse);
                    
                    machineSfx.Play();


                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    // Aplicar una fuerza hacia arriba al objeto detectado
                    
                    rb2d.AddForce(Vector2.up * ObjectMoveForceUp, ForceMode2D.Impulse);
                    machineSfx.Play();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                   
                    // Aplicar una fuerza hacia la izquierda al objeto detectado
                    rb2d.AddForce(Vector2.left * ObjectMoveForceLeft, ForceMode2D.Impulse);
                    machineSfx.Play();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    // Aplicar una fuerza hacia arriba a la izquierda al objeto detectado
                    rb2d.AddForce(Vector2.left * ObjectMoveForceLeft, ForceMode2D.Impulse);
                    rb2d.AddForce(Vector2.up * ObjectMoveForceUp, ForceMode2D.Impulse);
                    machineSfx.Play();

                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    // Aplicar una fuerza hacia arriba a la derecha al objeto detectado
                    rb2d.AddForce(Vector2.right * ObjectMoveForceRight, ForceMode2D.Impulse);
                    rb2d.AddForce(Vector2.up * ObjectMoveForceUp, ForceMode2D.Impulse);
                    machineSfx.Play();

                }
                
                int dogLayer = firstElement.gameObject.layer;

                if (dogLayer == LayerMask.NameToLayer("Hat"))
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        PlayAudioClip(0);
                        sfx.Play();
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
                    else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C))
                    {
                        PlayAudioClip(4);
                        sfx.Play();
                    }
                }
                else if (dogLayer == LayerMask.NameToLayer("Scarf"))
                {
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        PlayAudioClip(1);
                        sfx.Play();
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
                    else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
                    {
                        PlayAudioClip(4);
                        sfx.Play();
                    }
                }
                else if (dogLayer == LayerMask.NameToLayer("Boot"))
                {
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        PlayAudioClip(2);
                        sfx.Play();
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
                    else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                    {
                        PlayAudioClip(4);
                        sfx.Play();
                    }
                }

               
            }
        }
        #endregion
        if (!gameover)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (paused)
                {
                    Time.timeScale = 1.0f;
                    pauseMenu.SetActive(false);
                    paused = false;
                }
                else
                {
                    paused = true;
                    Time.timeScale = 0.0f;
                    pauseMenu.SetActive(true);
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow)) 
        { 
            machineSfx.Play();
        }
      
    }

    public void AddScore(int points)
    {
        
        score += points;
        Debug.Log("Puntuación actual: " + score);
        scoreUI.text = $"Puntuacion:{score.ToString()}";

        if (score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        highscoreUI.text = "Mejor puntuacion:" + PlayerPrefs.GetInt("highscore");

    }
    public void DecreseScore(int points)
    {
        
            score -= points;
            Debug.Log("Puntuación actual: " + score);

    }

    public void DecreaseHP(int amount)
    {
        PlayAudioClip(5);
        sfx.Play();
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
        gameoverUI.SetActive(true);
        scoreUI.enabled = true;
        highscoreUI.enabled = true;
        bgSfx.enabled = false;
        endSfx.enabled = true;
        gameover = true;
        Debug.Log("¡Game Over!");

        StartCoroutine(ChangeTimeScaleWithDelay());
        if (score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        highscoreUI.text = "Mejor puntuacion:" + PlayerPrefs.GetInt("highscore");
    }

    public void PlayAudioClip(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < sfxClip.Length)
        {
             sfx.clip = sfxClip[clipIndex];
             sfx.Play();
        }
        else
        {
            Debug.LogWarning("Invalid clip index.");
        }
    }
    string GetDogName(GameObject dogObject)
    {
        // Obtener el nombre del GameObject
        string dogName = dogObject.tag;

        // Devolver el nombre del perro
        return dogName;
    }

    private IEnumerator ChangeTimeScaleWithDelay()
    {
        // Esperar el delay antes de cambiar el Time.timeScale
        yield return new WaitForSeconds(1);

        // Cambiar el Time.timeScale a 0
        Time.timeScale = 0;
    }

}
