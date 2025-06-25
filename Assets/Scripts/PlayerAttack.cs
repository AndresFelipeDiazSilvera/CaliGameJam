using UnityEngine;
using UnityEngine.InputSystem;

//clase auxiliar que contiene la informacion para cada punto de spawn
// [System.Serializable] la hace visible y configurable en el Inspector de Unity
[System.Serializable]
public class WindSpawnConfig
{
    public Transform spawnPoint; // El Transform del punto de spawn
    public Vector2 fixedWindDirection; // La direccion fija del viento desde este punto
    public float fixedRotationAngleZ; // El angulo Z (en grados) para el sprite de viento
}

public class PlayerAttack : MonoBehaviour
{
    public GameObject windAttackPrefab;

    //usamos un array de la clase aux para guardar las config de spawn
    public WindSpawnConfig[] windSpawnConfigs;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("PlayerAttack: No se encontro una camara principal con el tag 'MainCamera'. El ataque de viento no funcionara correctamente.");
        }

        // Verifica si la configuracion de los puntos de spawn es valida
        if (windSpawnConfigs == null || windSpawnConfigs.Length == 0)
        {
            Debug.LogWarning("PlayerAttack: No has asignado ninguna configuracion de punto de aparicion de viento en el Inspector.");
        }
        else
        {
            // verifica que todos los Transforms dentro de las configuraciones esten asignados
            foreach (var config in windSpawnConfigs)
            {
                if (config.spawnPoint == null)
                {
                    Debug.LogWarning("PlayerAttack: Una configuracion de viento tiene un 'Spawn Point' sin asignar.");
                }
            }
        }
    }

    public void OnAttack(InputValue value)
    {
        ActivateWindAttack();
    }

    void ActivateWindAttack()
    {
        // Validacion de componentes y configuraciones esenciales
        if (windAttackPrefab == null || mainCamera == null || windSpawnConfigs == null || windSpawnConfigs.Length == 0)
        {
            Debug.LogWarning("PlayerAttack: Componentes o configuraciones de viento no configuradas correctamente. Revisa el Inspector.");
            return;
        }

        // obtenemos la posicion del mouse en coordenadas del mundo
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition3D = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 mouseWorldPosition = new Vector2(mouseWorldPosition3D.x, mouseWorldPosition3D.y);

        // Encontrar la configuracion (punto de spawn + direccion/rotacion) mas cercana al mouse
        WindSpawnConfig bestConfig = null;
        float minDistance = float.MaxValue;

        foreach (WindSpawnConfig config in windSpawnConfigs)
        {
            if (config.spawnPoint == null) continue; // Saltar si un punto no esta asignado en una configuracion

            float distance = Vector2.Distance(config.spawnPoint.position, mouseWorldPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestConfig = config;
            }
        }

        if (bestConfig == null)
        {
            Debug.LogWarning("PlayerAttack: No se pudo encontrar un punto de aparicion valido mas cercano al mouse para lanzar el ataque.");
            return;
        }

        // Direccion del viento usamos la direccion fija definida en la configuracion
        Vector2 windDirection = bestConfig.fixedWindDirection.normalized;

        // Rotacion del objeto usamos el angulo fijo definido en la configuracion
        Quaternion windRotation = Quaternion.Euler(0f, 0f, bestConfig.fixedRotationAngleZ);

        // Instancia el prefab con la posicion del punto de spawn mas cercano y la rotacion fija
        GameObject currentWindAttack = Instantiate(windAttackPrefab, bestConfig.spawnPoint.position, windRotation);

        // obtenemos el script AttackSystem del objeto de viento instanciado
        AttackSystem windScript = currentWindAttack.GetComponent<AttackSystem>();

        if (windScript != null)
        {
            // Asignar la direccion del viento
            windScript.windDirection = windDirection;
        }
        else
        {
            Debug.LogError("PlayerAttack: El prefab del viento instanciado no tiene un script AttackSystem adjunto.");
        }

        Destroy(currentWindAttack, 0.5f);
    }
}