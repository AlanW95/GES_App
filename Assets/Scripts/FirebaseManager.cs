using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    //References
    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser user;
    [Space(5f)]

    [Header("Login References")]
    [SerializeField]
    private TMP_InputField loginEmail;
    [SerializeField]
    private TMP_InputField loginPassword;
    [SerializeField]
    private TMP_Text loginOutputText;
    [Space(5f)]

    [Header("Register References")]
    [SerializeField]
    private TMP_InputField registerUsername;
    [SerializeField]
    private TMP_InputField registerEmail;
    [SerializeField]
    private TMP_InputField registerPassword;
    [SerializeField]
    private TMP_InputField registerConfirmPassword;
    [SerializeField]
    private TMP_Text registerOutputText;

    private void Awake() {

        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(checkDependancyTask =>
        {
            var dependencyStatus = checkDependancyTask.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                Debug.Log("Signed Out");
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log($"Signed In: {user.DisplayName}");
            }
        }
    }

    public void ClearOutputs()
    {
        loginOutputText.text = "";
        registerOutputText.text = "";
    }

    public void LoginButton()
    {
        StartCoroutine(LoginLogic(loginEmail.text, loginPassword.text));
    }

    public void RegisterButton()
    {
        StartCoroutine(RegisterLogic(registerUsername.text, registerEmail.text, registerPassword.text, registerConfirmPassword.text));
    }

    private IEnumerator LoginLogic(string _email, string _password)
    {
        Credential credential = EmailAuthProvider.GetCredential(_email, _password);

        var loginTask = auth.SignInWithCredentialAsync(credential);

        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            FirebaseException firebaseException = (FirebaseException)loginTask.Exception.GetBaseException();
            AuthError error = (AuthError)firebaseException.ErrorCode;
            string output = "Unknown error, please try again!";

            switch (error)
            {
                case AuthError.MissingEmail:
                    output = "Please enter your email";
                    break;
                case AuthError.MissingPassword:
                    output = "Please enter your password";
                    break;
                case AuthError.InvalidEmail:
                    output = "Invalid email";
                    break;
                case AuthError.WrongPassword:
                    output = "Incorrect password";
                    break;
                case AuthError.UserNotFound:
                    output = "Account does not exist";
                    break;
            }

            loginOutputText.text = output;
        }
        else
        {
            if (user.IsEmailVerified)
            {
                yield return new WaitForSeconds(1f);
                //TODO: Change scene to the welcome/ home screen for the app

            }
            else
            {
                //TODO: Send verification email to user

                //TODO: Send to welcome/ home screen as temporary state
            }
        }
    }

    private IEnumerator RegisterLogic(string _username, string _email, string _password, string _confirmPassword)
    {
        if (_username == "")
        {
            registerOutputText.text = "Please enter a username";
        } 
        else if (_password != _confirmPassword)
        {
            registerOutputText.text = "Passwords do not match!";
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "Unknown error, please try again!";

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        output = "Invalid email";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "Email is already in use";
                        break;
                    case AuthError.WeakPassword:
                        output = "Weak password";
                        break;
                    case AuthError.MissingEmail:
                        output = "Please enter your email";
                        break;
                    case AuthError.MissingPassword:
                        output = "Please enter your password";
                        break;
                }

                registerOutputText.text = output;
            }
            else
            {
                //Profile is create for user
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username,

                    //TODO: Give profile default photo
                };

                var defaultUserTask = user.UpdateUserProfileAsync(profile);

                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);

                if (defaultUserTask.Exception != null)
                {
                    user.DeleteAsync();
                    FirebaseException firebaseException = (FirebaseException)defaultUserTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "Unknown error, please try again!";

                    switch (error)
                    {
                        case AuthError.Cancelled:
                            output = "Update user cancelled";
                            break;
                        case AuthError.SessionExpired:
                            output = "Session expired";
                            break;
                    }

                    registerOutputText.text = output;
                }
                else
                {
                    Debug.Log($"Firebase user has been created successfully: {user.DisplayName} ({user.UserId})");

                    //TODO: Send a verficiation email to the user
                }
            }
        }
    }
}
