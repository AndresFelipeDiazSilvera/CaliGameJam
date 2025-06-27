using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;
    public bool isWin = false;
    private bool hasWonDisplayed = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnManager.wave == 5 && spawnManager.EnemysEnable() == 0)
        {
            isWin = true;
            if (hasWonDisplayed)
            {
                loadNextMiniGame();
                hasWonDisplayed = false;
            }
        }
        else
        {
            isWin = false;
            hasWonDisplayed = false;
        }
    }

    public void loadNextMiniGame()
    {
        //TO DO implementar logica para cmabiar de scena
        if (isWin)
        {
            Debug.Log("Has ganado");
        }
    }

    //TO DO implementar otros metodos si se requieren 
}
