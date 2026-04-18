using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;
using System.Collections.Generic;
using Firebase.Extensions;

public class GameManager: MonoBehaviour
{
    
    private FirebaseFirestore db;

    
    public TextMeshProUGUI Level1Text;
    public TextMeshProUGUI Level2Text;
    public TextMeshProUGUI Level3Text;
    public AudioSource audioSource;

    public AudioClip brickHitSound;
    public AudioClip paddleHitSound;
    
    private FirebaseAuth auth;
    
    public int score = 0;
    
    
    public int totalBricks;
    
    public TextMeshProUGUI scoreText;

    
    public TextMeshProUGUI winText;
    
    
    public GameObject restartButton;

    
    public GameObject ball;
    
    
    public TextMeshProUGUI gameOverText;
    
    
    public GameObject startPanel;
    
    
    public void ShowGameOver()
    {
        
        
        gameOverText.gameObject.SetActive(true);
        
        if (restartButton != null)
            restartButton.SetActive(true);
        else
            Debug.LogError("RestartButton is NULL");
        
        
        SaveScore(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        
        
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
        
        score += 10;
        
        scoreText.text = ""+score;
        
        totalBricks--;

        
        if (totalBricks <= 0)
        {
            SaveScore(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            
            winText.gameObject.SetActive(true);
            
            
            restartButton.SetActive(true);
            
            
            ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            
            Debug.Log("YOU WIN");
        }
    }
   
}
