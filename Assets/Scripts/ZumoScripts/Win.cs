using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;
    public bool isWin = false;

    // Update is called once per frame
    void Update()
    {
        if (spawnManager.wave == 5 && spawnManager.EnemysEnable() == 0)
        {
            isWin = true;
            loadNextMiniGame();
        }
        else
        {
            isWin = false;
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
