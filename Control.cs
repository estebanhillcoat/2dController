using UnityEngine;

public class Control : MonoBehaviour
{
    [Header("Movimiento")]
    public float acceleration = 20f;
    public float deceleration = 30f;
    public float maxSpeed = 5f;
    public float runMultiplier = 1.5f;

    [Header("Salto")]
    public float jumpForce = 12f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private float currentSpeed = 0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Entrada horizontal
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift); // Shift para correr

        // Velocidad objetivo
        float targetSpeed = moveInput * maxSpeed;
        if (isRunning) targetSpeed *= runMultiplier;

        // Aceleración y desaceleración suave
        if (Mathf.Abs(targetSpeed) > Mathf.Abs(currentSpeed))
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, deceleration * Time.deltaTime);

        // Aplicar velocidad horizontal
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        // Verificar si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Salto con barra espaciadora
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            float finalJumpForce = jumpForce;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                finalJumpForce *= 1.5f;
            }

            rb.velocity = new Vector2(rb.velocity.x, finalJumpForce);
        }




        // invertir sprite según dirección
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1); // Mirando a la derecha
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Mirando a la izquierda
         // aca termina la parte que invierte el script



    }
}
