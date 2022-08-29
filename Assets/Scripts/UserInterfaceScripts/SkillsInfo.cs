using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SkillsInfo : MonoBehaviour
{
    [Header("Skill Display References")]
    [SerializeField]
    private TMP_InputField skillInput;
    [SerializeField]
    private GameObject skillInputField;
    [SerializeField]
    private GameObject[] skillsList;
    [SerializeField]
    private Button[] bottomBannerButtons;
    [SerializeField]
    private GameObject noResultsFound;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject addSkillField;
    [SerializeField]
    private TMP_InputField addSkillInputField;
    /*[SerializeField]
    private GameObject SkillLevelSelectPrefab;*/
    [Space(5f)]

    [Header("Skill Definition Window")]
    [SerializeField]
    private GameObject definitionWindow;
    [SerializeField]
    private TMP_Text skillName;
    [SerializeField]
    private TMP_Text skillDefinition;
    [Space(5f)]

    [Header("Skill Data")]
    public SkillData _addNewSkillData;
    public DynamicInterfaceAreaUI dynamicInterfaceManager;

    public string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < skillsList.Length; i++)
        {
            skillsList[i].SetActive(true);
        }
        definitionWindow.SetActive(false);
        noResultsFound.SetActive(false);
        skillInputField.SetActive(true);

        //alphabet
        char[] alphabetArray = alphabet.ToCharArray();
    }

    void Update()
    {   
        //TODO: THERE IS AN ERROR WITH THE SEARCH FUNCTION, IF YOU SEARCH SKILLS WITH MORE THAN ONE WORD, IT DOESN'T APPEAR
        //TODO: MUST FIX THIS!!

        // if the skill input text equals the skill name text on each skill
        if (skillInput.text == "")
        {
            for (int i = 0; i < skillsList.Length; i++)
            {
                skillsList[i].SetActive(true);
            }
            continueButton.SetActive(false);
            noResultsFound.SetActive(false);
            addSkillField.SetActive(false);
        }

        foreach (char character in alphabet)
        {
            if (skillInput.text.Contains(character))
            {
                for (int i = 0; i < skillsList.Length; i++)
                {
                    skillsList[i].SetActive(false);
                }
                break;
            } else if (!skillInput.text.Contains(character))
            {
                noResultsFound.SetActive(true);
            }
        }

        foreach (char character in alphabet)
        {
            if (addSkillInputField.text.Contains(character))
            {
                continueButton.SetActive(true);
                break;
            }
        }

        //Search function, if the input field contain the name or letters preferably of a word already then it will stay
        for (int i = 0; i < skillsList.Length; i++)
        {
            //skillsList[i].SetActive(true);

            if (skillInput.text.Contains(skillsList[i].name))
            {
                skillsList[i].SetActive(true);
                noResultsFound.SetActive(false);
            }
        }
    }

    public void AddNewCustomSkill()
    {
        addSkillField.SetActive(true);
        addSkillInputField.text = "";
    }

    public void ResetSkillFields()
    {
        addSkillInputField.text = "";
        skillInput.text = "";
    }

    public void TransferSkillData()
    {
        //Remember to add the _addNewSkillData.Name, we must specify the name of the newly Skill due to separate scripts
        /*Debug.Log("Custom input field: " + addSkillInputField.text);*/

        //skillInput.text = addSkillInputField.text;
        Debug.Log(addSkillInputField.text);
        skillInput.text = addSkillInputField.text;

        //dynamicInterfaceManager._addNewSkillData.Name = skillInput.text;

        /*Debug.Log(skillInput.text);*/
        /*addSkillInputField.text = "";
        skillInput.text = "";*/
        /*Debug.Log(skillInput.text);
        Debug.Log("Test: " + _addNewSkillData.Name);*/
    }

    /*public Transform CreateSkillButton(string content, string description, int level, UnityAction _event, bool overrideCheck = true)
    {
        Transform ButtonHolder = CreatePrefab(EditButtonPrefab);
        ButtonHolder.GetComponent<HorizontalLayoutGroup>().padding.bottom = 0;
        SkillLevelSelectPrefab.GetComponentInChildren<TMP_Text>().text = content;
        SkillLevelSelectPrefab.GetComponentsInChildren<TMP_Text>()[1].text = description;
        for (int i = 0; i < 4; i++)
        {
            //Debug.Log(SkillLevelSelectPrefab.GetComponentInChildren<GridLayoutGroup>().name);
            if (i + 1 < level)
            {
                SkillLevelSelectPrefab.GetComponentInChildren<GridLayoutGroup>().transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                SkillLevelSelectPrefab.GetComponentInChildren<GridLayoutGroup>().transform.GetChild(i).gameObject.SetActive(false);
            }
        }


        Transform _buttonCreated = CreatePrefab(SkillLevelSelectPrefab, ButtonHolder);
        _buttonCreated.GetComponent<Button>().onClick.AddListener(_event);
        if (overrideCheck)
            ContinueButton = _buttonCreated.GetComponent<Button>();
        return _buttonCreated;
    }*/

    /*
     *  TODO FOR TODAY: ADDING REST OF SKILL LIST DETAILS, ADD NEW SKILL (HAVE IT ALWAYS DISPLAYING SEPARATELY FROM THE SKILL LIST), MAKE COPY OF DEFINITION WINDOW WITH INPUTFIELD
     */

    public void ShowSkillDefinition(int index)
    {
        switch (index)
        {
            //62 in total
            case 1:
                skillName.text = "Ability to accept Criticism";
                skillDefinition.text = "No skill definition available at this time.";

                break;
            case 2:
                skillName.text = "Active Listening to Others";
                skillDefinition.text = "Listening with all senses, focusing on the verbal and non-verbal communication of the person who is speaking, and demonstrating that you are engaged with the conversation. It is the process of listening attentively while someone else speaks without inturrupting, and then paraphrasing and reflecting back what is said, and withholding judgment and advice.";
                break;
            case 3:
                skillName.text = "Adaptability";
                skillDefinition.text = "Ability to accommodate changing environmental conditions.";
                break;
            case 4:
                skillName.text = "Analytical Thinking";
                skillDefinition.text = "Breaking a complex problem down into its individual parts and analyzing interrelationships; analytical thinking also includes critical thinking, logical thinking, and creativity.";
                break;
            case 5:
                skillName.text = "Applied Knowledge";
                skillDefinition.text = "Ability to translate knowledge constructed in one context (often a research context) to another contexts to solve a problem or improve practice. It implies practical use of knowledge.";
                break;
            case 6:
                skillName.text = "Approachableness";
                skillDefinition.text = "Ability to engage involving or encouraging friendliness or pleasant companionship with other people.";
                break;
            case 7:
                skillName.text = "Argumentation/ Discussion Skills";
                skillDefinition.text = "The ability to to interact and respond to what other people are saying. Talking with confidence and letting others speak without inturrpution or talking over them. Building on what others say, by critical thinking and asking thoughtful questions.";
                break;
            case 8:
                skillName.text = "Attention to Detail";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 9:
                skillName.text = "Authenticity";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 10:
                skillName.text = "Business and Commercial Awareness";
                skillDefinition.text = "Knowledge of the resources, strengths which the company needs to demonstrate in order to meet current challenges, being profitable and serve its customers.";
                break;
            case 11:
                skillName.text = "Career Management";
                skillDefinition.text = "The process of planning and managing your career development.";
                break;
            case 12:
                skillName.text = "Communicativeness";
                skillDefinition.text = "Ability to conduct effective and efficient communication which includes getting your message across clearly and managing challenging conversations with positive outcomes.";
                break;
            case 13:
                skillName.text = "Confidence";
                skillDefinition.text = "Ability to trust in your own judgment, capacities and abilities, not with arrogance but in a realistic and secure way.";
                break;
            case 14:
                skillName.text = "Confidentiality";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 15:
                skillName.text = "Conflict Resolution";
                skillDefinition.text = "Ability to remain calm, non-defensive and respectful during conflict. It involves having the capacity to empathize with other’s viewpoint, and having the ability to seek compromise and collaboration.";
                break;
            case 16:
                skillName.text = "Creative/ Innovative Thinking";
                skillDefinition.text = "Ability to generate new ideas how to solve the problem.";
                break;
            case 17:
                skillName.text = "Critical Thinking";
                skillDefinition.text = "Realistic and reliable thinking to understand a problem clearly.";
                break;
            case 18:
                skillName.text = "Customer Service";
                skillDefinition.text = "Ability to provide support to both prospective and existing customers with Professionalism, Patience and ‘People First’ attitude.";
                break;
            case 19:
                skillName.text = "Decision Making";
                skillDefinition.text = "Collecting and analysing information to decide on an appropriate course of action.";
                break;
            case 20:
                skillName.text = "Digital Literacy";
                skillDefinition.text = "The use of digital technologies to navigate specific tasks or comunitating from different digital environment.";
                break;
            case 21:
                skillName.text = "Diplomacy";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 22:
                skillName.text = "Discipline";
                skillDefinition.text = "The way of effective fulfilling tasks, where person is able to control themselves to act in accordance with expectations, sticks to the plan, is resistant to situational and environmental factors and is able to complete the task of action.";
                break;
            case 23:
                skillName.text = "Flexibility";
                skillDefinition.text = "Easy and hassle free adjusting to changes in plans, tasks at work.";
                break;
            case 24:
                skillName.text = "Good Judgement";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 25:
                skillName.text = "Growth Orientation";
                skillDefinition.text = "A disposition of self-improvement by setting new and challenging goals for oneself. It is associated with a growth mindset, which means believing that one’s skills can improve over time thanks to hard work.";
                break;
            case 26:
                skillName.text = "Independence at Work";
                skillDefinition.text = "Being self-sufficient in accomplishing the tasks assigned by the supervisor.";
                break;
            case 27:
                skillName.text = "Initiative";
                skillDefinition.text = "Independent setting of tasks and goals at work, doing things without being asked, solving not obvious problems and undertaking own actions and activities.";
                break;
            case 28:
                skillName.text = "Leadership";
                skillDefinition.text = "The ability to motivate a group of people to act toward achieving a common goal. It involves setting direction, building an inspiring vision, and creating something new. Leadership is about mapping out where you need to go to succeed as a team or an organization; and it is dynamic, exciting, and inspiring.";
                break;
            case 29:
                skillName.text = "Listening";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 30:
                skillName.text = "Logical Reasoning";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 31:
                skillName.text = "Multitasking";
                skillDefinition.text = "Easily switching attention and focus between task which makes an impression of working on a multiple tasks in the same time and leads to their effective fulfillments simultaneously.";
                break;
            case 32:
                skillName.text = "Patience";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 33:
                skillName.text = "Persistance";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 34:
                skillName.text = "Planning";
                skillDefinition.text = "The process of preparing the list of tasks and making decisions about the number and order of actions, their timeline, and ways of realization to achieve a proper and correct fulfillment of project or complex tasks.";
                break;
            case 35:
                skillName.text = "Presentation Skills";
                skillDefinition.text = "Presenting information about yourself, your work and your viewpoint clearly and effectively.";
                break;
            case 36:
                skillName.text = "Prioritising";
                skillDefinition.text = "Determining the order of execution of tasks, the amount of time devoted to them based on their importance, urgency as well as time and personnel resources.";
                break;
            case 37:
                skillName.text = "Problem Solving";
                skillDefinition.text = "Ability to plan action to get rid of obstacles to completing tasks.";
                break;
            case 38:
                skillName.text = "Project Management";
                skillDefinition.text = "Project management is primarily: planning, management and control skills.";
                break;
            case 39:
                skillName.text = "Propriety/ Personal Culture";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 40:
                skillName.text = "Providing Feedback";
                skillDefinition.text = "The ability to give effective feedback that is timely, specific, realistic and balanced while being supportive.";
                break;
            case 41:
                skillName.text = "Punctuality";
                skillDefinition.text = "Punctuality means being on time: both to work and on deadlines.";
                break;
            case 42:
                skillName.text = "Reading";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 43:
                skillName.text = "Research Skills";
                skillDefinition.text = "Finding, evaluating and interpreting relevant information in reference to analyzed topic with aim to find solutions to a problem, increase effectiveness and develop.";
                break;
            case 44:
                skillName.text = "Responsibility/ Commitment";
                skillDefinition.text = "Responsibility can be defined as a high level of commitment to one’s duties. Being responsible means taking accountability for one’s actions, words, and performance at work.";
                break;
            case 45:
                skillName.text = "Self-Awareness";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 46:
                skillName.text = "Self-Motivation";
                skillDefinition.text = "The internal state that helps you initiate and continue a goal-oriented activity, despite obstacles, until it is completed.";
                break;
            case 47:
                skillName.text = "Self-Presentation";
                skillDefinition.text = "Behavior with which people try to affect how they are perceived and judged by others; much social behavior is influenced by self-presentational motives and goals (Miller & Rowland, 2019).";
                break;
            case 48:
                skillName.text = "Self-Reflection";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 49:
                skillName.text = "Sense of Humor";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 50:
                skillName.text = "Social Media Management";
                skillDefinition.text = "Abilities you use to help you create effective social media strategies and campaigns. They help you complete your daily tasks as a social media manager and come up with effective social media marketing for your brand.";
                break;
            case 51:
                skillName.text = "Speaking";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 52:
                skillName.text = "Speaking Fluency";
                skillDefinition.text = "Ability to speak easily, reasonably quickly and without having to stop and pause a lot in your language of communication.";
                break;
            case 53:
                skillName.text = "Specialistic Industry Skills";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 54:
                skillName.text = "Stamina";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 55:
                skillName.text = "Statistical Skills";
                skillDefinition.text = "The collection, organization, analysis, and interpretation of numerical data. They are a combination of other skills, such as math, computer literacy, data analysis, and critical thinking.";
                break;
            case 56:
                skillName.text = "Stress Management";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 57:
                skillName.text = "Teamwork";
                skillDefinition.text = "The collaborative effort of a group to achieve a common goal or to complete a task in the most effective and efficient way.";
                break;
            case 58:
                skillName.text = "Time Management";
                skillDefinition.text = "Refers to the planning, prioritizing, and scheduling of tasks to create work efficiency in an environment of competing demands.";
                break;
            case 59:
                skillName.text = "Work Ethic";
                skillDefinition.text = "A term used to describe a person’s dedication in relation to their job. Especially a set of standards of behavior and beliefs regarding what is and isn't acceptable to do at work.";
                break;
            case 60:
                skillName.text = "Work Under Pressure";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 61:
                skillName.text = "Writing";
                skillDefinition.text = "No skill definition available at this time.";
                break;
            case 62:
                skillName.text = "Writing Skills";
                skillDefinition.text = "Having knowledge of writing structure and ability to effectively communicate your ideas through writing with your target audience in mind (e.g. informal, formal and technical writing).";
                break;
            default:
                skillName.text = "Ability to accept Criticism";
                skillDefinition.text = "No skill definition available at this time.";
                break;
        }

        definitionWindow.SetActive(true);
        for (int i = 0; i < bottomBannerButtons.Length; i++)
        {
            bottomBannerButtons[i].interactable = false;
        }
    }

    public void HideSkillDefinition()
    {
        definitionWindow.SetActive(false);
        for (int i = 0; i < bottomBannerButtons.Length; i++)
        {
            bottomBannerButtons[i].interactable = true;
        }
    }

    public void PassSkillNameExt()
    {
        skillInput.text = skillName.text;
        //dynamicInterfaceManager._addNewSkillData.Name = skillInput.text;
        for (int i = 0; i < skillsList.Length; i++)
        {
            skillsList[i].SetActive(false);
        }
        skillsList[0].SetActive(true);
        noResultsFound.SetActive(false);
        continueButton.SetActive(true);
    }

    public void PassingSkillValidation()
    {
        if (addSkillField.activeInHierarchy == true)
        {
            dynamicInterfaceManager._addNewSkillData.Name = addSkillInputField.text;
        }
        else
        {
            dynamicInterfaceManager._addNewSkillData.Name = skillInput.text;
        }
    }

    // This function will pass as when the user click the actual button meaning it will select that certain skill for their choosing
    //TODO: Add in the functionality to progress to the next screen for selecting the skill level.
    public void PassSkillName(int index)
    {
        switch (index)
        {
            //62 in total
            case 0:
                skillName.text = "Ability to accept Criticism";
                PassSkillNameExt();
                break;
            case 1:
                skillName.text = "Active Listening to Others";
                PassSkillNameExt();
                break;
            case 2:
                skillName.text = "Adaptability";
                PassSkillNameExt();
                break;
            case 3:
                skillName.text = "Analytical Thinking";
                PassSkillNameExt();
                break;
            case 4:
                skillName.text = "Applied Knowledge";
                PassSkillNameExt();
                break;
            case 5:
                skillName.text = "Approachableness";
                PassSkillNameExt();
                break;
            case 6:
                skillName.text = "Argumentation/ Discussion Skills";
                PassSkillNameExt();
                break;
            case 7:
                skillName.text = "Attention to Detail";
                PassSkillNameExt();
                break;
            case 8:
                skillName.text = "Authenticity";
                PassSkillNameExt();
                break;
            case 9:
                skillName.text = "Business and Commercial Awareness";
                PassSkillNameExt();
                break;
            case 10:
                skillName.text = "Career Management";
                PassSkillNameExt();
                break;
            case 11:
                skillName.text = "Communicativeness";
                PassSkillNameExt();
                break;
            case 12:
                skillName.text = "Confidence";
                PassSkillNameExt();
                break;
            case 13:
                skillName.text = "Confidentiality";
                PassSkillNameExt();
                break;
            case 14:
                skillName.text = "Conflict Resolution";
                PassSkillNameExt();
                break;
            case 15:
                skillName.text = "Creative/ Innovative Thinking";
                PassSkillNameExt();
                break;
            case 16:
                skillName.text = "Critical Thinking";
                PassSkillNameExt();
                break;
            case 17:
                skillName.text = "Customer Service";
                PassSkillNameExt();
                break;
            case 18:
                skillName.text = "Decision Making";
                PassSkillNameExt();
                break;
            case 19:
                skillName.text = "Digital Literacy";
                PassSkillNameExt();
                break;
            case 20:
                skillName.text = "Diplomacy";
                PassSkillNameExt();
                break;
            case 21:
                skillName.text = "Discipline";
                PassSkillNameExt();
                break;
            case 22:
                skillName.text = "Flexibility";
                PassSkillNameExt();
                break;
            case 23:
                skillName.text = "Good Judgement";
                PassSkillNameExt();
                break;
            case 24:
                skillName.text = "Growth Orientation";
                PassSkillNameExt();
                break;
            case 25:
                skillName.text = "Independence at Work";
                PassSkillNameExt();
                break;
            case 26:
                skillName.text = "Initiative";
                PassSkillNameExt();
                break;
            case 27:
                skillName.text = "Leadership";
                PassSkillNameExt();
                break;
            case 28:
                skillName.text = "Listening";
                PassSkillNameExt();
                break;
            case 29:
                skillName.text = "Logical Reasoning";
                PassSkillNameExt();
                break;
            case 30:
                skillName.text = "Multitasking";
                PassSkillNameExt();
                break;
            case 31:
                skillName.text = "Patience";
                PassSkillNameExt();
                break;
            case 32:
                skillName.text = "Persistance";
                PassSkillNameExt();
                break;
            case 33:
                skillName.text = "Planning";
                PassSkillNameExt();
                break;
            case 34:
                skillName.text = "Presentation Skills";
                PassSkillNameExt();
                break;
            case 35:
                skillName.text = "Prioritising";
                PassSkillNameExt();
                break;
            case 36:
                skillName.text = "Problem Solving";
                PassSkillNameExt();
                break;
            case 37:
                skillName.text = "Project Management";
                PassSkillNameExt();
                break;
            case 38:
                skillName.text = "Propriety/ Personal Culture";
                PassSkillNameExt();
                break;
            case 39:
                skillName.text = "Providing Feedback";
                PassSkillNameExt();
                break;
            case 40:
                skillName.text = "Punctuality";
                PassSkillNameExt();
                break;
            case 41:
                skillName.text = "Reading";
                PassSkillNameExt();
                break;
            case 42:
                skillName.text = "Research Skills";
                PassSkillNameExt();
                break;
            case 43:
                skillName.text = "Responsibility/ Commitment";
                PassSkillNameExt();
                break;
            case 44:
                skillName.text = "Self-Awareness";
                PassSkillNameExt();
                break;
            case 45:
                skillName.text = "Self-Motivation";
                PassSkillNameExt();
                break;
            case 46:
                skillName.text = "Self-Presentation";
                PassSkillNameExt();
                break;
            case 47:
                skillName.text = "Self-Reflection";
                PassSkillNameExt();
                break;
            case 48:
                skillName.text = "Sense of Humor";
                PassSkillNameExt();
                break;
            case 49:
                skillName.text = "Social Media Management";
                PassSkillNameExt();
                break;
            case 50:
                skillName.text = "Speaking";
                PassSkillNameExt();
                break;
            case 51:
                skillName.text = "Speaking Fluency";
                PassSkillNameExt();
                break;
            case 52:
                skillName.text = "Specialistic Industry Skills";
                PassSkillNameExt();
                break;
            case 53:
                skillName.text = "Stamina";
                PassSkillNameExt();
                break;
            case 54:
                skillName.text = "Statistical Skills";
                PassSkillNameExt();
                break;
            case 55:
                skillName.text = "Stress Management";
                PassSkillNameExt();
                break;
            case 56:
                skillName.text = "Teamwork";
                PassSkillNameExt();
                break;
            case 57:
                skillName.text = "Time Management";
                PassSkillNameExt();
                break;
            case 58:
                skillName.text = "Work Ethic";
                PassSkillNameExt();
                break;
            case 59:
                skillName.text = "Work Under Pressure";
                PassSkillNameExt();
                break;
            case 60:
                skillName.text = "Writing";
                PassSkillNameExt();
                break;
            case 61:
                skillName.text = "Writing Skills";
                PassSkillNameExt();
                break;
            default:
                skillName.text = "Ability to accept Criticism";
                PassSkillNameExt();
                break;
        }
    }
}
