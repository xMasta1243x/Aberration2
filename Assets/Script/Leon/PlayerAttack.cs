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

    void Start()
    {
        animator = GetComponent<Animator>();
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
                Attack();
            }
        }
    }

   void Attack()
{
    // Actualizar el tiempo para el siguiente disparo
    nextFireTime = Time.time + fireRate;

    // Iniciar la animación de ataque
    animator.SetBool("isAttacking", true);

    // Calcular la dirección de disparo desde el firePoint hacia el puntero del mouse
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = mousePosition - firePoint.position;

    // Si el personaje mira hacia la izquierda, invertimos la dirección en el eje X para disparar a la izquierda
    if (transform.localScale.x < 0)
    {
        direction.x *= -1;
        // Corregimos la posición del firePoint cuando el personaje mira a la izquierda
        firePoint.localPosition = new Vector3(-firePoint.localPosition.x, firePoint.localPosition.y, firePoint.localPosition.z);
    }
    else
    {
        // Si el personaje mira hacia la derecha, aseguramos que la posición del firePoint sea la correcta
        firePoint.localPosition = new Vector3(Mathf.Abs(firePoint.localPosition.x), firePoint.localPosition.y, firePoint.localPosition.z);
    }

    // Normalizar la dirección y disparar en esa dirección
    direction.Normalize();

    // Calcular el ángulo de rotación hacia la dirección de disparo
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    // Crear el proyectil/bala en el punto de disparo
    GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    // Aplicar una velocidad a la bala en la dirección de disparo
    Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
    bulletRb.velocity = direction * bulletSpeed;

    // Detener la animación de ataque después de un breve período (ajustar según la duración de la animación)
    StartCoroutine(StopAttackingAnimation());
}


    

    IEnumerator StopAttackingAnimation()
    {
        yield return new WaitForSeconds(0.2f); // Ajusta el tiempo según la duración de la animación de ataque
        animator.SetBool("isAttacking", false);
    }
}
