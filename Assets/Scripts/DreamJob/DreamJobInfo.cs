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
    [SerializeField]
    private TMP_Text dreamJobTitle;
    [SerializeField]
    private TMP_Text displayDreamJobContentText;
    [SerializeField]
    private List<Toggle> displayDreamJobContent; //prefab
    [SerializeField]
    private Slider dreamJobProgressSlider;
    [SerializeField]
    private GameObject DreamJobScreen;
    [SerializeField]
    private float maxSliderValue;
    [SerializeField]
    private GameObject selectADreamJob;
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
    public AccountManager accountManager;

    public List<string> dreamJobSkillsList = new List<string>();

    private UserInterfaceManagerUI userInterfaceManager;
    private DynamicInterfaceAreaUI dynamicInterfaceManager;

    [SerializeField] GameObject Toggles;
    [SerializeField] Transform maskPanel;


    /*[System.Serializable]
    public struct DreamJobSkillData
    {
        public string[] skill;
    }*/

    private void Awake()
    {
        userInterfaceManager = FindObjectOfType<UserInterfaceManagerUI>();
        dynamicInterfaceManager = FindObjectOfType<DynamicInterfaceAreaUI>();
    }

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

        //Constantly checking if all the toggles are on, if so... it has met the required to complete the Dream Job
        /*if (DreamJobScreen.activeInHierarchy == true)
        {
            Debug.Log("running if statement");
            
        }*/

        


        /*for (int i = 0; i < dreamJobSkillsList.Count; i++)
        {
            GameObject toggle = (GameObject)Instantiate(Toggles);
            toggle.GetComponentInChildren<TextMeshProUGUI>().text = dreamJobSkillsList[i];

            toggle.transform.SetParent(maskPanel.transform, false);
            toggle.SetActive(true);
            toggle.transform.localScale = new Vector3(1, 1, 1);
            dreamJobProgressSlider.maxValue = dreamJobSkillsList.Count;
            displayDreamJobContent.Add(toggle);
        }*/

        /*for (int i = 0; i < dreamJobSkillsList.Count; i++)
        {
            Debug.Log(Toggles.GetComponentInChildren<Toggle>().isOn);
        }*/

        /*if (Toggles.GetComponentInChildren<Toggle>().isOn == true)
        {
            //make text 100% complete
            Debug.Log("All Dream Jobs skills have been met");
        }

        if (Toggles.GetComponentInChildren<Toggle>().isOn == true)
        {
            dreamJobProgressSlider.value += 1;
        } else if (Toggles.GetComponentInChildren<Toggle>().isOn == false)
        {
            dreamJobProgressSlider.value -= 1;
        }*/
    }

    public void AddNewCustomDreamJob()
    {
        addDreamJobField.SetActive(true);
        for (int i = 0; i < lawList.Length; i++) { lawList[i].SetActive(false); }
        for (int i = 0; i < educationList.Length; i++) { educationList[i].SetActive(false); }
    }

    public void CheckSlider()
    {
        if (dreamJobProgressSlider.value == maxSliderValue)
        {
            Debug.Log("Congrats you have met the required skill set list for your Dream Job!");
        }
    }

    public void Toggle_Changed(bool newValue)
    {
        if (newValue) 
        {
            dreamJobProgressSlider.value += 1;
        }

        if (!newValue)
        {
            dreamJobProgressSlider.value -= 1;
        }
        
        /*for (int i = 0; i < displayDreamJobContent.Count; i++)
        {
            //displayDreamJobContent
            dreamJobProgressSlider.value++;
        }*/
        /*if (displayDreamJobContent[0].isOn == true)
        {
            Debug.Log("HELLO THERE!");
            dreamJobProgressSlider.value = dreamJobProgressSlider.value + 1;
        }*/

        /*for (int i = 0; i < displayDreamJobContent.Count; i++)
        {
            *//*if (displayDreamJobContent[i].onValueChanged.AddListener(delegate { AddValueToDreamJobSlider(); } ))*//*
            //displayDreamJobContent[i].isOn == true;
            if (displayDreamJobContent[i].isOn == true)
            {
                Debug.Log("Toggle activated");
                dreamJobProgressSlider.value++;
            }

            if (displayDreamJobContent[i].isOn == false)
            {
                Debug.Log("Toggle deactivated");
                dreamJobProgressSlider.value--;
            }
        }*/

        /*foreach (Toggle t in displayDreamJobContent)
        {
            for (int i = 0; i < displayDreamJobContent.Count; i++)
            {
                if (t.isOn == true)
                {
                    Debug.Log("Toggle activated");
                    break;
                }

                if (t.isOn == false)
                {
                    Debug.Log("Toggle deactivated");
                    break;
                }
            }
        }*/

        /*for (int i = 0; i < displayDreamJobContent.Count; i++)
        {
            if (displayDreamJobContent[i].GetComponentInChildren<Toggle>().isOn == true)
            {
                Debug.Log("Toggle is on");
                dreamJobProgressSlider.value += 1;
            }

            if (displayDreamJobContent[i].GetComponentInChildren<Toggle>().isOn == false)
            {
                Debug.Log("Toggle is off");
                dreamJobProgressSlider.value -= 1;
            }

            *//*Toggle dreamJob = displayDreamJobContent[i].GetComponentInChildren<Toggle>();

            if (dreamJob.isOn == true)
            {
                Debug.Log("Toggle on");
                dreamJobProgressSlider.value += 1;
            }

            if (dreamJob.isOn == false)
            {
                Debug.Log("Toggle off");
                dreamJobProgressSlider.value -= 1;
            }*//*
        }*/
    }

    private string ListToText(List<string> list)
    {
        string result = "";
        foreach (var listMember in list)
        {
            //Transform _prefab = CreatePrefab
            result += listMember.ToString() + "\n";
            
        }
        return result;
    }

    public void DisplayDreamJob()
    {
        for (int i = 0; i < dreamJobSkillsList.Count; i++)
        {
            GameObject toggle = (GameObject)Instantiate(Toggles);
            toggle.GetComponentInChildren<TextMeshProUGUI>().text = dreamJobSkillsList[i];

            toggle.transform.SetParent(maskPanel.transform, false);
            toggle.SetActive(true);
            toggle.transform.localScale = new Vector3(1, 1, 1);
            dreamJobProgressSlider.maxValue = dreamJobSkillsList.Count;
            maxSliderValue = dreamJobProgressSlider.maxValue;
            dreamJobProgressSlider.value = 0;
            displayDreamJobContent.Add(toggle.GetComponentInChildren<Toggle>());
            selectADreamJob.SetActive(false);

            //if any of the skills from the Dream Job fit then it will recognise this
            for (int x = 0; x < accountManager.localUserAccount._skills.Count; x++)
            {
                if (accountManager.localUserAccount._skills[x].Name.Contains(dreamJobSkillsList[i]))
                {
                    displayDreamJobContent[i].isOn = true;
                }
            }
        }

        userInterfaceManager.Open_MyDreamJob();
    }

    #region Pass Dream Job Names
    public void PassLawName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Administration/ Corporate Support";
                dreamJobTitle.text = dreamJobName;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills 
                //IT Skills (Digital Literacy), Research Skill
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Law, Legal documentation and terminologies");
                dreamJobSkillsList.Add("Transcribing and proof-reading Legal documents");
                dreamJobSkillsList.Add("Knowledge of basic administrative skills");
                dreamJobSkillsList.Add("Basic knowledge of Billing and Accounting");
                dreamJobSkillsList.Add("Legal Drafting Skills");

                //Organisational Skills
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[12].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[22].Skill.ToString());

                //General Workplace Skills
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 1:
                dreamJobName = "Data & Analytics (Legal Analyst)";
                dreamJobTitle.text = dreamJobName;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, IT Skills (Digital Literacy)
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Technology & analytics in Law practice");
                dreamJobSkillsList.Add("Machine Learning & Programming");
                dreamJobSkillsList.Add("Data Visualization");
                dreamJobSkillsList.Add("Data Cleaning");
                dreamJobSkillsList.Add("Data Analysis Tools");
                dreamJobSkillsList.Add("Data Management");
                dreamJobSkillsList.Add("Knowledge of Law, Legal documentation & Terminologies");
                dreamJobSkillsList.Add("Legal Research Skills");
                dreamJobSkillsList.Add("Thorough upto date knowledge of data protection laws, databases and tracking systems");
                dreamJobSkillsList.Add("Mathematics");

                //Organisational Skills B
                // Attention to Detail/ Due Dilegence, Discipline, Multitasking, Planning, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communication Skills, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Confidentiality, Critical Thinking, Self-Motivation, Stress Management
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[12].Skill.ToString());

                //General Workplace Skills E
                //Business & Commercial Awareness, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 2:
                dreamJobName = "Legal Professional";
                dreamJobTitle.text = dreamJobName;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, IT Skills (Digital Literacy)
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Law & Legal Matters");
                dreamJobSkillsList.Add("Legal Research Skills");
                dreamJobSkillsList.Add("Financial Expertise");
                dreamJobSkillsList.Add("Knowledge of Equality & Diversity Issues");
                dreamJobSkillsList.Add("Legal Drafting Skills");

                //Organisational Skills B
                // Attention to Detail, Discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communication Skills, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Confidentiality, Critical Thinking, Self-Motivation, Stress Management
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[12].Skill.ToString());

                //General Workplace Skills E
                //Business & Commercial Awareness, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 3:
                dreamJobName = "Governance";
                dreamJobTitle.text = dreamJobName;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, IT Skills (Digital Literacy)
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Law & Legal Matters");
                dreamJobSkillsList.Add("Financial Expertise");
                dreamJobSkillsList.Add("Knowledge of Policy & Governance Legislations");

                //Organisational Skills B
                //Project Management, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening to Others, Communication Skills, Conflict Resolution, Leadership Skills, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[12].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Creativity, Critical Thinking, Diplomacy, Patience, Self-Awareness, Stress Management, Willingness to Learn/ Growth Orientation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[17].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[18].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Commercial Awareness, Cultural Awareness, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 4:
                dreamJobName = "Strategy";
                dreamJobTitle.text = dreamJobName;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Mathematical and Statistical Knowledge, Digital Literacy Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Budget Allocation/ Cost Management");
                dreamJobSkillsList.Add("Strategy Planning");
                dreamJobSkillsList.Add("Logical and Inductive Reasoning");

                //Organisational Skills B
                //Attention to Detail - Due Diligence, Discipline, Multitaskings, Planning, Prioritising, Project Mangement, Punctionality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communication Skills, Leadership
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Critical Thinking, Logical Reasoning, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[16].Skill.ToString());

                //General Workplace Skills E
                //Commercial Awareness, Cultural Awareness, Negotiation Skills, Persuasion Skills, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[11].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 5:
                dreamJobName = "Planning";
                dreamJobTitle.text = dreamJobName;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Research Skills, Digital Literacy Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Relevant Laws and Legal Issues");

                //Organisational Skills B
                //Attention to Detail - Due Diligence, Discipline, Multitasking, Prioritizing
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communication, People Management/ Customer Service, Self-Confidence, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[10].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Critical Thinking, Initiative, Patience, Persistance, Work Under Pressure
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[15].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[22].Skill.ToString());

                //General Workplace Skills E
                //Commercial Awareness, Creative Problem-Solving, Networking, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
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
