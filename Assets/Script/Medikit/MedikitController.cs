using UnityEngine;

public class MedikitController : MonoBehaviour
{
    public AudioClip pickupSound; // Sonido a reproducir cuando se recoja el medikit

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtener el componente AudioSource del jugador
            AudioSource playerAudioSource = other.GetComponent<AudioSource>();

            // Reproducir el sonido del medikit si se ha asignado uno
            if (pickupSound != null && playerAudioSource != null)
            {
                playerAudioSource.PlayOneShot(pickupSound);
            }

            // Eliminar el objeto Medikit
            Destroy(gameObject);
        }
    }
}
