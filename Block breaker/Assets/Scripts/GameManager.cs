using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;
using System.Collections.Generic;
using Firebase.Extensions;

public class GameManager: MonoBehaviour
{
    //Database
    private FirebaseFirestore db;

    //Text of scores of the three levels
    public TextMeshProUGUI Level1Text;
    public TextMeshProUGUI Level2Text;
    public TextMeshProUGUI Level3Text;
    //Auth
    private FirebaseAuth auth;
    //Score counter
    public int score = 0;
    
    //Total bricks count the conditions to win
    public int totalBricks;
    // Represents text on screen (UI), UI text object
    public TextMeshProUGUI scoreText;

    // this win text object
    public TextMeshProUGUI winText;
    
    // The object Restart button
    public GameObject restartButton;

    //this game object of the ball 
    public GameObject ball;
    
    //Game over text object
    public TextMeshProUGUI gameOverText;
    
    //Start panel object
    public GameObject startPanel;
    
    //Function to show Game over text if the ball falls and fails level
    public void ShowGameOver()
    {
        
        //Show text once game is over and function is called set to true(1)
        gameOverText.gameObject.SetActive(true);
        
        if (restartButton != null)
            restartButton.SetActive(true);
        else
            Debug.LogError("RestartButton is NULL");
        
        //Calling of function to save score to database
        SaveScore(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        
        //Show restart button when you lose the round
        restartButton.SetActive(true);
        Time.timeScale = 0f;
        
    }

    void Start()
    {
        db= FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
        //At the start they count the bricks 
        Time.timeScale = 0f;
        totalBricks = FindObjectsOfType<Brick>().Length;

        if (Level1Text != null)
        {
            LoadScore();
        }
    }

    public void LoadScore()
    {
        if (auth.CurrentUser == null) return;

        string userId = auth.CurrentUser.UserId;

        db.Collection("users").Document(userId).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.Result.Exists) return;

            var data = task.Result.ToDictionary();

            Level1Text.text = "Level 1: " + GetScore(data, "Level 1");
            Level2Text.text = "Level 2: " + GetScore(data, "Level 2");
            Level3Text.text = "Level 3: " + GetScore(data, "Level 3");
        });
    }

    //Function to save score after playing game or round
    void SaveScore(string levelName)
    {
        if (auth.CurrentUser == null) return;
        
        string userId = auth.CurrentUser.UserId;

        DocumentReference docRef = db.Collection("users").Document(userId);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Dictionary<string, object> data;
            if (task.Result.Exists)
            {
                data = task.Result.ToDictionary();
            }
            else
            {
                data = new Dictionary<string, object>();
            }

            int newScore = score;
            if (data.ContainsKey(levelName))
            {
                int oldScore = (int)(long)data[levelName];
                if (newScore > oldScore)
                {
                    data[levelName] = newScore;
                }
            }
            else
            {
                data[levelName] = newScore;
            }

            docRef.SetAsync(data);

            Debug.Log("High score updated");
        });
    }

    string GetScore(Dictionary<string, object> data, string level)
    {
        if (!data.ContainsKey(level))
            return "0";
        
        long scoreValue = (long)data[level];

        return scoreValue.ToString();
    }
    public void AddScore()
    {   
        //Increase score by 10 with every brick broken
        score += 10;
        //Update the UI counter for the player
        scoreText.text = ""+score;
        //Decrease the total brick count by one with every count + meaning a brick was broken
        totalBricks--;

        //Check if brick counts is 0 meaning all breaks are broken
        if (totalBricks <= 0)
        {
            SaveScore(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            //Shows you win ui on screen for player
            winText.gameObject.SetActive(true);
            
            //After win condition met, the restart button will be showen
            restartButton.SetActive(true);
            
            //Stop the ball once all bricks are gone win condition
            ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            //Output Win cause all bricks are gone
            Debug.Log("YOU WIN");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
