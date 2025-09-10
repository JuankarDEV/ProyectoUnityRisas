using UnityEngine;

public class Control : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;

    public Transform groundCheck; // Objeto vacío en los pies del jugador
    public float checkRadius = 0.2f;
    public LayerMask groundMask;

    public bool isSprinting; 
    public float sprintingSpeedMultiplier = 1.5f; 

    private float sprintSpeed = 1f;

    public float staminaUseAmount = 5f; 
    private StaminaBar staminaSlider; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        staminaSlider = FindObjectOfType<StaminaBar>();
    }

    void Update()
    {

                    // Posición del mouse en mundo
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // Cambiar el flip horizontal según la posición del mouse
            Vector3 localScale = transform.localScale;

            if (mousePos.x > transform.position.x)
                localScale.x = Mathf.Abs(localScale.x);  // Mirando a la derecha
            else
                localScale.x = -Mathf.Abs(localScale.x); // Mirando a la izquierda

            transform.localScale = localScale;
            
        // Detectar suelo (círculo pequeño bajo el jugador)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundMask);

        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed * sprintSpeed, rb.linearVelocity.y);

        // Saltar
        JumpCheck();

        // Correr
        RunCheck();
    }

    void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void RunCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;

            if (isSprinting)
            {
                staminaSlider.UseStamina(staminaUseAmount);
            }
            else
            {
                staminaSlider.UseStamina(0);
            }
        }

        sprintSpeed = isSprinting ? sprintingSpeedMultiplier : 1f;
    }
}