using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogoSequence : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float logoDisplayTime = 2f;
    [SerializeField] private string nextSceneName = "MainMenu";

    private void Start()
    {
        StartCoroutine(PlayLogoSequence());
    }

    private IEnumerator PlayLogoSequence()
    {

        yield return Fade(1, 0);


        yield return new WaitForSeconds(logoDisplayTime);


        yield return Fade(0, 1);


        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator Fade(float from, float to)
    {
        float timer = 0f;
        Color color = fadePanel.color;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, timer / fadeDuration);
            fadePanel.color = new Color(color.r, color.g, color.b, alpha);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        fadePanel.color = new Color(color.r, color.g, color.b, to);
    }
}
