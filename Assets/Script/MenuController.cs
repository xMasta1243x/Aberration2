using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Cargar la escena del juego principal
        SceneManager.LoadScene("GameScene");
    }

    public GameObject optionsCanvas; // Referencia al objeto del canvas "Menu de opciones"

    public void OpenSettings()
    {
        // Activar el canvas de opciones para mostrarlo en pantalla
        optionsCanvas.SetActive(true);
    }

    public void CloseSettings()
    {
        // Desactivar el canvas de opciones para ocultarlo de pantalla
        optionsCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        // Salir de la aplicación (esto solo funcionará en una compilación, no en el editor de Unity)
        Application.Quit();
    }

    [SerializeField] private AudioMixer audioMixer;
    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
