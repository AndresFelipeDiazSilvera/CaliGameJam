using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] float windForce = 10f;
    [SerializeField] public Vector2 windDirection = Vector2.right;
    private Vector2 awayFromAttack;
    public GameObject attacker; // Asigna esto cuando el ataque es instanciado o lanzado
    // metodo para la colicion del ataque
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("viento choco con" + other.gameObject.tag);
        // Verificamos si el objeto que entro en el trigger es un Player o un Enemy
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            // Si el atacante y el objeto colisionado son ambos enemigos, y no son el mismo objeto, no los empujes.
            if (attacker != null && attacker.CompareTag("Enemy") && other.CompareTag("Enemy") && other.gameObject != attacker)
            {
                Debug.Log("Evitando empuje entre enemigos: " + attacker.name + " atacando a " + other.gameObject.name);
                return; // Salimos del método sin aplicar la fuerza
            }
            Rigidbody2D targetRigidbody = other.GetComponent<Rigidbody2D>();
            if (targetRigidbody != null)
            {
                // Aplicamos una fuerza de impulso instantanea
                awayFromAttack = other.gameObject.transform.position - transform.position;//empuje
                targetRigidbody.AddForce(awayFromAttack * windForce, ForceMode2D.Impulse);
                Debug.Log("Viento empujo a: " + other.gameObject.name);
            }
        }
    }
    //para visualizar la direccion del viento en el editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan; // Color para el gizmo del viento
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + windDirection.normalized * 2f);
        Gizmos.DrawSphere((Vector2)transform.position + windDirection.normalized * 2f, 0.1f);
    }
}