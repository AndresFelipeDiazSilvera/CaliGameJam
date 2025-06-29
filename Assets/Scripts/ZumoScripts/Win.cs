using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    //[SerializeField] SpawnManager spawnManager;
    public bool isWin = false;
    private EnemyCount enemyCount;
     private void Awake() 
    {
        enemyCount = GetComponent<EnemyCount>();
        // Verificar si enemyCount es nulo para evitar errores.
        if (enemyCount == null)
        {
            Debug.LogError("EnemyCount component not found on this GameObject!", this);
        }
    }

    void Update()
    {
        // Solo verificamos si hay enemigos si el componente EnemyCount existe.
        if (enemyCount != null && enemyCount.EnemysEnable() == 0)
        {
            isWin = true; 
            // Obtenemos el nombre de la escena actual una sola vez.
            string currentSceneName = SceneManager.GetActiveScene().name;
            LoadNextMiniGame(currentSceneName);
        }
        else
        {
            // Si no se cumplen las condiciones para ganar, aseguramos que isWin sea false.
            isWin = false;
        }
    }

    public void LoadNextMiniGame(string nameSceneActual)
    {
        Debug.Log("Has ganado en la escena: " + nameSceneActual);

        switch (nameSceneActual)
        {
            case "CelestialMonk1":
                //SceneManager.LoadScene("1"); // Cambia "1" por el nombre de la siguiente escena.
                Debug.Log("Cargando la siguiente escena para CelestialMonk1...");
                break;
            case "CelestialMonk2":
                //SceneManager.LoadScene("2"); // Cambia "2" por el nombre de la siguiente escena.
                Debug.Log("Cargando la siguiente escena para CelestialMonk2...");
                break;
            case "CelestialMonk3":
                //SceneManager.LoadScene("3"); // Cambia "3" por el nombre de la siguiente escena.
                Debug.Log("Cargando la siguiente escena para CelestialMonk3...");
                break;
            case "CelestialMonk4":
                //SceneManager.LoadScene("4"); // Cambia "4" por el nombre de la siguiente escena.
                Debug.Log("Cargando la siguiente escena para CelestialMonk4...");
                break;
            case "CelestialMonk5":
                //SceneManager.LoadScene("5"); // Cambia "5" por el nombre de la siguiente escena.
                Debug.Log("Cargando la siguiente escena para CelestialMonk5...");
                break;
            default:
                Debug.LogWarning("Escena no reconocida para la carga del siguiente mini-juego: " + nameSceneActual);
                break;
        }
    }
}
