using System.Collections;
using UnityEngine;

[System.Serializable]
public class WindSpawnConfigEnemy 
{
    public Transform spawnPoint;
    public Vector2 fixedWindDirection;
    public float fixedRotationAngleZ;
}

public class EnemyAttack : MonoBehaviour
{
    public GameObject windAttackPrefab;
    public WindSpawnConfigEnemy[] windSpawnConfigs;

    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackRange = 4f;
    private AudioManager audioManager;
    public Transform targetPlayer; 
    private bool canAttack = true;
    private EnemyAi enemyAi; 

    void Awake() 
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        enemyAi = GetComponent<EnemyAi>(); 
        if (enemyAi == null)
        {
            Debug.LogError("EnemyAttack: No se encontró el script EnemyAi en este GameObject. ¡Asegúrate de agregarlo!");
        }

        if (targetPlayer == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                targetPlayer = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("EnemyAttack: No se encontró un objeto con la etiqueta 'Player'. El enemigo no podrá atacar al jugador.");
            }
        }

        if (windSpawnConfigs == null || windSpawnConfigs.Length == 0)
        {
            Debug.LogWarning("EnemyAttack: No has asignado ninguna configuración de punto de aparición de viento para el enemigo.");
        }
        else
        {
            foreach (var config in windSpawnConfigs)
            {
                if (config.spawnPoint == null)
                {
                    Debug.LogWarning("EnemyAttack: Una configuración de viento del enemigo tiene un 'Spawn Point' sin asignar.");
                }
            }
        }
    }

    public void SetTargetPlayer(Transform newTarget)
    {
        targetPlayer = newTarget;
    }

    // <-- ¡Nuevo método para que EnemyAi consulte si el enemigo está en rango de ataque!
    public bool IsAttackingRange()
    {
        if (targetPlayer == null) return false; // Si no hay objetivo, no esta en rango de ataque
        float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);
        return distanceToPlayer < attackRange;
    }

    void Update()
    {
        if (canAttack && targetPlayer != null && !enemyAi.isBeingImpulsed)
        {
            if (IsAttackingRange()) 
            {
                ActivateWindAttack();
                StartCoroutine(Cooldown());
            }
        }
    }

    void ActivateWindAttack()
    {
        if (windAttackPrefab == null || windSpawnConfigs == null || windSpawnConfigs.Length == 0)
        {
            Debug.LogWarning("EnemyAttack: Componentes o configuraciones de viento no configuradas correctamente para el enemigo.");
            return;
        }

        WindSpawnConfigEnemy bestConfig = null; 
        float minDistance = float.MaxValue;

        foreach (WindSpawnConfigEnemy config in windSpawnConfigs) 
        {
            if (config.spawnPoint == null) continue;

            float distance = Vector2.Distance(config.spawnPoint.position, targetPlayer.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestConfig = config;
            }
        }

        if (bestConfig == null)
        {
            Debug.LogWarning("EnemyAttack: No se pudo encontrar un punto de aparición válido para que el enemigo lance el ataque.");
            return;
        }

        Vector2 windDirection = bestConfig.fixedWindDirection.normalized;
        Quaternion windRotation = Quaternion.Euler(0f, 0f, bestConfig.fixedRotationAngleZ);

        GameObject currentWindAttack = Instantiate(windAttackPrefab, bestConfig.spawnPoint.position, windRotation);
        currentWindAttack.transform.SetParent(this.transform);

        AttackSystem windScript = currentWindAttack.GetComponent<AttackSystem>();
        if (windScript != null)
        {
            windScript.windDirection = windDirection;
            audioManager.PlayAudioEnemyAttack();
        }
        else
        {
            Debug.LogError("EnemyAttack: El prefab del viento instanciado no tiene un script AttackSystem adjunto.");
        }

        Destroy(currentWindAttack, 0.5f);
    }

    IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}