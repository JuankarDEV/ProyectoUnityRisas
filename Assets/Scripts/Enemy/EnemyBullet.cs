using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, 3f); // Destruye la bala después de 3 segundos
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Resta vida al jugador
            GameManager.Instance.LoseHealth(damage);

            // Destruye la bala
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // Destruye la bala al chocar con el suelo o paredes
            Destroy(gameObject);
        }
    }
}
