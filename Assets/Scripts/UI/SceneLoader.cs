using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public string firstScene = "Monk of the Wind";

    public void LoadMainScene()
    {
        SceneManager.LoadScene(firstScene);
    }
}
