using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    
    //limit the movement of the paddle so it doesn't go beyond the two walls
    public float minX = -4f;

    public float maxX = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        // -1 is left , 1 is right , 0 means no input
        float move= Input.GetAxis("Horizontal");
        //Move paddle in horizontal movement, time delta keeps movement smooth regardless of fps count
        transform.Translate(new Vector2(move * speed * Time.deltaTime, 0));
        
        // keep paddle postion between min and max
        float limit= Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(limit, transform.position.y, 0);
    }
    
}
