using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    
    //limit the movement of the paddle so it doesn't go beyond the two walls
    float moveDirection= 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
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
