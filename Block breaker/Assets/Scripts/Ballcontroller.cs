using UnityEngine;
using UnityEngine.SceneManagement;

public class Ballcontroller : MonoBehaviour
{
    
    private Rigidbody2D rb;
    
    public float speed= 6f;
    
    public GameManager gm;
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            FindObjectOfType<GameManager>().audioSource.PlayOneShot(
                FindObjectOfType<GameManager>().paddleHitSound
            );
        }
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("LoseZone"))
        {
            
            Debug.Log("Game Over");
            
            gm.ShowGameOver();
            
            
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void LaunchBall()
    {
        rb.linearVelocity = new Vector2(2f, speed);
    }
    
}
