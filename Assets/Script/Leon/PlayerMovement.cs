using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del personaje
    private Vector2 movementInput; // Entrada del movimiento
    private Rigidbody2D rb; // Referencia al componente Rigidbody2D
    private Animator animator; // Referencia al componente Animator
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Obtener la entrada del movimiento del mando de Xbox One
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        movementInput = new Vector2(horizontalInput, verticalInput).normalized;

        // Actualizar la animación
        if (movementInput != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Voltear el sprite horizontalmente en función de la dirección del movimiento
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        // Mover el personaje en función de la entrada del mando de Xbox One
        rb.velocity = movementInput * moveSpeed;
    }
}




