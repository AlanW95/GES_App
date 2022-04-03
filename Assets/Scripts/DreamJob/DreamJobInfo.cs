using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DreamJobInfo : MonoBehaviour
{
    [Header("Dream Job Display References")]
    [SerializeField]
    private TMP_InputField dreamJobInput;
    [SerializeField]
    private GameObject dreamJobInputField;
    [SerializeField]
    private string dreamJobName;
    [SerializeField]
    private GameObject[] lawList;
    [SerializeField]
    private GameObject[] educationList;
    //TODO: Add for more sub-categories - we are going to add an others category too
    /*[SerializeField]
    private Button[] bottomBannerButtons;*/
    [SerializeField]
    private GameObject noResultsFound;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject addDreamJobField;
    [SerializeField]
    private TMP_InputField addDreamJobInputField;
    [Space(5f)]

    //THIS MAY NOT BE NEEDED, PRECAUTION FOR THE TIME BEING
    /*[Header("Dream Job Definition Window")]
    [SerializeField]
    private GameObject definitionWindow;
    [SerializeField]
    private TMP_Text dreamJobName;
    [SerializeField]
    private TMP_Text dreamJobDefinition;
    [Space(5f)]*/

    [Header("Search Functionality")]
    public string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz&";
    [Space(5f)]

    //We want to grab the skill information so we can link it with Dream Job that will be selected by the user
    [Header("Dream Job Skill Information")]
    public SkillsRepository skillData;

    public static List<string> dreamJobSkillsList = new List<string>();

    /*[System.Serializable]
    public struct DreamJobSkillData
    {
        public string[] skill;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < lawList.Length; i++)
        {
            lawList[i].SetActive(true);
        }

        for (int i = 0; i < educationList.Length; i++)
        {
            educationList[i].SetActive(true);
        }

        //definitionWindow.SetActive(false);
        noResultsFound.SetActive(false);
        continueButton.SetActive(false);
        dreamJobInputField.SetActive(true);

        //alphabet for the search function, may need to add in functionality for hyphenation etc
        char[] alphabetArray = alphabet.ToCharArray();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: THERE IS AN ERROR WITH THE SEARCH FUNCTION, IF YOU SEARCH A DREAM JOB WITH MORE THAN ONE WORD, IT DOESN'T APPEAR
        //TODO: MUST FIX THIS!! THIS HAS TO BE FIXED IN THE SKILLS INFO SCRIPT TOO!

        //if the Dream Job Input Field text equals the Dream Job name text on each one it will appear
        if (dreamJobInput.text == "")
        {
            for (int i = 0; i < lawList.Length; i++) { lawList[i].SetActive(true); }
            for (int i = 0; i < educationList.Length; i++) { educationList[i].SetActive(true); }
            //TODO: OTHER DREAM JOB CATEGORIES ARE TO BE ADDED HERE

            continueButton.SetActive(false);
            noResultsFound.SetActive(false);
            addDreamJobField.SetActive(false);
        }

        foreach (char character in alphabet)
        {
            if (dreamJobInput.text.Contains(character))
            {
                for (int i = 0; i < lawList.Length; i++) { lawList[i].SetActive(false); }
                for (int i = 0; i < educationList.Length; i++) { educationList[i].SetActive(false); }
                //TODO: ADD MORE DREAM JOBS HERE AS THEY ARE ADDED
                break;
            } else if (!dreamJobInput.text.Contains(character))
            {
                noResultsFound.SetActive(true);
            }
        }

        foreach (char character in alphabet)
        {
            if (addDreamJobInputField.text.Contains(character))
            {
                continueButton.SetActive(true);
                break;
            }
        }

        //Search function, if the input field contains the name or letter preferably of a word already then it will stay
        for (int i = 0; i < lawList.Length; i++)
        {
            if (dreamJobInput.text.Contains(lawList[i].name)) { lawList[i].SetActive(true); noResultsFound.SetActive(false); }
            if (dreamJobInput.text.Contains(educationList[i].name)) { educationList[i].SetActive(true); noResultsFound.SetActive(false); }
            //TODO: ADD MORE DREAM JOBS HERE AS THEY ARE ADDED
        }
    }

    public void AddNewCustomDreamJob()
    {
        addDreamJobField.SetActive(true);
        for (int i = 0; i < lawList.Length; i++) { lawList[i].SetActive(false); }
        for (int i = 0; i < educationList.Length; i++) { educationList[i].SetActive(false); }
    }

    #region Pass Dream Job Names
    public void PassLawName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Administration/ Corporate Support";
                Debug.Log(dreamJobName);

                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list whenever we have to

                //dreamJobSkillsList.Add = skillData.hardSkillList.skills[0].Skill;
                /*for (int i = 0; i < skillData.Length; i++)
                {
                    skillData[i].hardSkillList.skills[0].Skill = dreamJobName;
                }*/
                break;
            case 1:
                dreamJobName = "Data & Analytics (Legal Analyst)";
                break;
            case 2:
                dreamJobName = "Legal Professional";
                break;
            case 3:
                dreamJobName = "Governance";
                break;
            case 4:
                dreamJobName = "Strategy";
                break;
            case 5:
                dreamJobName = "Planning";
                break;
            case 6:
                dreamJobName = "Research & Development";
                break;
            case 7:
                dreamJobName = "Press Relations";
                break;
            case 8:
                dreamJobName = "Teaching & Education";
                break;
            case 9:
                dreamJobName = "Environment & Sustainability";
                break;
        }
    }

    public void PassEducationName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Audio-Visual Technician";
                break;
            case 1:
                dreamJobName = "Careers Advisor";
                break;
            case 2:
                dreamJobName = "Child Protection Officer";
                break;
            case 3:
                dreamJobName = "Communication Support Worker";
                break;
            case 4:
                dreamJobName = "Community Education Co-ordinator";
                break;
            case 5:
                dreamJobName = "Nursery Worker";
                break;
            case 6:
                dreamJobName = "Ofsted Inspector";
                break;
            case 7:
                dreamJobName = "Online Tutor";
                break;
            case 8:
                dreamJobName = "Outdoors Activities Instructor";
                break;
            case 9:
                dreamJobName = "Play Worker";
                break;
            case 10:
                dreamJobName = "Portage Home Visitor";
                break;
            case 11:
                dreamJobName = "Primary School Worker";
                break;
            case 12:
                dreamJobName = "Prison Instructor";
                break;
            case 13:
                dreamJobName = "QCF Assessor";
                break;
            case 14:
                dreamJobName = "R&D Manager";
                break;
            case 15:
                dreamJobName = "School Business Manager";
                break;
            case 16:
                dreamJobName = "School for Life Teacher";
                break;
            case 17:
                dreamJobName = "SEN Teacher";
                break;
            case 18:
                dreamJobName = "SEN Teaching Assistant";
                break;
            case 19:
                dreamJobName = "Training Manager";
                break;
            case 20:
                dreamJobName = "Training Officer";
                break;
            case 21:
                dreamJobName = "Youth Worker";
                break;
        }
    }
    #endregion Pass Dream Job Names
}
