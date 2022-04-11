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
    }

    public void TransferSkillData()
    {
        //Remmeber to add the _addNewSkillData.Name, we must specify the name of the newly Skill due to separate scripts
        dynamicInterfaceManager._addNewSkillData.Name = addSkillInputField.text;
        skillInput.text = addSkillInputField.text;
        addSkillInputField.text = "";
        skillInput.text = "";
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
                skillDefinition.text = "Ability to accept Criticism definition will go here.";

                break;
            case 2:
                skillName.text = "Active Listening to Others";
                skillDefinition.text = "Active Listening to Others definition will go here.";
                break;
            case 3:
                skillName.text = "Adaptability";
                skillDefinition.text = "Adaptability definition will go here.";
                break;
            case 4:
                skillName.text = "Analytical Thinking";
                skillDefinition.text = "Analytical Thinking definition will go here.";
                break;
            case 5:
                skillName.text = "Applied Knowledge";
                skillDefinition.text = "Applied Knowledge definition will go here.";
                break;
            case 6:
                skillName.text = "Approachableness";
                skillDefinition.text = "Approachableness definition will go here.";
                break;
            case 7:
                skillName.text = "Argumentation/ Discussion Skills";
                skillDefinition.text = "Definition will go here.";
                break;
            case 8:
                skillName.text = "Attention to Detail";
                skillDefinition.text = "Definition will go here.";
                break;
            case 9:
                skillName.text = "Authenticity";
                skillDefinition.text = "Definition will go here.";
                break;
            case 10:
                skillName.text = "Business and Commercial Awareness";
                skillDefinition.text = "Definition will go here.";
                break;
            case 11:
                skillName.text = "Career Management";
                skillDefinition.text = "Definition will go here.";
                break;
            case 12:
                skillName.text = "Communicativeness";
                skillDefinition.text = "Definition will go here.";
                break;
            case 13:
                skillName.text = "Confidence";
                skillDefinition.text = "Definition will go here.";
                break;
            case 14:
                skillName.text = "Confidentiality";
                skillDefinition.text = "Definition will go here.";
                break;
            case 15:
                skillName.text = "Conflict Resolution";
                skillDefinition.text = "Definition will go here.";
                break;
            case 16:
                skillName.text = "Creative/ Innovative Thinking";
                skillDefinition.text = "Definition will go here.";
                break;
            case 17:
                skillName.text = "Critical Thinking";
                skillDefinition.text = "Definition will go here.";
                break;
            case 18:
                skillName.text = "Customer Service";
                skillDefinition.text = "Definition will go here.";
                break;
            case 19:
                skillName.text = "Decision Making";
                skillDefinition.text = "Definition will go here.";
                break;
            case 20:
                skillName.text = "Digital Literacy";
                skillDefinition.text = "Definition will go here.";
                break;
            case 21:
                skillName.text = "Diplomacy";
                skillDefinition.text = "Definition will go here.";
                break;
            case 22:
                skillName.text = "Discipline";
                skillDefinition.text = "Definition will go here.";
                break;
            case 23:
                skillName.text = "Flexibility";
                skillDefinition.text = "Definition will go here.";
                break;
            case 24:
                skillName.text = "Good Judgement";
                skillDefinition.text = "Definition will go here.";
                break;
            case 25:
                skillName.text = "Growth Orientation";
                skillDefinition.text = "Definition will go here.";
                break;
            case 26:
                skillName.text = "Independence at Work";
                skillDefinition.text = "Definition will go here.";
                break;
            case 27:
                skillName.text = "Initiative";
                skillDefinition.text = "Definition will go here.";
                break;
            case 28:
                skillName.text = "Leadership";
                skillDefinition.text = "Definition will go here.";
                break;
            case 29:
                skillName.text = "Listening";
                skillDefinition.text = "Definition will go here.";
                break;
            case 30:
                skillName.text = "Logical Reasoning";
                skillDefinition.text = "Definition will go here.";
                break;
            case 31:
                skillName.text = "Multitasking";
                skillDefinition.text = "Definition will go here.";
                break;
            case 32:
                skillName.text = "Patience";
                skillDefinition.text = "Definition will go here.";
                break;
            case 33:
                skillName.text = "Persistance";
                skillDefinition.text = "Definition will go here.";
                break;
            case 34:
                skillName.text = "Planning";
                skillDefinition.text = "Definition will go here.";
                break;
            case 35:
                skillName.text = "Presentation Skills";
                skillDefinition.text = "Definition will go here.";
                break;
            case 36:
                skillName.text = "Prioritising";
                skillDefinition.text = "Definition will go here.";
                break;
            case 37:
                skillName.text = "Problem Solving";
                skillDefinition.text = "Definition will go here.";
                break;
            case 38:
                skillName.text = "Project Management";
                skillDefinition.text = "Definition will go here.";
                break;
            case 39:
                skillName.text = "Propriety/ Personal Culture";
                skillDefinition.text = "Definition will go here.";
                break;
            case 40:
                skillName.text = "Providing Feedback";
                skillDefinition.text = "Definition will go here.";
                break;
            case 41:
                skillName.text = "Punctuality";
                skillDefinition.text = "Definition will go here.";
                break;
            case 42:
                skillName.text = "Reading";
                skillDefinition.text = "Definition will go here.";
                break;
            case 43:
                skillName.text = "Research Skills";
                skillDefinition.text = "Definition will go here.";
                break;
            case 44:
                skillName.text = "Responsibility/ Commitment";
                skillDefinition.text = "Definition will go here.";
                break;
            case 45:
                skillName.text = "Self-Awareness";
                skillDefinition.text = "Definition will go here.";
                break;
            case 46:
                skillName.text = "Self-Motivation";
                skillDefinition.text = "Definition will go here.";
                break;
            case 47:
                skillName.text = "Self-Presentation";
                skillDefinition.text = "Definition will go here.";
                break;
            case 48:
                skillName.text = "Self-Reflection";
                skillDefinition.text = "Definition will go here.";
                break;
            case 49:
                skillName.text = "Sense of Humor";
                skillDefinition.text = "Definition will go here.";
                break;
            case 50:
                skillName.text = "Social Media Management";
                skillDefinition.text = "Definition will go here.";
                break;
            case 51:
                skillName.text = "Speaking";
                skillDefinition.text = "Definition will go here.";
                break;
            case 52:
                skillName.text = "Speaking Fluency";
                skillDefinition.text = "Definition will go here.";
                break;
            case 53:
                skillName.text = "Specialistic Industry Skills";
                skillDefinition.text = "Definition will go here.";
                break;
            case 54:
                skillName.text = "Stamina";
                skillDefinition.text = "Definition will go here.";
                break;
            case 55:
                skillName.text = "Statistical Skills";
                skillDefinition.text = "Definition will go here.";
                break;
            case 56:
                skillName.text = "Stress Management";
                skillDefinition.text = "Definition will go here.";
                break;
            case 57:
                skillName.text = "Teamwork";
                skillDefinition.text = "Definition will go here.";
                break;
            case 58:
                skillName.text = "Time Management";
                skillDefinition.text = "Definition will go here.";
                break;
            case 59:
                skillName.text = "Work Ethic";
                skillDefinition.text = "Definition will go here.";
                break;
            case 60:
                skillName.text = "Work Under Pressure";
                skillDefinition.text = "Definition will go here.";
                break;
            case 61:
                skillName.text = "Writing";
                skillDefinition.text = "Definition will go here.";
                break;
            case 62:
                skillName.text = "Writing Skills";
                skillDefinition.text = "Definition will go here.";
                break;
            default:
                skillName.text = "Ability to accept Criticism";
                skillDefinition.text = "Definition will go here.";
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
        for (int i = 0; i < skillsList.Length; i++)
        {
            skillsList[i].SetActive(false);
        }
        skillsList[0].SetActive(true);
        noResultsFound.SetActive(false);
        continueButton.SetActive(true);
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
