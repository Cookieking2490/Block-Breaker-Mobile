using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase;
using Firebase.Extensions;

public class AuthManager : MonoBehaviour
{
    // Email input object
    public TMP_InputField emailInput;
  
    // Password input object
    public TMP_InputField passwordInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    //Ui login panel object
    public GameObject loginPanel;

    //Fire base authentication object
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

    //Log out function 
    public void Logout()
    {
        //Authentication log out
        auth.SignOut();
        //Show login Panel
        loginPanel.SetActive(true);
        //Hide menu
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
        //Remove login panel
        loginPanel.SetActive(false);
        
        FindObjectOfType<GameManager>().LoadScore();
        
        //Show the menu panel
        startPanel.SetActive(true);
        
        //Game stays paused
        Time.timeScale = 0f;
    }
    
}
