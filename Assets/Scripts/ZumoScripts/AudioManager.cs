using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip fallEnemy;
    [SerializeField] AudioClip fallPlayer;
    [SerializeField] AudioClip windAttackPlayer;
    [SerializeField] AudioClip windAttackEnemy;
    [SerializeField] AudioClip crash;
    [SerializeField] private AudioSource player;
    [SerializeField] private AudioSource ambiente;
    [SerializeField] private AudioSource enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (player == null)
        {
            player = GetComponent<AudioSource>();
        }
        if (ambiente == null)
        {
            ambiente = GetComponent<AudioSource>();
        }
        if (enemy == null)
        {
            enemy = GetComponent<AudioSource>();
        }
    }

    //metodos para reproducir los audios de player
    public void PlayAudioPlayerAttack()
    {   //Player es el audio sorce dedicada a sonidos del Player
        player.PlayOneShot(windAttackPlayer);
    }
    public void PlayAudioPlayerFall()
    {   //Player es el audio sorce dedicada a sonidos del Player
        player.PlayOneShot(fallPlayer);
    }
    //TO DO CREAR LOS METODOS PARA REPRODUCIR AUDIO DE PLAYER QUE SE NECESITEN

    //metodos Para reProducir los audios del Ambiente
    public void PlayAudioCrash()
    {
        //ambiente es el audio sorce dedicada a sonidos del ambiente
        ambiente.PlayOneShot(crash);
    }
    //TO DO CREAR LOS METODOS PARA REPRODUCIR AUDIO DE AMBIENTE QUE SE NECESITEN

    //metodos Para reProducir los audios de enemy
    public void PlayAudioEnemyFall()
    {
        //enemy es el audio sorce dedicada a sonidos del enemy
        enemy.PlayOneShot(fallEnemy);
    }
     public void PlayAudioEnemyAttack()
    {   //player es el audio sorce dedicada a sonidos del player
        enemy.PlayOneShot(windAttackEnemy);
    }
    //TO DO CREAR LOS METODOS PARA REPRODUCIR AUDIO DE ENEMIGO QUE SE NECESITEN
}
