using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;

    Vector3 dragStartPos;

    Rigidbody2D rb;
    LineRenderer lr;    
    Touch touch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        // Verificamos la entrada táctil o el mouse             
        HandleTouchInput();
        HandleMouseInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                DragStart(touch.position);

            if (touch.phase == TouchPhase.Moved)
                Dragging(touch.position);

            if (touch.phase == TouchPhase.Ended)
                DragRelease(touch.position);
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // Si el botón izquierdo del mouse acaba de ser presionado        
            DragStart(Input.mousePosition);

        // No hay un "MousePhase.Moved" explícito, la posición del mouse se actualiza continuamente
        // Asumimos que si el botón está presionado, estamos "arrastrando"
        if (Input.GetMouseButton(0))        
            Dragging(Input.mousePosition);
                

        if (Input.GetMouseButtonUp(0)) // Si el botón izquierdo del mouse acaba de ser soltado        
            DragRelease(Input.mousePosition);        
    }

    private void DragStart(Vector3 screenPosition)
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(screenPosition);
        dragStartPos.z = 0f;//Solo 2D
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

    private void DragRelease(Vector3 screenPosition)
    {
        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(screenPosition);
        dragReleasePos.z = 0f; //Solo 2D
        rb.angularVelocity = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        
        float angle = Mathf.Atan2(clampedForce.y, clampedForce.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90;

        rb.AddForce(clampedForce, ForceMode2D.Impulse);
    }
}