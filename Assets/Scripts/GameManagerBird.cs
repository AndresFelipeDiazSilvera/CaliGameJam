using UnityEngine;

public class GameManagerBird : MonoBehaviour
{
    public static GameManagerBird Instance;
    
    [Header("Game State")]
    private int score = 0;
    private bool gameActive = true;
    
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
        Time.timeScale = 1f;
    }
    
    public void GameOver()
    {
        gameActive = false;
        Debug.Log("Game Over! Score: " + score + " - Press R to restart");
    }
    
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }
    
    public bool IsGameActive()
    {
        return gameActive;
    }
    
    public int GetScore()
    {
        return score;
    }
}