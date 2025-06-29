using UnityEngine;

public class GamePauseManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;

    private void Start()
    {

        Time.timeScale = 0f;
        if (instructionsPanel != null)
            instructionsPanel.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        if (instructionsPanel != null)
            instructionsPanel.SetActive(false);
    }
}
