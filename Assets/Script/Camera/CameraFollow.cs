using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (virtualCamera == null)
            return;

        // La cámara seguirá automáticamente al objetivo configurado en el componente "Cinemachine Virtual Camera".
        // No necesitas hacer cálculos manuales para la posición deseada ni limitar las coordenadas.

        // Interpola suavemente la posición actual de la cámara hacia la posición del objetivo.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, virtualCamera.transform.position, smoothSpeed * Time.deltaTime);

        // Actualiza la posición de la cámara.
        transform.position = smoothedPosition;
    }
}
