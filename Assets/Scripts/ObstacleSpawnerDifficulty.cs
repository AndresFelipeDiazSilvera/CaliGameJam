using UnityEngine;

public class ObstacleSpawnerDifficulty : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject obstaclePrefab;
    public GameObject collectiblePrefab;
    
    [Header("Difficulty Levels")]
    public DifficultyLevel[] difficultyLevels = new DifficultyLevel[3];
    
    private float nextSpawnTime;
    private int currentLevel = 0;
    private GameManagerBird gameManager;
    
    [System.Serializable]
    public class DifficultyLevel
    {
        public string levelName;
        public int scoreThreshold;
        public float spawnRate;
        public float obstacleSpeed;
        public float spawnRangeY;
        public float collectibleChance;
        public Color levelColor = Color.white;
    }
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerBird>();
        SetupDefaultLevels();
    }
    
    void SetupDefaultLevels()
    {
        // Nivel 1 - Fácil
        difficultyLevels[0].levelName = "Nivel 1 - Fácil";
        difficultyLevels[0].scoreThreshold = 0;
        difficultyLevels[0].spawnRate = 2.5f;
        difficultyLevels[0].obstacleSpeed = 2f;
        difficultyLevels[0].spawnRangeY = 2f;
        difficultyLevels[0].collectibleChance = 0.4f;
        difficultyLevels[0].levelColor = Color.green;
        
        // Nivel 2 - Medio
        difficultyLevels[1].levelName = "Nivel 2 - Medio";
        difficultyLevels[1].scoreThreshold = 50;
        difficultyLevels[1].spawnRate = 1.8f;
        difficultyLevels[1].obstacleSpeed = 3f;
        difficultyLevels[1].spawnRangeY = 3f;
        difficultyLevels[1].collectibleChance = 0.3f;
        difficultyLevels[1].levelColor = Color.yellow;
        
        // Nivel 3 - Difícil
        difficultyLevels[2].levelName = "Nivel 3 - Difícil";
        difficultyLevels[2].scoreThreshold = 120;
        difficultyLevels[2].spawnRate = 1.2f;
        difficultyLevels[2].obstacleSpeed = 4f;
        difficultyLevels[2].spawnRangeY = 4f;
        difficultyLevels[2].collectibleChance = 0.2f;
        difficultyLevels[2].levelColor = Color.red;
    }
    
    void Update()
    {
        if (gameManager == null || !gameManager.IsGameActive()) return;
        
        UpdateDifficulty();
        
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + GetCurrentSpawnRate();
        }
    }
    
    void UpdateDifficulty()
    {
        int score = gameManager.GetScore();
        int newLevel = 0;
        
        // Determinar nivel actual basado en puntuación
        for (int i = difficultyLevels.Length - 1; i >= 0; i--)
        {
            if (score >= difficultyLevels[i].scoreThreshold)
            {
                newLevel = i;
                break;
            }
        }
        
        // Cambiar nivel si es necesario
        if (newLevel != currentLevel)
        {
            currentLevel = newLevel;
            OnLevelChanged();
        }
    }
    
    void OnLevelChanged()
    {
        Debug.Log("¡Nivel cambiado a: " + difficultyLevels[currentLevel].levelName + "!");
        
        // Cambiar color de fondo o efectos visuales
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, 
            difficultyLevels[currentLevel].levelColor, 0.3f);
    }
    
    void SpawnObstacle()
    {
        DifficultyLevel current = difficultyLevels[currentLevel];
        
        // Posición aleatoria en Y
        float randomY = Random.Range(-current.spawnRangeY, current.spawnRangeY);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0);
        
        // Crear obstáculo
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        obstacle.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * current.obstacleSpeed;
        
        // Spawn de coleccionable basado en probabilidad del nivel
        if (Random.Range(0f, 1f) < current.collectibleChance)
        {
            Vector3 collectiblePos = spawnPos + Vector3.up * 2f;
            GameObject collectible = Instantiate(collectiblePrefab, collectiblePos, Quaternion.identity);
            collectible.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * current.obstacleSpeed;
        }
    }
    
    float GetCurrentSpawnRate()
    {
        return difficultyLevels[currentLevel].spawnRate;
    }
    
    public string GetCurrentLevelName()
    {
        return difficultyLevels[currentLevel].levelName;
    }
    
    public int GetCurrentLevel()
    {
        return currentLevel + 1;
    }
}