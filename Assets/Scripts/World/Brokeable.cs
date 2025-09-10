using UnityEngine;

public class Brokeable : MonoBehaviour
{
    // Sprites para los diferentes estados del objeto
    public Sprite NormalSprite;
    public Sprite DamageSprite;
    public Sprite BrokenSprite;
    
    public AudioClip  WoodImpactSound; // Sonido de impacto en madera
    public AudioClip  WoodDestroySound; // Sonido de destrucción de madera

// Referencias a componentes
    private SpriteRenderer sr;
    private Collider2D col;
    private AudioSource audioSource; 

    public GameObject brokenEffect; // Efecto de partículas al romperse
// Vida del objeto
    public int health = 3;

    void Start()
    {
        //obtener referencias a componentes y establecer sprite inicial "Puerta Normal"
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        sr.sprite = NormalSprite;
    }

// Método para recibir daño
    public void TakeDamage(int damage)
    {
        // Reducir la vida, le pasamos la variable damage por si queremos que otras armas hagan mas daño 
        health -= damage;

        //si es 2 cambia a sprite dañado, si es 1 cambia a sprite roto y desactiva el collider, si es 0 destruye el objeto
        if (health == 2)
        {
            sr.sprite = DamageSprite;
            audioSource.PlayOneShot(WoodImpactSound); // Reproducir sonido de impacto
        }
        else if (health <= 1)
        {
            sr.sprite = BrokenSprite;
            col.enabled = false; // Desactivar el collider para que el jugador pueda pasar
            audioSource.PlayOneShot(WoodDestroySound); // Reproducir sonido de destrucción
            if (brokenEffect != null)
            {
                GameObject particles = Instantiate(brokenEffect, transform.position, Quaternion.identity);
                Destroy(particles, 2f); // destruye las partículas tras 2 segundos
            }
        }
        if (health <= 0)
        {
            Destroy(gameObject, 2f);
        }
    }

    void Update()
    {
        
    }
}
