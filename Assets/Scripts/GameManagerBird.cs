using UnityEngine;

public class GameManagerBird : MonoBehaviour
{
    public static GameManagerBird Instance;
    
    [Header("Game State")]
    private int score = 0;
    private bool gameActive = true;
    private int currentLevel = 1;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        StartGame();
    }
    
    void Update()
    {
        // Reiniciar juego con R
        if (!gameActive && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    
    public void StartGame()
    {
        gameActive = true;
        score = 0;
        currentLevel = 1;
        Time.timeScale = 1f;
        Debug.Log("¡Juego iniciado! Nivel: " + currentLevel);
    }
    
    public void GameOver()
    {
        gameActive = false;
        Debug.Log("Game Over!");
        Debug.Log("Puntuación Final: " + score);
        Debug.Log("Nivel Alcanzado: " + currentLevel);
        Debug.Log("Presiona R para reiniciar");
    }
    
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    public void AddScore(int points)
    {
        score += points;
        UpdateLevel();
        Debug.Log("Score: " + score + " | Nivel: " + currentLevel);
    }
    
    void UpdateLevel()
    {
        int newLevel = 1;
        
        // Determinar nivel basado en puntuación
        if (score >= 120)
            newLevel = 3;
        else if (score >= 50)
            newLevel = 2;
        else
            newLevel = 1;
        
        // Notificar cambio de nivel
        if (newLevel > currentLevel)
        {
            currentLevel = newLevel;
            OnLevelUp();
        }
    }
    
    void OnLevelUp()
    {
        Debug.Log("¡NIVEL " + currentLevel + " ALCANZADO!");
        // Aquí puedes agregar efectos visuales, sonidos, etc.
    }
    
    public bool IsGameActive()
    {
        return gameActive;
    }
    
    public int GetScore()
    {
        return score;
    }
    
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}