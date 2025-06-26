using System;
using System.Collections;
using UnityEngine;

public class FallInVoid : MonoBehaviour
{
    private SpawnManager spawnManager;
    private AudioManager audioManager;
    void Start()
    {
        spawnManager = FindAnyObjectByType<SpawnManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("cayo al vacio");
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DeathPlayer(collision.gameObject));
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //TO DO implementar muerte
            StartCoroutine(DeathEnemy(collision.gameObject));
        }
    }

    public IEnumerator DeathEnemy(GameObject Enemy)
    {
        Debug.Log("El enemigo está muriendo...");

        //TO DO Activar animacion de muerte

        //TO DO Implementar Sonido de muerte
        /*if (audioManager != null)
        {
            audioManager.DeadPlaySound();
        }*/

        // Esperar a que la animacion termine
        yield return new WaitForSeconds(0.1f);

        // Notificar al spawnManager (si existe)
        if (spawnManager != null)
        {
            spawnManager.EnemyDied(Enemy);
        }
    }
    public IEnumerator DeathPlayer(GameObject player)
    {
        Debug.Log("El enemigo está muriendo...");

        //TO DO Activar animacion de muerte

        //TO DO Implementar Sonido de muerte
        /*if (audioManager != null)
        {
            audioManager.DeadPlaySound();
        }*/

        // Esperar a que la animacion termine
        yield return new WaitForSeconds(0.1f);

        //resetea lo pocision
        player.transform.position = new Vector2(0, 0);
    }
}
