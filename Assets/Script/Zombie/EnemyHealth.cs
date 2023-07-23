using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // Vida máxima del enemigo
    private int currentHealth; // Vida actual del enemigo

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        // Restar el daño a la vida actual del enemigo
        currentHealth -= damageAmount;

        // Verificar si el enemigo ha quedado sin vida
        if (currentHealth <= 0)
        {
            // Si ha quedado sin vida, destruir al enemigo
            Destroy(gameObject);
        }
    }
}
