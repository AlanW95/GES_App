using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditAccountInformationUI : MonoBehaviour
{
    public GameObject InformationEditPrefab;
    //private TMP_Text InformationEditTextPrefab;
    public GameObject SaveCancelPrefab;
    private List<GameObject> CreatedInformationEditPrefabs = new List<GameObject>();
    private List<TMP_InputField> InformationInputFields = new List<TMP_InputField>();

    private void Awake()
    {
        InformationEditPrefab.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //ConfigureEditInformationScreen();
    }

    private void ConfigureEditInformationScreen()
    {
        //Clear Existing data
        if(CreatedInformationEditPrefabs.Count != 0)
        {
            for(int i=0; i<CreatedInformationEditPrefabs.Count-1; i++)
            {
                Destroy(CreatedInformationEditPrefabs[i]);
            }
            CreatedInformationEditPrefabs.Clear();
            InformationInputFields.Clear();
        }

        CreateEditInformationContent("first name", TMP_InputField.ContentType.Name);
        CreateEditInformationContent("last name", TMP_InputField.ContentType.Name);
        CreateEditInformationContent("email", TMP_InputField.ContentType.EmailAddress);
        //CreateEditInformationContent("phone", TMP_InputField.ContentType.IntegerNumber);
        CreateEditInformationContent("university", TMP_InputField.ContentType.Name);
        SaveCancelPrefab.transform.SetAsLastSibling();

        UserAccount localUserAccount = FindObjectOfType<AccountManager>().localUserAccount;
        InformationInputFields[0].text = localUserAccount.firstName;
        InformationInputFields[1].text = localUserAccount.lastName;
        InformationInputFields[2].text = localUserAccount.emailAddress;
        InformationInputFields[3].text = localUserAccount.university;
    }

    private void CreateEditInformationContent(string information, TMP_InputField.ContentType _contentType, int _characterLimit = 0)
    {
        InformationEditPrefab.GetComponentInChildren<TMP_Text>().text = information;
        //InformationEditTextPrefab.text = information;
        CreatedInformationEditPrefabs.Add(Instantiate(InformationEditPrefab, InformationEditPrefab.transform.parent));
        InformationInputFields.Add(CreatedInformationEditPrefabs[CreatedInformationEditPrefabs.Count - 1].GetComponentInChildren<TMP_InputField>());
        InformationInputFields[InformationInputFields.Count - 1].contentType = _contentType;
        InformationInputFields[InformationInputFields.Count - 1].characterLimit = _characterLimit;
        CreatedInformationEditPrefabs[CreatedInformationEditPrefabs.Count - 1].SetActive(true);
    }

    public void SaveData()
    {
        UserAccount localUserAccount = FindObjectOfType<AccountManager>().localUserAccount;
        localUserAccount.firstName = InformationInputFields[0].text;
        localUserAccount.lastName = InformationInputFields[1].text;
        localUserAccount.emailAddress = InformationInputFields[2].text;
        localUserAccount.university = InformationInputFields[3].text;

        FindObjectOfType<DatabaseManager>().SaveData();
        //DatabaseManager
    }
}
