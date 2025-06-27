using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public string firstScene = "InitScene";

    public void LoadMainScene()
    {
        SceneManager.LoadScene(firstScene);
    }
}
