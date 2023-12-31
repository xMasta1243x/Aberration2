using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Image hp;
    public float hpActual;
    public float hpMaxima;
    
    // Agrega una referencia al componente AudioSource del jugador
    public AudioSource audioSource;
    public AudioClip hurtSound; // Sonido a reproducir cuando el jugador pierda vida

    void Start()
    {
        hpActual = hpMaxima;

        // Obtener la referencia al componente AudioSource del jugador
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        hp.fillAmount = hpActual / hpMaxima;
    }

    // Método para recibir daño y actualizar la barra de vida
    public void TakeDamage(float damageAmount)
    {
        hpActual -= damageAmount;
        hpActual = Mathf.Max(hpActual, 0f);
        UpdateHealthBar();

        // Reproducir el sonido de pérdida de vida
        audioSource.PlayOneShot(hurtSound);

        if (hpActual <= 0f)
        {
            Die();
        }
    }

    // Método para actualizar la barra de vida en la interfaz de usuario
    private void UpdateHealthBar()
    {
        hp.fillAmount = hpActual / hpMaxima;
    }

    private void Die()
    {
    // Puedes colocar aquí cualquier lógica adicional antes de reiniciar la escena (por ejemplo, una animación de muerte).

    // Obtener el nombre de la escena actual
    string currentSceneName = SceneManager.GetActiveScene().name;

    // Reiniciar la escena actual
    SceneManager.LoadScene(currentSceneName);
    }

    public void Heal(float healAmount)
    {
        hpActual += healAmount;
        hpActual = Mathf.Min(hpActual, hpMaxima);
        UpdateHealthBar();
    }

}
