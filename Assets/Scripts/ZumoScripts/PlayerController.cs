using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float windForce;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] float impulseDuration ;
    public Vector2 inputs;
    public bool isBeingImpulsed = false;
    public bool isFalling = false;
    private float currentImpulseTime = 0f;
    private Rigidbody2D rB2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputs = playerInput.actions["Move"].ReadValue<Vector2>();
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
    void FixedUpdate()
    {
        if (!isBeingImpulsed)
        {
            Vector2 movimiento = inputs.normalized;
            if (movimiento != Vector2.zero)
            {
                //movimineto 
                rB2D.AddForce(movimiento * speed , ForceMode2D.Force);
            }
            else
            {
                rB2D.linearVelocity *= 0.9f; // Reduce la velocidad gradualmente si no hay input
            }
        }
    }
     public void StartImpulse()
    {
        isBeingImpulsed = true;
        currentImpulseTime = 0f;
    }
}
