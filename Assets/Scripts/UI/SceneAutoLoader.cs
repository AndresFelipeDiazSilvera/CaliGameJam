using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneAutoLoader : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "NextScene";
    [SerializeField] private float delayInSeconds = 30f;
    [SerializeField] private TMP_Text timerText;

    private float timeRemaining;

    private void Start()
    {
        timeRemaining = delayInSeconds;
    }

    private void Update()
    {
        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = "" + seconds;
    }
}
