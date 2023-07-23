using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab del proyectil/bala
    public Transform firePoint; // Punto desde el cual se disparará el proyectil
    public float fireRate = 0.5f; // Tiempo entre disparos
    public float bulletSpeed = 10f; // Velocidad del proyectil
    private float nextFireTime; // Tiempo para el siguiente disparo
    private Animator animator; // Referencia al componente Animator
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer

    public Transform leftFirePoint; // Punto desde el cual se disparará el proyectil hacia la izquierda

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Comprobar si el personaje puede atacar (solo si está en estado "Idle")
        bool isIdle = !animator.GetBool("isWalking");
        bool canAttack = isIdle && !animator.GetBool("isAttacking");

        if (canAttack && Time.time >= nextFireTime)
        {
            // Atacar cuando se presiona el botón "Fire3" o el botón "X" del mando de Xbox One
            if (Input.GetButtonDown("Fire3") || Input.GetButtonDown("X"))
            {
                // Calcular la dirección de disparo en función de la escala del Sprite del jugador
                Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;

                // Obtener la posición del firePoint actual o el leftFirePoint si dispara hacia la izquierda
                Transform currentFirePoint = spriteRenderer.flipX ? leftFirePoint : firePoint;

                // Crear el proyectil/bala en el punto de disparo
                GameObject newBullet = Instantiate(bulletPrefab, currentFirePoint.position, Quaternion.identity);

                // Aplicar una velocidad a la bala en la dirección de disparo
                Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
                bulletRb.velocity = direction * bulletSpeed;

                // Actualizar el tiempo para el siguiente disparo
                nextFireTime = Time.time + fireRate;

                // Iniciar la animación de ataque
                animator.SetBool("isAttacking", true);

                // Detener la animación de ataque después de un breve período (ajustar según la duración de la animación)
                StartCoroutine(StopAttackingAnimation());
            }
        }
    }

    IEnumerator StopAttackingAnimation()
    {
        yield return new WaitForSeconds(0.2f); // Ajusta el tiempo según la duración de la animación de ataque
        animator.SetBool("isAttacking", false);
    }
}

