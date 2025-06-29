using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reloads the current active scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
