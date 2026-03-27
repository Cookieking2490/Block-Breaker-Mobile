using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        //Find GameManager in scene
        gm = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
            //Increase score when brick breaks
            gm.AddScore();
            //once ball collide with brick it breaks
            Destroy(gameObject);
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }
}
