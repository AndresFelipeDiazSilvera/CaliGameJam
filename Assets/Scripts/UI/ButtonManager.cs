using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour

{

    [Header("Image to show")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject credits;

    public void ShowAndHideCredits()
    {
        if (mainMenu != null && credits != null)
        {
            mainMenu.SetActive(true);
            credits.SetActive(false);
        }
    }

    public void ShowAndHideMainBG()
    {
        if (mainMenu != null && credits != null)
        {
            mainMenu.SetActive(false);
            credits.SetActive(true);
        }
    }
}

