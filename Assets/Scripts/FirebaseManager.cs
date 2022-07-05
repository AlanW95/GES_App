using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using System.Linq;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    //References
    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference DBreference;
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
    [Space(5f)]

    //TODO: Add SkillsField and ExperienceField
    [Header("Register 2 References")]
    [SerializeField]
    private TMP_InputField universityField;
    [SerializeField]
    private TMP_InputField profileUsernameField;
    /*[SerializeField]
    private TMP_InputField skillsField;
    [SerializeField]
    private TMP_InputField experienceField;*/
    [Space(5f)]

    [Header("Forget Password References")]
    [SerializeField]
    private TMP_InputField forgetPasswordEmail;
    [SerializeField]
    private GameObject forgotPasswordOutput;
    [SerializeField]
    private TMP_Text forgotPasswordOutputText;
    [Space(5f)]

    [Header("Coach Selection References")]
    [SerializeField]
    private TMP_Text coachSelectionWelcomeText;
    [SerializeField]
    private TMP_Text coachEmotionText;
    private string selectedCoach;
    [SerializeField]
    private GameObject coachText;
    [SerializeField]
    private TMP_Text coachSelectionText;
    [SerializeField]
    private int coach = 0;
    private string selectedEmotion;
    [SerializeField]
    private GameObject emotionText;
    [SerializeField]
    private TMP_Text emotionSelectionText;
    [SerializeField]
    private int emotion = 0;
    [SerializeField]
    private Button[] coachButtons;
    [Space(5f)]

    [Header("Home References")]
    [SerializeField]
    private TMP_Text welcomeOutputText;
    [Space(5f)]

    [Header("Profile References")]
    [SerializeField]
    private TMP_Text profileNameOutputText;
    [SerializeField]
    private TMP_Text profileUniversityText;
    [SerializeField]
    private TMP_Text profileUsernameText;
    [Space(5f)]

    [Header("Bottom Banner")]
    [SerializeField]
    private TMP_Text bottomBannerOutputText;

    [Header("Managers")]
    [SerializeField]
    private AccountManager accountManager;
    [SerializeField]
    private DynamicInterfaceAreaUI dynamicUIManager;

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

    /*private void FixedUpdate()
    {
        //TODO: Add UniversityField, SkillsField and ExperienceField
        SaveProfileButton();
        *//*StartCoroutine(UpdateSkills(int.Parse(skillsField.text)));
        StartCoroutine(UpdateExperience(int.Parse(experienceField.text)));*//*
    }*/

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

        //database
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        /*FirebaseDatabase.DefaultInstance
            .GetReference("users").Child(user.UserId).Child("skills")
            .ValueChanged += HandleUpdateSkill;*/
    }

    /*public void HandleUpdateSkill(object sender, ValueChangedEventArgs args)
    {
        DataSnapshot snapshot = args.Snapshot;
        Debug.Log("Handle Update Skill: " + snapshot.Value);
    }*/

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
                StartCoroutine(LoadUserData(/*null*/));
                //CallUpdateSkills();

                //TODO: ADD BACK IN LATER - LOADING IN DATA WITH ACCOUNT
                //StartCoroutine(LoadSkills());

                /*AuthUIManager.instance.HomeScreen();*/

                //TODO: WE WILL RETURN TO THIS SCREEN AFTER THE LAUNCH BUILD
                //AuthUIManager.instance.CoachSelectionScreen();

                AuthUIManager.instance.HomeScreen();

                /*for (int i=0; i<coachButtons.Length; i++)
                {
                    coachButtons[i].interactable = true;
                }*/

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
                loginOutputText.text = "Signing in...";
                StartCoroutine(SignOutLogic());
                coachSelectionWelcomeText.text = $"Hi {user.DisplayName}!";
                coachEmotionText.text = $"How are you feeling today, {user.DisplayName}?";
                welcomeOutputText.text = $"Welcome {user.DisplayName} to the GES App!";
                profileNameOutputText.text = $"{ user.DisplayName}";
                universityField.text = $"{profileUniversityText.text}";
                bottomBannerOutputText.text = $"Signed in as: {user.DisplayName}";
            }
        }
    }

    public void ClearOutputs()
    {
        loginOutputText.text = "";
        registerOutputText.text = "";
    }

    public void ClearRegisterFields()
    {
        registerUsername.text = "";
        registerEmail.text = "";
        registerPassword.text = "";
        registerConfirmPassword.text = "";
    }

    public void ClearLoginFields()
    {
        loginEmail.text = "";
        loginPassword.text = "";
    }

    public void LoginButton()
    {
        //SaveDataButton();
        StartCoroutine(LoginLogic(loginEmail.text, loginPassword.text));
    }

    public void RegisterButton()
    {
        StartCoroutine(RegisterLogic(registerUsername.text, registerEmail.text, registerPassword.text, registerConfirmPassword.text));
    }

    //For the time being all database references are saved to the database when a button is pressed
    //TODO: Change so data is automatically being saved through the coroutine - we don't want to be pressing a button to save data to the database

    //TODO: We would want to say that when the user is created, so in the RegisterLogic then it adds these to the database. We will be adding;
    //Username
    //Name
    //Email
    //Password
    //University
    //Skill
    //Experience
    //Reference
    //Artifact
    public void SaveDataButton()
    {
        //StartCoroutine(UpdateUsernameAuth(loginEmail.text));

        //StartCoroutine(UpdateUsernameDatabase("hello world"));
        StartCoroutine(UpdateUsernameDatabase(loginEmail.text));
        //Debug.Log(loginEmail.text);
        //Debug.Log("User data to be sent to database");
    }

    public void SaveProfileButton()
    {
        AuthUIManager.instance.ProfileScreen();

        if (universityField.text == "")
        {
            //do nothing
        } else
        {
            StartCoroutine(UpdateUniversity(universityField.text));

            profileUniversityText.text = universityField.text;
            profileUsernameText.text = profileUsernameField.text;
            universityField.text = "";
            profileUsernameField.text = "";
        }
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
            ClearLoginFields();
            ClearRegisterFields();
            //return to login screen
            AuthUIManager.instance.LoginScreen();
            loginOutputText.text = "User successfully signed out.";
            //TODO: REMOVE ALL FROM THE LIST - ADD REST
            accountManager.localUserAccount._skills.Clear();
            StartCoroutine(SignOutLogic());
        }
        else
        {
            //do nothing
        }
    }

    //-----------------------------------------------------------------------------------------

    private IEnumerator SignOutLogic()
    {
        loginOutput.SetActive(true);
        yield return new WaitForSeconds(4);
        loginOutput.SetActive(false);
        coachSelectionText.text = "";
        emotionSelectionText.text = "";
        //welcomeOutputText.text = "Welcome to the \nGES App!";
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
                StartCoroutine(LoadUserData(/*null*/));
                //TODO: RE-ADD THIS BACK IN LATER
                //StartCoroutine(LoadSkills());
                //CallUpdateSkills();
                yield return new WaitForSeconds(1f);

                // Change scene to the home screen for the app
                //AuthUIManager.instance.HomeScreen();
                //TODO: ENTER THE USERNAME
                coachButtons[6].interactable = false;
                coachButtons[13].interactable = false;
                //TODO: WE WILL RETURN TO THE COACH SELECTION AFTER THE LAUNCH BUILD OF THE APP
                //AuthUIManager.instance.CoachSelectionScreen();
                AuthUIManager.instance.HomeScreen();
                ClearLoginFields();
                ClearRegisterFields();
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

                    ClearLoginFields();
                    ClearRegisterFields();
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

    /*private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase Auth update user profile function passing the profile with the username
        var ProfileTask = user.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }
    }*/

    #region Write to Database

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        yield return new WaitForSeconds(5);

        var DBTask = DBreference.Child("users").Child(user.UserId).Child("username").SetValueAsync(_username);

        Debug.Log("UpdateUsernameDatabase has been ran");

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateUniversity(string _university)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("university").SetValueAsync(_university);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else
        {
            //University is now updated
        }
    }

    private IEnumerator UpdateCoach(string _coach)
    {
        //Addition of time was added to accomodate the database storing the users different selections
        string dateTimeString = System.DateTime.Now.ToString();
        System.DateTime dateTime = System.DateTime.Parse(dateTimeString);

        var DBTask = DBreference.Child("users").Child(user.UserId).Child("coach").Child(dateTime.ToString("yyyy-MM-dd HH:mm")).SetValueAsync(_coach);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Coach is now updated
        }
    }

    private IEnumerator SendSkills(string _skill, string _description, int _level)
    {

        var DBTask = DBreference.Child("users").Child(user.UserId).Child("skills").Child(_skill).Child("name").SetValueAsync(_skill);
        DBreference.Child("users").Child(user.UserId).Child("skills").Child(_skill).Child("description").SetValueAsync(_description);
        DBreference.Child("users").Child(user.UserId).Child("skills").Child(_skill).Child("level").SetValueAsync(_level);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Skills are now updated
        }
    }

    public void CallSendSkills(string _skill, string _description, int _level)
    {
        StartCoroutine(SendSkills(_skill, _description, _level));
    }

    private IEnumerator UpdateExperience(string _place, string _role, string _startDate, string _endDate, string _description, string _comments, List<string> _skills/*, List<string> _courseOccurred*/)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("experiences").Child(_role).Child("place").SetValueAsync(_place);
        DBreference.Child("users").Child(user.UserId).Child("experiences").Child(_role).Child("role").SetValueAsync(_role);
        DBreference.Child("users").Child(user.UserId).Child("experiences").Child(_role).Child("start").SetValueAsync(_startDate);
        DBreference.Child("users").Child(user.UserId).Child("experiences").Child(_role).Child("end").SetValueAsync(_endDate);
        DBreference.Child("users").Child(user.UserId).Child("experiences").Child(_role).Child("description").SetValueAsync(_description);
        DBreference.Child("users").Child(user.UserId).Child("experiences").Child(_role).Child("comments").SetValueAsync(_comments);

        foreach (var x in _skills)
        {
            if (x != null)
            {
                //Debug.Log(x.ToString());
                DBreference.Child("users").Child(user.UserId).Child("experiences").Child(_role).Child("skills").Child(x.ToString()).Child("name").SetValueAsync(x.ToString());
            } else
            {
                DBreference.Child("users").Child(user.UserId).Child("experiences").Child(_role).Child("skills").SetValueAsync("null");
            }
        }

        //TODO: ADD WITH REFERENCES AND ARTIFACTS NOT NEEDED IN EXPERIENCES
        /*foreach (var x in _courseOccurred)
        {
            //Debug.Log(x.ToString());
            DBreference.Child("users").Child(user.UserId).Child("experiences").Child(_role).Child("occurrence").Child(x.ToString()).SetValueAsync(x.ToString());
        }*/

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Experiences are now update
        }
    }

    public void CallSendExperiences(string _experience, string _role, string _startDate, string _endDate, string _description, string _comments, List<string> _skills/*, List<string> _courseOccurred*/)
    {
        StartCoroutine(UpdateExperience(_experience, _role, _startDate, _endDate, _description, _comments, _skills/*, _courseOccurred*/));
    }

    private IEnumerator UpdateArtifacts(string _type, string _title, string _description, string _url, string _content, List<string> _skills, List<string> _experiences)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("artifacts").Child(_title).Child("title").SetValueAsync(_title);
        DBreference.Child("users").Child(user.UserId).Child("artifacts").Child(_title).Child("type").SetValueAsync(_type);
        DBreference.Child("users").Child(user.UserId).Child("artifacts").Child(_title).Child("description").SetValueAsync(_description);
        if (_url != null)
        {
            DBreference.Child("users").Child(user.UserId).Child("artifacts").Child(_title).Child("url").SetValueAsync(_url);
        } else
        {
            DBreference.Child("users").Child(user.UserId).Child("artifacts").Child(_title).Child("url").SetValueAsync("null");
        }

        DBreference.Child("users").Child(user.UserId).Child("artifacts").Child(_title).Child("content").SetValueAsync(_content);

        foreach (var x in _skills)
        {
            if (x != null)
            {
                //Debug.Log(x.ToString());
                DBreference.Child("users").Child(user.UserId).Child("artifacts").Child(_title).Child("skills").Child(x.ToString()).Child("name").SetValueAsync(x.ToString());
            } else
            {
                DBreference.Child("users").Child(user.UserId).Child("artifacts").Child(_title).Child("skills").Child(x.ToString()).SetValueAsync("null");
            }

        }

        foreach (var x in _experiences)
        {
            if (x != null)
            {
                //Debug.Log(x.ToString());
                DBreference.Child("users").Child(user.UserId).Child("artifacts").Child(_title).Child("experiences").Child(x.ToString()).Child("name").SetValueAsync(x.ToString());
            } else
            {
                DBreference.Child("users").Child(user.UserId).Child("artifacts").Child(_title).Child("experiences").SetValueAsync("null");
            }

        }

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Artifacts are now updated
        }
    }

    public void CallSendArtifacts(string _type, string _title, string _description, string _url, string _content, List<string> _skills, List<string> _experiences)
    {
        StartCoroutine(UpdateArtifacts(_type, _title, _description, _url, _content, _skills, _experiences));
    }

    private IEnumerator UpdateReferences(string _name, string _email, string _position, string _phonenumber, List<string> _skills)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("references").Child(_name).Child("name").SetValueAsync(_name);
        DBreference.Child("users").Child(user.UserId).Child("references").Child(_name).Child("email").SetValueAsync(_email);
        DBreference.Child("users").Child(user.UserId).Child("references").Child(_name).Child("position").SetValueAsync(_position);
        DBreference.Child("users").Child(user.UserId).Child("references").Child(_name).Child("phonenumber").SetValueAsync(_phonenumber);

        foreach (var x in _skills)
        {
            if (x != null)
            {
                //Debug.Log(x.ToString());
                DBreference.Child("users").Child(user.UserId).Child("references").Child(_name).Child("skills").Child(x.ToString()).Child("name").SetValueAsync(x.ToString());
            }
            else
            {
                DBreference.Child("users").Child(user.UserId).Child("references").Child(_name).Child("skills").SetValueAsync("null");
            }
        }

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else
        {
            //References are now updated
        }
    }

    public void CallSendReferences(string _name, string _email, string _position, string _phonenumber, List<string> _skills)
    {
        StartCoroutine(UpdateReferences(_name, _email, _position, _phonenumber, _skills));
    }

    private IEnumerator UpdateDreamJob(string _dreamjob, List<string> _dreamjobSkills)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("dreamjob").Child(_dreamjob).SetValueAsync(_dreamjobSkills);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Dream job has been updated
        }
    }

    #endregion Write to Database

    #region Coach and Emotion Selection

    public void Coach1() { coach = 1; CoachSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[0].interactable = false; coachButtons[13].interactable = false; }
    public void Coach2() { coach = 2; CoachSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[1].interactable = false; coachButtons[13].interactable = false; }
    public void Coach3() { coach = 3; CoachSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[2].interactable = false; coachButtons[13].interactable = false; }
    public void Coach4() { coach = 4; CoachSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[3].interactable = false; coachButtons[13].interactable = false; }
    public void Coach5() { coach = 5; CoachSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[4].interactable = false; coachButtons[13].interactable = false; }
    public void Coach6() { coach = 6; CoachSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[5].interactable = false; coachButtons[13].interactable = false; }

    public void CoachSelection()
    {
        switch (coach)
        {
            case 1:
                //Calm Clarice has been selected
                coachText.SetActive(true);
                coachSelectionText.text = "You have selected Calm Clarice!";
                selectedCoach = "Calm Clarice";

                //TODO: Add Clarice picture to the CoachQuestionsScreen

                break;
            case 2:
                //Funny Fabio has been selected
                coachText.SetActive(true);
                coachSelectionText.text = "You have selected Funny Fabio!";
                selectedCoach = "Funny Fabio";

                //TODO: Add Fabio picture to the CoachQuestionsScreen

                break;
            case 3:
                //Optimistic Alen has been selected
                coachText.SetActive(true);
                coachSelectionText.text = "You have selected Optimistic Alen!";
                selectedCoach = "Optimistic Alen";

                //TODO: Add Alen picture to the CoachQuestionsScreen

                break;
            case 4:
                //Enthusiastic Emily has been selected
                coachText.SetActive(true);
                coachSelectionText.text = "You have selected Enthusiastic Emily!";
                selectedCoach = "Enthusiastic Emily";

                //TODO: Add Emily picture to the CoachQuestionsScreen

                break;
            case 5:
                //Ambitious Fred has been selected
                coachText.SetActive(true);
                coachSelectionText.text = "You have selected Ambitious Fred!";
                selectedCoach = "Ambitious Fred";

                //TODO: Add Fred picture to the CoachQuestionsScreen

                break;
            case 6:
                //Logical Sam has been selected
                coachText.SetActive(true);
                coachSelectionText.text = "You have selected Logical Sam!";
                selectedCoach = "Logical Sam";

                //TODO: Add Sam picture to the CoachQuestionsScreen

                break;
            default:
                //No coach has been selected
                coachText.SetActive(true);
                coachSelectionText.text = "You still have to select a coach!";
                selectedCoach = "";
                break;
        }
    }

    public void ConfirmCoachSelection()
    {
        StartCoroutine(UpdateCoach(selectedCoach));

        //TODO: Change to the CoachQuestionScreen
        AuthUIManager.instance.CoachQuestionScreen();
    }

    /*
     * COACH FINISHED
     */

    /*
     * SELECTION OF EMOTION
     */
    private IEnumerator UpdateEmotion(string _emotion)
    {
        //Addition of time was added to accomodate the database storing the users different selections
        string dateTimeString = System.DateTime.Now.ToString();
        System.DateTime dateTime = System.DateTime.Parse(dateTimeString);

        var DBTask = DBreference.Child("users").Child(user.UserId).Child("coach").Child("emotions").Child(dateTime.ToString("yyyy-MM-dd HH:mm")).SetValueAsync(_emotion);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Emotion is now updated
        }
    }

    public void Emotion1() { emotion = 1; EmotionSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[7].interactable = false; }
    public void Emotion2() { emotion = 2; EmotionSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[8].interactable = false; }
    public void Emotion3() { emotion = 3; EmotionSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[9].interactable = false; }
    public void Emotion4() { emotion = 4; EmotionSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[10].interactable = false; }
    public void Emotion5() { emotion = 5; EmotionSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[11].interactable = false; }
    public void Emotion6() { emotion = 6; EmotionSelection(); for (int i = 0; i < coachButtons.Length; i++) { coachButtons[i].interactable = true; } coachButtons[12].interactable = false; }

    public void EmotionSelection()
    {
        switch (emotion)
        {
            case 1:
                //Anxious has been selected
                emotionText.SetActive(true);
                emotionSelectionText.text = "I am feeling anxious today.";
                selectedEmotion = "Anxious";
                break;
            case 2:
                //Curious has been selected
                emotionText.SetActive(true);
                emotionSelectionText.text = "I am feeling curious today.";
                selectedEmotion = "Curious";
                break;
            case 3:
                //Excited has been selected
                emotionText.SetActive(true);
                emotionSelectionText.text = "I am feeling excited today!";
                selectedEmotion = "Excited";
                break;
            case 4:
                //Lost has been selected
                emotionText.SetActive(true);
                emotionSelectionText.text = "I am feeling lost today.";
                selectedEmotion = "Lost";
                break;
            case 5:
                //Proud has been selected
                emotionText.SetActive(true);
                emotionSelectionText.text = "I am feeling proud today!";
                selectedEmotion = "Proud";
                break;
            case 6:
                //Tired has been selected
                emotionText.SetActive(true);
                emotionSelectionText.text = "I am feeling tired today.";
                selectedEmotion = "Tired";
                break;
            default:
                //No emotion has been selected
                emotionText.SetActive(true);
                emotionSelectionText.text = "You still have to select an emotion!";
                selectedEmotion = "";
                break;
        }
    }

    public void ConfirmEmotionSelection()
    {
        StartCoroutine(UpdateEmotion(selectedEmotion));

        //TODO: Change to the HomeScreen
        AuthUIManager.instance.HomeScreen();
    }

    #endregion Coach and Emotion Selection

    //TODO: Uncomment/ add StartCoroutine
    /*private IEnumerator UpdateSkills(int _skills)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("skills").SetValueAsync(_skills);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Skills is now updated
        }
    }

    //TODO: Uncomment/ add StartCoroutine
    private IEnumerator UpdateExperience(int _experience)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).Child("experience").SetValueAsync(_experience);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Experience is now updated
        }
    }*/

    //Loading in User Data with the Firebase Realtime Database
    private IEnumerator LoadUserData(/*string skill*/)
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            profileUniversityText.text = "No University assigned";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            profileUniversityText.text = snapshot.Child("university").Value.ToString();

            #region Skills Load Data
            //Debug.Log(snapshot.Child("skills").ChildrenCount.ToString());

            foreach (DataSnapshot childSnapshot in snapshot.Child("skills").Children.Skip(0))
            {
                /*Debug.Log(childSnapshot.Child("name").Value.ToString());
                Debug.Log(childSnapshot.Child("description").Value.ToString());
                Debug.Log(childSnapshot.Child("level").Value.ToString());*/
                string skill = childSnapshot.Child("name").Value.ToString();
                string description = childSnapshot.Child("description").Value.ToString();
                int level = int.Parse(childSnapshot.Child("level").Value.ToString());
                /*Debug.Log("The skill is: " + skill);*/

                dynamicUIManager._addNewSkillData = new SkillData();
                dynamicUIManager._addNewSkillData.Name = skill;
                dynamicUIManager._addNewSkillData.LevelName = description;
                dynamicUIManager._addNewSkillData.Level = level;
                //Debug.Log("The skill is: " + dynamicUIManager._addNewSkillData.Name + dynamicUIManager._addNewSkillData.LevelName + dynamicUIManager._addNewSkillData.Level);
                dynamicUIManager.CreateSkillButton(dynamicUIManager._addNewSkillData.Name, dynamicUIManager._addNewSkillData.LevelName, dynamicUIManager._addNewSkillData.Level, null);
                dynamicUIManager.SaveSkill();
                dynamicUIManager._addNewSkillData = null;
            }
            #endregion Skills Load Data

            #region Experience Load Data
            //Debug.Log(snapshot.Child("experiences").Value.ToString());

            foreach (DataSnapshot childSnapshot in snapshot.Child("experiences").Children.Skip(0))
            {
                /*Debug.Log(childSnapshot.Child("role").Value.ToString());
                Debug.Log(childSnapshot.Child("place").Value.ToString());
                Debug.Log(childSnapshot.Child("description").Value.ToString());
                Debug.Log(childSnapshot.Child("comments").Value.ToString());
                Debug.Log(childSnapshot.Child("start").Value.ToString());
                Debug.Log(childSnapshot.Child("end").Value.ToString());
                foreach (DataSnapshot innerChildSnapshot in childSnapshot.Child("skills").Children.Skip(0))
                {
                    Debug.Log(innerChildSnapshot.Child("name").Value.ToString());
                }*/

                dynamicUIManager._addNewExperienceData = new ExperienceData();
                dynamicUIManager._addNewExperienceData.RoleInExperience = childSnapshot.Child("role").Value.ToString();
                dynamicUIManager._addNewExperienceData.ExperienceLocale = childSnapshot.Child("place").Value.ToString();
                dynamicUIManager._addNewExperienceData.Description = childSnapshot.Child("description").Value.ToString();
                dynamicUIManager._addNewExperienceData.Comments = childSnapshot.Child("comments").Value.ToString();
                dynamicUIManager._addNewExperienceData.StartDate = System.DateTime.Parse(childSnapshot.Child("start").Value.ToString());
                dynamicUIManager._addNewExperienceData.EndDate = System.DateTime.Parse(childSnapshot.Child("end").Value.ToString());
                foreach (DataSnapshot innerChildSnapshot in childSnapshot.Child("skills").Children.Skip(0))
                {
                    dynamicUIManager._addNewExperienceData.Skills.Add(innerChildSnapshot.Child("name").Value.ToString());
                }
                foreach (DataSnapshot innerChildSnapshot in childSnapshot.Child("course").Children.Skip(0))
                {
                    dynamicUIManager._addNewExperienceData.CourseOccured.Add(innerChildSnapshot.Child("name").Value.ToString());
                }
                dynamicUIManager.SaveExperience();
                dynamicUIManager._addNewExperienceData = null;
            }
            #endregion Experience Load Data

            #region Artifact Load Data
            //Debug.Log(snapshot.Child("artifacts").Value.ToString());

            foreach (DataSnapshot childSnapshot in snapshot.Child("artifacts").Children.Skip(0))
            {
                /*Debug.Log(childSnapshot.Child("title").Value.ToString());
                Debug.Log(childSnapshot.Child("type").Value.ToString());
                Debug.Log(childSnapshot.Child("description").Value.ToString());
                Debug.Log(childSnapshot.Child("url").Value.ToString());

                foreach (DataSnapshot innerChildSnapshot in childSnapshot.Child("skills").Children.Skip(0))
                {
                    if (innerChildSnapshot.Child("name").Value != null)
                    {
                        Debug.Log(innerChildSnapshot.Child("name").Value.ToString());
                    } else
                    {
                        Debug.Log(innerChildSnapshot.Child("name").Value == null);
                    }
                    
                }
                foreach (DataSnapshot innerChildSnapshot in childSnapshot.Child("experiences").Children.Skip(0))
                {
                    if (innerChildSnapshot.Child("name").Value != null)
                    {
                        Debug.Log(innerChildSnapshot.Child("name").Value.ToString());
                    } else
                    {
                        Debug.Log(innerChildSnapshot.Child("name").Value == null);
                    }
                }*/

                dynamicUIManager._addNewArtifactData = new ArtifactData();
                dynamicUIManager._addNewArtifactData.Title = childSnapshot.Child("title").Value.ToString();
                if (childSnapshot.Child("type").Value.ToString() == "Document")
                {
                    dynamicUIManager._addNewArtifactData.type = ArtifactData.ArtifactType.Document;
                }
                if (childSnapshot.Child("type").Value.ToString() == "Image")
                {
                    dynamicUIManager._addNewArtifactData.type = ArtifactData.ArtifactType.Image;
                }
                if (childSnapshot.Child("type").Value.ToString() == "Link")
                {
                    dynamicUIManager._addNewArtifactData.type = ArtifactData.ArtifactType.Link;
                }
                if (childSnapshot.Child("type").Value.ToString() == "Repository")
                {
                    dynamicUIManager._addNewArtifactData.type = ArtifactData.ArtifactType.Repository;
                }
                if (childSnapshot.Child("type").Value.ToString() == "Video")
                {
                    dynamicUIManager._addNewArtifactData.type = ArtifactData.ArtifactType.Video;
                }
                if (childSnapshot.Child("type").Value.ToString() == "Note")
                {
                    dynamicUIManager._addNewArtifactData.type = ArtifactData.ArtifactType.Note;
                }

                dynamicUIManager._addNewArtifactData.Description = childSnapshot.Child("description").Value.ToString();
                dynamicUIManager._addNewArtifactData.URL = childSnapshot.Child("url").Value.ToString();

                foreach (DataSnapshot innerChildSnapshot in childSnapshot.Child("skills").Children.Skip(0))
                {
                    if (innerChildSnapshot.Child("name").Value != null)
                    {
                        //Debug.Log(innerChildSnapshot.Child("name").Value.ToString());
                        dynamicUIManager._addNewArtifactData.Skills.Add(innerChildSnapshot.Child("name").Value.ToString());
                    }
                }
                foreach (DataSnapshot innerChildSnapshot in childSnapshot.Child("experiences").Children.Skip(0))
                {
                    if (innerChildSnapshot.Child("name").Value != null)
                    {
                        //Debug.Log(innerChildSnapshot.Child("name").Value.ToString());
                        dynamicUIManager._addNewArtifactData.Experiences.Add(innerChildSnapshot.Child("name").Value.ToString());
                    }
                }
                dynamicUIManager.SaveArtifact();
                dynamicUIManager._addNewArtifactData = null;
            }
            #endregion Artifact Load Data

            #region Reference Load Data
            //Debug.Log(snapshot.Child("references").Value.ToString());

            foreach (DataSnapshot childSnapshot in snapshot.Child("references").Children.Skip(0))
            {
                /*Debug.Log(childSnapshot.Child("name").Value.ToString());
                Debug.Log(childSnapshot.Child("email").Value.ToString());
                Debug.Log(childSnapshot.Child("position").Value.ToString());

                foreach (DataSnapshot innerChildSnapshot in childSnapshot.Child("skills").Children.Skip(0))
                {
                    if (innerChildSnapshot.Child("name").Value != null)
                    {
                        Debug.Log(innerChildSnapshot.Child("name").Value.ToString());
                    }
                    else
                    {
                        Debug.Log(innerChildSnapshot.Child("name").Value == null);
                    }
                }*/

                dynamicUIManager._addNewReferenceData = new ReferenceData();
                dynamicUIManager._addNewReferenceData.Name = childSnapshot.Child("name").Value.ToString();
                dynamicUIManager._addNewReferenceData.Email = childSnapshot.Child("email").Value.ToString();
                dynamicUIManager._addNewReferenceData.Position = childSnapshot.Child("position").Value.ToString();

                foreach (DataSnapshot innerChildSnapshot in childSnapshot.Child("skills").Children.Skip(0))
                {
                    if (innerChildSnapshot.Child("name").Value != null)
                    {
                        //Debug.Log(innerChildSnapshot.Child("name").Value.ToString());
                        dynamicUIManager._addNewReferenceData.Skills.Add(innerChildSnapshot.Child("name").Value.ToString());
                    }
                }
                dynamicUIManager.SaveReference();
                dynamicUIManager._addNewReferenceData = null;
            }
            #endregion Reference Load Data
        }
    }

    /*private void LoadSkillData()
    {
        var DBTask = DBreference.Child("users").Child(user.UserId).EqualTo("skills")
            .ValueChanged += (object sender2, ValueChangedEventArgs e2) =>
            {
                if (e2.DatabaseError != null)
                {
                    Debug.LogError(e2.DatabaseError.Message);
                }

                if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
                {
                    foreach (var childSnapshot in e2.Snapshot.Children)
                    {
                        var name = childSnapshot.Child("name").Value.ToString();

                        Debug.Log(name.ToString());
                    }
                }
            }
    }*/

    /*void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        var skillsItems = args.Snapshot.Value as Dictionary<string, object>;
        foreach (var item in skillsItems)
        {
            Debug.Log(item.Key); //kdq6...
            var values = item.Value as Dictionary<string, object>;
            foreach (var v in values)
            {
                Debug.Log(v.Key + ":" + v.Value); //category: livingroom, code: 126...
            }
        }
    }*/

    /*public IEnumerator LoadSkills()
    {
        *//*//TODO: RE-ADD IN AFTER MEETING
        var DBTask = DBreference.Child("users").Child(user.UserId).GetValueAsync();
        FirebaseDatabase.DefaultInstance.GetReference("skills").GetValueAsync().ContinueWith(DBTask =>
        {
            if (DBTask.IsFaulted)
            {
                //handle the error...
            }
            else if (DBTask.IsCompleted)
            {
                DataSnapshot snapshot = DBTask.Result;
                var dictionary = snapshot.Value as Dictionary<string, object>;
                if (dictionary != null)
                {
                    //dictionary stuff here
                    Debug.Log(DBTask);
                }
            }
        });*//*

        //DBreference.Child("users").Child(user.UserId).Child("skills").

        var DBTask = DBreference.Child("users").Child(user.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //no data exists so nothing will be loaded
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;



            for (int i = 0; i < accountManager.localUserAccount._skills.Count; i++)
            {

            }

            //CLOSEST I GOT
            accountManager.localUserAccount._skills.Add((SkillData)snapshot.Child("skills").Value);
            Debug.Log(accountManager.localUserAccount._skills.ToString());

            //accountManager.localUserAccount._skills.Add((SkillData)snapshot.Child("skills").Value);

            //accountManager.localUserAccount._skills.Add(snapshot.Child("skills").Value);

            //accountManager.localUserAccount._skills.
            //accountManager.localUserAccount._skills.Add(snapshot.Child("skills").Value;
        }
    }*/
}
