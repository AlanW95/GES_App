using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DynamicInterfaceAreaUI : MonoBehaviour
{
    public Transform _ScrollRectHolder;
    public GameObject HeaderTextHolder;
    public GameObject EditSingleDetailPrefab;
    public GameObject EditLongContentTextBoxPrefab;
    public GameObject EditToggleListGroupPrefab;
    public GameObject EditToggleItemPrefab;
    public GameObject EditDatePrefab;
    public GameObject EditButtonPrefab;
    public GameObject ButtonPrefab;
    public GameObject SkillLevelSelectPrefab;
    public GameObject SpaceFiller;

    public GameObject DisplayContentTextPrefabGroup;
    public GameObject DisplayContentTextPrefabItem;

    public GameObject SkillDisplayContent;

    public GameObject DisplayLineTextPrefabGroup;
    public GameObject DisplayLineTextPrefabItem;

    public List<GameObject> CreatedScreenGameObjects = new List<GameObject>();
    public List<GameObject> SubElementGameObjects = new List<GameObject>();
    public Button ContinueButton;

    public ExperienceData _addNewExperienceData;
    public ArtifactData _addNewArtifactData;
    public ReferenceData _addNewReferenceData;
    public SkillData _addNewSkillData;


    public GameObject SkillReferenceProjectTitle;
    public GameObject SkillReferenceProjectContent;
    public GameObject SkillExperienceTitle;
    public GameObject SkillExperienceContent;
    public GameObject SkillItemExtension;

    private int parentIndexSize = 0;
    private int currentInactiveChildObjects = 0;
    private float boundrySize = 25f;

    public AccountManager accountManager;
    private UserInterfaceManagerUI userInterfaceManager;

    [SerializeField]
    private TMP_InputField selectedSkill;

    private void Awake()
    {
        accountManager = FindObjectOfType<AccountManager>();
        userInterfaceManager = FindObjectOfType<UserInterfaceManagerUI>();
        currentInactiveChildObjects = _ScrollRectHolder.transform.childCount;

    }

    // Start is called before the first frame update
    void Start()
    {
        //AddNewReference(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (ContinueButton != null)
        {
            bool enableButton = true;
            for (int i = 0; i < CreatedScreenGameObjects.Count; i++)
            {
                if (CreatedScreenGameObjects[i] != null && CreatedScreenGameObjects[i].GetComponent<ContentDataIdentiferUI>() != null && CreatedScreenGameObjects[i].GetComponent<ContentDataIdentiferUI>().getCanContinue() == false)
                {
                    enableButton = false;
                    //Debug.Log(CreatedScreenGameObjects[i].name);
                }
            }
            ContinueButton.interactable = enableButton;
        }
    }

    public void Configure_Top_Banner(bool _disable = false, bool _throwWarning = false, string title = "", UnityAction _backButtonAction = null, Sprite _UI_Image = null, UnityAction _secondaryButtonAction = null)
    {
        if (userInterfaceManager == null)
            userInterfaceManager = FindObjectOfType<UserInterfaceManagerUI>();

        userInterfaceManager.Configure_Top_Banner(_disable, _throwWarning, title, _backButtonAction, _UI_Image, _secondaryButtonAction);
    }

    public void GetSkillInfo()
    {

    }

    public void AddNewSkill(int pageNumber)
    {
        DestroyCurrentScreens();
        int totalPages = 4; //originally int totalPages = 3;
        /* 5 PAGES IN TOTAL
         * 
         * 1. Add Skill page
         * 2. Share skill with Skill Repository (send to database)
         * 3. Level of the Skill
         * 4. Share skill level with the Skill Repository (send to database)
         * 5. Skill Summary (could be removed)
         *
         */

        //if Page 1 - NEW Skill Page - so we have to refer to the new skill page created where the user can delete a skill.
        //page number 1 is only needed for the top banner, as a custom gameobject is being used for this with the list of skills.
        if (pageNumber == 1)
        {
            Configure_Top_Banner(false, true, "Add Skill", delegate { userInterfaceManager.ChangeWindow(userInterfaceManager.HomeScreen); }, null, null);
        }
        else
        {
            Configure_Top_Banner(false, false, "Add Skill", delegate { AddNewSkill(pageNumber - 1); }, null, null);
        }

        if (pageNumber == 1)
        {
            /*if (_addNewSkillData == null)
            {
                _addNewSkillData = new SkillData();
            }
            //selec.text = _addNewSkillData.Name;
            string _skillName = selectedSkill.text;

            CaptureStringData(ref _addNewSkillData.Name, _skillName.ToString());

            ContentDataIdentiferUI _skillName = selectedSkill.*/

            //new skills page
            //userInterfaceManager.Open_AddSkills();

            /*if (_addNewSkillData == null)*/
            /*if (_addNewSkillData == null)
            {
                _addNewSkillData = new SkillData();
            }*/

            //CreateHeaderText("Add new Skill", pageNumber + "/" + totalPages, "Enter skill information:");
            /*ContentDataIdentiferUI _skillName = CreateEditInformationContent("Name", _addNewSkillData.Name, TMP_InputField.ContentType.Name);*/
            /*StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));*/
            /*CreateButton("Continue", delegate
            {
                AddNewSkill(pageNumber + 1);
            });*/
        }
        //Page 2 - 
        //TODO: CHANGE TO PAGE 3: Level of Skill because we need to add a new screen in for the confirmation of sharing with the crowd repository.
        else if (pageNumber == 2)
        {
            Configure_Top_Banner(false, false, "Share Skill", delegate { AddNewSkill(pageNumber - 1); }, null, null);
            CreateHeaderText(null, pageNumber + "/" + totalPages, selectedSkill.text);
            CreateDisplayGroup("Would you like to share this skill to the crowdsourced repository? This will allow other users to find and add it to their own skill portfolio.");

            

            CreateButton("Yes, I would love to contribute!",
            delegate
            {
                
                AddNewSkill(pageNumber + 1);
                //TODO: Send skill to Firebase Realtime Database
            }); 
            CreateButton("No thank you!",
             delegate
             {
                 CaptureStringData(ref _addNewSkillData.Name, _skillName);
                 AddNewSkill(pageNumber + 1);
             });
        }
        else if (pageNumber == 3)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "Select skill level:");
            CreateDisplayGroup("<b>" + selectedSkill.text + "</b>");
            /*Transform _holder = CreateDisplayGroup(_addNewSkillData.Name).parent;*/
            /*CreateDisplayGroup(_addNewSkillData.LevelName, _holder);*/
            CreateSkillButton("Novice", "Little or no knowledge or experience.", 1, delegate { AddNewSkill(pageNumber + 1); });
            CreateSkillButton("Advanced Beginner", "Basic knowledge or experience.", 2, delegate { AddNewSkill(pageNumber + 1); });
            CreateSkillButton("Competent", "Intermediate knowledge or experience.", 3, delegate { AddNewSkill(pageNumber + 1); });
            CreateSkillButton("Proficient", "Broad knowledge or experience.", 4, delegate { AddNewSkill(pageNumber + 1); });
            CreateSkillButton("Expert", "Extensive knowledge or experience.", 5, delegate { AddNewSkill(pageNumber + 1); });
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Not sure?",
            delegate
            {
                SaveSkill();
                _addNewSkillData = null;
                AddNewSkill(pageNumber + 1);
                //TODO: DO NOT send skill to Firebase Realtime Database
            });

            /* CreateHeaderText("Skill Summary", pageNumber + "/" + totalPages, "Please check all information before proceeding.");
            CreateDisplayGroup("<b>Skill</b>");
            Transform _holder = CreateDisplayGroup(_addNewSkillData.Name).parent;
            CreateDisplayGroup(_addNewSkillData.LevelName, _holder);

            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Confirm",
              delegate
              {
                  SaveSkill();
                  _addNewSkillData = null;
                  userInterfaceManager.Open_Files();// (userInterfaceManager.FilesScreen);                   //Go to next page.
              }); */
        }
        else if (pageNumber == 4)
        {
            Configure_Top_Banner(false, false, "Share Acquisition", delegate { AddNewSkill(pageNumber - 1); }, null, null);
            CreateHeaderText(null, pageNumber + "/" + totalPages, selectedSkill.text);
            CreateDisplayGroup("Would you like to share this acquisition to the crowdsourced repository? This will allow the app to suggest new skills to you and others in similar situations based on your current skills and education.");
            CreateButton("Yes, I would love to contribute!",
            delegate
            {
                SaveSkill();
                _addNewSkillData = null;
                /*AddNewSkill(pageNumber + 1);*/
                userInterfaceManager.Open_Files();
                //TODO: Send skill to Firebase Realtime Database
            });
            CreateButton("No thank you!",
             delegate
             {
                 SaveSkill();
                 _addNewSkillData = null;
                 Debug.Log(_addNewSkillData);
                 userInterfaceManager.Open_Files();
                 /*AddNewSkill(pageNumber + 1);*/
             });
        }
        /*else if (pageNumber == 5)
        {
            CreateHeaderText("Skill Summary", pageNumber + "/" + totalPages, "Please check all information before proceeding.");
            CreateDisplayGroup("<b>Skill</b>");
            Transform _holder = CreateDisplayGroup(_addNewSkillData.Name).parent;
            CreateDisplayGroup(_addNewSkillData.LevelName, _holder);

            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Confirm",
              delegate
              {
                  SaveSkill();
                  _addNewSkillData = null;
                  userInterfaceManager.Open_Files();// (userInterfaceManager.FilesScreen);                   //Go to next page.
              });
        }*/
    }
 
    public void AddNewArtifact(int pageNumber)
    {
        DestroyCurrentScreens();
        int totalPages = 5;


        if (pageNumber == 1)
        {
            Configure_Top_Banner(false, true, "Add Artifact", delegate { userInterfaceManager.ChangeWindow(userInterfaceManager.HomeScreen); }, null, null);
        }
        else
        {
            Configure_Top_Banner(false, false, "Add Artifact", delegate { AddNewArtifact(pageNumber - 1); }, null, null);
        }


        if (pageNumber == 1)
        {
            if (_addNewArtifactData == null)
                _addNewArtifactData = new ArtifactData();

            CreateHeaderText("Add new artifact", pageNumber + "/" + totalPages, "content text here");
            CreateButton("Document", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Document; AddNewArtifact(pageNumber + 1); });
            CreateButton("Image", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Image; AddNewArtifact(pageNumber + 1); });
            CreateButton("Link", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Link; AddNewArtifact(pageNumber + 1); });
            CreateButton("Repository", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Repository; AddNewArtifact(pageNumber + 1); });
            CreateButton("Video", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Video; AddNewArtifact(pageNumber + 1); });
            CreateButton("Note", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Note; AddNewArtifact(pageNumber + 1); });
        }
        else if (pageNumber == 2)
        {
            CreateHeaderText("Add new " + _addNewArtifactData.type.ToString() + " artifact", pageNumber + "/" + totalPages, "[Text]");
            ContentDataIdentiferUI _title = CreateEditInformationContent("Title", _addNewArtifactData.Title, TMP_InputField.ContentType.Name);
            ContentDataIdentiferUI _description = CreateLongContextPrefab(_addNewArtifactData.Description, TMP_InputField.ContentType.Standard, "Enter description...");
            ContentDataIdentiferUI _url = CreateEditInformationContent("URL", _addNewArtifactData.URL, TMP_InputField.ContentType.Standard, false);

            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate
            {
                CaptureStringData(ref _addNewArtifactData.Title, _title);
                CaptureStringData(ref _addNewArtifactData.Description, _description);
                CaptureStringData(ref _addNewArtifactData.URL, _url);
                AddNewArtifact(pageNumber + 1);
            });

        }
        else if (pageNumber == 3)
        {
            CreateHeaderText("Add new artifact", pageNumber + "/" + totalPages, "Add relevant skills:");
            ContentDataIdentiferUI _toggleHolder = CreateToggleItem(getDatabaseSkills(accountManager.localUserAccount._skills), _addNewArtifactData.Skills, false);

            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue",
            delegate
            {
                ContentDataIdentiferUI[] _listItems = _toggleHolder.GetComponentsInChildren<ContentDataIdentiferUI>();
                for (int i = 0; i < _listItems.Length; i++)
                {
                    if (_listItems[i]._toggleItem != null && _listItems[i]._toggleItem.isOn)
                        _addNewArtifactData.Skills.Add(_listItems[i]._ToggleItemName.text);
                }

                AddNewArtifact(pageNumber + 1);
            });
        }
        else if (pageNumber == 4)
        {
            CreateHeaderText("Add new artifact", pageNumber + "/" + totalPages, "Add relevant experiences:");
            ContentDataIdentiferUI _toggleHolder = CreateToggleItem(getDatabaseExperiences(accountManager.localUserAccount._experiences), _addNewArtifactData.Experiences, true);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue",
            delegate
            {
                ContentDataIdentiferUI[] _listItems = _toggleHolder.GetComponentsInChildren<ContentDataIdentiferUI>();
                for (int i = 0; i < _listItems.Length; i++)
                {
                    if (_listItems[i]._toggleItem != null && _listItems[i]._toggleItem.isOn)
                        _addNewArtifactData.Experiences.Add(_listItems[i]._ToggleItemName.text);
                }

                SaveArtifact();
                AddNewArtifact(pageNumber + 1);
                _addNewArtifactData = null;
            });
        }
        else if (pageNumber == 5)
        {
            CreateHeaderText("Artefact Summary", pageNumber + "/" + totalPages, "Please check all information before proceeding.");
            CreateDisplayGroup("<b>Artefact</b>");
            Transform _holder = CreateDisplayGroup(_addNewArtifactData.type.ToString()).parent;
            CreateDisplayGroup(_addNewArtifactData.Title, _holder);
            CreateDisplayGroup(_addNewArtifactData.Description, _holder);

            CreateDisplayGroup("<b>Skill(s) Associated</b>");
            _holder = null;
            for (int i = 0; i < _addNewArtifactData.Skills.Count; i++)
            {
                if (_holder == null)
                    _holder = CreateDisplayGroup(_addNewArtifactData.Skills[i]).parent;
                else
                    CreateDisplayGroup(_addNewArtifactData.Skills[i], _holder);
            }

            CreateDisplayGroup("<b>Experience(s) Associated</b>");
            _holder = null;
            for (int i = 0; i < _addNewArtifactData.Experiences.Count; i++)
            {
                if (_holder == null)
                    _holder = CreateDisplayGroup(_addNewArtifactData.Experiences[i]).parent;
                else
                    CreateDisplayGroup(_addNewArtifactData.Experiences[i], _holder);
            }

            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Confirm",
              delegate
              {
                  SaveArtifact();
                  _addNewArtifactData = null;
                  userInterfaceManager.Open_Files(); //Go to next page.
              });
        }
    }

    public void AddNewReference(int pageNumber)
    {
        DestroyCurrentScreens();
        int totalPages = 3;

        if (pageNumber == 1)
        {
            Configure_Top_Banner(false, true, "Add Reference", delegate { userInterfaceManager.ChangeWindow(userInterfaceManager.HomeScreen); }, null, null);
        }
        else
        {
            Configure_Top_Banner(false, false, "Add Reference", delegate { AddNewReference(pageNumber - 1); }, null, null);
        }

        if (pageNumber == 1)
        {
            if (_addNewReferenceData == null)
                _addNewReferenceData = new ReferenceData();

            CreateHeaderText("Add new reference", pageNumber + "/" + totalPages, "Contact Information");
            ContentDataIdentiferUI _name = CreateEditInformationContent("Name", _addNewReferenceData.Name, TMP_InputField.ContentType.Name);
            ContentDataIdentiferUI _position = CreateEditInformationContent("Position", _addNewReferenceData.Position, TMP_InputField.ContentType.Standard);
            ContentDataIdentiferUI _email = CreateEditInformationContent("Email", _addNewReferenceData.Email, TMP_InputField.ContentType.EmailAddress);
            //CreateEditInformationContent("Phone Number", TMP_InputField.ContentType.IntegerNumber, false);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateDisplayGroup("<b>By pressing continue you confirm that you have consent from the named reference to have their data processed and stored within this app and associated database(s).</b>");
            CreateButton("Continue", delegate
            {
                CaptureStringData(ref _addNewReferenceData.Name, _name);
                CaptureStringData(ref _addNewReferenceData.Position, _position);
                CaptureStringData(ref _addNewReferenceData.Email, _email);
                AddNewReference(pageNumber + 1);
            });
        }
        else if (pageNumber == 2)
        {
            CreateHeaderText("Add new reference", pageNumber + "/" + totalPages, "Skills that referee can vouch for:");
            ContentDataIdentiferUI _toggleHolder = CreateToggleItem(getDatabaseSkills(accountManager.localUserAccount._skills), _addNewReferenceData.Skills, false);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue",
            delegate
            {
                ContentDataIdentiferUI[] _listItems = _toggleHolder.GetComponentsInChildren<ContentDataIdentiferUI>();
                for (int i = 0; i < _listItems.Length; i++)
                {
                    if (_listItems[i]._toggleItem != null && _listItems[i]._toggleItem.isOn)
                        _addNewReferenceData.Skills.Add(_listItems[i]._ToggleItemName.text);
                }

                //SaveReference();
                AddNewReference(pageNumber + 1);
                //_addNewReferenceData = null;
            });
        }
        else if (pageNumber == 3)
        {
            CreateHeaderText("Reference Summary", pageNumber + "/" + totalPages, "Please check all information before proceeding.");
            CreateDisplayGroup("<b>Reference</b>");
            Transform _holder = CreateDisplayGroup(_addNewReferenceData.Name).parent;
            CreateDisplayGroup(_addNewReferenceData.Position, _holder);

            CreateDisplayGroup("<b>Contact Info</b>");
            _holder = CreateDisplayGroup(_addNewReferenceData.Email).parent;
            CreateDisplayGroup(_addNewReferenceData.PhoneNumber, _holder);

            CreateDisplayGroup("<b>Skills Associated</b>");
            _holder = null;
            for (int i = 0; i < _addNewReferenceData.Skills.Count; i++)
            {
                if (_holder == null)
                    _holder = CreateDisplayGroup(_addNewReferenceData.Skills[i]).parent;
                else
                    CreateDisplayGroup(_addNewReferenceData.Skills[i], _holder);
            }

            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Confirm",
              delegate
              {
                  SaveReference();
                  _addNewReferenceData = null;
                  userInterfaceManager.Open_Files(); //Go to next page.
              });
        }
    }

    public void AddNewExperiencePage(int pageNumber)
    {
        DestroyCurrentScreens();
        int totalPages = 7;

        if (pageNumber == 1)
        {
            Configure_Top_Banner(false, true, "Add Experience", delegate { userInterfaceManager.ChangeWindow(userInterfaceManager.HomeScreen); }, null, null);
        }
        else
        {
            Configure_Top_Banner(false, false, "Add Experience", delegate { AddNewExperiencePage(pageNumber - 1); }, null, null);
        }

        if (pageNumber == 1)
        {
            if (_addNewExperienceData == null)
                _addNewExperienceData = new ExperienceData();

            CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "Please enter a new experience.");
            ContentDataIdentiferUI _name = CreateEditInformationContent("Experience", _addNewExperienceData.ExperienceLocale, TMP_InputField.ContentType.Name);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { CaptureStringData(ref _addNewExperienceData.ExperienceLocale, _name); AddNewExperiencePage(pageNumber + 1); });
        }
        else if (pageNumber == 2)
        {
            CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "On what dates did this experience occur?");
            ContentDataIdentiferUI _startDate = CreateDatePrefab("Start Date", _addNewExperienceData.StartDate).GetComponent<ContentDataIdentiferUI>();
            ContentDataIdentiferUI _endDate = CreateDatePrefab("End Date", _addNewExperienceData.EndDate).GetComponent<ContentDataIdentiferUI>();
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { CaptureDate(ref _addNewExperienceData.StartDate, _startDate); CaptureDate(ref _addNewExperienceData.EndDate, _endDate); AddNewExperiencePage(pageNumber + 1); });

        } else if(pageNumber == 3)
        {

        
            CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "What skills does this experience support?");
            ContentDataIdentiferUI _toggleHolder = CreateToggleItem(getDatabaseSkills(accountManager.localUserAccount._skills), _addNewExperienceData.Skills, false);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue",
            delegate
            {
                ContentDataIdentiferUI[] _listItems = _toggleHolder.GetComponentsInChildren<ContentDataIdentiferUI>();
                for (int i = 0; i < _listItems.Length; i++)
                {
                    if (_listItems[i]._toggleItem != null && _listItems[i]._toggleItem.isOn)
                        _addNewExperienceData.Skills.Add(_listItems[i]._ToggleItemName.text);
                }

                AddNewExperiencePage(pageNumber + 1);
            });

        }
        else if (pageNumber == 4)
        {
            CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "Give a brief description of the experience.");
            ContentDataIdentiferUI _prefab = CreateLongContextPrefab(_addNewExperienceData.Description, TMP_InputField.ContentType.Standard);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { _addNewExperienceData.Description = _prefab._inputField.text; AddNewExperiencePage(pageNumber + 1); });
            //ContentDataIdentiferUI _toggleHolder = CreateToggleItem(getDatabaseExperiences(accountManager.localUserAccount._experiences), _addNewExperienceData.CourseOccured, true);
            //CreateButton("Continue",
            //delegate
            //{
            //    ContentDataIdentiferUI[] _listItems = _toggleHolder.GetComponentsInChildren<ContentDataIdentiferUI>();
            //    for (int i = 0; i < _listItems.Length; i++)
            //    {
            //        if (_listItems[i]._toggleItem != null && _listItems[i]._toggleItem.isOn)
            //            _addNewExperienceData.CourseOccured.Add(_listItems[i]._ToggleItemName.text);
            //    }

            //    AddNewExperiencePage(pageNumber + 1);
            //});

            //CreateButton("Continue", delegate { AddNewExperiencePage(pageNumber + 1); });
        }
        else if (pageNumber == 5)
        {
            CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "What was your primary role in this experience?");
            ContentDataIdentiferUI _prefab = CreateLongContextPrefab(_addNewExperienceData.RoleInExperience, TMP_InputField.ContentType.Standard);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { _addNewExperienceData.RoleInExperience = _prefab._inputField.text; AddNewExperiencePage(pageNumber + 1); });
        }
        else if (pageNumber == 6)
        {
            CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "Additional comments.");
            ContentDataIdentiferUI _prefab = CreateLongContextPrefab(_addNewExperienceData.Comments, TMP_InputField.ContentType.Standard);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { _addNewExperienceData.Comments = _prefab._inputField.text; AddNewExperiencePage(pageNumber + 1); });
        }
        //else if (pageNumber == 6)
        //{
        //    //CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "content text here");
        //    //CreateEditInformationContent("first name", _addNewExperienceData. TMP_InputField.ContentType.Name);
        //    //CreateEditInformationContent("last name", TMP_InputField.ContentType.Name);
        //    //CreateEditInformationContent("email", TMP_InputField.ContentType.EmailAddress);
        //    //CreateButton("Continue", delegate { AddNewExperiencePage(pageNumber + 1); });
        //}
        else if (pageNumber == 7) //summary of information
        {
            CreateHeaderText("Experience Summary", pageNumber + "/" + totalPages, "Please check all information.");
            CreateDisplayGroup("<b>Experience</b>");
            CreateDisplayGroup(_addNewExperienceData.ExperienceLocale);

            CreateDisplayGroup("<b>Role</b>");
            CreateDisplayGroup(_addNewExperienceData.RoleInExperience);

            CreateDisplayGroup("<b>About</b>");
            CreateDisplayGroup(_addNewExperienceData.Description);

            CreateDisplayGroup("<b>Comments</b>");
            CreateDisplayGroup(_addNewExperienceData.Comments);


            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Confirm",
              delegate
              {
                  SaveExperience();
                  _addNewExperienceData = null;
                  userInterfaceManager.Open_Files(); //Go to next page.
              });

            //CreateButton("Continue", delegate { AddNewExperiencePage(pageNumber + 1); SaveExperience(); });


        }
        else if (pageNumber == 8)
        {
            CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "content text here");
        }
        else if (pageNumber == 9)
        {
            CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "content text here");
        }

        //CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "content text here");
        //List<string> SkillsFromDataBase = new List<string>();
        //SkillsFromDataBase.Add("fdsaffffffffffffffffffff");
        //SkillsFromDataBase.Add("4353dshfjkdhsjkfhdskjhfjkds B");
        //SkillsFromDataBase.Add("fdskjfdskljhfkldskfldsjklfsdjliflkds C");
        //CreateDisplayGroup(SkillsFromDataBase);

        //List<string> lefttext = new List<string>();
        //List<string> rightText = new List<string>();

        //lefttext.Add("left a");
        //lefttext.Add("left b");
        //rightText.Add("right a");
        //rightText.Add("right b");
        //CreateDisplayLine(lefttext, rightText);

        //CreateButton("Continue", delegate { AddNewExperiencePage(pageNumber + 1); });
    }



    public void CaptureDate(ref System.DateTime dataItem, ContentDataIdentiferUI _dataSource)
    {
        dataItem = _dataSource.GetDate();
    }

    public void CaptureStringData(ref string dataItem, ContentDataIdentiferUI _dataSource)
    {
        dataItem = _dataSource._inputField.text.ToString();
    }

    void SaveSkill()
    {
        accountManager.localUserAccount.SaveSkill(_addNewSkillData);
    }

    void SaveArtifact()
    {
        accountManager.localUserAccount.SaveArtifact(_addNewArtifactData);
    }

    void SaveReference()
    {
        accountManager.localUserAccount.SaveReference(_addNewReferenceData);
    }

    void SaveExperience()
    {
        accountManager.localUserAccount.SaveExperience(_addNewExperienceData);
    }

    public List<string> getDatabaseReference(List<ReferenceData> _references)
    {
        List<string> _data = new List<string>();
        _data.Add("N/A");

        for (int i = 0; i < _references.Count; i++)
        {
            _data.Add(_references[i].Name);
        }

        return _data;
    }

    public List<string> getDatabaseExperiences(List<ExperienceData> _experiences)
    {
        List<string> _data = new List<string>();
        _data.Add("N/A");

        for (int i = 0; i < _experiences.Count; i++)
        {
            _data.Add(_experiences[i].RoleInExperience);
        }

        return _data;
    }

    public List<string> getDatabaseArtifacts(List<ArtifactData> _artifacts)
    {
        List<string> _data = new List<string>();
        _data.Add("N/A");

        for (int i = 0; i < _artifacts.Count; i++)
        {
            _data.Add(_artifacts[i].ArtificatContent);
        }

        return _data;
    }

    public List<string> getDatabaseSkills(List<SkillData> _skillData)
    {
        List<string> _data = new List<string>();
        _data.Add("N/A");

        for (int i = 0; i < _skillData.Count; i++)
        {
            _data.Add(_skillData[i].Name);
        }

        return _data;
    }

    //public void RecordPageData(ref object dataSource, ContentDataIdentiferUI _contentSciptID)
    //{
    //    dataSource = _contentSciptID._inputField;
    //}

    public void CreateHeaderText(string header, string pageNumberMax, string content)
    {
        TMP_Text HeaderText = HeaderTextHolder.transform.GetComponentsInChildren<TMP_Text>()[0];
        TMP_Text PageText = HeaderTextHolder.transform.GetComponentsInChildren<TMP_Text>()[1];
        TMP_Text ContentText = HeaderTextHolder.transform.GetComponentsInChildren<TMP_Text>()[2];

        HeaderText.text = header;
        PageText.text = pageNumberMax;
        ContentText.text = content;

        CreatePrefab(HeaderTextHolder);
    }

    private ContentDataIdentiferUI CreateEditInformationContent(string information, string defaultData, TMP_InputField.ContentType _contentType, bool requireInput = true, int _characterLimit = 0)
    {
        EditSingleDetailPrefab.GetComponentInChildren<TMP_Text>().text = information;
        EditSingleDetailPrefab.GetComponentInChildren<TMP_InputField>().text = defaultData;
        EditSingleDetailPrefab.GetComponentInChildren<TMP_InputField>().contentType = _contentType;
        EditSingleDetailPrefab.GetComponentInChildren<TMP_InputField>().characterLimit = _characterLimit;
        EditSingleDetailPrefab.GetComponent<ContentDataIdentiferUI>().requireInput = requireInput;

        return CreatePrefab(EditSingleDetailPrefab).GetComponent<ContentDataIdentiferUI>();
    }

    public ContentDataIdentiferUI CreateLongContextPrefab(string defaultData, TMP_InputField.ContentType _contentType, string defaultText = "", bool _canEdit = true)
    {
        if (defaultData == "")
            EditLongContentTextBoxPrefab.GetComponentInChildren<TMP_InputField>().placeholder.GetComponent<TMP_Text>().text = defaultText;
        else
            EditLongContentTextBoxPrefab.GetComponentInChildren<TMP_InputField>().placeholder.GetComponent<TMP_Text>().text = "";

        EditLongContentTextBoxPrefab.GetComponentInChildren<TMP_InputField>().text = defaultData;

        if (_canEdit == false)
        {
            EditLongContentTextBoxPrefab.GetComponentInChildren<TMP_InputField>().interactable = false;
        }
        else
        {
            EditLongContentTextBoxPrefab.GetComponentInChildren<TMP_InputField>().contentType = _contentType;
        }

        return CreatePrefab(EditLongContentTextBoxPrefab).GetComponent<ContentDataIdentiferUI>();
    }

    public ContentDataIdentiferUI CreateToggleItem(List<string> content, List<string> defaultData, bool forceSingleToggleGroup = false)
    {
        Transform _toggleGroupHolder = CreatePrefab(EditToggleListGroupPrefab);
        _toggleGroupHolder.GetComponent<ToggleGroup>().allowSwitchOff = !forceSingleToggleGroup;

        //Debug.Log(content.Count);

        for (int i = 0; i < content.Count; i++)
        {
            EditToggleItemPrefab.GetComponentInChildren<TMP_Text>().text = content[i];
            EditToggleItemPrefab.GetComponentInChildren<Toggle>().isOn = false;
            if (forceSingleToggleGroup == true)
                EditToggleItemPrefab.GetComponentInChildren<Toggle>().group = _toggleGroupHolder.GetComponent<ToggleGroup>();
            else
                EditToggleItemPrefab.GetComponentInChildren<Toggle>().group = null;

            for (int x = 0; x < defaultData.Count; x++)
            {
                //Debug.Log(defaultData[x] + " "+  content[i]);
                if (defaultData[x] == content[i])
                {
                    EditToggleItemPrefab.GetComponentInChildren<Toggle>().isOn = true;
                }
            }

            CreatePrefab(EditToggleItemPrefab, _toggleGroupHolder);
        }

        return _toggleGroupHolder.GetComponent<ContentDataIdentiferUI>();
    }

    public Transform CreateDatePrefab(string content, System.DateTime defaultData)
    {
        EditDatePrefab.GetComponentInChildren<TMP_Text>().text = content;
        Transform _prefab = CreatePrefab(EditDatePrefab);

        //Debug.Log(defaultData.Year.ToString());

        if (defaultData.Year.ToString() != "1")
        {
            _prefab.GetComponent<ContentDataIdentiferUI>()._DOB_DD.text = defaultData.Date.Day.ToString("00");
            _prefab.GetComponent<ContentDataIdentiferUI>()._DOB_MM.text = defaultData.Date.Month.ToString("00");
            _prefab.GetComponent<ContentDataIdentiferUI>()._DOB_YY.text = defaultData.Date.Year.ToString("0000");
        }

        CreatedScreenGameObjects.Add(_prefab.gameObject);
        return _prefab;
    }


    public void CreateButtons(List<string> contents, List<UnityAction> _events)
    {
        Transform ButtonHolder = CreatePrefab(EditButtonPrefab);

        for (int i = 0; i < contents.Count; i++)
        {
            Transform _button = CreateButton(contents[i], _events[i], false);
            //Debug.Log(_button.name + i);
            if (i == 0)
                ContinueButton = _button.GetComponent<Button>();
            //ButtonPrefab.GetComponentInChildren<TMP_Text>().text = contents[i];
            //ButtonPrefab.GetComponent<Button>().onClick.AddListener(_events[i]);
            //CreatePrefab(ButtonPrefab, ButtonHolder);
        }
    }

    public Transform CreateSkillButton(string content, string description, int level, UnityAction _event, bool overrideCheck = true)
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
    }

    public Transform CreateButton(string content, UnityAction _event, bool overrideCheck = true)
    {
        Transform ButtonHolder = CreatePrefab(EditButtonPrefab);
        ButtonHolder.GetComponent<HorizontalLayoutGroup>().padding.bottom = 0;
        ButtonPrefab.GetComponentInChildren<TMP_Text>().text = content;
        Transform _buttonCreated = CreatePrefab(ButtonPrefab, ButtonHolder);
        _buttonCreated.GetComponent<Button>().onClick.AddListener(_event);
        if (overrideCheck)
            ContinueButton = _buttonCreated.GetComponent<Button>();
        return _buttonCreated;
    }

    private int GetSpaceFillerIndex() { return parentIndexSize + currentInactiveChildObjects; }

    public IEnumerator CreateSpaceFiller(int currentIndex)
    {
        yield return null;
        float size = this.transform.GetComponent<RectTransform>().rect.height - _ScrollRectHolder.transform.GetComponent<RectTransform>().rect.height - boundrySize;
        //Debug.Log(this.transform.GetComponent<RectTransform>().rect.height + " " + _ScrollRectHolder.transform.GetComponent<RectTransform>().rect.height + " " + size);
        Transform _prefabSpaceFiller = CreatePrefab(SpaceFiller);
        CreatedScreenGameObjects.Add(_prefabSpaceFiller.gameObject);
        _prefabSpaceFiller.transform.SetSiblingIndex(currentIndex);
        _prefabSpaceFiller.GetComponent<LayoutElement>().minHeight = size;
        yield return null;
    }

    public Transform CreateDisplayGroup(string contents, Transform DisplayGroupTextHolder = null)//, List<string> _rightText)
    {
        if (DisplayGroupTextHolder == null)
            DisplayGroupTextHolder = CreatePrefab(DisplayContentTextPrefabGroup);

        DisplayContentTextPrefabItem.GetComponentInChildren<TMP_Text>().text = contents;
        return CreatePrefab(DisplayContentTextPrefabItem, DisplayGroupTextHolder);

    }

    public void CreateDisplayGroup(List<string> contents)//, List<string> _rightText)
    {
        Transform DisplayGroupTextHolder = CreatePrefab(DisplayContentTextPrefabGroup);

        for (int i = 0; i < contents.Count; i++)
        {
            CreateDisplayGroup(contents[i], DisplayGroupTextHolder);
            //DisplayContentTextPrefabItem.GetComponentInChildren<TMP_Text>().text = contents[i];
            //CreatePrefab(DisplayContentTextPrefabItem, DisplayGroupTextHolder);
        }
    }


    public void CreateDisplayLine(List<string> leftText, List<string> rightText)//, List<string> _rightText)
    {
        Transform DisplayGroupTextHolder = CreatePrefab(DisplayLineTextPrefabGroup);

        for (int i = 0; i < leftText.Count; i++)
        {
            DisplayLineTextPrefabItem.GetComponentsInChildren<TMP_Text>()[0].text = leftText[i];
            DisplayLineTextPrefabItem.GetComponentsInChildren<TMP_Text>()[1].text = rightText[i];
            CreatePrefab(DisplayLineTextPrefabItem, DisplayGroupTextHolder);
        }
    }

    Transform CreatePrefab(GameObject _prefabItem, Transform _parentPrefab = null)
    {
        if (_parentPrefab == null)
        {
            _parentPrefab = _prefabItem.transform.parent;
            parentIndexSize++;
        }

        CreatedScreenGameObjects.Add(Instantiate(_prefabItem, _parentPrefab));
        CreatedScreenGameObjects[CreatedScreenGameObjects.Count - 1].SetActive(true);
        return CreatedScreenGameObjects[CreatedScreenGameObjects.Count - 1].transform;
    }

    void DestroyCurrentScreens(GameObject _exception = null)
    {
        parentIndexSize = 0;
        for (int i = 0; i < CreatedScreenGameObjects.Count; i++)
        {
            if (_exception == null || GameObject.ReferenceEquals(_exception, CreatedScreenGameObjects[i]) == false)
                Destroy(CreatedScreenGameObjects[i]);
        }
        CreatedScreenGameObjects.Clear();
        CreatedScreenGameObjects.Add(_exception);
        //ContinueButton = null;
    }



    public void CreateSkillsDisplayContent()
    {
        DestroyCurrentScreens();

        if (accountManager.localUserAccount._skills.Count == 0)
        {
            //Do empty
            CreateDisplayGroup("<b><align=center>No Skills!</align></b>");
            return;
        }


        for (int i = 0; i < accountManager.localUserAccount._skills.Count; i++)
        {
            CreateSkillDisplayItem(accountManager.localUserAccount._skills[i]);
        }
    }


    public void CreateTotalSkillDisplay(SkillData _skill)
    {
        SkillDisplayContent.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = _skill.Name;

        int expCount = 0;
        int projCount = 0;
        int refCount = 0;

        for (int i = 0; i < accountManager.localUserAccount._experiences.Count; i++)
        {
            for (int x = 0; x < accountManager.localUserAccount._experiences[i].Skills.Count; x++)
            {
                if (accountManager.localUserAccount._experiences[i].Skills[x] == _skill.Name)
                {
                    expCount++;
                }
            }
        }

        for (int i = 0; i < accountManager.localUserAccount._artifacts.Count; i++)
        {
            for (int x = 0; x < accountManager.localUserAccount._artifacts[i].Skills.Count; x++)
            {
                if (accountManager.localUserAccount._artifacts[i].Skills[x] == _skill.Name)
                {
                    projCount++;
                }
            }
        }

        for (int i = 0; i < accountManager.localUserAccount._references.Count; i++)
        {
            for (int x = 0; x < accountManager.localUserAccount._references[i].Skills.Count; x++)
            {
                if (accountManager.localUserAccount._references[i].Skills[x] == _skill.Name)
                {
                    refCount++;
                }
            }
        }

        SkillDisplayContent.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<TMP_Text>().text = expCount.ToString();// _skill.; experiences
        SkillDisplayContent.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponentInChildren<TMP_Text>().text = projCount.ToString();// _skill.; projects
        SkillDisplayContent.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponentInChildren<TMP_Text>().text = refCount.ToString();// _skill.; References



        for (int i = 0; i < 4; i++)
        {
            if (i + 1 < _skill.Level)
            {
                SkillDisplayContent.transform.GetChild(0).GetComponentInChildren<GridLayoutGroup>().transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                SkillDisplayContent.transform.GetChild(0).GetComponentInChildren<GridLayoutGroup>().transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        Transform _createdGameobject = CreatePrefab(SkillDisplayContent);

        _createdGameobject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectSkillToExpand(_createdGameobject.gameObject, _skill, 1); });
        _createdGameobject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectSkillToExpand(_createdGameobject.gameObject, _skill, 2); });
        _createdGameobject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectSkillToExpand(_createdGameobject.gameObject, _skill, 3); });
    }

    public void CreateSkillDisplayItem(SkillData _skill)
    {
        SkillDisplayContent.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = _skill.Name;

        int expCount = 0;
        int projCount = 0;
        int refCount = 0;

        for (int i = 0; i < accountManager.localUserAccount._experiences.Count; i++)
        {
            for (int x = 0; x < accountManager.localUserAccount._experiences[i].Skills.Count; x++)
            {
                if (accountManager.localUserAccount._experiences[i].Skills[x] == _skill.Name)
                {
                    expCount++;
                }
            }
        }

        if(accountManager.localUserAccount == null)
        {
            Debug.LogError("FAIL");
            return;
        }


        for (int i = 0; i < accountManager.localUserAccount._artifacts.Count; i++)
        {
           // Debug.Log(accountManager.localUserAccount._artifacts[i].Title);
            if (accountManager.localUserAccount._artifacts[i] != null)
            {
                //Debug.Log(accountManager.localUserAccount._artifacts[i].Skills.Count);
                for (int x = 0; x < accountManager.localUserAccount._artifacts[i].Skills.Count; x++)
                {
                    if (accountManager.localUserAccount._artifacts[i].Skills[x] == _skill.Name)
                    {
                        projCount++;
                    }
                }
            }
        }

        for (int i = 0; i < accountManager.localUserAccount._references.Count; i++)
        {
            for (int x = 0; x < accountManager.localUserAccount._references[i].Skills.Count; x++)
            {
                if (accountManager.localUserAccount._references[i].Skills[x] == _skill.Name)
                {
                    refCount++;
                }
            }
        }

        SkillDisplayContent.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<TMP_Text>().text = expCount.ToString();// _skill.; experiences
        SkillDisplayContent.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponentInChildren<TMP_Text>().text = projCount.ToString();// _skill.; projects
        SkillDisplayContent.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponentInChildren<TMP_Text>().text = refCount.ToString();// _skill.; References

        for (int i = 0; i < 4; i++)
        {
            if (i + 1 < _skill.Level)
            {
                SkillDisplayContent.transform.GetChild(0).GetComponentInChildren<GridLayoutGroup>().transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                SkillDisplayContent.transform.GetChild(0).GetComponentInChildren<GridLayoutGroup>().transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        //CreatePrefab(SkillDisplayContent).gameObject.name = _skill.Name;
        Transform _createdGameobject = CreatePrefab(SkillDisplayContent);
        _createdGameobject.name = _skill.Name;

        _createdGameobject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectSkillToExpand(_createdGameobject.gameObject, _skill, 1); });
        _createdGameobject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectSkillToExpand(_createdGameobject.gameObject, _skill, 2); });
        _createdGameobject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectSkillToExpand(_createdGameobject.gameObject, _skill, 3); });
    }

    public void SelectSkillToExpand(GameObject _prefab, SkillData _skillData, int optionsScreen)
    {
        DestroyCurrentScreens(_prefab);

        int count = 0;

        if (optionsScreen == 0)
        {

        }
        else if (optionsScreen == 1)
        {
            CreatePrefab(SkillExperienceTitle);
            for (int i = 0; i < accountManager.localUserAccount._experiences.Count; i++)
            {
                for(int x = 0; x < accountManager.localUserAccount._experiences[i].Skills.Count; x++)
                {
                    if (accountManager.localUserAccount._experiences[i].Skills[x] == _skillData.Name)
                    {
                        CreateDisplayExpansionExperience(accountManager.localUserAccount._experiences[i], false);
                        count++;
                    }
                }
            }
        }
        else if (optionsScreen == 3)
        {
            CreateSkillReferenceProjectTitle(null, "Name", "Position");
            for (int i = 0; i < accountManager.localUserAccount._references.Count; i++)
            {
                for (int x = 0; x < accountManager.localUserAccount._references[i].Skills.Count; x++)
                {
                    if (accountManager.localUserAccount._references[i].Skills[x] == _skillData.Name)
                    {
                        CreateDisplayExpansionReferences(accountManager.localUserAccount._references[i], false);
                        count++;
                    }
                }
            }
        }
        else if (optionsScreen == 2)
        {
            CreateSkillReferenceProjectTitle(null, "Name", "Type");
            for (int i = 0; i < accountManager.localUserAccount._artifacts.Count; i++)
            {
                if (accountManager.localUserAccount._artifacts[i] != null)
                {
                    for (int x = 0; x < accountManager.localUserAccount._artifacts[i].Skills.Count; x++)
                    {
                        if (accountManager.localUserAccount._artifacts[i].Skills[x] == _skillData.Name)
                        {
                            CreateDisplayExpansionProject(accountManager.localUserAccount._artifacts[i], false);
                            count++;
                        }
                    }
                }
                else
                {
                    accountManager.localUserAccount._artifacts.RemoveAt(i);
                }
            }
        }

        if (count == 0)
        {
            CreateDisplayGroup("<b><align=center>No Data Added!</align></b>");
        }
    }

    public void DisplayAllExperiences()
    {
        DestroyCurrentScreens();
        CreatePrefab(SkillExperienceTitle);
        for (int i = 0; i < accountManager.localUserAccount._experiences.Count; i++)
        {
          // for (int x = 0; x < accountManager.localUserAccount._experiences[i].Skills.Count; x++)
          //  {
                CreateDisplayExpansionExperience(accountManager.localUserAccount._experiences[i], true);
         //   }
        }

        if (accountManager.localUserAccount._experiences.Count == 0)
        {
            CreateDisplayGroup("<b><align=center>No Data Added!</align></b>");
        }
    }

    public void DisplayAllArtefacts()
    {
        DestroyCurrentScreens();
        CreateSkillReferenceProjectTitle(null, "Name", "Type");
        for (int i = 0; i < accountManager.localUserAccount._artifacts.Count; i++)
        {
            CreateDisplayExpansionProject(accountManager.localUserAccount._artifacts[i], true);
        }

        if (accountManager.localUserAccount._artifacts.Count == 0)
        {
            CreateDisplayGroup("<b><align=center>No Data Added!</align></b>");
        }
    }

    public void DisplayAllReferences()
    {
        DestroyCurrentScreens();
        CreateSkillReferenceProjectTitle(null, "Name", "Position");
        for (int i = 0; i < accountManager.localUserAccount._references.Count; i++)
        {
            CreateDisplayExpansionReferences(accountManager.localUserAccount._references[i], true);
        }

        if (accountManager.localUserAccount._references.Count == 0)
        {
            CreateDisplayGroup("<b><align=center>No Data Added!</align></b>");
        }
    }

    public void CreateDisplayExpansionExperience(ExperienceData _experienceData, bool displaySkills = false)
    {
        GameObject ParentItemHolder = null;

        SkillExperienceContent.transform.GetChild(0).transform.GetComponentsInChildren<TMP_Text>()[0].text = _experienceData.ExperienceLocale;// + " ! ";
        SkillExperienceContent.transform.GetChild(0).transform.GetComponentsInChildren<TMP_Text>()[1].text = _experienceData.StartDate.Year.ToString("0000") + "-"+ _experienceData.EndDate.Year.ToString("0000");
        SkillExperienceContent.transform.GetChild(0).transform.GetComponentsInChildren<TMP_Text>()[2].text = _experienceData.RoleInExperience;// + " ! ";
        ParentItemHolder = CreatePrefab(SkillExperienceContent).gameObject;

        if (displaySkills)
        {
            for (int x = 0; x < _experienceData.Skills.Count; x++)
            {
                CreateSkillExpansionItem(ParentItemHolder.transform, _experienceData.Skills[x]);
            }
        }
    }

    public void CreateDisplayExpansionProject(ArtifactData _artifactData, bool displaySkills = false)
    {
        GameObject ParentItemHolder = null;

        SkillReferenceProjectContent.transform.GetChild(0).transform.GetComponentsInChildren<TMP_Text>()[0].text = _artifactData.Title;
        SkillReferenceProjectContent.transform.GetChild(0).transform.GetComponentsInChildren<TMP_Text>()[1].text = _artifactData.type.ToString();
        ParentItemHolder = CreatePrefab(SkillReferenceProjectContent).gameObject; //.transform.GetChild(1).gameObject;

        if (displaySkills)
        {
            for (int x = 0; x < _artifactData.Skills.Count; x++)
            {
                CreateSkillExpansionItem(ParentItemHolder.transform, _artifactData.Skills[x]);
            }
        }
    }

    public void CreateDisplayExpansionReferences(ReferenceData _referenceData, bool displaySkills = false)
    {
        GameObject ParentItemHolder = null;


        SkillReferenceProjectContent.transform.GetChild(0).transform.GetComponentsInChildren<TMP_Text>()[0].text = _referenceData.Name;
        SkillReferenceProjectContent.transform.GetChild(0).transform.GetComponentsInChildren<TMP_Text>()[1].text = _referenceData.Position;
        ParentItemHolder = CreatePrefab(SkillReferenceProjectContent).gameObject;

        if (displaySkills)
        {
            for (int x = 0; x < _referenceData.Skills.Count; x++)
            {
                CreateSkillExpansionItem(ParentItemHolder.transform, _referenceData.Skills[x]);
            }
        }
    }

    public void CreateSkillReferenceProjectTitle(Transform _holder, string name, string positionType)
    {
        SkillReferenceProjectTitle.GetComponentsInChildren<TMP_Text>()[0].text = name;
        SkillReferenceProjectTitle.GetComponentsInChildren<TMP_Text>()[1].text = positionType;
        CreatePrefab(SkillReferenceProjectTitle, _holder);
    }

    //public void CreateSkillExperienceTitle(Transform _holder, string name, string positionType)

    public void CreateSkillExpansionItem(Transform _holder, string content)
    {
        SkillItemExtension.transform.GetComponentInChildren<TMP_Text>().text = content;
        CreatePrefab(SkillItemExtension, _holder);
    }
}
