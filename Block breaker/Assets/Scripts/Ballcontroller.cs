using UnityEngine;
using UnityEngine.SceneManagement;

public class Ballcontroller : MonoBehaviour
{
    //stores the component of the ball
    private Rigidbody2D rb;
    //Controls how fast the ball moves vertically
    public float speed= 6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameManager gm;
    void Start()
    {
        //gets rigidbody2D attached to the ball
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        
    }

    //Function to see if ball touches the Lose zone 
    void OnTriggerEnter2D(Collider2D collision)
    {
        // if collision with LoseZone happens print Game over
        if (collision.CompareTag("LoseZone"))
        {
            //Console output for testing
            Debug.Log("Game Over");
            
            gm.ShowGameOver();
            
            //Stop the ball once game is over and it touchs LoseZone
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void LaunchBall()
    {
        rb.linearVelocity = new Vector2(2f, speed);
    }

    // Update is called once per frame note: empty because ball movement is handled by physics
    void Update()
    {
        
    }
}
