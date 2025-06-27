using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 2f;
    private float spriteWidth;

    void Start()
    {
        // Obtener el ancho del sprite (asumiendo que ambos son iguales)
        spriteWidth = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Mover el fondo a la izquierda
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // Reposicionar si uno de los hijos sale de la pantalla
        foreach (Transform child in transform)
        {
            if (child.position.x < -spriteWidth)
            {
                float rightMostX = GetRightmostChildX();
                child.position = new Vector3(rightMostX + spriteWidth, child.position.y, child.position.z);
            }
        }
    }

    float GetRightmostChildX()
    {
        float maxX = float.MinValue;
        foreach (Transform child in transform)
        {
            if (child.position.x > maxX)
                maxX = child.position.x;
        }
        return maxX;
    }
}