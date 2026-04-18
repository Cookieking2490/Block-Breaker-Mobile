using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    
    
    float moveDirection= 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * speed, 0);
    }

    public void MoveLeft()
    {
        moveDirection = -1f;
    }

    public void MoveRight()
    {
        moveDirection = 1f;
    }

    public void Stop()
    {
        moveDirection = 0f;
    }
    
}
