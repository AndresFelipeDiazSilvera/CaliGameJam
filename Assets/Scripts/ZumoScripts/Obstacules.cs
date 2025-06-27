using UnityEngine;

public class Obstacules : MonoBehaviour
{
    [SerializeField] float forceImpulse;
     void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Obstáculo chocó con: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            //TO DO LLAMAR LA ANIMACION CUANDO CHOCAN CON EL OBSTACULO
            Rigidbody2D rB2D = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rB2D != null)
            {
                Vector2 awayFromObstacule = (collision.gameObject.transform.position - transform.position).normalized; //empuje
                rB2D.AddForce(forceImpulse * awayFromObstacule, ForceMode2D.Impulse);
                Debug.Log("Se empujó al: " + collision.gameObject.tag);

                // Notificar al PlayerController que esta siendo impulsado
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.StartImpulse();
                }
            }
        }
    }

    public void PlayAnimacion()
    {
        //TO DO IMPLEMENTAR ANIMACION Y PARTICULAS
    }
}
