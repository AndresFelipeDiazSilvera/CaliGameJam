using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float windForce;
    [SerializeField] float impulseDuration;
    [SerializeField] private EnemyAttack enemyAttack;

    public bool isBeingImpulsed = false;
    public bool isFalling = false;
    private float currentImpulseTime = 0f;
    private Rigidbody2D enemyRB2D;
    private GameObject target;
    private Animator animator; 

    void Awake()
    {
        enemyRB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 

        if (enemyAttack == null)
        {
            enemyAttack = GetComponent<EnemyAttack>();
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

    void Update()
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
        
        UpdateAnimations();
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
        else if (enemyAttack.IsAttackingRange())
        {
            enemyRB2D.linearVelocity *= 0.5f;
        }
    }

    
   private void UpdateAnimations()
{
    
    animator.SetFloat("Speed", enemyRB2D.linearVelocity.sqrMagnitude);

   
    if (enemyRB2D.linearVelocity.sqrMagnitude > 0.1f) 
    {
       
        Vector2 moveDirection = enemyRB2D.linearVelocity.normalized;
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
    }
}

    public void StartImpulse()
    {
        isBeingImpulsed = true;
        currentImpulseTime = 0f;
        if (enemyRB2D != null)
        {
            enemyRB2D.linearVelocity = Vector2.zero;
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