using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float windForce;
    [SerializeField] float impulseDuration; 
    [SerializeField] private EnemyAttack enemyAttack;
    public bool isBeingImpulsed = false; // Estado para cuando el enemigo es empujado por una fuerza externa
    public bool isFalling = false; //estado para cuando caen 
    private float currentImpulseTime = 0f;
    private Rigidbody2D enemyRB2D;
    private GameObject target;

    void Awake()
    {
        enemyRB2D = GetComponent<Rigidbody2D>();
        // Asegúrate de que enemyAttack esté asignado, ya sea en el Inspector o con GetComponent
        if (enemyAttack == null)
        {
            enemyAttack = GetComponent<EnemyAttack>();
        }
        if (enemyAttack == null)
        {
            Debug.LogError("EnemyAI: No se encontró el script EnemyAttack en este GameObject. ¡Asegúrate de agregarlo!");
        }
    }

    void OnEnable()
    {
        isBeingImpulsed = false;
        isFalling = false;
        currentImpulseTime = 0f;
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        if (enemyAttack != null && enemyAttack.targetPlayer == null)
        {
            enemyAttack.SetTargetPlayer(newTarget.transform);
        }
    }

    void Update() // Para controlar la duración del impulso
    {
        if (isBeingImpulsed)
        {
            currentImpulseTime += Time.deltaTime;
            if (currentImpulseTime >= impulseDuration)
            {
                isBeingImpulsed = false;
                currentImpulseTime = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (target != null && !isBeingImpulsed && !enemyAttack.IsAttackingRange() && !isFalling) 
        {
            Vector2 lookDirection = (target.transform.position - transform.position).normalized;
            enemyRB2D.AddForce(lookDirection * speed);

            Debug.DrawLine(transform.position, target.transform.position, Color.red);
            Debug.DrawRay(transform.position, lookDirection * 5f, Color.blue);
        }
        else if (enemyAttack.IsAttackingRange()) // Si esta en rango de ataque, reducir la velocidad
        {
            enemyRB2D.linearVelocity *= 0.5f; // Reduce la velocidad gradualmente para detenerse
        }
    }

    public void StartImpulse()
    {
        isBeingImpulsed = true;
        currentImpulseTime = 0f;
        if (enemyRB2D != null)
        {
            enemyRB2D.linearVelocity = Vector2.zero; // Detener movimiento al ser impulsado
        }
    }

    void OnDisable()
    {
        if (enemyRB2D != null)
        {
            enemyRB2D.linearVelocity = Vector2.zero;
            enemyRB2D.angularVelocity = 0f;
        }
    }
}