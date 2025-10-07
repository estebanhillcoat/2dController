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
        // Movimiento horizontal (izquierda/derecha)
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift); // Shift para correr como loco

        // Calculamos la velocidad que queremos alcanzar
        float targetSpeed = moveInput * maxSpeed;
        if (isRunning) targetSpeed *= runMultiplier;

        // Aceleramos o frenamos suavemente según corresponda
        if (Mathf.Abs(targetSpeed) > Mathf.Abs(currentSpeed))
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, deceleration * Time.deltaTime);

        // Aplicamos la velocidad horizontal al Rigidbody
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        // Chequeamos si está pisando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Salta cuando apretás la barra espaciadora y está en el piso
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Damos vuelta el sprite según para dónde se mueve
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1); // Mirando a la derecha
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Mirando a la izquierda
        // Acá termina la parte que lo hace mirar para el otro lado
    }
}
