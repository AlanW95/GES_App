using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccountLoginCreationUI : MonoBehaviour
{
    public GameObject RegistrationScreen;
    public GameObject LoginScreen;
    public GameObject ResetPasswordScreen;

    public TMP_InputField EmailField;
    public TMP_InputField PasswordField;
    public TMP_Text PasswordTextField;

    public Button PasswordShowHideButton;

    public TMP_Text ErrorLoginText;

    private AccountManager accountManager;
    private DatabaseManager DBManager;

    private void Awake()
    {
        accountManager = FindObjectOfType<AccountManager>();
        DBManager = FindObjectOfType<DatabaseManager>();
    }

    void SwitchPreHomeScreen()
    {
        PasswordField.text = "";
    }

    public void AttemptLogin()
    {
        StartCoroutine(DBManager.Login(EmailField.text, PasswordField.text, this));
        ErrorLoginText.enabled = false;
    }

    public void AttemptRegistration()
    {
        StartCoroutine(DBManager.Register(EmailField.text, PasswordField.text, this));
        ErrorLoginText.enabled = false;

    }

    public void DisplayError()
    {
        ErrorLoginText.enabled = true;
        ErrorLoginText.text = DatabaseManager.getErrorCodeText(DBManager.getNetworkRecievedCode());
    }

    public void ShowPassword(bool show)
    {
        PasswordField.placeholder.GetComponent<TMP_Text>().enabled = true;
        PasswordTextField.enabled = true;
        if (show)
        {
            PasswordField.placeholder.GetComponent<TMP_Text>().text = PasswordField.text;
            PasswordTextField.enabled = false;
        }
        else
            PasswordField.placeholder.GetComponent<TMP_Text>().enabled = false;
    }

    public void SaveDataTest()
    {
        StartCoroutine(DBManager.SaveData());// EmailField.text, PasswordField.text, JsonUtility.ToJson(accountManager.localUserAccount), this));
        ErrorLoginText.enabled = false;
    }

}
