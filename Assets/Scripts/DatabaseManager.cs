using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    private int networkRecievedCode = -1;
    public int getNetworkRecievedCode() { return this.networkRecievedCode; }

    public AccountManager accountManager;

    private string configuredUserName;
    private string configuredUserPassword;

    private void Awake()
    {
        accountManager = FindObjectOfType<AccountManager>();
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Login());
       // StartCoroutine(SaveData());
        //StartCoroutine(Register());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public string getErrorCodeText(int errorCode)
    {
        string errorTextValue = "";
        switch(errorCode)
        {
            case 0:
                errorTextValue = "No error identified.";
                break;
            case 1:
                errorTextValue = "Network Error - Please try again later!";
                break;
            case 2:
                errorTextValue = "Unknown Error - Please try again later!";
                break;
            case 3:
                errorTextValue = "Account already exists!";
                break;
            case 4:
                errorTextValue = "Unknown Error - Please try again later!";
                break;
            case 5:
                errorTextValue = "Invalid Login Credentials!";
                break;
            case 6:
                errorTextValue = "Invalid Login Credentials!";
                break;
            case 7:
                errorTextValue = "Invalid Characters Used!";
                break;
            default:
                errorTextValue = "Unknown Error - Please try again later!";
                break;
        }
        return errorTextValue;
    }

    private bool ValidateText(string userID)
    {
        //Todo: Add a check to prevent SQL injection
        return true;
    }

    public IEnumerator Register(string userID, string userPassword, AccountLoginCreationUI accountLoginUI)
    {
        userID = userID.ToLower();
        if (!ValidateText(userID))
        {
            networkRecievedCode = 7;
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("username", userID);
        form.AddField("password", userPassword);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/registration.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            } //else { Debug.Log(www.downloadHandler.text); }

            if (www.downloadHandler.text == "0")
            {
                Debug.Log("User created succesfully!");
            }
            Debug.Log(www.downloadHandler.text.ToString());
            networkRecievedCode = int.Parse(www.downloadHandler.text[0].ToString());
            if (getNetworkRecievedCode() != 0) { accountLoginUI.DisplayError(); }
        }
    }

    public IEnumerator Login(string userID, string userPassword, AccountLoginCreationUI accountLoginUI)
    {
        userID = userID.ToLower();

        if(!ValidateText(userID))
        {
            networkRecievedCode = 7;
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("username", userID);
        form.AddField("password", userPassword);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/accountlogin.php", form);
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        if (www.downloadHandler.text[0] == '0')
        {
            //Debug.Log("User login successful");
        } else { Debug.Log("User Error: " + www.downloadHandler.text); }

        networkRecievedCode = int.Parse(www.downloadHandler.text[0].ToString());

        if (getNetworkRecievedCode() != 0) { accountLoginUI.DisplayError(); }

        string[] dataResults = www.downloadHandler.text.Split('\t');
        accountManager.localUserAccount = JsonUtility.FromJson<UserAccount>(dataResults[1]);

        configuredUserName = userID;
        configuredUserPassword = userPassword;
    }

    public IEnumerator SaveData()//string userID, string userPassword, string userData_json, AccountLoginCreationUI accountLoginUI)
    {
        //userID = userID.ToLower();
       // yield return new WaitForSeconds(1);
        WWWForm form = new WWWForm();
        form.AddField("username", configuredUserName);
        form.AddField("password", configuredUserPassword);
        form.AddField("userdata", JsonUtility.ToJson(accountManager.localUserAccount));

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/savedata.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else { Debug.Log(www.downloadHandler.text); }

            if (www.downloadHandler.text == "0")
            {
                Debug.Log("Data saved!");
            }
            networkRecievedCode = int.Parse(www.downloadHandler.text[0].ToString());
        }
    }
}
