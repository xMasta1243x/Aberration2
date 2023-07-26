using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menudepausa : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public string pauseButton = "Joystick Button 7"; // Configurando el bot�n de pausa como "Joystick Button 7".
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inv;

    private bool isPaused = false;

    public float moveSpeed = 10f; // Velocidad de movimiento del menú
    private Vector3 originalPosition; // Posición original del menú


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Item"))
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                if (Bag[i].GetComponent<Image>().enabled == false)
                {
                    Debug.Log("Se ha añadido el objeto!");

                    Bag[i].GetComponent<Image>().enabled = true;
                    Bag[i].GetComponent<Image>().sprite = coll.GetComponent<SpriteRenderer>().sprite;
                    break;
                }
            }
        }
    }



    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        originalPosition = transform.position;
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

    public void ResumeGame()
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
        // Agrega aquí la lógica para curar al jugador.
        // Por ejemplo, puedes restaurar su salud o aplicar cualquier efecto de curación.

        int healingAmount = 5; // Cantidad de curación
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healingAmount);

                // Quita un GameObject de la lista "Bag"
                if (Bag.Count > 0)
                {
                    for (int i = Bag.Count - 1; i >= 0; i--)
                    {
                        if (Bag[i].GetComponent<Image>().enabled == true)
                        {
                            Bag[i].GetComponent<Image>().enabled = false;
                            break;
                        }
                    }
                }
            }
        }
    }
}

