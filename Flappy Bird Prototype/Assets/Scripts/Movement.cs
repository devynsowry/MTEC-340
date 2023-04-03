using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    private float leftEdge;

    private void Start()
    {
        // Whenever pipes are past the edge of screen, no matter the size
        // - 1f to make sure the pipes are off of the screen completely
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        // Constantly move the pipes left using deltaTime
        transform.position += Vector3.left * speed * Time.deltaTime;
        // If pipes are off the screen, destroy them
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
