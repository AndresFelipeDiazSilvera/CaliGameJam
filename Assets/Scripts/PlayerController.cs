using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float windForce;
    [SerializeField] PlayerInput playerInput;
    public Vector2 inputs;
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
    }
    void FixedUpdate()
    {
        Vector2 movimiento = inputs.normalized;
        if (movimiento!=Vector2.zero)
        {
            rB2D.MovePosition(rB2D.position + speed * Time.fixedDeltaTime * movimiento);
        }
    }
}
