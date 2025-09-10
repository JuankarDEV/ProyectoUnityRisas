using UnityEngine;

public class AI : MonoBehaviour
{
    // Vida del enemigo
    public int lifes = 3;
    public void LooseLife(int lifesToLoose)
    {
        lifes -= lifesToLoose;
        if (lifes <= 0)
        {
            // Llama a GameManager para registrar la muerte del enemigo

            if (GameManager.Instance != null)
            {
                GameManager.Instance.EnemyKilled();
            }
            // Destruye el objeto enemigo

            Destroy(gameObject);
        }
    }

    void Die()
    {
        GameManager.Instance.EnemyKilled();
        Destroy(gameObject);
    }
}
