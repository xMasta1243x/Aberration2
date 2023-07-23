using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damageAmount = 1; // Cantidad de daño causado por la bala

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si la bala ha colisionado con un enemigo
        if (collision.CompareTag("Enemy"))
        {
            // Obtener el componente de vida del enemigo (si tiene)
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            // Si el enemigo tiene el componente EnemyHealth, le resta vida
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }

            // Destruir la bala al colisionar con un enemigo
            Destroy(gameObject);
        }
        // Verificar si la bala ha colisionado con un collider con tag "Wall"
        else if (collision.CompareTag("Wall"))
        {
            // Destruir la bala al colisionar con un muro
            Destroy(gameObject);
        }
    }
}

