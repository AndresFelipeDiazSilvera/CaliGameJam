using UnityEngine;

public class PlayerBird : MonoBehaviour
{
    [Header("Movement Settings")]
    public float jumpForce = 6f;

    private Rigidbody2D rb;
    private bool isAlive = true;
    private bool gameStarted = false;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = 2f; // Activar gravedad desde el inicio
    }

    void Update()
    {
        if (!isAlive) return;

        // Detectar entrada de espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameStarted)
            {
                StartGame();
            }
            Jump();
        }

        // Rotar el pajaro según velocidad
        if (gameStarted)
        {
            RotateBird();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        // Ya no es necesario modificar gravityScale aquí
    }

    void Jump()
    {
        if (!gameStarted) return;

        // Resetear velocidad y aplicar salto
        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = Vector2.up * jumpForce;
    }

    void RotateBird()
    {
        // Rotar basado en la velocidad vertical
        float angle = rb.linearVelocity.y * 4f;
        angle = Mathf.Clamp(angle, -90f, 45f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
        else if (other.CompareTag("Collectible"))
        {
            CollectItem(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void CollectItem(GameObject item)
    {
        // Buscar GameManagerBird
        GameManagerBird gameManager = FindAnyObjectByType<GameManagerBird>();
        if (gameManager != null)
        {
            gameManager.AddScore(10);
        }
        Destroy(item);
    }

    void Die()
    {
        if (!isAlive) return;

        isAlive = false;
        rb.linearVelocity = Vector2.zero;

        // Activar la animación de muerte
        animator.SetBool("isDead", true);

        // Notificar al GameManagerBird
        GameManagerBird gameManager = FindAnyObjectByType<GameManagerBird>();
        if (gameManager != null)
        {
            gameManager.GameOver();
        }

        Debug.Log("Player died!");
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public bool HasStarted()
    {
        return gameStarted;
    }

    public void ActivateGameplay()
    {
        gameStarted = true;
        // Ya no es necesario modificar gravityScale aquí
    }
}