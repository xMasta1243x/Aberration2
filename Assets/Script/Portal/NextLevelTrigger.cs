using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    // Nombre de la escena del siguiente nivel (asegúrate de que el nombre sea correcto)
    public string nextLevelName = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobar si el objeto que entra en el Collider es el jugador (puedes utilizar tags si lo prefieres)
        if (other.CompareTag("Player"))
        {
            // Cargar la siguiente escena
            SceneManager.LoadScene(nextLevelName);
        }
    }
}