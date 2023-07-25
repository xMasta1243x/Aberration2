using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector3 desiredPosition;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Calcula la posición deseada de la cámara basada en la posición del objetivo.
        desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Limita las coordenadas X e Y de la posición deseada para que estén dentro de los límites establecidos.
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // Interpola suavemente la posición actual de la cámara hacia la posición deseada.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Actualiza la posición de la cámara.
        transform.position = smoothedPosition;
    }
}