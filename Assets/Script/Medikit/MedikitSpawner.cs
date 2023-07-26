using UnityEngine;

public class MedikitSpawner : MonoBehaviour
{
    public GameObject medikitPrefab; // Prefab del Medikit a soltar
    public Transform[] spawnPoints; // Array de posiciones donde aparecerán los medikits
    public int[] healthThresholds; // Array de umbrales de vida en los que aparecerán los medikits

    private bool[] hasSpawnedMedikit; // Array para controlar si ya se ha generado un medikit para cada umbral

    private void Start()
    {
        // Inicializar el array hasSpawnedMedikit
        hasSpawnedMedikit = new bool[healthThresholds.Length];
        for (int i = 0; i < hasSpawnedMedikit.Length; i++)
        {
            hasSpawnedMedikit[i] = false;
        }
    }

    private void Update()
    {
        // Obtener la referencia al componente EnemyHealth
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();

        // Verificar si el enemigo ha alcanzado un umbral de vida y si aún no se ha generado el medikit
        for (int i = 0; i < healthThresholds.Length; i++)
        {
            if (!hasSpawnedMedikit[i] && enemyHealth.GetCurrentHealth() <= healthThresholds[i])
            {
                SpawnMedikit(i);
                hasSpawnedMedikit[i] = true;
            }
        }

        // Agregar Debug.Log para verificar si se obtiene correctamente el componente EnemyHealth
        if (enemyHealth != null)
        {
            Debug.Log("EnemyHealth found.");
        }
        else
        {
            Debug.Log("EnemyHealth not found.");
        }
    }

    private void SpawnMedikit(int index)
    {
        if (medikitPrefab != null && spawnPoints.Length > 0 && index >= 0 && index < spawnPoints.Length)
        {
            // Instanciar el Medikit en la posición del spawner seleccionado
            Instantiate(medikitPrefab, spawnPoints[index].position, Quaternion.identity);
        }
    }
}
