using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // Vida máxima del enemigo
    private int currentHealth; // Vida actual del enemigo
    private bool isDead = false; // Variable para controlar si el enemigo ha muerto
    private bool isHit = false; // Variable para controlar si el enemigo ha sido golpeado
    private Animator animator; // Referencia al componente Animator
    private AudioSource audioSource; // Referencia al componente AudioSource

    public AudioClip hitSound; // Sonido cuando el enemigo es atacado
    public AudioClip deathSound; // Sonido cuando el enemigo muere

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damageAmount)
    {
        // Si el enemigo ya está muerto, no procesar más daño
        if (isDead)
            return;

        // Restar el daño a la vida actual del enemigo
        currentHealth -= damageAmount;

        // Verificar si el enemigo ha quedado sin vida
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Activar el parámetro "isHit" para reproducir la animación de "Atacado"
            isHit = true;
            animator.SetBool("isHit", true);

            // Reproducir el sonido de ataque
            if (audioSource && hitSound)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }
    }

    private void Die()
    {
        // Establecer el parámetro "isDead" en true para activar la animación de "Muerte"
        animator.SetBool("isDead", true);
        isDead = true;

        // Reproducir el sonido de muerte
        if (audioSource && deathSound)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Desactivar el collider para evitar interacciones mientras la animación se reproduce
        GetComponent<Collider2D>().enabled = false;

        // Aquí puedes agregar cualquier otro comportamiento que quieras para cuando el enemigo muera
        // Por ejemplo, detener el movimiento, desactivar la IA, etc.
    }

    // Propiedad para obtener el estado de isDead
    public bool IsDead
    {
        get { return isDead; }
    }

    // Propiedad para obtener el estado de isHit
    public bool IsHit
    {
        get { return isHit; }
    }

    // Método llamado desde el evento de animación "Muerte" cuando la animación termina
    public void OnDeathAnimationEnd()
    {
        // Restablecer el parámetro "isDead" a false para evitar que la animación se repita continuamente
        animator.SetBool("isDead", false);

        // Destruir el objeto del enemigo después de que la animación haya terminado y el parámetro "isDead" se haya restablecido
        Destroy(gameObject);
    }

    // Método llamado desde el evento de animación "Atacado" cuando la animación termina
    public void OnHitAnimationEnd()
    {
        // Restablecer el parámetro "isHit" a false para evitar que la animación se repita continuamente
        isHit = false;
        animator.SetBool("isHit", false);
    }
}

