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
    private GameObject loginOutput;
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
    private GameObject registerOutput;
    [SerializeField]
    private TMP_Text registerOutputText;

    [Header("Forget Password References")]
    [SerializeField]
    private TMP_InputField forgetPasswordEmail;
    [SerializeField]
    private GameObject forgotPasswordOutput;
    [SerializeField]
    private TMP_Text forgotPasswordOutputText;

    [Header("Home References")]
    [SerializeField]
    private TMP_Text welcomeOutputText;

    [Header("Profile References")]
    [SerializeField]
    private TMP_Text profileNameOutputText;

    [Header("Bottom Banner")]
    [SerializeField]
    private TMP_Text bottomBannerOutputText;

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

    private void Start()
    {
        StartCoroutine(CheckAndFixDependencies());
    }

    private IEnumerator CheckAndFixDependencies()
    {
        var checkAndFixDependenciesTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(predicate: () => checkAndFixDependenciesTask.IsCompleted);

        var dependencyResult = checkAndFixDependenciesTask.Result;

        if (dependencyResult == DependencyStatus.Available)
        {
            InitializeFirebase();
        }
        else
        {
            Debug.LogError($"Could not resolve all Firebase dependencies: (dependencyResult)");
        }
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        StartCoroutine(CheckAutoLogin());

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private IEnumerator CheckAutoLogin()
    {
        yield return new WaitForEndOfFrame();

        if (user != null)
        {
            var reloadUserTask = user.ReloadAsync();

            yield return new WaitUntil(predicate: () => reloadUserTask.IsCompleted);

            AutoLogin();
        }
        else
        {
            AuthUIManager.instance.LoginScreen();
        }
    }

    private void AutoLogin()
    {
        if (user != null)
        {
            if (user.IsEmailVerified)
            {
                //Change scene to home screen
                //GameManager.instance.ChangeScene(1); //from the video tutorial

                AuthUIManager.instance.HomeScreen();
            }
            else
            {
                StartCoroutine(SendVerificationEmail());
            }
        }
        else
        {
            AuthUIManager.instance.LoginScreen();
        }
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
                welcomeOutputText.text = $"Welcome {user.DisplayName}!";
                profileNameOutputText.text = $"{ user.DisplayName}";
                bottomBannerOutputText.text = $"Signed in as: {user.DisplayName}";
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

    public void ForgetPassword()
    {
        StartCoroutine(ResetPassword(forgetPasswordEmail.text));
    }

    public void SignOutButton()
    {
        if (user != null)
        {
            //signs current user out
            auth.SignOut();
            Debug.Log("User has been successfully signed out");
            //return to login screen
            AuthUIManager.instance.LoginScreen();
        }
        else
        {
            //do nothing
        }
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
            loginOutput.SetActive(true);
            yield return new WaitForSeconds(5);
            loginOutput.SetActive(false);
        }
        else
        {
            if (user.IsEmailVerified)
            {
                yield return new WaitForSeconds(1f);

                // Change scene to the home screen for the app
                AuthUIManager.instance.HomeScreen();
            }
            else
            {
                //TODO: Send verification email to user
                StartCoroutine(SendVerificationEmail());
                //Temporary State for Testing
                /*AuthUIManager.instance.HomeScreen();*/

            }
        }
    }

    private IEnumerator RegisterLogic(string _username, string _email, string _password, string _confirmPassword)
    {
        if (_username == "")
        {
            registerOutputText.text = "Please enter a username";
            registerOutput.SetActive(true);
            yield return new WaitForSeconds(5);
            registerOutput.SetActive(false);
        } 
        else if (_password != _confirmPassword)
        {
            registerOutputText.text = "Passwords do not match!";
            registerOutput.SetActive(true);
            yield return new WaitForSeconds(5);
            registerOutput.SetActive(false);
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
                registerOutput.SetActive(true);
                yield return new WaitForSeconds(5);
                registerOutput.SetActive(false);
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
                    registerOutput.SetActive(true);
                    yield return new WaitForSeconds(5);
                    registerOutput.SetActive(false);
                }
                else
                {
                    Debug.Log($"Firebase user has been created successfully: {user.DisplayName} ({user.UserId})");

                    //Send a verficiation email to the user
                    StartCoroutine(SendVerificationEmail());
                }
            }
        }
    }

    private IEnumerator SendVerificationEmail()
    {
        if (user != null)
        {
            var emailTask = user.SendEmailVerificationAsync();

            yield return new WaitUntil(predicate: () => emailTask.IsCompleted);
            
            if (emailTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)emailTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "Unknown error, try again!";

                switch (error)
                {
                    case AuthError.Cancelled:
                        output = "Verification task was cancelled";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        output = "Invalid email";
                        break;
                    case AuthError.TooManyRequests:
                        output = "Too many requests";
                        break;
                }

                AuthUIManager.instance.AwaitVerification(false, user.Email, output);
            }
            else
            {
                AuthUIManager.instance.AwaitVerification(true, user.Email, null);
                Debug.Log("Email sent successfully");
            }
        }
    }

    private IEnumerator ResetPassword(string _email)
    {
        AuthUIManager.instance.ForgetPasswordScreen();

        var passwordTask = auth.SendPasswordResetEmailAsync(_email); //changed to input field text that has been entered

        yield return new WaitUntil(predicate: () => passwordTask.IsCompleted);

        if (passwordTask.IsCanceled)
        {
            Debug.LogError("SendPasswordResetEmailAsync was cancelled.");
        }
        if (passwordTask.IsFaulted)
        {
            Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + passwordTask.Exception);
        }

        if (passwordTask.Exception != null)
        {
            FirebaseException firebaseException = (FirebaseException)passwordTask.Exception.GetBaseException();
            AuthError error = (AuthError)firebaseException.ErrorCode;
            string output = "No account is associated with this email. Please register an account.";

            forgotPasswordOutputText.text = output;
            forgotPasswordOutput.SetActive(true);
            yield return new WaitForSeconds(5);
            forgotPasswordOutput.SetActive(false);
        }
        else
        {
            if (user.IsEmailVerified)
            {
                Debug.Log("Password reset email sent successfully.");
                string output = "Password reset email sent. You should receive an email shortly.";
                forgotPasswordOutputText.text = output;
                forgotPasswordOutput.SetActive(true);
                yield return new WaitForSeconds(5);
                forgotPasswordOutput.SetActive(false);
            }
            else
            {
                Debug.Log("No account is associated with this email. Please register an account.");
                forgotPasswordOutput.SetActive(true);
                yield return new WaitForSeconds(5);
                forgotPasswordOutput.SetActive(false);

            }
        }
    }
}
