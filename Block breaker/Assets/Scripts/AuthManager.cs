using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase;
using Firebase.Extensions;

public class AuthManager : MonoBehaviour
{
    
    public TMP_InputField emailInput;
    
    public TMP_InputField passwordInput;
    
    public GameObject loginPanel;
    
    private FirebaseAuth auth;

    public GameObject startPanel;
    
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase Ready");
                
                //Check if user is Already logged in
                if (auth.CurrentUser != null)
                {
                    Debug.Log("User is logged in");

                    loginPanel.SetActive(false);
                    startPanel.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
            else
            {
                Debug.LogError("Firebase Not ready: " + task.Result);
            }
        });
    }

    
    public void Logout()
    {
        
        auth.SignOut();
        
        loginPanel.SetActive(true);
        
        startPanel.SetActive(false);
        Debug.Log("User logged out");
    }

    public void Register()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("Registered Successfully");
            }
            else
            {
                Debug.LogError("Register Failed" + task.Exception);
            }
        });
    }

    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("Login Success");
                EnterGame();
            }
            else
            {
                Debug.LogError("Login Failed" + task.Exception);
            }
        });
    }

    void EnterGame()
    {
        
        loginPanel.SetActive(false);
        
        if(startPanel != null)
            startPanel.SetActive(true);
        
        FindObjectOfType<GameManager>().LoadScore();
        
        
        startPanel.SetActive(true);
        
        
        Time.timeScale = 0f;
    }
    
}
