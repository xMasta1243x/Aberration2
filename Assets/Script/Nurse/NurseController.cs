using UnityEngine;

public class NurseController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2.0f;
    public float chaseDistance = 5.0f;
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;
    public float attackDistance = 1.5f;
    public float closeAttackDistance = 1.0f;
    public float farMoveSpeed = 2.0f;
    public float closeMoveSpeed = 4.0f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;
    private bool canAttack = true;
    private bool isAttacking = false;
    private bool isCooldown = false; // Variable para controlar el tiempo de cooldown entre ataques

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (enemyHealth.IsDead)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        if (enemyHealth.IsHit)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        if (isAttacking)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            // Si está muy cerca, ataca directamente al jugador
            if (distanceToPlayer <= attackDistance)
            {
                if(!isCooldown)
                {
                    AttackPlayer();
                }
            }
            else
            {
                // Establecer la velocidad de movimiento según la distancia al jugador
                float currentMoveSpeed = (distanceToPlayer < closeAttackDistance) ? closeMoveSpeed : farMoveSpeed;

                // Mueve al enemigo en la dirección del jugador con la velocidad adecuada
                transform.position += direction * currentMoveSpeed * Time.deltaTime;

                // Activa la animación de caminar
                animator.SetBool("isWalking", true);

                // Voltea al enemigo según su dirección
                if (direction.x > 0)
                    spriteRenderer.flipX = false;
                else if (direction.x < 0)
                    spriteRenderer.flipX = true;
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void AttackPlayer()
    {
        // Inicia la animación de ataque
        StartAttackAnimation();

        // Realizar el ataque al jugador aquí
        player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

        isAttacking = true;
        isCooldown = true; // Inicia el cooldown
        Invoke(nameof(ResetAttackCooldown), attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        // Finaliza la animación de ataque
        EndAttackAnimation();

        isAttacking = false;
        isCooldown = false; // Finaliza el cooldown
    }

    private void StartAttackAnimation()
    {
        // Activa la animación de ataque
        animator.SetBool("IsAttacking", true);
    }

    private void EndAttackAnimation()
    {
        // Desactiva la animación de ataque
        animator.SetBool("IsAttacking", false);
    }
}

