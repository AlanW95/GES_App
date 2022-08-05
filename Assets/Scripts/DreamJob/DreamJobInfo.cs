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
    [SerializeField]
    private GameObject[] literatureList;
    [SerializeField]
    private GameObject[] linguisticList;
    [SerializeField]
    private GameObject[] scienceEngineeringList;
    [SerializeField]
    private GameObject[] humanitiesList;
    [SerializeField]
    private GameObject[] mediaSciencesList;
    [SerializeField]
    private GameObject[] psychologyList;
    [SerializeField]
    private GameObject[] businessStudiesList;
    [SerializeField]
    private GameObject[] geographyEarthList;
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
    [SerializeField]
    private GameObject dreamJobCompletionWindow;
    [SerializeField]
    public string selectedDreamJob;
    [SerializeField]
    public int selectedDreamJobCategory;
    [SerializeField]
    public int selectedDreamJobIndex;
    [SerializeField]
    public bool screen;
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
    public FirebaseManager firebaseManager;

    public List<string> dreamJobSkillsList = new List<string>();

    [SerializeField] private UserInterfaceManagerUI userInterfaceManager;
    [SerializeField] private DynamicInterfaceAreaUI dynamicInterfaceManager;

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

        for (int i = 0; i < literatureList.Length; i++)
        {
            literatureList[i].SetActive(true);
        }

        for (int i = 0; i < linguisticList.Length; i++)
        {
            linguisticList[i].SetActive(true);
        }

        for (int i = 0; i < scienceEngineeringList.Length; i++)
        {
            scienceEngineeringList[i].SetActive(true);
        }

        for (int i = 0; i < humanitiesList.Length; i++)
        {
            humanitiesList[i].SetActive(true);
        }

        for (int i = 0; i < mediaSciencesList.Length; i++)
        {
            mediaSciencesList[i].SetActive(true);
        }

        for (int i = 0; i < psychologyList.Length; i++)
        {
            psychologyList[i].SetActive(true);
        }

        for (int i = 0; i < businessStudiesList.Length; i++)
        {
            businessStudiesList[i].SetActive(true);
        }

        for (int i = 0; i < geographyEarthList.Length; i++)
        {
            geographyEarthList[i].SetActive(true);
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
            for (int i = 0; i < literatureList.Length; i++) { literatureList[i].SetActive(true); }
            for (int i = 0; i < linguisticList.Length; i++) { linguisticList[i].SetActive(true); }
            for (int i = 0; i < scienceEngineeringList.Length; i++) { scienceEngineeringList[i].SetActive(true); }
            for (int i = 0; i < humanitiesList.Length; i++) { humanitiesList[i].SetActive(true); }
            for (int i = 0; i < mediaSciencesList.Length; i++) { mediaSciencesList[i].SetActive(true); }
            for (int i = 0; i < psychologyList.Length; i++) { psychologyList[i].SetActive(true); }
            for (int i = 0; i < businessStudiesList.Length; i++) { businessStudiesList[i].SetActive(true); }
            for (int i = 0; i < geographyEarthList.Length; i++) { geographyEarthList[i].SetActive(true); }
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
                for (int i = 0; i < literatureList.Length; i++) { literatureList[i].SetActive(false); }
                for (int i = 0; i < linguisticList.Length; i++) { linguisticList[i].SetActive(false); }
                for (int i = 0; i < scienceEngineeringList.Length; i++) { scienceEngineeringList[i].SetActive(false); }
                for (int i = 0; i < humanitiesList.Length; i++) { humanitiesList[i].SetActive(false); }
                for (int i = 0; i < mediaSciencesList.Length; i++) { mediaSciencesList[i].SetActive(false); }
                for (int i = 0; i < psychologyList.Length; i++) { psychologyList[i].SetActive(false); }
                for (int i = 0; i < businessStudiesList.Length; i++) { businessStudiesList[i].SetActive(false); }
                for (int i = 0; i < geographyEarthList.Length; i++) { geographyEarthList[i].SetActive(false); }
                //TODO: ADD MORE DREAM JOBS HERE AS THEY ARE ADDED
                break;
            } else if (!dreamJobInput.text.Contains(character))
            {
                //noResultsFound.SetActive(true);
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
            if (dreamJobInput.text.Contains(lawList[i].name)) { lawList[i].SetActive(true); noResultsFound.SetActive(false); }if (dreamJobInput.text.Contains(educationList[i].name)) { educationList[i].SetActive(true); noResultsFound.SetActive(false); }
        }

        for (int i = 0; i < educationList.Length; i++)
        {
            if (dreamJobInput.text.Contains(educationList[i].name)) { educationList[i].SetActive(true); noResultsFound.SetActive(false); }
        }

        for (int i = 0; i < literatureList.Length; i++)
        {
            if (dreamJobInput.text.Contains(literatureList[i].name)) { literatureList[i].SetActive(true); noResultsFound.SetActive(false); }
        }

        for (int i = 0; i < linguisticList.Length; i++)
        {
            if (dreamJobInput.text.Contains(linguisticList[i].name)) { linguisticList[i].SetActive(true); noResultsFound.SetActive(false); }
        }

        for (int i = 0; i < scienceEngineeringList.Length; i++)
        {
            if (dreamJobInput.text.Contains(scienceEngineeringList[i].name)) { scienceEngineeringList[i].SetActive(true); noResultsFound.SetActive(false); }
        }

        for (int i = 0; i < humanitiesList.Length; i++)
        {
            if (dreamJobInput.text.Contains(humanitiesList[i].name)) { humanitiesList[i].SetActive(true); noResultsFound.SetActive(false); }
        }

        for (int i = 0; i < mediaSciencesList.Length; i++)
        {
            if (dreamJobInput.text.Contains(mediaSciencesList[i].name)) { mediaSciencesList[i].SetActive(true); noResultsFound.SetActive(false); }
        }

        for (int i = 0; i < psychologyList.Length; i++)
        {
            if (dreamJobInput.text.Contains(psychologyList[i].name)) { psychologyList[i].SetActive(true); noResultsFound.SetActive(false); }
        }

        for (int i = 0; i < businessStudiesList.Length; i++)
        {
            if (dreamJobInput.text.Contains(businessStudiesList[i].name)) { businessStudiesList[i].SetActive(true); noResultsFound.SetActive(false); }
        }

        for (int i = 0; i < geographyEarthList.Length; i++)
        {
            if (dreamJobInput.text.Contains(geographyEarthList[i].name)) { geographyEarthList[i].SetActive(true); noResultsFound.SetActive(false); }
        }

        //TODO: ADD MORE DREAM JOBS HERE AS THEY ARE ADDED

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
        for (int i = 0; i < literatureList.Length; i++) { literatureList[i].SetActive(false); }
        for (int i = 0; i < linguisticList.Length; i++) { linguisticList[i].SetActive(false); }
        for (int i = 0; i < scienceEngineeringList.Length; i++) { scienceEngineeringList[i].SetActive(false); }
        for (int i = 0; i < humanitiesList.Length; i++) { humanitiesList[i].SetActive(false); }
        for (int i = 0; i < mediaSciencesList.Length; i++) { mediaSciencesList[i].SetActive(false); }
        for (int i = 0; i < psychologyList.Length; i++) { psychologyList[i].SetActive(false); }
        for (int i = 0; i < businessStudiesList.Length; i++) { businessStudiesList[i].SetActive(false); }
        for (int i = 0; i < geographyEarthList.Length; i++) { geographyEarthList[i].SetActive(false); }
    }

    public void CheckSlider()
    {
        if (dreamJobProgressSlider.value == maxSliderValue)
        {
            //Debug.Log("Congrats you have met the required skill set list for your Dream Job!");

            //You have met all your skills required for your dream job! No data has been recorded in this build of the app. You are free to close the app.
            dreamJobCompletionWindow.SetActive(true);
        }
    }

    public void Close_DreamJobCompletionWindow()
    {
        dreamJobCompletionWindow.SetActive(false);
    }

    public void Toggle_Changed(bool newValue)
    {
        if (newValue) 
        {
            dreamJobProgressSlider.value += 2;
        }

        if (!newValue)
        {
            dreamJobProgressSlider.value -= 2;
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
            displayDreamJobContent.Add(toggle.GetComponentInChildren<Toggle>());
            selectADreamJob.SetActive(false);
            //dreamJobProgressSlider.value = 0;

            dreamJobProgressSlider.maxValue = dreamJobSkillsList.Count * 2;
            maxSliderValue = dreamJobProgressSlider.maxValue;

            //if any of the skills from the Dream Job fit then it will recognise this
            for (int x = 0; x < accountManager.localUserAccount._skills.Count; x++)
            {
                if (accountManager.localUserAccount._skills[x].Name.Contains(dreamJobSkillsList[i]))
                {
                    displayDreamJobContent[i].isOn = true;
                    //dreamJobProgressSlider.value += 1;

                    /*dreamJobProgressSlider.maxValue = dreamJobSkillsList.Count + dreamJobProgressSlider.value;
                    maxSliderValue = dreamJobProgressSlider.maxValue;*/
                }
            }
        }
        Debug.Log("The bool is: " + screen);
        if (screen) { userInterfaceManager.Open_MyDreamJob(); }
        SaveDreamJobWithFirebase();
    }

    public void SaveDreamJobWithFirebase()
    {
        //TODO: CHANGE THIS TO SUIT THE DREAM JOB SELECTION MORE EFFICIENTLY
        firebaseManager.CallSendDreamJob(selectedDreamJob, selectedDreamJobCategory, selectedDreamJobIndex);
    }

    public IEnumerator DreamJobDelay()
    {
        userInterfaceManager.Open_Home();
        yield return new WaitForSeconds(1f);
    }

    #region Pass Dream Job Names

    #region PassLawNames
    public void PassLawName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Administration & Corporate Support";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 0;
                selectedDreamJobIndex = index;
                screen = true;
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

                //Personality
                //Resilience, Orderliness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());

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
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 0;
                selectedDreamJobIndex = index;
                screen = true;
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

                //Personality
                //Resilience, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

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
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 0;
                selectedDreamJobIndex = index;
                screen = true;
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

                //Personality
                //Industriousness, Resilience, Intellect
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

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
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 0;
                selectedDreamJobIndex = index;
                screen = true;
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

                //Personality
                //Empathy (COMPASSION), Enthusiasm, Resilience
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

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
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 0;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Mathematical and Statistical Knowledge, Digital Literacy Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Budget Allocation & Cost Management");
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

                //Personality
                //Resilience, Orderliness, Assertiveness, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

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
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 0;
                selectedDreamJobIndex = index;
                screen = true;
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

                //Personality
                //Empathy (Compassion), Resilience, Orderliness, Industriousness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());

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
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 0;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy Skills, Research Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of relevant Laws and Legal Issues");
                dreamJobSkillsList.Add("Knowledge of Policy and Governance Legislations");
                dreamJobSkillsList.Add("Upto-date Knowledge of Relevant Legal Technologies");

                //Organisational Skills B
                //Attention to Detail/ Due Diligence, Discipline, Prioritizing, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communication, Leadership, Teamwork and Collaboration
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Creativity, Critical Thinking, Growth-Orientation, Proactive/ Initiative, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Business and Commercial Awareness, Creative Problem Solving, Negotiations Skills, Networking, Persuasion Skills
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[11].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Orderliness, Industriousness, Enthusiasm
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 7:
                dreamJobName = "Press Relations";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 0;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy Skills, Research Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Marketing and Branding Skills");
                dreamJobSkillsList.Add("Knowledge and Awareness of Different Media Agendas");
                dreamJobSkillsList.Add("Knowledge of Law and PR Basics");

                //Organisational Skills B
                //Attention to Detail, Multitasking, Prioritizing, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communication, Leadership, Presentation Skills, Providing Feedback, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Creativity and Imagination, Proactive/ Initiative, Stress Management, Work Under Pressure
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[15].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Collaboration and Relationship Management, Commercial Awareness, Creative Problem Solving, Cultural Awareness, Decision Making, Persuasion Skills, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[11].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Openness
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 8:
                dreamJobName = "Teaching & Education";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 0;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy Skills, Applied Knowledge, Research Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to Create and Implement Best Pedagogical Strategies");
                dreamJobSkillsList.Add("Upto Date Knowledge of Pedagogical Tools and Packages");
                dreamJobSkillsList.Add("Knowledge of the Relevant Subjects");

                //Organisational Skills B
                //Discipline, Planning, Prioritizing, Attention to Detail, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening Skills, Approachableness/ Socialable, Communicativeness, Confidence, Leadership, Speaking Fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Sense of Humour, Creativity, Critical Thinking, Flexibility, Patience, Responsibility/ Commitment
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Positive Attitude to Work, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy (Compassion), Enthusiasm, Openness, Intellect
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 9:
                dreamJobName = "Environment & Sustainability";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 0;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy Skills, Research Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Relevant Laws and Legal Issues");
                dreamJobSkillsList.Add("In-depth Knowledge of Relevant Policy, Regulations, and Legislations");
                dreamJobSkillsList.Add("Legal Drafting Skills");

                //Organisational Skills B
                //Attention to Detail, Multitasking
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communication Skills, Writing Skills, Speaking Fluency, Team Player
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Critical Thinking, Flexibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Proven Persuation Skills, Commercial Awareness, Effective Collaboration, Proven Negotiation Skills
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[11].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Compassion, Enthusiasm, Resilience, Industriousness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassLawNames

    #region PassEducationNames
    public void PassEducationName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Audio-Visual Technician";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Digital Literacy Skills, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //custom skills not in the repository
                /*dreamJobSkillsList.Add("Knowledge of Relevant Laws and Legal Issues");
                dreamJobSkillsList.Add("In-depth Knowledge of Relevant Policy, Regulations, and Legislations");
                dreamJobSkillsList.Add("Legal Drafting Skills");*/

                //Organisational Skills B
                //Planning, Discipline, Multitasking, Punctuality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Customer Service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Flexibility, Responsibility, Self-Motivation, Patience, Initiative
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Postive Attitude to Work, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Resilience
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 1:
                dreamJobName = "Careers Advisor";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Digital Literacy Skills, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Counselling Skills");
                dreamJobSkillsList.Add("Knowledge of Psychology");
                dreamJobSkillsList.Add("Teaching Knowledge");

                //Organisational Skills B
                //Discipline, Planning, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communicativeness, Customer Service, Discussion Skills, Providing Feedback, Speaking Fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Responsibility, Self-Motivation, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Career Management, Cultural Awareness, Independence at Work, Positive Attitude to Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 2:
                dreamJobName = "Child Protection Officer";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Thinking, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Counselling Skills");
                dreamJobSkillsList.Add("Knowledge of Psychology");

                //Organisational Skills B
                //Discipline, Planning, Prioritising, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communicativeness, Customer Service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Patience, Flexibility, Work Under Pressure, Responsibility, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[15].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Decision Making, Negotiating Skills, Positive Attitude to Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy, Resilience
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 3:
                dreamJobName = "Communication Support Worker";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Thinking, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching Practices");
                dreamJobSkillsList.Add("Knowledge of Psychology");
                dreamJobSkillsList.Add("Sign Language & Deaf Studies");

                //Organisational Skills B
                //Discipline, Planning, Prioritizing, Punctuality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communicativeness, Customer Service, Providing Feedback, Socialable
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Patience, Flexible, Self-Motivation, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Positive Attitude to Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy, Resilience
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 4:
                dreamJobName = "Community Education Co-ordinator";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Thinking, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                /*dreamJobSkillsList.Add("Knowledge of Teaching Practices");
                dreamJobSkillsList.Add("Knowledge of Psychology");
                dreamJobSkillsList.Add("Sign Language & Deaf Studies");*/

                //Organisational Skills B
                //Discipline, Multitasking, Planning, Prioritizing, Project Management, Punctionality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Customer Service, Socialable (approachableness)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Initiative, Critical Thinking, Patience, Flexibility, Responsibility, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Networking, Positive Attitude to Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 5:
                dreamJobName = "Nursery Worker";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                /*dreamJobSkillsList.Add("Knowledge of Teaching Practices");
                dreamJobSkillsList.Add("Knowledge of Psychology");
                dreamJobSkillsList.Add("Sign Language & Deaf Studies");*/

                //Organisational Skills B
                //Discipline, Multitasking, Punctionality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening skills, Communicativeness, Customer Service, Socialable (approachableness)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Patience, Flexible, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Positive Attitude to Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 6:
                dreamJobName = "Ofsted Inspector";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Observation & Interviewing Skills");

                //Organisational Skills B
                //Discipline, Planning, Punctuality, Time Management, Meticulousness (B10)
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening Skills, Argumentation/ Discussion Skills, Communicativeness, Leadership, Providing Feedback
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Critical Thinking, Responsiblity, Initiative, Growth Orientation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Business and Commercial Awareness, Decision Making, Independence at Work, Networking, Positive Attitude at Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //No personality skills

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 7:
                dreamJobName = "Online Tutor";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching & Curriculum Design");
                dreamJobSkillsList.Add("Creating Best Conditions for Learning & Teaching");
                dreamJobSkillsList.Add("Knowledge of Digital Media");

                //Organisational Skills B
                //Discipline, Planning, Priotising, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Customer Service, Presentation Skills, Speaking Fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Flexibility, Initiative, Creative/ Innovative Thinking, Responsiblity, Self Presentation, Growth Orientation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Problem Solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //no personality skills

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 8:
                dreamJobName = "Outdoors Activities Instructor";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching & Instruction");

                //Organisational Skills B
                //Discipline, Attention to Detail, Multitasking, Planning, Punctionality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Leadership Skills, Speaking Fluency, Teamwork, Socialable (Approachableness)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Patience, Initiative, Responsiblity, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Independence at Work, Positive Attitude At Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 9:
                dreamJobName = "Play Worker";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("First Aid");
                dreamJobSkillsList.Add("Counselling Skills");

                //Organisational Skills B
                //Discipline, Multitasking, Planning
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active listening skills, Communicativeness, Customer Service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Patience, Flexiblity, Creative Thinking, Critical Thinking, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Positive Attitude at Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy, Resilience
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 10:
                dreamJobName = "Portage Home Visitor";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Knowledge of Psychology");
                dreamJobSkillsList.Add("Ability to Create and Implement Best Pedagogical Strategies");
                dreamJobSkillsList.Add("Knowledge of Early Childhood Studies");

                //Organisational Skills B
                //Discipline, Punctionality, Time MAnagement, Prioritizing
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening to Others, Communicativeness, Customer Service, Providing Feedback
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Patience, Flexibility, Creative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Positive Attitude to Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Resilience
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 11:
                dreamJobName = "Primary School Worker";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to Create and Implement Best Pedagogical Strategies");

                //Organisational Skills B
                //Discipline, Multitasking, Planning, Prioritizing, Punctionality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communicativeness, Discussion Skills, Providing Feedback, Speaking Fluency, Socialable (Approachableness)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Patience, Flexiblity, Creative Thinking, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Decision Making, Independence at Work, Positive Attitude to Work, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 12:
                dreamJobName = "Prison Instructor";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy Skills, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to Create and Implement Best Pedagogical Strategies");

                //Organisational Skills B
                //Discipline, Meticulous (Attention to Detail), Multitasking, Planning, Prioritizing, Punctuality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communicativeness, Customer Service, Discussion Skills, Providing Feedback, Speaking Fluency, Socialable (Approachableness)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Patience, Flexiblity, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Independence at Work, Adaptability, Cultural Awareness, Positive Attitude at Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 13:
                dreamJobName = "QCF Assessor";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy Skills, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to Create and Implement Best Pedagogical Strategies");
                dreamJobSkillsList.Add("Monitoring and Evaluation");

                //Organisational Skills B
                //Discipline, Meticulous (Attention to Detail), Prioritizing, Project Management, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communicativeness, Customer Service, Discussion Skills, Leadership, Providing Feedback, Writing Skils, Socialable (Approachableness)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Flexiblity, Critical Thinking, Initiative, Responsiblity
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Business & Commercial Awareness, Decision Making, Independence at Work, Negotiating Skills, Networking, Positive Attitude, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 14:
                dreamJobName = "R&D Manager";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy, Applied Knowledge, Research Skills, Statistical Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //custom skills not in the repository
                /*dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to Create and Implement Best Pedagogical Strategies");
                dreamJobSkillsList.Add("Monitoring and Evaluation");*/

                //Organisational Skills B
                //Discipline, Meticulous (Attention to Detail), Multitasking, Planning, Prioritizing, Project Management, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communicativeness, Customer Service, Discussion Skills, Leadership, Providing Feedback, Speaking Fluency, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Critical Thinking, Creative/ Innovative Thinking, Initiative
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Business and Commercial Awareness, Cultural Awareness, Decision Making, Independence at Work, Negotiating Skills, Netowkring, Positive Attitude to Work, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Resilience, Intellect, Industriousness, Orderliness, Enthusiasm
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 15:
                dreamJobName = "School Business Manager";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Skills, Digital Literacy, Applied Knowledge, Statistical Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of HR and Employment Laws");
                

                //Organisational Skills B
                //Project Management, Planning, Time Management, Multitasking, Prioritizing, Discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Leadership, Customer Service, Communicativeness, Discussion Skills, Teamwork, Active Listening, Speaking Fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Flexibility, Critical Thinking, Creative Thinking, Growth Orientation, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Business & Cultural Awareness, Adaptability, Networking, Negotiating Skills, Cultural Awareness, Positive Attitude at Work, Work Ethic, Independence at Work, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Resilience
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 16:
                dreamJobName = "School for Life Teacher";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to Create and Implement Best Pedagogical Strategies");


                //Organisational Skills B
                //Meticulous (Attention to Detail), Discipline, Punctionality, Planning, Multitasking, Time Management, Prioritizing
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Leadership, Customer Service, Communicativeness, Presentation Skills, Active Listening, Providing Feedback
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Creative Thinking, Initiative, Patience, Responsibility, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Independence at Work, Decision Making, Work Ethic, Positive Attitude at Work, Adaptability, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //no personality skills

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 17:
                dreamJobName = "SEN Teacher";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Thinkinkg, Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to Create and Implement Best Pedagogical Strategies");


                //Organisational Skills B
                //Discipline, Prioritizing, Planning, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Speaking Fluency, Active Listening Skills, Socialable (Approachableness)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Patience, Flexibility, Responsibility, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Positive Attitude to Work, Cultural Awareness, Work Ethic, Decision Making, Problem Solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 18:
                dreamJobName = "SEN Teaching Assistant";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Thinkinkg, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to Create and Implement Best Pedagogical Strategies");


                //Organisational Skills B
                //Discipline, Prioritizing, Punctuality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communicativeness, Presentation Skills, Speaking Fluency, Socialable (Approachableness)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Patience, Flexibility, Responsibility, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Positive Attitude at Work, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 19:
                dreamJobName = "Training Manager";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Analytical Thinkinkg, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to Create and Implement Best Pedagogical Strategies");


                //Organisational Skills B
                //Discipline, Planning, Prioritizing
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communicativeness, Customer Service, Discussion Skills, Leadership, Speaking Fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Initiative, Self-Motivation, Flexibility, Critical Thinking, Growth Orientation, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Business and Cultural Awareness, Career Management, Positive Attitude to Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //no personality skills

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 20:
                dreamJobName = "Training Officer";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Teaching Knowledge");
                dreamJobSkillsList.Add("Curriculum Design");


                //Organisational Skills B
                //Planning, Prioritizing, Discipline, Punctionality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Discussion Skills, Communicativeness, Teamwork, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Critical Thinking, Creative Thinking, Responsibility, Self Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Positive Attitude to Work, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
            case 21:
                dreamJobName = "Youth Worker";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 1;
                selectedDreamJobIndex = index;
                screen = true;
                //Debug.Log(dreamJobName);

                //dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Psychology");


                //Organisational Skills B
                //Time Mangagement, Punctuality, Discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Customer Service, Communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D 
                //Responsibility, Self Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Positive Attitude at Work, Cultural Awareness, WOrk Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills
                //no foreign language skills

                //Personality
                //Empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassEducationNames

    #region PassLiteratureNames
    public void PassLiteratureName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Editorial Roles";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //Industry/ Hard Skills  A
                //Analytical, Proficient IT, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Proof-Reading Skills");
                dreamJobSkillsList.Add("Knowledge of Basic Administrative tasks");
                dreamJobSkillsList.Add("Up to date knowledge of Digital Packages");

                //Organisational Skills B
                //Attention to Detail/ Due diligence, Discipline, Multi-tasking, Prioritizing, Punctuality, Time Management, Planning
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Excellent Written Communication Skills, Providing feedback
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Ability to work under pressure, Patience, Self-motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[15].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Decision Making, Independece at work, Negotiation skills, work ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Orderliness, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Teaching & Education";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Applied Knowledge, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to create and implement best Pedagogical strategies");
                dreamJobSkillsList.Add("Knowledge of the relevant subjects");
                dreamJobSkillsList.Add("Upto date knowledge of pedagogical tools and packages");

                //Organisational Skills B
                //Discipline, Planning, Prioritisng, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Approachableness/ Socialable, Communicativeness, Confidence, Leadership, Speaking Fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Sense of Humour (D13), Creativity, Critical Thinking, Flexibility, Patience, Responsibility/ Commitment, Self-Awareness
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[18].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Decision Making, Positive Attitude to Work, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Empathy (Compassion), Enthusiasm, Openness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Training";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //Industry/ Hard Skills  A
                //Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to create and implement best Pedagogical strategies");

                //Organisational Skills B
                //Discipline, Planning, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening to Others, Communicativeness, Customer Service, Discussion Skills, Leadership, Speaking Fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Creativity, Critical Thinking, Flexibility, Growth Orientation, Initiative, Responsibility, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Business and Commercial Awareness, Career Management, Positive Attitude to Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Empathy (Compassion), Enthusiasm, Openness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 3:
                dreamJobName = "Media Correspondant";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //Industry/ Hard Skills  A
                //Digital Literacy, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Investigative Reporting");
                dreamJobSkillsList.Add("Technical Skills");

                //Organisational Skills B
                //Attention to Detail, Discipline, Planning, Priotising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Speaking Fluency, Team Work
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Authentic Self-Representation (D4), Authenticity, Critical Thinking, Growth Orientation,
                //Initiative, Flexibility, Persistance, Responsibility, Self-Motivation, Self-Reflection
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[19].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[21].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[22].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Decision Making, Positive Attitude to work,
                //Problem-solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Industriousness, Enthusiasm, Openness, Stability
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 4:
                dreamJobName = "Creative & Content Writer";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Editing Skills");
                dreamJobSkillsList.Add("Knowledge of Language and Grammar");
                dreamJobSkillsList.Add("Knowledge of basics");

                //Organisational Skills B
                //Discipline, Planning, Priortising, Punctionality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Presentation Skills, Writing Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Ability to face Criticism, Authenticity, Creativity & Imagination, Growth-Orientation,
                //Initiative/ Proactivity, Mindfulness/ Self-Awareness, Persistance
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[13].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[18].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[21].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[22].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Stability, Industriousness, Enthusiasm, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 5:
                dreamJobName = "Performing Arts";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //Industry/ Hard Skills  A
                //Analytical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of the Basics");

                //Organisational Skills B
                //Attention to Detail, Discipline, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Confidence, Presentation Skills, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[10].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Ability to face Criticism, Authenticity, Critical Thinking, Flexbility, Growth Orientation,
                //Persistance, Self-Motivation, Self-Reflection, Self-Awareness, Self-Promotion (Presentation),
                //Stamina, Stress Management
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[13].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[18].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[19].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[20].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[21].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[22].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Networking & Collab, Positive Attitude to Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Industriousness, Enthusiasm, Openness, Compassion
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassLiteratureNames

    #region PassLinguisticNames
    public void PassLinguisticName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Administration & Corporate Support";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Administration Skills");
                dreamJobSkillsList.Add("To be able to use a computer and the main software package competently");
                dreamJobSkillsList.Add("Language Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Planning, Discipline, Prioritising, Punctuality, Time Management, Attention to Detail
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Customer Service, Speaking Fluency, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Self-Motivation, Growth-Orientation, Patience, Persistence, Stress Management
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[22].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[9].Skill.ToString());

                //General Workplace Skills E
                //Independence at Work, Positive Attitude at Work
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Empathy, Orderliness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Teaching & Education";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to create and implement best Pedagogical strategies");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time Management, Planning, Multitasking, Punctuality, Discipline, Prioritizing, Attention to Detail
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Active Listening, Speaking Fluency, Leadership, Communicativeness, Presentation Skills, Confidence, Approachableness/ Socialable
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Creative Thinking, Sense of Humour, Patience, Self-Awareness, Flexibility, Self-Motivation, Responsibility, Initiative
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[18].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());

                //General Workplace Skills E
                //Positive Attitude to Work, Cultural Awareness, Work Ethic, Indepedence at work, Decision making, adaptability, problem solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Empathy (Compassion), Orderliness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Training";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching and the ability to design courses");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time Management, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Communicativeness, Customer Service, Teamwork, Leadership Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative, Flexibility, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Decision Making, Business and Commercial Awareness, Positive Attitude to Work, Career Management
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Empathy (Compassion), Enthusiasm, Openness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 3:
                dreamJobName = "Media Correspondant";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Media Production and Communication");
                dreamJobSkillsList.Add("Knowledge of Language");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Punctuality, Time Management, Attention to Detail
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active listening, Communicativeness, Speaking Fluency, Presentation Skills, Argumentation/ Discussion Skills, Teamwork, Writing Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, self-presentation, flexibility, self-motivation, initiative, authenticity, persistance, self-reflection
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[21].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[22].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[19].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Positive Attitude to Work, Work Ethic, Networking, Decision Making, Problem Solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Industriousness, Enthusiasm, Openness, Stability
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 4:
                dreamJobName = "Marketing & Communication";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Maths Knowledge");
                dreamJobSkillsList.Add("Knowledge of Media Production and Communication");
                dreamJobSkillsList.Add("Knowledge of Language");
                dreamJobSkillsList.Add("The ability to sell products and services");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //no organisational skills
                //dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Customer Service, Presentation Skills, Active Listening to Others, Speaking Fluency, Leadership, Writing Skills, Socialable (approachableness)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative, Growth-orientation, self-presentation, self-motivation, flexibility, patience
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Business and Commercial Awareness, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Empathy (compassion)
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                
                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 5:
                dreamJobName = "Press Relations";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Media Production and Communication");
                dreamJobSkillsList.Add("Language Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Discipline, Punctuality, Time Management, Meticulous (Attention to Detail)
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening Skills, Teamwork, Presentation Skills, Communicativeness, Customer Service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-motivation, growth-orientation, flexibility, initiative, stress management,
                //work under pressure, authenticity
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[15].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[21].Skill.ToString());

                //General Workplace Skills E
                //Networking, Positive Attitude at Work, Work Ethic, Persuasion Skills
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[11].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //no personality skills
                //dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                
                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 6:
                dreamJobName = "Human Resource";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Administration Skills");
                dreamJobSkillsList.Add("Knowledge of Human Resources and Employment Law");
                dreamJobSkillsList.Add("Sensitivity");
                dreamJobSkillsList.Add("Understanding");

                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Planning, Discipline, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Customer Service, Writing Skills, ACtive Listening to Others,
                //Leadership, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-motivation, flexibility, initative, responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());

                //General Workplace Skills E
                //Adaptability
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                
                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //no personality skills
                //dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 7:
                dreamJobName = "Managerial";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Maths Knowledge");
                dreamJobSkillsList.Add("Knowledge in a specialist subject area");
                dreamJobSkillsList.Add("Sensitivity");
                dreamJobSkillsList.Add("Understanding");

                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Discipline, Time Management, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Active Listening to Others, Providing Feedback,
                //Teamwork, Customer Service, Leadership
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility, Critical Thinking, Flexibility, Initiative
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());

                //General Workplace Skills E
                //Cultural Awareness, Career Management, Negotiating skills, Decision making,
                //Business and Commericial Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //no personality skills
                //dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 8:
                dreamJobName = "Research & Development";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Specialist Knowledge");

                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Multitasking, Planning, Discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Providing Feedback, Active Listening, Speaking Fluency,
                //Presentation skills, Writing Skills, Customer Service, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Initative, Growth-orientation, self-motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Indepedence at work, Business and commercial awareness,
                //career management, problem solving, work ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //no personality skills
                //dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 9:
                dreamJobName = "Editorial";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Media Production and Communication");

                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Discipline, Multitasking, Planning, Prioritizing, Punctuality, Time Management,
                //Meticulous (Attention to Detail)
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Argumentation/ Discussion Skills, Communicativeness,
                //Leadership, Providing Feedback, Speaking Fluency,
                //Writing Skills, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-motivation, Flexibility, Initiative, Ability to work under pressure, patience
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[13].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, cultural awareness, Work ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                
                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resillience, Orderliness, Intellect, Stability
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 10:
                dreamJobName = "Library";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Language");
                dreamJobSkillsList.Add("Administration Skills");

                //Industry/ Hard Skills A
                //Digital Literacy, Research Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Attention to Detail
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening to Others, Communicativeness, Customer Service,
                //Writing skills, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-presentation, self-motivation, flexibility, critical thinking, initiative,
                //responsibility, stamina, persistence
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[20].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[22].Skill.ToString());

                //General Workplace Skills E
                //Problem solving, independence at work, work ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Compassion, Orderliness, Enthusiasm
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 11:
                dreamJobName = "Speech & Language Therapist";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Language Knowledge");
                dreamJobSkillsList.Add("Knowledge of Psychology");
                dreamJobSkillsList.Add("Counselling Skills");
                dreamJobSkillsList.Add("Understanding");

                //Industry/ Hard Skills A
                //Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Planning, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Speaking Fluency, Communicativeness, Customer Service,
                //Discussion skills, Providing Feedback, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Decision Making, Independence at work, 
                //positive attitude to work, problem solving, work ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Compassion
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 12:
                dreamJobName = "Language Services";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Good Language Skills");
                dreamJobSkillsList.Add("Knowledge of English language");
                dreamJobSkillsList.Add("Knowledge of Psychology");

                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Planning, Multitasking, Prioritizing
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Customer Service, Communicativeness, Providing Feedback,
                //Argumentation/ Discussion Skills, Speaking Fluency, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Critical Thinking, Growth Orientation, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, adaptability, negotiating skills, cultural awareness, 
                //positive attitude at work, work ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());

                //Foreign Language Skills F
                //speaking, listening
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[2].Skill.ToString());

                //Personality G
                //Empathy, Politeness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 13:
                dreamJobName = "Creative Writing";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Language");
                dreamJobSkillsList.Add("Handle your own tax and accounts, if freelance");
                dreamJobSkillsList.Add("Knowledge of Media Production and Communication");

                //Industry/ Hard Skills A
                //Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Planning, Multitasking, Time Management, Prioritizing, Meticulous (Attention to Detail)
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Writing Skills, Communicativeness, Presentation Skills, Active Listening,
                //Aguementation/ Discussion Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative, Creative Thinking, Responsiblity, Self-Motivation, Patience, growth-orientation
                //, self-presentation, flexibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, Networking, work ethic, positive attitude to work,
                //adaptability, career management
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills

                //Personality G
                //no personality emotions

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 14:
                dreamJobName = "Special Education Needs (SEN) Teacher";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to create and implement best Pedagogical strategies");

                //Industry/ Hard Skills A
                //Digital Literacy, Applied Knowledge, Analytical Thinking
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //Organisational Skills B
                //Discipline, Prioritizing, Planning, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Speaking Fluency, Active Listening Skills, Socialable (C14)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Responsibility, Critical Thinking, Patience
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Positive Attitude to Work, Cultural Awareness, Work Ethic, Decision Making, Problem Solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills

                //Personality G
                //empathy
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 15:
                dreamJobName = "SEN Teaching Assistant";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to create and implement best Pedagogical strategies");

                //Industry/ Hard Skills A
                //Digital Literacy, Analytical Thinking
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //Organisational Skills B
                //Discipline, Prioritizing, Punctionality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening Skills, Communicativeness, Presentation Skills, Speaking Fluency, Socialable (Approachableness)
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[11].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Responsibility, Critical Thinking, Patience
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[11].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness, Positive Attitude to Work, Problem Solving, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills

                //Personality G
                //empathy (compassion)
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 16:
                dreamJobName = "Training Manager";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Ability to create and implement best Pedagogical strategies");

                //Industry/ Hard Skills A
                //Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Discipline, Planning, Prioritizing
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening Skills, Communicativeness, Customer Serivce, Discussion Skills, Leadership, Speaking Fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative, Self-Motivation, Flexibility, Critical Thinking, Growth Orientation, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());

                //General Workplace Skills E
                //Business and Commercial Awareness, Career Management, Positive Attitude to Work, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills

                //Personality G
                //empathy (compassion), enthusism, openness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 17:
                dreamJobName = "Training Officer";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Teaching");
                dreamJobSkillsList.Add("Curriculum Design");

                //Industry/ Hard Skills A
                //Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Discipline, Planning, Prioritizing, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening Skills, Discussion Skills, Communicativeness, Teamwork, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Critical Thinking, Creative Thinking, Responsibility, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Positive Attitude to Work, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills

                //Personality G
                //empathy (compassion), enthusism, openness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 18:
                dreamJobName = "Youth Worker";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Psychology");

                //Industry/ Hard Skills A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time Management, Punctuality, Discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening to Others, Customer Service, Communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Positive Attitude to Work, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills

                //Personality G
                //empathy (compassion)
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassLinguisticNames

    #region PassScienceEngineeringNames
    public void PassScienceEngineeringName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Data & Analytics";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Domain Knowledge");
                dreamJobSkillsList.Add("Ambitious");

                //Industry/ Hard Skills  A
                //Digital Literacy, Applied Knowledge, Statistical, Analytical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //No from B column

                //Communication and Interpersonal Skills C
                //Communicativeness, Customer Service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Critical Thinking, Logical Reasoning
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[16].Skill.ToString());

                //General Workplace Skills E
                //Business and Commercial Awareness, Independence at Work
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //no from G column or in repository, added to custom

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Engineering";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Science & Technology Knowledge");
                dreamJobSkillsList.Add("Mathematical Knowledge");
                dreamJobSkillsList.Add("Deductive Reasoning");
                dreamJobSkillsList.Add("Inductive Reasoning");
                dreamJobSkillsList.Add("Design Skills");
                dreamJobSkillsList.Add("Persistence");

                //Industry/ Hard Skills  A
                //Digital Literacy, Applied Knowledge, Analytical Thinking
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //nothing

                //Communication and Interpersonal Skills C
                //Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Critical Thinking, Creative/ Innovative Thinking, Initiative
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Problem Solving, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Research & Development";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Domain Knowledge");
                dreamJobSkillsList.Add("Reasoning Skills");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Research, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //no b column

                //Communication and Interpersonal Skills C
                //Communicativeness, Presentation Skills, Customer Service, Writing Skills, ACtive Listening
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Industriousness, Enthusiasm, Assertiveness, Intellect
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 3:
                dreamJobName = "Security";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 2;
                selectedDreamJobIndex = index;
                screen = true;

                //Industry/ Hard Skills  A
                //Analytical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //custom skills not in the repository
                dreamJobSkillsList.Add("Domain Knowledge");

                //Organisational Skills B
                //Planning
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Communicativeness, Customer Service, Active Listening
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initative, Critical Thinking, Self-Motivation, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Positive Attitude to Work, Problem Solving, Independence at Work, Adaptability, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Orderliness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassScienceEngineeringNames

    #region PassHumanitiesNames
    public void PassHumanitiesName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Press Relations";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Media Knowledge");
                dreamJobSkillsList.Add("Humanistic Knowledge");
                dreamJobSkillsList.Add("Social Psychology Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy, Social Media Management
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());

                //Organisational Skills B
                //Planning, Multitasking, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Speaking Fluency, Writing Skills, Teamwork, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-Motivation, Responsibility, Propriety/ Personal Culture, Creative/ Innovative Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Business and Commercial Awareness, POsitive Attitude to Work, Networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Politeness, Enthusiasm, Assertiveness, Intellect
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Marketing & Communication";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Humanistic Knowledge");
                dreamJobSkillsList.Add("Knowledge in the field of Social Psychology");
                dreamJobSkillsList.Add("Knowledge of Marketing Techniques");
                dreamJobSkillsList.Add("Advertising Knowledge");
                dreamJobSkillsList.Add("Market Research");
                dreamJobSkillsList.Add("Psychology of Advertisement");

                //Industry/ Hard Skills  A
                //Digital Literacy, Social Media
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());

                //Organisational Skills B
                //Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation/ Discussion Skills, Speaking Fluency, Writing Skills, Communicativeness, Presentation Skills, Customer Service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Self-Motivation, Propriety, Initiative, Creative/ Innovative Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Business and Commercial Awareness, Problem Solving, Negotiating Skills, Positive Attitude to Work, Networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Assertiveness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Administration & Corporate Support";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Law");
                dreamJobSkillsList.Add("Computer Software Knowledge");
                dreamJobSkillsList.Add("Knowledge of Organization");
                dreamJobSkillsList.Add("Office Skills");

                //Industry/ Hard Skills  A
                //Digital Literacy, Research Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Planning, Multitasking, Punctuality, Discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString()); 
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Communicativeness, Customer Service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Self-Motivation, Propriety/ Personal Culture
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());

                //General Workplace Skills E
                //Positive Attitude to Work
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Agreebleness, Politeness, Conscientiousness, Industriousness, Orderliness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 3:
                dreamJobName = "Teaching & Education";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Industry Knowledge");
                dreamJobSkillsList.Add("Teaching Skills");
                dreamJobSkillsList.Add("Teaching Methodology");
                dreamJobSkillsList.Add("Knowledge of Developmental Psychology");
                dreamJobSkillsList.Add("Knowledge of Learning Techniques");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time Management, Multitasking, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Active Listening to Others, Argumentation/ Discussion, Speaking Fluency, Writing Skills, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-Orientation, Self-Motivation, Responsibility, Propriety, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Decision Making, Positive Attitude at Work
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Agreeableness, Compassion, Politeness, Enthusiasm, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 4:
                dreamJobName = "Research & Development";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Methodological Awareness");
                dreamJobSkillsList.Add("Statistical Knowledge");
                dreamJobSkillsList.Add("General or Specialistic Humanitic Knowledge");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Planning, Punctuality, Discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation/ Discussion Skills, Writing skills, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-Orientation, Self-Presentation, Self-Motivation, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Independence at work, decision making, positive attitude at work, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //speaking, writing, listening, reading
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[3].Skill.ToString());

                //Personality G
                //Conscientiousness, Industriousness, Orderliness, Enthusiasm, Intellect
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 5:
                dreamJobName = "Governance";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Social Psychology");
                dreamJobSkillsList.Add("Knowledge of Governance Techniques");
                dreamJobSkillsList.Add("Knowledge of Economy");

                //Industry/ Hard Skills  A
                //no industry/ hard skills

                //Organisational Skills B
                //Time Management, Project Management, Planning, Punctuality, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, Active Listening to Others, Argumentation, Speaking Fluency, Teamwork, Leadership, Communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-Presentation, Self-Motivation, Responsibility, Propriety, Initative, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Independence at Work, Decision making, Business and Commercial Awareness, Problem Solving, Career Management,
                //Negotiating skills, positive attitude at work, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //speaking, writing, listening, reading 
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[3].Skill.ToString());

                //Personality G
                //Stability, Resilience, Assertiveness, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 6:
                dreamJobName = "Managerial";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Organisational Psychology");
                dreamJobSkillsList.Add("Knowledge of Motivational Psychology");
                dreamJobSkillsList.Add("Coaching Skills");
                dreamJobSkillsList.Add("Media Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Planning, Multitasking, Punctuality, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Active Listening to Others, Argumentation/ Discussion Skills, speaking Fluency,
                //Teamwork, Leadership, Communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-Orientation, Self-presentation, self-motivation, responsibility, propriety/ personal culture,
                //initiative, creative/ innovative thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, decision making, business and commercial awareness, problem solving,
                //career management, negotiating skills, positive attitude at work, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //speaking, writing, listening, reading
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[0].Skill.ToString()); 
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[1].Skill.ToString()); 
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[2].Skill.ToString()); 
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[3].Skill.ToString());

                //Personality G
                //Stability, Resilience, Industriousness, Assertiveness, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 7:
                dreamJobName = "Curator";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("History Knowledge");
                dreamJobSkillsList.Add("History of Arts Knowledge");
                dreamJobSkillsList.Add("Arts Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy, Research Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Project Management, Planning, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Self-Motvation, Responsibility, Propriety, Initative, Creative/ Innovative Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Decision Making, Problem Solving, Negotiating skills, positive attitude at work, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //speaking, writing, listening, reading
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[3].Skill.ToString());

                //Personality G
                //Resilience, Politeness, Orderliness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString()); 
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString()); 

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 8:
                dreamJobName = "Media Correspondent";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Media Knowledge");
                dreamJobSkillsList.Add("Humanistic Knowledge");
                dreamJobSkillsList.Add("Social Psychology Knowledge");
                dreamJobSkillsList.Add("KNowledge of News");

                //Industry/ Hard Skills  A
                //Digital Literacy, Social Media, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Time Management, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening to Others, Argumentation, Speaking Fluency, Writing Skills, Teamwork, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-Motivation, Responsibility, Propriety, Initiative, Creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Negotiating skills, positive attitude at work, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Compassion, Politeness, Extraversion, Enthusiasm, Intellect, Openness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassHumanitiesNames

    #region PassMediaSciencesNames
    public void PassMediaSciencesName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Performing Arts";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Computer Software Knowledge");
                dreamJobSkillsList.Add("Art Knowledge");
                dreamJobSkillsList.Add("Manual Dexterity");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Planning, Multitasking, Punctuality, Discipline, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active listening to others, argumentation, teamwork, leadership, communicativeness, presentation skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, positive attitude to work, problem solving, work ethic, adaptability
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Politeness, Industriousness, Orderliness, Enthusiasm, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Press Relations";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Computer Knowledge");
                dreamJobSkillsList.Add("Broadcast Knowledge");
                dreamJobSkillsList.Add("Media Knowledge");
                dreamJobSkillsList.Add("Knowledge of News");

                //Industry/ Hard Skills  A
                //Digital Literacy, Social Media, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Planning, Multitasking
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening to others, argumentation, speaking fluency, writing skills, communicativeness, presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-presentation, flexibility, responsibility, propriety, initiative, creative/ innovative thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Business and commercial awareness, problem solving, negotiating skills, positive attitude to work, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Politeness, Industriousness, Enthusiasm, Assertiveness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Creative Writing";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Broadcast Knowledge");
                dreamJobSkillsList.Add("Media Knowledge");
                dreamJobSkillsList.Add("Knowledge of News");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Social Media, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Time Management, Planning, Multitasking, Punctuality, Discipline, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening, argumentation, writing skills, communicativeness, presentation, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-presentation, flexibility, self-motivation, responsibility, propriety, initiative,
                //creative/innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, negotiating skills, positive attitude to work, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Industriousness, Orderliness, Extraversion, Enthusiasm, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 3:
                dreamJobName = "Media Correspondent";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Media Knowledge");
                dreamJobSkillsList.Add("Knowledge of News");

                //Industry/ Hard Skills  A
                //Research, 
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Attention to detail, Discipline, Multitasking, Planning, Punctuality, Time Management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active listening to others, communicativeness, presentation skills, providing feedback, speaking fluency, teamwork, writing skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Authenticity, Creative/ Innovative Thinking, Critical Thinking, Flexibility, Persistance, Responsibility, Self-motivation, self-presentation, self-reflection
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[21].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[22].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[19].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Networking, Positive Attitude to Work, Cultural Awareness, Work Ethic
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Politeness, Industriousness, Orderliness, Enthusiasm, Assertiveness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 4:
                dreamJobName = "Editorial Roles";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Office Skills");
                dreamJobSkillsList.Add("Media Knowledge");
                dreamJobSkillsList.Add("Computer Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy, Social Media
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());

                //Organisational Skills B
                //Time Management, planning, multitasking, punctuality, discipline, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, argumentation, speaking fluency, writing skills, teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-motivation, responsibility, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, negotiating skills, positive attitude to work
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills

                //Personality G
                //Industriousness, orderliness, intellect, openness
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());


                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 5:
                dreamJobName = "Managerial";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Media Knowledge");
                dreamJobSkillsList.Add("Knowledge of News");
                dreamJobSkillsList.Add("Computer Software Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Planning, Multitasking, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening to others, argumentation, speaking fluency, teamwork, leadership, communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, self-presentation, responsibility, propriety, initiative, creative/ innovative thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, decision making, business and commercial, problem solving,
                //career management, negotiating skills, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills

                //Personality G
                //Stability, Resilience, Industriousness, Assertiveness, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 6:
                dreamJobName = "Customer Insights";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Marketing Knowledge");
                dreamJobSkillsList.Add("Advertising Knowledge");
                dreamJobSkillsList.Add("Market Research");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //Project management, planning, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening, argumentation, speaking fluency, writing skills,
                //teamwork, communicativeness, presentation skills, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, flexibility, responsibility, propriety, initiative, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work ethic, independence at work, decision making, adaptability, business and commercial,
                //problem solving, negotiating skills, positive attitude to work, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign

                //Personality G
                //Resilience, Politeness, Orderliness, Enthusiasm, Assertiveness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 7:
                dreamJobName = "Administration & Corporate Support";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Computer Software Knowledge");
                dreamJobSkillsList.Add("Knowledge of Organization");
                dreamJobSkillsList.Add("Media Knowledge");

                //Industry/ Hard Skills  A
                //Analytical, Digital, Social Media, Research, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[6].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, planning, multitasking, discipline, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening to others, argumentation, speaking fluency, writing skills, teamwork, communicativeness, presentation skills, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, self-presentation, flexibility, self-motivation, responsibility, propriety, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, business and commercial, problem solving, career management, negotiating skills, positive attitude to work
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills F
                //no foreign 

                //Personality G
                //Stability, Resilience, Conscientiousness, Industriousness, Orderliness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 8:
                dreamJobName = "Marketing & Communications";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge in the Field");
                dreamJobSkillsList.Add("Knowledge of Marketing Techniques");
                dreamJobSkillsList.Add("Advertising Knowledge");
                dreamJobSkillsList.Add("Market Research");
                dreamJobSkillsList.Add("Psychological Knowledge");
                dreamJobSkillsList.Add("Psychology of Advertisement");

                //Industry/ Hard Skills  A
                //Analytical, Digital, Social Media, Applied Knowledge, Research, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, planning, multitasking, discipline, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening, argumentation, speaking fluency, writing skills, teamwork,
                //communicativeness, presentation skills, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, self-presentation, flexibility, self-motivation, responsibility, propriety, initiative,
                //creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, decision making, adaptability, business and commercial,
                //career management, negotiating skills, positive attitude to work, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Industriousness, Enthusiasm, Assertiveness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 9:
                dreamJobName = "Designing (Sound, Production)";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Advertising Knowledge");
                dreamJobSkillsList.Add("Computer Software Knowledge");
                dreamJobSkillsList.Add("Broadcast Knowledge");
                dreamJobSkillsList.Add("Media Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, planning
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active listening, argumentation, teamwork, presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-motivation, initiative, creative/ innovative thinking, critical thinking, flexibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, adaptability, business and commercial, positive attitude to work
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Industriousness, Orderliness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 10:
                dreamJobName = "Environment & Sustainability";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Environmental Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy, Applied Knowledge, Research, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //Project management, planning, multitasking, discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening, argumentation, speaking fluency, writing, communicativeness, presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-motivation, responsibility, propriety, initiative, creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work ethic, independence at work, decision making, adaptability, negotiating skills, positive attitude to work, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Industriousness, Enthusiasm, Assertiveness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 11:
                dreamJobName = "Teaching & Education";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Learning Techniques");
                dreamJobSkillsList.Add("Teaching Skills");
                dreamJobSkillsList.Add("Computer Software Knowledge");
                dreamJobSkillsList.Add("Media Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time management, multitasking
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening, argumentation, speaking fluency, writing skills, communicativeness, presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, responsiblity, propriety, creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work ethic, decision making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Politeness, Enthusiasm, Intellect, Stability, Resilience
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 12:
                dreamJobName = "Research & Development";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Methodology Awareness");
                dreamJobSkillsList.Add("Statistical Knowledge");
                dreamJobSkillsList.Add("Ability to work with a database");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Applied Knowledge, Research, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, planning, multitasking, punctuality, discipline, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, argumentation, writing skills, teamwork, communicativeness, presentation skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, flexibility, self-motivation, responsibility, creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work ethic, independence at work, adaptability, business and commercial, cultural awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Industriousness, Orderliness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 13:
                dreamJobName = "Professional BJ (Presenter, Producer, Camera Work, Lighting, Multimedia)";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Computer Software Knowledge");
                dreamJobSkillsList.Add("Media Knowledge");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, planning, multitasking, punctuality, discipline, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening, speaking fluency, writing skills, teamwork, communicativeness,
                //presentation skills, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, self-presentation, flexibility, self-motivation, propriety, initiative, creative/ innovative, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work ethic, decision making, adaptability, business and commercial, problem solving, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Politeness, Industriousness, Orderliness, Enthusiasm, Assertiveness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 14:
                dreamJobName = "Broadcasting Engineer";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Computer Knowledge");
                dreamJobSkillsList.Add("Broadcast Knowledge");
                dreamJobSkillsList.Add("Media Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time management, planning, multitasking, punctuality, discipline, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active listening, teamwork, communicativeness, presentation skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Responsibility, initiative, creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work ethic, decision making, adaptability, business and commercial, problem solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Orderliness, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 15:
                dreamJobName = "Talent Agent";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Art Knowledge");
                dreamJobSkillsList.Add("Media Knowledge");
                dreamJobSkillsList.Add("Photography");

                //Industry/ Hard Skills  A
                //Digital Literacy, Social Media Management
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, planning, multitasking, punctuality, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening, argumentation, speaking fluency, writing skills,
                //leadership, communicativeness, presentation skills, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, self-presentation, flexibility, self-motivation, responsibility,
                //propriety, initiative, creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, decision making, adaptability, business and commercial, problem solving,
                //career management, negotiating skills, positive attitude to work, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Extraversion, Enthusiasm, Assertiveness, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 16:
                dreamJobName = "Stylist";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Art Knowledge");
                dreamJobSkillsList.Add("Photography");
                dreamJobSkillsList.Add("Fashion Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time management, planning, multitasking, punctuality, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening to others, argumentation, speaking fluency,
                //communicativeness, presentation, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, self-presentation, flexibility, self-motivation, responsibility, propriety, initiative,
                //creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, decision making, adaptability, business and commercial, problem solving,
                //career management, negotiating, positive attitude to work, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Politeness, Industriousness, Enthusiasm, Assertiveness, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 17:
                dreamJobName = "Direction & Editing";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Broadcast Knowledge");
                dreamJobSkillsList.Add("Media Knowledge");
                dreamJobSkillsList.Add("Knowledge of News");
                dreamJobSkillsList.Add("Computer Software Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time management, planning, multitasking, punctuality, discipline, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation, teamwork, leadership, communicativeness, presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Self-motivation, responsibility, initative, creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, business and commercial, positive attitude to work, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resillience, Industriousness, Enthusiasm, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassMediaSciencesNames

    #region PassPsychologyNames
    public void PassPsychologyName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Administration & Corporate Support";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Organizational Psychology");
                dreamJobSkillsList.Add("Knowledge of Positive Psychology");
                dreamJobSkillsList.Add("Knowledge of Motivational Psychology");
                dreamJobSkillsList.Add("Coaching Skills");
                dreamJobSkillsList.Add("Knowledge of Personality Psychology");

                //Industry/ Hard Skills  A
                //Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Planning, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Active listening to others, speaking fluency, leadership, communicativeness, presentation skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Self-motivation, Propriety/ personal culture, initiative, creative/ innovative thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Cultural Awareness, Business and Commercial Awareness, Problem Solving, Career Management, Positive Attitude to Work
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Enthusiasm, ASsertiveness, Intellect, Openness, Compassion
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Training";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Organizational Psychology");
                dreamJobSkillsList.Add("Knowledge of Motivational Psychology");
                dreamJobSkillsList.Add("Coaching Skills");
                dreamJobSkillsList.Add("Knowledge on Emotions and Emotion Regulation");
                dreamJobSkillsList.Add("Knowledge of Personality Psychology");

                //Industry/ Hard Skills  A
                //Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time Management, Planning, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Active Listening to Others, Speaking Fluency, Leadership, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Propriety/ Personal Culture, Initiative, Creative/ Innovative Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Enthusiasm, ASsertiveness, Intellect, Openness, Compassion
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Teaching & Education";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Developmental Psychology");
                dreamJobSkillsList.Add("Knowledge of Cognitive Psychology");
                dreamJobSkillsList.Add("Knowledge of Learning Techniques");
                dreamJobSkillsList.Add("Knowledge of Individual Differences and their Impact on Learning Processes");
                dreamJobSkillsList.Add("Teaching Skills");

                //Industry/ Hard Skills  A
                //Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //no b column

                //Communication and Interpersonal Skills C
                //Providing Feedback, ACtive Listening to Others, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility, Propriety/ Personal Culture, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Independence at Work, Adaptability, Problem Solving, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Compassion, Stability
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 3:
                dreamJobName = "Research & Development";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Methodological Awareness");
                dreamJobSkillsList.Add("Statistical Knowledge");
                dreamJobSkillsList.Add("General or Specialistic Psychological Knowledge");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Research, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Planning, Multitasking, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation/ Discussion Skills, SPeaking Fluency, Writing Skills, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-Orientation, Self-Motivation, Initiative, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Independence at Work, Decision Making, Adaptability, Networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Industriousness, Orderliness, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 4:
                dreamJobName = "Managerial";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Organizational Psychology");
                dreamJobSkillsList.Add("Knowledge of Motivational Psychology");
                dreamJobSkillsList.Add("Coaching Skills");
                dreamJobSkillsList.Add("Knowledge of Personality Psychology");
                dreamJobSkillsList.Add("Psychological Assessment");

                //Industry/ Hard Skills  A
                //Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time Management, Planning
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Active Listening to Others, Speaking Fluency, Teamwork, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Propriety/ Personal Culture, Initiative, Creative/ Innovative Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Decision Making, Adaptability, Business and Commercial Awareness, Problem Solving, Positive Attitude to Work, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Compassion, Orderliness, Enthusiasm, Assertiveness, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 5:
                dreamJobName = "Counselling & Therapy";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Clinical Psychology");
                dreamJobSkillsList.Add("Knowledge of Personality Psychology");
                dreamJobSkillsList.Add("Emotion Regulation Strategies");
                dreamJobSkillsList.Add("Knowledge of Therapeutic Techniques (Certified)");

                //Industry/ Hard Skills  A
                //Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time Management, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Active Listening to Others, Speaking Fluency, Communicativeness, Customer Service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Responsibility, Propriety/ Personal Culture
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Independence at Work, Problem Solving, Positive Attitude to Work, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Compassion, Politeness, Stability, Resilience, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 6:
                dreamJobName = "Health Professional";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Clinical Psychology");
                dreamJobSkillsList.Add("Knowledge of Therapeutic Psychology");
                dreamJobSkillsList.Add("Diagnostic Skills");
                dreamJobSkillsList.Add("Psychological Assessment");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Planning, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Active Listening to Others, Speaking Fluency, Leadership, Communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Responsibility, Propriety/ Personal Culture
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Independence at Work, Decision Making, Problem Solving, Positive Attitude at Work, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Compassion, Politeness, Stability, Resilience, Orderliness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 7:
                dreamJobName = "Health & Safety";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Occupational Stress Factors");
                dreamJobSkillsList.Add("Knowledge of Coping with Stress Strategies");
                dreamJobSkillsList.Add("Knowledge of Organisational Psychology");
                dreamJobSkillsList.Add("Coaching Skills");
                dreamJobSkillsList.Add("Psychological Assessment");

                //Industry/ Hard Skills  A
                //Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Project Management, Planning, Priorisintg
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Active Listening to Others, Speaking Fluency, Communicativeness, Presentation Skills, Customer Service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility, Propriety/ Personal Culture
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Adaptability, Business and Commercial Awareness, Problem Solving, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Compassion, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 8:
                dreamJobName = "Governance";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Social Psychology");
                dreamJobSkillsList.Add("Knowledge of Politics");
                dreamJobSkillsList.Add("Knowledge of Governance Techniques");
                dreamJobSkillsList.Add("Knowledge of Economy");
                dreamJobSkillsList.Add("Coaching Skills");
                dreamJobSkillsList.Add("General Psychological Knowledge");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Social Media, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Planning, Discipline, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Speaking Fluency, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-Presentation, Flexibility, Responsibility, Propriety/ Personal Culture
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Problem Solving, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Industriousness, Orderliness, Assertiveness, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 9:
                dreamJobName = "Human Resource ES";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Knowledge of Organisational Psychology");
                dreamJobSkillsList.Add("Psychological Assessment");
                dreamJobSkillsList.Add("Knowledge of Personality Psychology");
                dreamJobSkillsList.Add("Coaching Skills");
                dreamJobSkillsList.Add("Knowledge of Human Resource and Employment Law");
                dreamJobSkillsList.Add("Administration Skills");
                dreamJobSkillsList.Add("Business Management Skills");

                //Industry/ Hard Skills  A
                //Digital Literacy, Social Media, Applied Knowledge, Research Skills
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Planning
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Active Listening to Others, Speaking Fluency, Writing Skills, Teamwork, Communicativeness, Presentation Skills, Customer Service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-Presentation, Flexibility, Propriety/ Personal Culture, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Decision Making, Business and Commercial Awareness, Career Management, Negotiating Skills, Cultural Awareness, Networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Compassion, Politeness, Industriousness, Orderliness, Intellect
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 10:
                dreamJobName = "Data & Analytics";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Quantitative Research");
                dreamJobSkillsList.Add("Psychological Knowledge");
                dreamJobSkillsList.Add("Psychometry");
                dreamJobSkillsList.Add("Statistics");
                dreamJobSkillsList.Add("Statistical Software");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Applied, Research, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //Time Management, PLanning, Discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing Feedback, Argumentation/ Discussion, Writing, Presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-Motivation, Responsibility, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work Ethic, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Conscientiousness, Industriousness, Orderliness, Intellect
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 11:
                dreamJobName = "Customer Insight";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Marketing Knowledge");
                dreamJobSkillsList.Add("Advertising Knowledge");
                dreamJobSkillsList.Add("Market Research");
                dreamJobSkillsList.Add("Conducting Focus Groups");
                dreamJobSkillsList.Add("Conducting Individuals Interviews");
                dreamJobSkillsList.Add("Qualitative Research");
                dreamJobSkillsList.Add("Quantitative Research");
                dreamJobSkillsList.Add("Psychological Knowledge");
                dreamJobSkillsList.Add("Psychology of Advertisement");
                dreamJobSkillsList.Add("Knowledge of Social Psychology");

                //Industry/ Hard Skills  A
                //Analytical, Digital, Applied Knowledge, Research, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //Project Management, Planning, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening to others, argumentation/ discussion, speaking fluency, writing skills, teamwork, communicativeness, presentation, customer
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, flexibility, responsibility, propriety, initiative, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work ethic, independence at work, decision making, adaptability, business and commercial, problem solving, negotiating, positive attitude, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Politeness, Orderliness, Enthusiasm, Assertiveness, Intellect, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 12:
                dreamJobName = "Marketing & Communication";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Marketing Knowledge");
                dreamJobSkillsList.Add("Advertising Knowledge");
                dreamJobSkillsList.Add("Market Research");
                dreamJobSkillsList.Add("Psychological Knowledge");
                dreamJobSkillsList.Add("Psychology of Advertisement");
                dreamJobSkillsList.Add("Knowledge of Social Psychology");

                //Industry/ Hard Skills  A
                //Analytical, Digital, Social Media
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());

                //Organisational Skills B
                //Time Management, Planning, Multitasking, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Argumentation, Speaking fluency, teamwork, communicativeness, presentation, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, flexibility, propriety, initiative, creative/ innovative thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Decision making, adaptability, business and commercial, positive attitude, cultural awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Politeness, Extraversion, Enthusiasm, Openness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 13:
                dreamJobName = "Press Relations";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Marketing Knowledge");
                dreamJobSkillsList.Add("Administration");
                dreamJobSkillsList.Add("Management");
                dreamJobSkillsList.Add("Psychological Knowledge");
                dreamJobSkillsList.Add("Psychology of Advertisement");
                dreamJobSkillsList.Add("Knowledge of Social Psychology");

                //Industry/ Hard Skills  A
                //Digital, social media
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());

                //Organisational Skills B
                //Time Managmeent, Project management, planning, multitasking, punctuality, discipline, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, argumentation, speaking fluency, writing skills, leadership, communicativeness, presentation, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Responsibility, Propriety, Initiative, Creative thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Decision making, business and commercial awareness, problem solving, negotiating skills, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Industriousness, Extraversion, Assertiveness, Openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 14:
                dreamJobName = "Career Advisor";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Career Councelling");
                dreamJobSkillsList.Add("Knowledge of Psychology");
                dreamJobSkillsList.Add("Assessment");
                dreamJobSkillsList.Add("Knowledge of Labour Market");

                //Industry/ Hard Skills  A
                //Analytical, Applied Knowledge, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Time management, planning, punctuality, discipline, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening to others, speaking fluency, communicativeness, presentation skills, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, flexibility, self-motivation, responsibility, propriety, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Work ethic, independence at work, decision making, bsuiness and commercial, career management, negotiating, positive attitude
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Resilience, Compassion, politeness, orderliness, extraversion, openness
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassPsychologyNames

    #region PassBusinessStudiesNames
    public void PassBusinessStudyNames(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Accountancy";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Economics & Accounting");
                dreamJobSkillsList.Add("Maths Knowledge");
                dreamJobSkillsList.Add("Administration Skills");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //no skills

                //Communication and Interpersonal Skills C
                //Active listening to others, communicativeness, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Critical thinking, responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());

                //General Workplace Skills E
                //Work ethic, independence at work, adaptability
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Economist";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Economics & Accounting");
                dreamJobSkillsList.Add("Maths Knowledge");
                dreamJobSkillsList.Add("Logical Reasoning");
                dreamJobSkillsList.Add("Deductive Reasoning");

                //Industry/ Hard Skills  A
                //Analytical thinking, digital literacy, statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //no skills

                //Communication and Interpersonal Skills C
                //Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Business and Commercial Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Ambitious, Industriousness, Intellect
                dreamJobSkillsList.Add("Ambitious");
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Corporate Finance";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Economics & Accounting");
                dreamJobSkillsList.Add("Maths Knowledge");
                dreamJobSkillsList.Add("Financial Management Skills");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //project management
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Leadership, Communicativeness, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, business and commercial awareness, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Assertiveness, Openness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassBusinessStudiesNames

    #region PassGeographyEarthNames
    public void PassGeographyEarthName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Research & Development";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Domain Knowledge");
                dreamJobSkillsList.Add("Math Knowledge");
                dreamJobSkillsList.Add("Mathematical Reasoning");
                dreamJobSkillsList.Add("Deductive Reasoning");
                dreamJobSkillsList.Add("Inductive Reasoning");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Applied Knowledge, Research, Statistical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[5].Skill.ToString());

                //Organisational Skills B
                //no skills

                //Communication and Interpersonal Skills C
                //active listening, writing skills, communicativeness, presentation skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Critical Thinking, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());

                //General Workplace Skills E
                //Independence at work, problem solving, adaptability
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Persistent, Resilience, Orderliness, Assertiveness, Intellect
                dreamJobSkillsList.Add("Persistent");
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Design";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills & Knowledge");
                dreamJobSkillsList.Add("Coordination");
                dreamJobSkillsList.Add("Logical Reasoning");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //no skills

                //Communication and Interpersonal Skills C
                //Communicativeness, Presentation Skills, Teamwork, Active Listening to Others
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Creative/ Innovative Thinking, Initiative, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());

                //General Workplace Skills E
                //Problem Solving, Adaptability
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Persistent, Compassion, Industriousness, Enthusiasm
                dreamJobSkillsList.Add("Persistent");
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassGeographyEarthNames

    #region PassMedVetLifeSciencesNames
    public void PassMedVetLifeSciencesName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Health Professional";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Domain Knowledge");
                dreamJobSkillsList.Add("Public Health Knowledge");

                //Industry/ Hard Skills  A
                //Analytical, APplied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time management, multitasking, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Active listening, Speaking fluency, communicativeness, customer service
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Problem Solving, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Persistent, Determinant, Understanding, Patience
                dreamJobSkillsList.Add("Persistent");
                dreamJobSkillsList.Add("Determinant");
                dreamJobSkillsList.Add("Understanding");
                dreamJobSkillsList.Add("Patience");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Veterinary Services";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Domain Knowledge");
                dreamJobSkillsList.Add("Animal Health Knowledge");
                dreamJobSkillsList.Add("Biology Knowledge");
                dreamJobSkillsList.Add("Counselling Skills");
                dreamJobSkillsList.Add("Active Learning");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //no skills

                //Communication and Interpersonal Skills C
                //Customer service, teamwork, communicativeness, active listening, speaking fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Problem Solving, Networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Patience, Compassion
                dreamJobSkillsList.Add("Patience");
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Pharmaceutical";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Domain Knowledge");
                dreamJobSkillsList.Add("Maths Knowledge");
                dreamJobSkillsList.Add("Administration Skills");
                dreamJobSkillsList.Add("Active Learning");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //no skills

                //Communication and Interpersonal Skills C
                //Customer Service, Communicativeness, Active Listening, Teamwork, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative, Growth-Orientation, Critical Thinking, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());

                //General Workplace Skills E
                //no skills

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Patience, Persistent, Industriousness, Stability
                dreamJobSkillsList.Add("Patience");
                dreamJobSkillsList.Add("Persistent");
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassMedVetLifeSciencesNames

    #region PassArchitectureNames
    public void PassArchitectureName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Creative Arts";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time Management, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Problem Solving, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Assertiveness, Openness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Commercial";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");
                dreamJobSkillsList.Add("Entrepreneurship Skills");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //time management, punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Presentation skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Problem Solving, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Assertiveness, Openness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Managerial";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Entrepreneurship Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Applied Knowledge, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, multitasking, punctuality, planning
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation, Teamwork, Presentation skills, Providing feedback
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, problem solving, negotiating skills
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //enthusiasm, assertiveness, decisiveness, tolerance for stress
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Decisiveness");
                dreamJobSkillsList.Add("Tolerance for Stress");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 3:
                dreamJobName = "Industrial";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time management, punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Presentation Skill
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Industriousness, Assertiveness, Lifelong Learning Ability
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 4:
                dreamJobName = "Planning";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical Thinking
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, punctuality, multitasking
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Leadership
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Creative/ Innovative Thinking, Critical Thinking, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());

                //General Workplace Skills E
                //Decision making, problem solving, negotiating
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Assertiveness, Openness, Decisiveness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Decisiveness");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 5:
                dreamJobName = "Research & Development";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");
                dreamJobSkillsList.Add("Professional Knowledge");
                dreamJobSkillsList.Add("Divergent Thinking");
                dreamJobSkillsList.Add("Self-Regulation");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time management, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Initiative, Creative/ Innovative Thinking, Critical Thinking, REsponsibility, Self-Moviation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());

                //General Workplace Skills E
                //Decision making, problem solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Industriousness, Assertiveness, Lifelong Learning Ability, Tolerance for Stress
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");
                dreamJobSkillsList.Add("Tolerance for Stress");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 6:
                dreamJobName = "Teaching & Education";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Curriculum Design");
                dreamJobSkillsList.Add("Instructional Design");
                dreamJobSkillsList.Add("Teaching Skills");
                dreamJobSkillsList.Add("Pedagogical Knowledge");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy, Applied knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Planning, Prioritising, Discipline, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active listening, discussion skills, communicativeness, teamwork, presentation skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility, Critical Thinking, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Positive Attitude to Work, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Empathy (Compassion), Lifelong Learning Ability
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 7:
                dreamJobName = "Engineering & Design";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical Thinking
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Leadership
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, Problem solving, Negotiating skills
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Industriousness, Assertiveness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 8:
                dreamJobName = "Environment & Sustainability";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");
                dreamJobSkillsList.Add("Professional Knowledge");
                dreamJobSkillsList.Add("Ecological Thinking");
                dreamJobSkillsList.Add("Interdisciplinary Thinking");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time management, punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Problem Solving, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Assertiveness, Social Awareness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Social Awareness");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassArchitectureNames

    #region PassArtsNames
    public void PassArtsName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Designing & Illustration";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time Management, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Initiative, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Problem Solving, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Industriousness, Assertiveness, Openness, Lifelong Learning Ability
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Marketing";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Entrepreneurship Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Multitasking, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation, TEamwork, Presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, problem solving, business and commercial, negotiating skills, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Assertiveness, Openness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Creative Arts";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");

                //Industry/ Hard Skills  A
                //Applied Knowledge, Digital Literacy, Analytical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //Organisational Skills B
                //Time management, punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Problem Solving, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //enthusiasm, assertiveness, industriousness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 3:
                dreamJobName = "Performing Arts";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");

                //Industry/ Hard Skills  A
                //Analytical Thinking, Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time management, punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Presentation Skill
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Problem Solving, Decision Making
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //enthusiasm, assertiveness, industriousness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 4:
                dreamJobName = "Teaching & Education";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Curriculum Design");
                dreamJobSkillsList.Add("Instructional Design");
                dreamJobSkillsList.Add("Teaching Skills");
                dreamJobSkillsList.Add("Pedagogical Knowledge");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Planning, Prioritizing, Discipline, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening to Others, Discussion Skills, Communicativeness, Teamwork, Presentation Skills, Speaking Fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility, Critical Thinking, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, POsitive attitude, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Empathy, Enthusiasm, Lifelong Learning Ability
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassArtsNames

    #region PassTheologyNames
    public void PassTheologyName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Chaplain";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                //no custom skills

                //Industry/ Hard Skills  A
                //no hard skills

                //Organisational Skills B
                //Prioritizing
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening to Ohters, Speaking Fluency, Providing Feedback, Communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //no skills

                //General Workplace Skills E
                //Cultural Awareness, Negotiating Skills
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Compassion, Politeness, Interpersonal Sensitivity
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Interpersonal Sensitivity");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Teaching & Education";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Curriculum Design");
                dreamJobSkillsList.Add("Instructional Design");
                dreamJobSkillsList.Add("Teaching Skills");
                dreamJobSkillsList.Add("Pedagogical Knowledge");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Planning, Prioritising, Discipline, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Discussion skills, Communicativeness, Speaking Fluency, Teamwork, Presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility, Critical Thinking, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Positive Attitude, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Empathy, Enthusiasm, Lifelong Learning Ability
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Social Worker";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                //no custom

                //Industry/ Hard Skills  A
                //no hard skills

                //Organisational Skills B
                //Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, Active listening, Communicativeness, Discussion
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative, Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());

                //General Workplace Skills E
                //Negotiating, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Politeness, Empathy, Social Sensitivity
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Social Sensitivity");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassTheologyNames

    #region PassSocialPoliticalNames
    public void PassSocialPoliticalName(int index)
    {
        switch (index)
        {
            case 0:
                dreamJobName = "Administration & Corporate Support";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Entrepreneurship Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Social Media
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, planning, punctuality, discipline, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation/ Discussion, Leadership
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, problem solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Resilience, Industriousness, Tolerance for Stress
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add("Tolerance for Stress");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 1:
                dreamJobName = "Environment & Sustainability";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Professional Knowledge");
                dreamJobSkillsList.Add("Ecological Thinking");
                dreamJobSkillsList.Add("Interdisciplinary Thinking");

                //Industry/ Hard Skills  A
                //no hard skills

                //Organisational Skills B
                //Planning
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Argumentation Discussion Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-motivation, Critical Thinking, Creative/ Innovative Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Cultural Awareness, Problem Solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Industriousness, Lifelong Learning Ability, Social Sensitivity
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");
                dreamJobSkillsList.Add("Social Sensitivity");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 2:
                dreamJobName = "Governance & Policy";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //Organisational Skills B
                //Project management, planning, prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Leadership, Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Critical Thinking, Creative/ Innovative Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());

                //General Workplace Skills E
                //Decision making, Problem Solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Industriousness, Openness, Lifelong Learning Ability, Decisiveness
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");
                dreamJobSkillsList.Add("Decisiveness");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 3:
                dreamJobName = "Industrial";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Design Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical Thinking
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //Organisational Skills B
                //Time Management, Project Management, Planning, Discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Growth-orientation, initiative
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());

                //General Workplace Skills E
                //Adaptability
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Industriousness, Lifelong Learning Ability
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 4:
                dreamJobName = "Managerial";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Entrepreneurship Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, multitasking, punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation, Teamwork, Presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, problem solving, business and commercial, negotiating skills, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Assertiveness, Openness, Decisiveness, Tolerance for Stress
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Decisiveness");
                dreamJobSkillsList.Add("Tolerance for Stress");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 5:
                dreamJobName = "Marketing & Communication";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Entrepreneurship Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, multitasking, punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation, Teamwork, Presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, problem solving, business and commercial, negotiating skills, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Assertiveness, Openness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 6:
                dreamJobName = "Campaigning & Fundraising";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                //no skills

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, multitasking, punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation, Teamwork, Presentation
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Self-presentation, flexibility, creative/ innovative thinking, critical thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, problem solving, business and commercial, negotiating skills, networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Assertiveness, Openness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 7:
                dreamJobName = "Planning";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Entrepreneurship Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //Organisational Skills B
                //Time management, project management, punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork, Leadership
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Creative/ Innovative, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, problem solving, negotiating skills
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Enthusiasm, Assertiveness, Openness, Decisiveness
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Decisiveness");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 8:
                dreamJobName = "Research & Development";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical, Digital Literacy, Applied Knowledge, Research
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[4].Skill.ToString());

                //Organisational Skills B
                //Time management, punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Teamwork
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Flexibility, Initiative, Creative/ Innovative Thinking, Critical Thinking
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());

                //General Workplace Skills E
                //Decision making, problem solving
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Industriousness, Assertiveness, Lifelong Learning Ability, Tolerance for Stress
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");
                dreamJobSkillsList.Add("Tolerance for Stress");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 9:
                dreamJobName = "Teaching & Education";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Curriculum Design");
                dreamJobSkillsList.Add("Instructional Design");
                dreamJobSkillsList.Add("Teaching Skills");
                dreamJobSkillsList.Add("Pedagogical Knowledge");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Digital Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Planning, Prioritising, Discipline, Punctuality
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[4].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Active Listening, Discussion, Communicativeness, Speaking Fluency, Teamwork, Presentation Skills
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility, Critical Thinking, Self-Motivation
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[3].Skill.ToString());

                //General Workplace Skills E
                //Adaptability, Positive Attitude, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Empathy, Enthusiasm, Openness, Lifelong Learning Ability
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.extraversionSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.intellectOpennessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 10:
                dreamJobName = "Strategy";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Entrepreneurship Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Analytical
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[0].Skill.ToString());

                //Organisational Skills B
                //Project Management, Planning, Multitasking, Discipline, Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Arugmentation, Speaking Fluency, Teamwork, Leadersip, Communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Initiative
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[6].Skill.ToString());

                //General Workplace Skills E
                //Work ethic, decision making, problem solving, negotiating skills
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[5].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Stability, Industriousness, Lifelong Learning Ability, Social Sensitivity
                dreamJobSkillsList.Add(skillData.emotionalStabilitySkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");
                dreamJobSkillsList.Add("Social Sensitivity");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 11:
                dreamJobName = "Training";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository
                dreamJobSkillsList.Add("Entrepreneurship Skills");
                dreamJobSkillsList.Add("Professional Knowledge");

                //Industry/ Hard Skills  A
                //Digtal Literacy, Applied Knowledge
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[3].Skill.ToString());

                //Organisational Skills B
                //Time management, discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Argumentation, Speaking Fluency, Leadership, Communicativeness, Presentation, Active listening
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[6].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[8].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //no skills

                //General Workplace Skills E
                //Business and Commercial, Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //no foreign language skills 

                //Personality G
                //Politeness, Orderliness, Lifelong Learning Ability
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.conscientiousnessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Lifelong Learning Ability");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 12:
                dreamJobName = "Press Relations";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository


                //Industry/ Hard Skills  A
                //Digital Literacy, Social Media
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());

                //Organisational Skills B
                //no skills

                //Communication and Interpersonal Skills C
                //Providing feedback, speaking fluency, writing skills, communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //no skills

                //General Workplace Skills E
                //Networking. Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //speaking, writing, listening, reading
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[3].Skill.ToString());

                //Personality G
                //Empathy, Social Awareness, Interpersonal Sensitivity
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add("Social Awareness");
                dreamJobSkillsList.Add("Interpersonal Sensitivity");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 13:
                dreamJobName = "Social Worker";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository


                //Industry/ Hard Skills  A
                //none

                //Organisational Skills B
                //no skills

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening to others, argumentation, speaking fluency, communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility, Propriety
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[5].Skill.ToString());

                //General Workplace Skills E
                //Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //none

                //Personality G
                //Compassion (Empathy), Interpersonal Sensitivity, Social Awareness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add("Social Awareness");
                dreamJobSkillsList.Add("Interpersonal Sensitivity");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 14:
                dreamJobName = "Police";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository


                //Industry/ Hard Skills  A
                //none

                //Organisational Skills B
                //discipline
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[5].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening, argumentation, speaking fluency, communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[2].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //Responsibility
                dreamJobSkillsList.Add(skillData.psvaSkillList.skills[4].Skill.ToString());

                //General Workplace Skills E
                //Cultural Awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //none

                //Personality G
                //Compassion (Empathy), Politeness, Interpersonal Sensitivity
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Interpersonal Sensitivity");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 15:
                dreamJobName = "Charity Officer";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository


                //Industry/ Hard Skills  A
                //none

                //Organisational Skills B
                //Prioritising
                dreamJobSkillsList.Add(skillData.organisationalSkillList.skills[6].Skill.ToString());

                //Communication and Interpersonal Skills C
                //Providing feedback, active listening, communicativeness, speaking fluency
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //no skills

                //General Workplace Skills E
                //Negotiating skills, cultural awareness
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[7].Skill.ToString());
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[9].Skill.ToString());

                //Foreign Language Skills F
                //none

                //Personality G
                //Compassion (Empathy), Politeness, Interpersonal Sensitivity
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Interpersonal Sensitivity");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;

            case 16:
                dreamJobName = "Media Correspondent";
                dreamJobTitle.text = dreamJobName;
                selectedDreamJob = dreamJobName;
                selectedDreamJobCategory = 3;
                selectedDreamJobIndex = index;
                screen = true;

                //custom skills not in the repository


                //Industry/ Hard Skills  A
                //Digital Literacy, Social Media
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add(skillData.hardSkillList.skills[2].Skill.ToString());

                //Organisational Skills B
                //no skills

                //Communication and Interpersonal Skills C
                //Providing feedback, Speaking Fluency, Writing Skills, Communicativeness
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[3].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[4].Skill.ToString());
                dreamJobSkillsList.Add(skillData.commIntSkillList.skills[7].Skill.ToString());

                //Personal Skills, Values and Attitudes D
                //no skills

                //General Workplace Skills E
                //networking
                dreamJobSkillsList.Add(skillData.generalWorkplaceSkillList.skills[10].Skill.ToString());

                //Foreign Language Skills F
                //speaking, writing, listening, reading
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[0].Skill.ToString()); 
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[1].Skill.ToString()); 
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[2].Skill.ToString()); 
                dreamJobSkillsList.Add(skillData.foreignLanguageSkillList.skills[3].Skill.ToString());

                //Personality G
                //Compassion (Empathy), Politeness, Interpersonal Sensitivity, Social Awareness
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[0].Skill.ToString());
                dreamJobSkillsList.Add(skillData.agreeablenessSkillList.skills[1].Skill.ToString());
                dreamJobSkillsList.Add("Interpersonal Sensitivity");
                dreamJobSkillsList.Add("Social Awareness");

                foreach (var x in dreamJobSkillsList)
                {
                    Debug.Log(x.ToString());
                }

                DisplayDreamJob();

                //TODO: WHEN WE WANT TO CLEAR THE LIST
                //dreamJobSkillsList.Clear(); //this should clear the list
                break;
        }
    }
    #endregion PassSocialPoliticalNames

    #endregion Pass Dream Job Names
}
