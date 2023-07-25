using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menudepausa : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public string pauseButton = "Joystick Button 7"; // Configurando el bot�n de pausa como "Joystick Button 7".

    private bool isPaused = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Verificar si el bot�n configurado para pausar (en este caso, "Joystick Button 7" o "Escape") ha sido presionado.
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (isPaused)
        {
            float verticalInput = Input.GetAxis("Vertical");
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoToMainMenu()
    {
        // Cambiar a la escena del men� principal. Aseg�rate de tener la escena correcta en el �ndice 0.
        SceneManager.LoadScene(0);
    }

    public void GoToOptions()
    {
        // Agrega aqu� la l�gica para abrir la escena de opciones.
        // Por ejemplo, puedes cargar una escena de opciones o mostrar/ocultar un canvas de opciones en la escena actual.
    }

    public void HealPlayer()
    {
        // Agrega aqu� la l�gica para curar al jugador.
        // Por ejemplo, puedes restaurar su salud o aplicar cualquier efecto de curaci�n.
    }
}

