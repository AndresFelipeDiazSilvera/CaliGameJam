using UnityEngine;

public class AutoDestroyerBird : MonoBehaviour
{
    [Header("Destroy Settings")]
    public float destroyDistance = 10f;
    
    void Update()
    {
        // Destruir objetos que salen de pantalla por la izquierda
        if (transform.position.x < -destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}