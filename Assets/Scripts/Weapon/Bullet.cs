using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1; // Daño que hace la bala

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AI enemyAI = collision.gameObject.GetComponent<AI>();
            if (enemyAI != null)
            {
                enemyAI.LooseLife(damage); // Llamamos al método del enemigo
            }

            // Destruir la bala después de impactar
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // Destruir la bala si choca con el suelo o paredes
            Destroy(gameObject);
        }
    }
}