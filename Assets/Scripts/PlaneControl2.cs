using UnityEngine;

public class PlaneControl2 : MonoBehaviour
{
    public float power = 10f;
    public float rotationThreshold = 50f;

    float gravityScale;
    Vector3 dragStartPos;

    Rigidbody2D rb;  
    LineRenderer lr;    
    Touch touch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        gravityScale = rb.gravityScale;
        rb.gravityScale = 0f;
        lr.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HandleTouchInput();
        HandledMoouseInput();
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.sqrMagnitude > rotationThreshold) // evitar errores cuando casi no se mueve
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90;
        }
    }


    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                DragStart(touch.position);
            }

            if(touch.phase == TouchPhase.Moved)
            {
                Dragging(touch.position);
            }

            if(touch.phase == TouchPhase.Ended)
            {
                DragEnd(touch.position);
            }
        }
    }


    private void HandledMoouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragStart(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Dragging(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            DragEnd(Input.mousePosition);
        }
    }


    private void DragStart(Vector3 screenPosition)
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(screenPosition);
        dragStartPos.z = 0;//solo 2D

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;  // Desactiva la gravedad mientras se arrastra
        rb.Sleep();            // Detiene por completo la simulación física


        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }

    private void Dragging(Vector3 screenPosition)
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(screenPosition);
        draggingPos.z = 0f; //Solo 2D
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }

    private void DragEnd(Vector3 screenPosition)
    {
        rb.gravityScale = gravityScale;       
        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(screenPosition);
        dragStartPos.z = 0;
        rb.angularVelocity = 0f;

        Vector3 direction = (dragStartPos - dragReleasePos).normalized;
        float magnitud = Vector3.Distance(dragStartPos, dragReleasePos);

        Vector3 force = direction * magnitud * power;

        // Eleva ligeramente para evitar que esté en contacto con el suelo
        transform.position += new Vector3(0, 0.5f, 0);
        rb.WakeUp();
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
