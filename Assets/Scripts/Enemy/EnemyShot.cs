using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject enemyBullet;
    public Transform spawnBulletPoint;
    public float bulletVelocity = 10f;
    public float shotRate = 2f;

    [Header("Visión")]
    public float visionDistance = 8f;
    public LayerMask obstacleMask;  // Capas que bloquean la visión
    private Transform player;

    private float shotTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
            Debug.LogWarning("No se encontró el jugador en la escena.");
    }

void Update()
{
    if (player == null) return;

    Vector2 direction = (player.position - transform.position).normalized;

    // Debug: dibuja línea de visión
    Debug.DrawRay(transform.position, direction * visionDistance, Color.red);

    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, visionDistance);

    foreach (var hit in hits)
    {
        if (hit.collider.gameObject == gameObject)
            continue; // Ignora tu propio collider

        Debug.Log("Raycast detectó: " + hit.collider.name + " | Tag: " + hit.collider.tag);

        if (hit.collider.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado, disparando!");
            if (Time.time >= shotTimer)
            {
                Shoot(direction);
                shotTimer = Time.time + shotRate;
            }
            break; // Encontramos al jugador, no necesitamos seguir
        }
    }
}
    

    void Shoot(Vector2 direction)
    {
        GameObject newBullet = Instantiate(enemyBullet, spawnBulletPoint.position, spawnBulletPoint.rotation);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Rotar la bala hacia el jugador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Aplicar velocidad
            rb.linearVelocity = direction * bulletVelocity;
        }
    }
}