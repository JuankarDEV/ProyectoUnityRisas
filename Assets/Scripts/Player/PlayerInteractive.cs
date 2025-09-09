using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    public Transform startPosition;

    private void OnTriggerEnter2D(Collider2D other) // ✅ ahora 2D
    {
        if (other.CompareTag("GunAmmo"))
        {
            GameManager.Instance.gunAmmo += other.GetComponent<AmmoBox>().ammo;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("HealthObject"))
        {
            GameManager.Instance.AddHealth(other.GetComponent<HealthObject>().health);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("DeathFloor"))
        {
            // Resta 50 de vida al jugador
            GameManager.Instance.LoseHealth(50);

            // Resetea la posición del jugador
            transform.position = startPosition.position;

            // Si usas Rigidbody2D, opcional reset de la velocidad
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            GameManager.Instance.LoseHealth(5);
        }
    }
}
