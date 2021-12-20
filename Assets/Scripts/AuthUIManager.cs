using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AuthUIManager : MonoBehaviour
{
    public static AuthUIManager instance;

    [Header("Splash Screen References")]
    [SerializeField]
    private GameObject splashUI;

    [Header("Account UI References")]
    [SerializeField]
    private GameObject checkingForAccountUI;
    [SerializeField]
    private GameObject loginUI;
    [SerializeField]
    private GameObject registerUI;
    [SerializeField]
    private GameObject verifyEmailUI;
    [SerializeField]
    private TMP_Text verifyEmailText;
    [SerializeField]
    private GameObject forgetPasswordUI;
    [SerializeField]
    private TMP_Text forgetPasswordText;

    [Header("Home References")]
    [SerializeField]
    private GameObject homeUI;
    [SerializeField]
    private GameObject bottomBanner;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ClearUI()
    {
        FirebaseManager.instance.ClearOutputs();
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        verifyEmailUI.SetActive(false);
        checkingForAccountUI.SetActive(false);
    }

    public void LoginScreen()
    {
        //conditional testing to see which hierarchy is active to then change to represented - used for signing out
        if (loginUI.activeInHierarchy == true)
        {
            ClearUI();
            loginUI.SetActive(false);
            homeUI.SetActive(true);
            bottomBanner.SetActive(true);
        }
        if (homeUI.activeInHierarchy == true)
        {
            ClearUI();
            loginUI.SetActive(true);
            homeUI.SetActive(false);
            bottomBanner.SetActive(false);
        }
        //TESTING PURPOSES ONLY - Can be removed at a later date
        if (verifyEmailUI.activeInHierarchy == true) //remove VerifyEmail for authentication verification email
        {
            ClearUI();
            loginUI.SetActive(false);
            homeUI.SetActive(true);
            bottomBanner.SetActive(true);
        }
        if (forgetPasswordUI.activeInHierarchy == true)
        {
            ClearUI();
            loginUI.SetActive(true);
            forgetPasswordUI.SetActive(false);
        }
    }

    public void SignInWithEmail()
    {
        loginUI.SetActive(true);
        splashUI.SetActive(false);
    }

    public void RegisterScreen()
    {
        ClearUI();
        registerUI.SetActive(true);
    }

    public void ForgetPasswordScreen()
    {
        ClearUI();
        forgetPasswordUI.SetActive(true);
        loginUI.SetActive(false);
        forgetPasswordText.text = $"Enter associated email below:";
    }

    public void HomeScreen()
    {
        ClearUI();
        homeUI.SetActive(true);
        bottomBanner.SetActive(true);
    }

    public void AwaitVerification(bool _emailSent, string _email, string _output)
    {
        ClearUI();
        verifyEmailUI.SetActive(true);
        if (_emailSent)
        {
            verifyEmailText.text = $"Sent email!\n Please verify {_email}";
        }
        else
        {
            verifyEmailText.text = $"Email not sent: {_output}\nPlease verify {_email}";
        }
    }
}
