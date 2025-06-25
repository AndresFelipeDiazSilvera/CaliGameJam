using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [Header("Animation Settings")]
    public float rotationSpeed = 90f;
    public float bobSpeed = 2f;
    public float bobHeight = 0.5f;
    
    private Vector3 startPos;
    
    void Start()
    {
        startPos = transform.position;
    }
    
    void Update()
    {
        // Rotación continua
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        
        // Movimiento de flotación
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // El PlayerController maneja la lógica de recolección
            // Este script solo se encarga de la animación
        }
    }
}