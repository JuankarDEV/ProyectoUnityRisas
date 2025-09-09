using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Image[] lifeIcons; //Arrat de iconos vida
    private int deathCount = 0;

     public TMP_Text loseText; // Texto de derrota

    public int maxDeaths = 3;
    public Text healthText;  // Texto para mostrar la salud actual en UI
    private bool gameEnded = false; // Variable para evitar que el juego siga comprobando después de terminar

    public Text ammoText;

    public TMP_Text winText;  // Texto para mostrar mensaje de victoria en UI (usando TextMeshPro)

    public static GameManager Instance { get; private set; }

    private int enemiesAlive;  // Conteo de enemigos vivos en la escena

    public int gunAmmo = 100;

    public Transform spawnPoint;

    public int health = 100;  // Salud actual y máxima del jugador
    public int maxHealth = 100;

    private GameObject player;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      

        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        UpdateLivesUI();
    }

    // Update is called once per frame
    void Update()
    {

            Cursor.lockState = CursorLockMode.None;
             Cursor.visible = true;

       ammoText.text = gunAmmo.ToString();
        healthText.text = health.ToString();

        if (gameEnded) return;


    }

    public void LoseHealth(int healthToReduce)
    {

        health -= healthToReduce;
        CheckHealth();

    }

    public void CheckHealth()
    {
        if (health <= 0)
        {
            deathCount++;
            Debug.Log("Has muerto. Veces muerto" + deathCount);
            if (deathCount >= maxDeaths)
            {
                Debug.Log("Has perdido. Demasiadas muertes");
                StartCoroutine(LoseGame());
            }
            else
            {
                Respawn();
            }
        }
        UpdateLivesUI();
    }
   public void Respawn()
{
    health = maxHealth;
    gunAmmo = 100;

    if (player != null && spawnPoint != null)
    {
        // Mover jugador al spawnPoint
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero; // Detener velocidad

        player.transform.position = spawnPoint.position;
    }

    Debug.Log("Jugador respawneado en spawnPoint.");
}
    // Método para añadir vida al jugador sin superar el máximo permitido
    public void AddHealth(int health)
    {
        if (this.health + health >= maxHealth) // No superar salud máxima
        {
            this.health = 100;
        }
        else
        {
            this.health += health;
        }
    }
    private void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            //Si indice es menor que las vidas restantes, se muestra activo
            lifeIcons[i].enabled = i < (maxDeaths - deathCount);
        }

    }

     // Método que se llama cuando un enemigo muere para decrementar el conteo
    public void EnemyKilled()
    {
        if (gameEnded) return;

        enemiesAlive--;

        // Si ya no quedan enemigos, iniciar la secuencia de victoria
        if (enemiesAlive <= 0)
        {
            StartCoroutine(WinGame());
        }
    }
    // Coroutine para mostrar mensaje de victoria y reiniciar escena después de unos segundos
    private IEnumerator WinGame()
    {
        winText.gameObject.SetActive(true); // Mostrar texto de victoria
        yield return new WaitForSeconds(3f); // Esperar 3 segundos
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar la escena actual
    }

    private IEnumerator LoseGame()
    {
        loseText.gameObject.SetActive(true); //Mostar mensaje derrota
        gameEnded = true;
        yield return new WaitForSeconds(3f); //Esperar antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reinicia Escena
    }
    
}
