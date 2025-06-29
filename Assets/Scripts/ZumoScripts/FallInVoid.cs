using System;
using System.Collections;
using UnityEngine;

public class FallInVoid : MonoBehaviour
{
    private SpawnManager spawnManager;
    private AudioManager audioManager;

    private ParticleSystem particle;
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
            collision.gameObject.GetComponent<PlayerController>().isFalling = true;
            //muerte player
            StartCoroutine(DeathPlayer(collision.gameObject));
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAi>().isFalling = true;
            //muerte enemigo
            StartCoroutine(DeathEnemy(collision.gameObject));
        }
    }

    public IEnumerator DeathEnemy(GameObject Enemy)
    {
        Debug.Log("El enemigo esta muriendo...");
        particle = Enemy.gameObject.GetComponent<EnemyAi>().particle;
        //ANIMACION Y PARTICULAS
        if (!particle.isPlaying)
        {
            particle.Play();
        }
        particle.Play();
        // Sonido de muerte
        if (audioManager != null)
        {
            audioManager.PlayAudioEnemyFall(); ;
        }

        // Esperar a que la animacion termine
        yield return new WaitForSeconds(1f);

        // Notificar al spawnManager (si existe)
        if (spawnManager != null)
        {
            spawnManager.EnemyDied(Enemy);
        }
    }
    public IEnumerator DeathPlayer(GameObject player)
    {
        Debug.Log("El player esta muriendo...");
        particle = player.gameObject.GetComponent<PlayerController>().particle;
        //ANIMACION Y PARTICULAS
        if (!particle.isPlaying)
        {
            particle.Play();
        }
        particle.Play();

        //}Sonido de muerte
        if (audioManager != null)
        {
            audioManager.PlayAudioPlayerFall();
        }

        // Esperar a que la animacion termine
        yield return new WaitForSeconds(1f);

        //resetea lo pocision
        player.transform.position = new Vector2(0, 0);
        player.GetComponent<PlayerController>().isFalling = false;
        spawnManager.Reset();
    }
}
