using UnityEngine;
using TMPro;

public class GameManager: MonoBehaviour
{
    //Score counter
    public int score = 0;
    
    //Total bricks count the conditions to win
    public int totalBricks;
    // Represents text on screen (UI), UI text object
    public TextMeshProUGUI scoreText;

    // this win text object
    public TextMeshProUGUI winText;

    //this game object of the ball 
    public GameObject ball;

    void Start()
    {
        //At the start they count the bricks 
        totalBricks = FindObjectsOfType<Brick>().Length;
        
    }
    public void AddScore()
    {   
        //Increase score by 10 with every brick broken
        score += 10;
        //Update the UI counter for the player
        scoreText.text = "Score: " + score;
        //Decrease the total brick count by one with every count + meaning a brick was broken
        totalBricks--;

        //Check if brick counts is 0 meaning all breaks are broken
        if (totalBricks <= 0)
        {
            //Shows you win ui on screen for player
            winText.gameObject.SetActive(true);
            
            //Stop the ball once all bricks are gone win condition
            ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            //Output Win cause all bricks are gone
            Debug.Log("YOU WIN");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
