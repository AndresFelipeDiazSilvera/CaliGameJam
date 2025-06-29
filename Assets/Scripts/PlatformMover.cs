using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 3f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
    }
}
