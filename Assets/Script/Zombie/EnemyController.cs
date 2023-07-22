using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2.0f;
    public float chaseDistance = 5.0f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            // Mueve al enemigo en la dirección del jugador
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Activa la animación de caminar
            animator.SetBool("isWalking", true);

            // Voltea al enemigo según su dirección
            if (direction.x > 0) // Si la dirección es positiva (mirando hacia la derecha)
                spriteRenderer.flipX = false;
            else if (direction.x < 0) // Si la dirección es negativa (mirando hacia la izquierda)
                spriteRenderer.flipX = true;
        }
        else
        {
            // Desactiva la animación de caminar si está fuera del rango de persecución
            animator.SetBool("isWalking", false);
        }
    }
}


