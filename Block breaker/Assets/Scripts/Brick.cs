using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameManager gm;
    public GameObject breakEffect;

    void Start()
    {   
     gm= FindObjectOfType<GameManager>();
    }

    void BreakBrick()
    {
        Instantiate(breakEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            FindObjectOfType<GameManager>().audioSource.PlayOneShot(
                FindObjectOfType<GameManager>().brickHitSound
                );
            BreakBrick();
            
            gm.AddScore();
            
            Destroy(gameObject);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }
}
