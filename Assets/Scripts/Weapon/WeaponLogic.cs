using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 30f;
    public float shotRate = 0.5f;
    private float shotRateTime = 0f;
    private AudioSource audioSource;
    public AudioClip shotSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RotateWeaponToCursor();

        if (Input.GetButton("Fire1") && Time.timeScale != 0 && Time.time > shotRateTime)
            Shoot();
    }

    void RotateWeaponToCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Comprobamos la escala X del padre para ver si el personaje está volteado
        if (transform.parent != null && transform.parent.localScale.x < 0)
        {
            // Si el padre está volteado, ajustamos la rotación 180 grados
            angle += 180f;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        if (GameManager.Instance != null && GameManager.Instance.gunAmmo > 0)
        {
            GameManager.Instance.gunAmmo--;

            if (audioSource != null && shotSound != null)
                audioSource.PlayOneShot(shotSound);

            GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 shootDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - spawnPoint.position).normalized;
                rb.AddForce(shootDir * shotForce, ForceMode2D.Impulse);
            }

            shotRateTime = Time.time + shotRate;
            Destroy(newBullet, 5f);
        }
    }
}