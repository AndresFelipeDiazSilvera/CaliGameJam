using UnityEngine;

public class Obstacules : MonoBehaviour
{
    [SerializeField] float forceImpulse;
    public Vector2 awayFromObstacule;
    public bool isimpulse = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        isimpulse = true;
        Debug.Log("obstaculo choco con" + collision.gameObject.tag);
        awayFromObstacule = collision.gameObject.transform.position - transform.position;//empuje
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D rB2D = collision.gameObject.GetComponent<Rigidbody2D>();
            if (isimpulse)
            {
                rB2D.AddForce(forceImpulse * awayFromObstacule, ForceMode2D.Impulse);
                Debug.Log("se empujo al" + collision.gameObject.tag);
                isimpulse = false;
            }
        }
    }

    public void PlayAnimacion()
    {
        if (gameObject.CompareTag("Obstaculo1"))
        {
            //TO DO implementar animacion y particulas
        }
        if (gameObject.CompareTag("Obstaculo2"))
        {
             //TO DO implementar animacion y particulas
        }
    }
}
