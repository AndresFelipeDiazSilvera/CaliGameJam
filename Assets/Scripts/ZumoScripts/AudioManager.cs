using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip ss;
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
        if (enemy==null)
        {
            enemy = GetComponent<AudioSource>();
        }
    }

    //metodos para reproducir los audios de player
    public void playAudioPlayer()
    {   //player es el audio sorce dedicada a sonidos del player
        player.PlayOneShot(ss);
    }
    //TO DO CREAR LOS METODOS PARA REPRODUCIR AUDIO DE PLAYER QUE SE NECESITEN

    //metodos para reproducir los audios de player
    public void playAudioAmbiente()
    {
        //ambiente es el audio sorce dedicada a sonidos del ambiente
        ambiente.PlayOneShot(ss);
    }
    //TO DO CREAR LOS METODOS PARA REPRODUCIR AUDIO DE AMBIENTE QUE SE NECESITEN

    //metodos para reproducir los audios de player
    public void playAudioEnemy()
    {
        //enemy es el audio sorce dedicada a sonidos del enemy
        enemy.PlayOneShot(ss);
    }
    //TO DO CREAR LOS METODOS PARA REPRODUCIR AUDIO DE ENEMIGO QUE SE NECESITEN
}
