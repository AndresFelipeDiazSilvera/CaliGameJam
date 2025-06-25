using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject obstaclePrefab;
    public GameObject collectiblePrefab;
    public float spawnRate = 2f;
    public float obstacleSpeed = 3f;
    public float spawnRangeY = 3f;
    
    private float nextSpawnTime;
    
    void Update()
    {
        if (!GameManagerBird.Instance.IsGameActive()) return;
        
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnRate;
        }
    }
    
    void SpawnObstacle()
    {
        // Posición aleatoria en Y
        float randomY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);
        
        // Crear obstáculo
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        obstacle.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * obstacleSpeed;
        
        // Probabilidad de spawn de coleccionable
        if (Random.Range(0f, 1f) < 0.3f) // 30% de probabilidad
        {
            Vector3 collectiblePos = spawnPos + Vector3.up * 2f;
            GameObject collectible = Instantiate(collectiblePrefab, collectiblePos, Quaternion.identity);
            collectible.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * obstacleSpeed;
        }
    }
}