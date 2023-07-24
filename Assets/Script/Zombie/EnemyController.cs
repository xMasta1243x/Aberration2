using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2.0f;
    public float chaseDistance = 5.0f;

    public float attackDamage = 10f; // Daño del ataque del enemigo
    public float attackCooldown = 1.5f; // Tiempo de retraso entre ataques
    public float attackDistance = 1.5f; // Distancia a la que el enemigo atacará al jugador

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth; // Referencia al script EnemyHealth
    private bool canAttack = true; // Variable para controlar el tiempo de retraso entre ataques
    private bool isAttacking = false; // Variable para indicar si el enemigo está atacando o no

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>(); // Obtener la referencia al script EnemyHealth
    }

    private void Update()
    {
        // Verificar si el enemigo está muerto
        if (enemyHealth.IsDead)
        {
            // Si está muerto, detener su movimiento y salir del método Update
            animator.SetBool("isWalking", false);
            return;
        }

        // Verificar si el enemigo está siendo atacado
        if (enemyHealth.IsHit)
        {
            // Si está siendo atacado, detener su movimiento y salir del método Update
            animator.SetBool("isWalking", false);
            return;
        }

        // Si el enemigo está atacando, detener su movimiento y salir del método Update
        if (isAttacking)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;

            // Verificar si el enemigo puede atacar y si está lo suficientemente cerca del jugador
            if (canAttack && distanceToPlayer <= attackDistance)
            {
                AttackPlayer();
            }
            else
            {
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
        }
        else
        {
            // Desactiva la animación de caminar si está fuera del rango de persecución
            animator.SetBool("isWalking", false);
        }
    }

    private void AttackPlayer()
    {
        // Realizar el ataque al jugador aquí
        // Por ejemplo, podrías llamar al método InflictDamage en el script del jugador para restar la vida del jugador
        player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

        // Activar el tiempo de retraso entre ataques para evitar ataques constantes
        canAttack = false;
        isAttacking = true; // El enemigo está atacando
        Invoke(nameof(ResetAttackCooldown), attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        // Reiniciar el tiempo de retraso entre ataques
        canAttack = true;
        isAttacking = false; // El enemigo ya no está atacando
    }
}








