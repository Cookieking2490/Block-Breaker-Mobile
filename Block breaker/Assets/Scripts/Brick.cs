using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameManager gm;

    void Start()
    {   
     gm= FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //Increase score when brick breaks
            gm.AddScore();
            //once ball collide with brick it breaks
            Destroy(gameObject);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }
}
