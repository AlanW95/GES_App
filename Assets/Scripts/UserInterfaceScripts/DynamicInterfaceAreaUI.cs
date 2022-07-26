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
    public GameObject WorkCoachPrefab;
    public GameObject DropDownSkillsPrefab;
    public GameObject EditURLButtonPrefab;
    public GameObject URLButtonPrefab;

    public GameObject OverlayWindow;

    public GameObject DisplayContentTextPrefabGroup;
    public GameObject DisplayContentTextPrefabItem;

    public GameObject SkillDisplayContent;

    public GameObject DisplayLineTextPrefabGroup;
    public GameObject DisplayLineTextPrefabItem;

    public List<GameObject> CreatedScreenGameObjects = new List<GameObject>();
    public List<GameObject> SubElementGameObjects = new List<GameObject>();
    public Button ContinueButton;
    public Button[] BottomBannerButtons;
    public GameObject EditButton;
    public Button AddButton;

    public ExperienceData _addNewExperienceData;
    public ArtifactData _addNewArtifactData;
    public ReferenceData _addNewReferenceData;
    public SkillData _addNewSkillData;

    public CVData _CVData;
    public List<string> CVSkills = new List<string>();
    public List<string> CVExperiences = new List<string>();
    public List<string> CVReferences = new List<string>();
    public List<string> CVArtifacts = new List<string>();

    public GameObject SkillReferenceProjectTitle;
    public GameObject SkillReferenceProjectContent;
    public GameObject SkillExperienceTitle;
    public GameObject SkillExperienceContent;
    public GameObject SkillItemExtension;

    private int parentIndexSize = 0;
    private int currentInactiveChildObjects = 0;
    private float boundrySize = 25f;

    public AccountManager accountManager;
    public UserInterfaceManagerUI userInterfaceManager;
    public SkillsInfo skillInfoManager;
    public SkillsRepository skillsRepositoryManager;
    public DreamJobInfo dreamJobInfoManager;
    public WriteToTextFile textFileManager;
    public FirebaseManager firebaseDatabaseManager;

    [SerializeField]
    private GameObject selectedDropDownSkillObject;
    private TMP_Dropdown selectedSkill;
    private string selectedSkillName;
    private int dropdownValue;

    private string learningSkill;
    private string learningSkillDefinition;
    private bool videoAudio;
    private bool paperArticleBlog;
    private bool freeCourses;
    private bool miniGames;

    private void Awake()
    {
        /*accountManager = FindObjectOfType<AccountManager>();
        userInterfaceManager = FindObjectOfType<UserInterfaceManagerUI>();
        skillInfoManager = FindObjectOfType<SkillsInfo>();*/
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

    public void GenerateSkillData()
    {
        if (_addNewSkillData == null)
        {
            _addNewSkillData = new SkillData();
        } else
        {
            Debug.LogError("Could not create new skill data");
        }

        skillInfoManager.PassingSkillValidation();

        /*if (_addNewSkillData == null)
        {
            _addNewSkillData = new SkillData();
        }*/

        //selectedSkill.text = _addNewSkillData.Name;
        /*_addNewSkillData.Name = selectedSkill.text;*/
        /*Debug.Log(_addNewSkillData.Name);*/
        /*skillInfoManager.TransferSkillData();*/

        //ContentDataIdentiferUI _skillName = CreateEditInformationContent("Name", _addNewSkillData.Name, TMP_InputField.ContentType.Name);
        /*_skillName.enabled = false;*/
        //CaptureStringData(ref _addNewSkillData.Name, _skillName);
    }

    public void AddButtonInteractable()
    {
        AddButton.interactable = true;
    }

    public void RemoveButtonInteractable()
    {
        AddButton.interactable = false;
    }

    public void AddNewSkill(int pageNumber)
    {
        DestroyCurrentScreens();
        int totalPages = 5; //originally int totalPages = 3;
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
            EditButton.SetActive(false);
            AddButton.interactable = false;
        }
        else
        {
            Configure_Top_Banner(false, false, "Add Skill", delegate { AddNewSkill(pageNumber - 1); }, null, null);
            AddButton.interactable = false;
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
            Configure_Top_Banner(false, false, "Share Skill", delegate { userInterfaceManager.Open_AddSkills(); }, null, null);
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateHeaderText(null, pageNumber + "/" + totalPages, _addNewSkillData.Name);
            CreateDisplayGroup("Would you like to share this skill to the crowdsourced repository? This will allow other users to find and add it to their own skill portfolio.");

            

            CreateButton("Yes, I would love to contribute!",
            delegate
            {
                
                AddNewSkill(pageNumber + 1);
                //TODO: Send skill to Firebase Realtime Database
            }, 255, 255, 255, 255, 255, 255); 
            CreateButton("No thank you!",
             delegate
             {
                 /*CaptureStringData(ref _addNewSkillData.Name, _skillName);*/
                 AddNewSkill(pageNumber + 1);
             }, 255, 255, 255, 255, 255, 255);
        }
        else if (pageNumber == 3)
        {
            //TODO: CHANGE THISSSS
            //Configure_Top_Banner(false, false, "Skill Level", delegate { AddNewSkill(pageNumber - 1); }, true, delegate { /*show skill level information*/ });
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateHeaderText(null, pageNumber + "/" + totalPages, "Select skill level:");
            CreateDisplayGroup("<b><u>" + _addNewSkillData.Name + "</b></u>");
            /*Transform _holder = CreateDisplayGroup(_addNewSkillData.Name).parent;*/
            /*CreateDisplayGroup(_addNewSkillData.LevelName, _holder);*/
            CreateSkillButton("Novice", "Little or no knowledge or experience.", 1, delegate { _addNewSkillData.Level = 1; _addNewSkillData.LevelName = "Novice"; AddNewSkill(pageNumber + 1); });
            CreateSkillButton("Advanced Beginner", "Basic knowledge or experience.", 2, delegate { _addNewSkillData.Level = 2; _addNewSkillData.LevelName = "Advanced Beginner"; AddNewSkill(pageNumber + 1); });
            CreateSkillButton("Competent", "Intermediate knowledge or experience.", 3, delegate { _addNewSkillData.Level = 3; _addNewSkillData.LevelName = "Competent"; AddNewSkill(pageNumber + 1); });
            CreateSkillButton("Proficient", "Broad knowledge or experience.", 4, delegate { _addNewSkillData.Level = 4; _addNewSkillData.LevelName = "Proficient"; AddNewSkill(pageNumber + 1); });
            CreateSkillButton("Expert", "Extensive knowledge or experience.", 5, delegate { _addNewSkillData.Level = 5; _addNewSkillData.LevelName = "Expert"; AddNewSkill(pageNumber + 1); });
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            //TODO: HAVE DISPLAY THE SKILL LEVEL INFO DRAWER
            CreateButton("Not sure?",
            delegate
            {
                userInterfaceManager.Open_SkillLevelDrawer();
                /*SaveSkill();
                _addNewSkillData = null;
                AddNewSkill(pageNumber + 1);*/
                //TODO: DO NOT send skill to Firebase Realtime Database
            }, 255, 255, 255, 255, 255, 255);

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
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateHeaderText(null, pageNumber + "/" + totalPages, _addNewSkillData.Name);
            CreateDisplayGroup("Would you like to share this acquisition to the crowdsourced repository? This will allow the app to suggest new skills to you and others in similar situations based on your current skills and education.");
            CreateButton("Yes, I would love to contribute!",
            delegate
            {
                //SaveSkill();
                //_addNewSkillData = null;
                Debug.Log(_addNewSkillData.Name);
                Debug.Log(_addNewSkillData.LevelName);
                AddNewSkill(pageNumber + 1);
                /*userInterfaceManager.Open_Files();*/
                //TODO: Send skill to Firebase Realtime Database
            }, 255, 255, 255, 255, 255, 255);
            CreateButton("No thank you!",
             delegate
             {
                 //SaveSkill();
                 //_addNewSkillData = null;
                 Debug.Log(_addNewSkillData.Name);
                 Debug.Log(_addNewSkillData.LevelName);
                 /*userInterfaceManager.Open_Files();*/
                 AddNewSkill(pageNumber + 1);
             }, 255, 255, 255, 255, 255, 255);
        }
        else if (pageNumber == 5)
        {
            CreateHeaderText("Skill Summary", pageNumber + "/" + totalPages, null);
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateWorkCoach(null, "Please check all the information for your skill before proceeding!");
            //CreateSkillsDropDown(); //JUST FOR TESTING PURPOSES
            CreateDisplayGroup("");
            CreateDisplayGroup("<b><u>Skill</b></u>");
            /*StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));*/
            /*Transform _holder = CreateDisplayGroup("<br>" + _addNewSkillData.Name).parent;
            CreateDisplayGroup("<br><br>" + _addNewSkillData.LevelName, _holder);
            CreateDisplayGroup("<br>" + _addNewSkillData.Level.ToString(), _holder);*/

            CreateSkillButton(_addNewSkillData.Name, _addNewSkillData.LevelName, _addNewSkillData.Level, null);

            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Confirm",
              delegate
              {
                  SaveSkill();
                  SaveSkillWithFirebase();
                  _addNewSkillData = null;
                  userInterfaceManager.Open_Files();// (userInterfaceManager.FilesScreen);                   //Go to next page.
                  AddButton.interactable = true;
              }, 255, 255, 255, 255, 255, 255);
        }
    }

    public void AddNewArtifact(int pageNumber)
    {
        DestroyCurrentScreens();
        int totalPages = 5;


        if (pageNumber == 1)
        {
            Configure_Top_Banner(false, true, "Add Artifact", delegate { userInterfaceManager.ChangeWindow(userInterfaceManager.HomeScreen); }, null, null);
            EditButton.SetActive(false);
            AddButton.interactable = false;
        }
        else
        {
            Configure_Top_Banner(false, false, "Add Artifact", delegate { AddNewArtifact(pageNumber - 1); }, null, null);
            AddButton.interactable = false;
        }


        if (pageNumber == 1)
        {
            AddButton.interactable = false;
            if (_addNewArtifactData == null)
                _addNewArtifactData = new ArtifactData();

            CreateHeaderText(null, pageNumber + "/" + totalPages, "What kind of artifact would you like to add?");
            CreateButton("Document", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Document; AddNewArtifact(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Image", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Image; AddNewArtifact(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Link", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Link; AddNewArtifact(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Repository", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Repository; AddNewArtifact(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Video", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Video; AddNewArtifact(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Note", delegate { _addNewArtifactData.type = ArtifactData.ArtifactType.Note; AddNewArtifact(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
        }
        else if (pageNumber == 2)
        {
            EditButton.SetActive(false);
            AddButton.interactable = false;
            //Document
            if (_addNewArtifactData.type == ArtifactData.ArtifactType.Document)
            {
                CreateHeaderText(null, pageNumber + "/" + totalPages, _addNewArtifactData.type.ToString() + " Artifact");

                //TODO: Using the Firebase Storage, we should be able to have an upload and download system for users to store uploads of images, documents etc.
                ContentDataIdentiferUI _url = CreateEditInformationContent("File Upload Coming Soon!", _addNewArtifactData.URL, TMP_InputField.ContentType.Standard, false);
                ContentDataIdentiferUI _title = CreateEditInformationContent("Title", _addNewArtifactData.Title, TMP_InputField.ContentType.Name);
                ContentDataIdentiferUI _description = CreateLongContextPrefab(_addNewArtifactData.Description, TMP_InputField.ContentType.Standard, "Enter brief description of the document here");
                StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
                CreateButton("Continue", delegate
                {
                    CaptureStringData(ref _addNewArtifactData.Title, _title);
                    CaptureStringData(ref _addNewArtifactData.Description, _description);
                    CaptureStringData(ref _addNewArtifactData.URL, _url);
                    AddNewArtifact(pageNumber + 1);
                }, 255, 255, 255, 255, 255, 255);
            }

            //Image
            if (_addNewArtifactData.type == ArtifactData.ArtifactType.Image)
            {
                CreateHeaderText(null, pageNumber + "/" + totalPages, _addNewArtifactData.type.ToString() + " Artifact");

                //TODO: Using the Firebase Storage, we should be able to have an upload and download system for users to store uploads of images, documents etc.
                ContentDataIdentiferUI _url = CreateEditInformationContent("File Upload Coming Soon! Enter URL of image as temporary measure.", _addNewArtifactData.URL, TMP_InputField.ContentType.Standard, false);
                ContentDataIdentiferUI _title = CreateEditInformationContent("Image Title", _addNewArtifactData.Title, TMP_InputField.ContentType.Name);
                ContentDataIdentiferUI _description = CreateLongContextPrefab(_addNewArtifactData.Description, TMP_InputField.ContentType.Standard, "Enter brief description of the image here");
                StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
                CreateButton("Continue", delegate
                {
                    CaptureStringData(ref _addNewArtifactData.Title, _title);
                    CaptureStringData(ref _addNewArtifactData.Description, _description);
                    CaptureStringData(ref _addNewArtifactData.URL, _url);
                    AddNewArtifact(pageNumber + 1);
                }, 255, 255, 255, 255, 255, 255);
            }

            //Link
            if (_addNewArtifactData.type == ArtifactData.ArtifactType.Link)
            {
                CreateHeaderText(null, pageNumber + "/" + totalPages, _addNewArtifactData.type.ToString() + " Artifact");

                ContentDataIdentiferUI _url = CreateEditInformationContent("Link", _addNewArtifactData.URL, TMP_InputField.ContentType.Standard, false);
                ContentDataIdentiferUI _title = CreateEditInformationContent("Title", _addNewArtifactData.Title, TMP_InputField.ContentType.Name);
                ContentDataIdentiferUI _description = CreateLongContextPrefab(_addNewArtifactData.Description, TMP_InputField.ContentType.Standard, "Enter brief description of link provided here");
                StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
                CreateButton("Continue", delegate
                {
                    CaptureStringData(ref _addNewArtifactData.Title, _title);
                    CaptureStringData(ref _addNewArtifactData.Description, _description);
                    CaptureStringData(ref _addNewArtifactData.URL, _url);
                    AddNewArtifact(pageNumber + 1);
                }, 255, 255, 255, 255, 255, 255);
            }

            //Repository
            if (_addNewArtifactData.type == ArtifactData.ArtifactType.Repository)
            {
                CreateHeaderText(null, pageNumber + "/" + totalPages, _addNewArtifactData.type.ToString() + " Artifact");

                ContentDataIdentiferUI _url = CreateEditInformationContent("Repository Link", _addNewArtifactData.URL, TMP_InputField.ContentType.Standard, false);
                ContentDataIdentiferUI _title = CreateEditInformationContent("Name of Repository", _addNewArtifactData.Title, TMP_InputField.ContentType.Name);
                ContentDataIdentiferUI _description = CreateLongContextPrefab(_addNewArtifactData.Description, TMP_InputField.ContentType.Standard, "Enter brief description of your repository here");
                StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
                CreateButton("Continue", delegate
                {
                    CaptureStringData(ref _addNewArtifactData.Title, _title);
                    CaptureStringData(ref _addNewArtifactData.Description, _description);
                    CaptureStringData(ref _addNewArtifactData.URL, _url);
                    AddNewArtifact(pageNumber + 1);
                }, 255, 255, 255, 255, 255, 255);
            }

            //YouTube Video
            if (_addNewArtifactData.type == ArtifactData.ArtifactType.Video)
            {
                CreateHeaderText(null, pageNumber + "/" + totalPages, _addNewArtifactData.type.ToString() + " Artifact");

                ContentDataIdentiferUI _url = CreateEditInformationContent("Video URL", _addNewArtifactData.URL, TMP_InputField.ContentType.Standard, false);
                ContentDataIdentiferUI _title = CreateEditInformationContent("Video Title", _addNewArtifactData.Title, TMP_InputField.ContentType.Name);
                ContentDataIdentiferUI _description = CreateLongContextPrefab(_addNewArtifactData.Description, TMP_InputField.ContentType.Standard, "Enter brief description of video here"); 
                StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
                CreateButton("Continue", delegate
                {
                    CaptureStringData(ref _addNewArtifactData.Title, _title);
                    CaptureStringData(ref _addNewArtifactData.Description, _description);
                    CaptureStringData(ref _addNewArtifactData.URL, _url);
                    AddNewArtifact(pageNumber + 1);
                }, 255, 255, 255, 255, 255, 255);
            }

            //Note
            if (_addNewArtifactData.type == ArtifactData.ArtifactType.Note)
            {
                CreateHeaderText(null, pageNumber + "/" + totalPages, _addNewArtifactData.type.ToString() + " Artifact");

                ContentDataIdentiferUI _title = CreateEditInformationContent("Title", _addNewArtifactData.Title, TMP_InputField.ContentType.Name);
                ContentDataIdentiferUI _description = CreateLongContextPrefab(_addNewArtifactData.Description, TMP_InputField.ContentType.Standard, "Enter your description here");
                StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
                CreateButton("Continue", delegate
                {
                    CaptureStringData(ref _addNewArtifactData.Title, _title);
                    CaptureStringData(ref _addNewArtifactData.Description, _description);
                    AddNewArtifact(pageNumber + 1);
                }, 255, 255, 255, 255, 255, 255);
            }
        }
        else if (pageNumber == 3)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "Add relevant skills:");
            EditButton.SetActive(false);
            AddButton.interactable = false;
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
            }, 255, 255, 255, 255, 255, 255);
        }
        else if (pageNumber == 4)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "Add relevant experiences:");
            EditButton.SetActive(false);
            AddButton.interactable = false;
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

                AddNewArtifact(pageNumber + 1);
                /*SaveArtifact();
                SaveArtifactWithFirebase();
                _addNewArtifactData = null;*/
            }, 255, 255, 255, 255, 255, 255);
        }
        else if (pageNumber == 5)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "<b>Artifact Summary</b><br><br>Please check all information before proceeding.");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateDisplayGroup("\n<b><u>Artifact</u></b>");
            Transform _holder = CreateDisplayGroup(_addNewArtifactData.type.ToString()).parent;
            CreateDisplayGroup(_addNewArtifactData.Title, _holder);
            CreateDisplayGroup(_addNewArtifactData.Description, _holder);

            CreateDisplayGroup("\n<b><u>Skill(s) Associated</u></b>");
            _holder = null;
            for (int i = 0; i < _addNewArtifactData.Skills.Count; i++)
            {
                if (_holder == null)
                    _holder = CreateDisplayGroup(_addNewArtifactData.Skills[i]).parent;
                else
                    CreateDisplayGroup(_addNewArtifactData.Skills[i], _holder);
            }

            CreateDisplayGroup("\n<b><u>Experience(s) Associated</u></b>");
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
                  SaveArtifactWithFirebase();

                  _addNewArtifactData = null;
                  userInterfaceManager.Open_Files(); //Go to next page.
                  AddButton.interactable = true;
              }, 255, 255, 255, 255, 255, 255);
        }
    }

    public void AddNewReference(int pageNumber)
    {
        DestroyCurrentScreens();
        int totalPages = 3;

        if (pageNumber == 1)
        {
            Configure_Top_Banner(false, false, "Add Reference", delegate { userInterfaceManager.ChangeWindow(userInterfaceManager.HomeScreen); }, null, null);
            EditButton.SetActive(false);
            AddButton.interactable = false;
        }
        else
        {
            Configure_Top_Banner(false, false, "Add Reference", delegate { AddNewReference(pageNumber - 1); }, null, null);
            AddButton.interactable = false;
        }

        if (pageNumber == 1)
        {
            if (_addNewReferenceData == null)
                _addNewReferenceData = new ReferenceData();

            CreateHeaderText(null, pageNumber + "/" + totalPages, "Contact Information");
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
            }, 255, 255, 255, 255, 255, 255);
        }
        else if (pageNumber == 2)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "Skills that referee can vouch for:");
            EditButton.SetActive(false);
            AddButton.interactable = false;
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
            }, 255, 255, 255, 255, 255, 255);
        }
        else if (pageNumber == 3)
        {
            CreateHeaderText("Reference Summary", pageNumber + "/" + totalPages, "Please check all information before proceeding.");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateDisplayGroup("\n<b><u>Reference</u></b>");
            Transform _holder = CreateDisplayGroup(_addNewReferenceData.Name).parent;
            CreateDisplayGroup(_addNewReferenceData.Position, _holder);

            CreateDisplayGroup("\n<b><u>Contact Info</u></b>");
            _holder = CreateDisplayGroup(_addNewReferenceData.Email).parent;
            CreateDisplayGroup(_addNewReferenceData.PhoneNumber, _holder);

            CreateDisplayGroup("\n<b><u>Skills Associated</u></b>");
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
                  SaveReferenceWithFirebase();

                  _addNewReferenceData = null;
                  userInterfaceManager.Open_Files(); //Go to next page.
                  AddButton.interactable = true;
              }, 255, 255, 255, 255, 255, 255);
        }
    }

    public void AddNewExperiencePage(int pageNumber)
    {
        DestroyCurrentScreens();
        float r;
        float g;
        float b;
        int totalPages = 14;

        if (pageNumber == 1)
        {
            Configure_Top_Banner(false, true, "Add Experience", delegate { userInterfaceManager.ChangeWindow(userInterfaceManager.HomeScreen); }, null, null);
            EditButton.SetActive(false);
            AddButton.interactable = false;
        }
        else
        {
            Configure_Top_Banner(false, false, "Add Experience", delegate { AddNewExperiencePage(pageNumber - 1); }, null, null);
            AddButton.interactable = false;
        }

        if (pageNumber == 1)
        {
            if (_addNewExperienceData == null)
                _addNewExperienceData = new ExperienceData();

            CreateHeaderText(null, pageNumber + "/" + totalPages, "What was your primary role/ job title in this experience?");
            ContentDataIdentiferUI _prefab = CreateLongContextPrefab(_addNewExperienceData.RoleInExperience, TMP_InputField.ContentType.Standard);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { _addNewExperienceData.RoleInExperience = _prefab._inputField.text; AddNewExperiencePage(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);

            /*ContentDataIdentiferUI _name = CreateEditInformationContent("Experience", _addNewExperienceData.ExperienceLocale, TMP_InputField.ContentType.Name);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { CaptureStringData(ref _addNewExperienceData.ExperienceLocale, _name); AddNewExperiencePage(pageNumber + 1); });*/

            //CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "What was your primary role in this experience?");
        }
        else if (pageNumber == 2)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "When did this experience take place?");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            ContentDataIdentiferUI _startDate = CreateDatePrefab("Start Date", _addNewExperienceData.StartDate).GetComponent<ContentDataIdentiferUI>();
            ContentDataIdentiferUI _endDate = CreateDatePrefab("End Date", _addNewExperienceData.EndDate).GetComponent<ContentDataIdentiferUI>();
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { CaptureDate(ref _addNewExperienceData.StartDate, _startDate); CaptureDate(ref _addNewExperienceData.EndDate, _endDate); AddNewExperiencePage(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);

        } else if (pageNumber == 3)
        {


            CreateHeaderText(null, pageNumber + "/" + totalPages, "What skills did this experience help you improve at? You may choose more than one.");
            EditButton.SetActive(false);
            AddButton.interactable = false;
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
            }, 255, 255, 255, 255, 255, 255);

        }
        else if (pageNumber == 4)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "Where did this experience take place?");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateButton("Academic", delegate { AddNewExperiencePage(pageNumber + 1); _addNewExperienceData.ExperienceLocale = "Academic"; }, 255, 255, 255, 255, 255, 255);
            CreateButton("Practical", delegate { AddNewExperiencePage(pageNumber + 1); _addNewExperienceData.ExperienceLocale = "Practical"; }, 255, 255, 255, 255, 255, 255);

        }
        else if (pageNumber == 5)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "Give a brief description of the experience.");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            ContentDataIdentiferUI _prefab = CreateLongContextPrefab(_addNewExperienceData.Description, TMP_InputField.ContentType.Standard);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { _addNewExperienceData.Description = _prefab._inputField.text; AddNewExperiencePage(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
            /*ContentDataIdentiferUI _toggleHolder = CreateToggleItem(getDatabaseExperiences(accountManager.localUserAccount._experiences), _addNewExperienceData.CourseOccured, true);
            CreateButton("Continue",
            delegate
            {
                ContentDataIdentiferUI[] _listItems = _toggleHolder.GetComponentsInChildren<ContentDataIdentiferUI>();
                for (int i = 0; i < _listItems.Length; i++)
                {
                    if (_listItems[i]._toggleItem != null && _listItems[i]._toggleItem.isOn)
                        _addNewExperienceData.CourseOccured.Add(_listItems[i]._ToggleItemName.text);
                }

                AddNewExperiencePage(pageNumber + 1);
            });

            CreateButton("Continue", delegate { AddNewExperiencePage(pageNumber + 1); });*/
        }
        else if (pageNumber == 6)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How committed were you to your assigned role?");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 7)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How committed were you to the team success?");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 8)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How committed were you to the team leadership?");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 9)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How committed were you to the team process?");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 10)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How accountable were you towards your work?");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 11)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How well did you communicate with the rest of the team?");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 12)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How well did you cooperate with the rest of the team?");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 13)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "Additional comments.");
            EditButton.SetActive(false);
            AddButton.interactable = false;
            ContentDataIdentiferUI _prefab = CreateLongContextPrefab(_addNewExperienceData.Comments, TMP_InputField.ContentType.Standard);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { _addNewExperienceData.Comments = _prefab._inputField.text; AddNewExperiencePage(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
        }
        //else if (pageNumber == 6)
        //{
        //    //CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "content text here");
        //    //CreateEditInformationContent("first name", _addNewExperienceData. TMP_InputField.ContentType.Name);
        //    //CreateEditInformationContent("last name", TMP_InputField.ContentType.Name);
        //    //CreateEditInformationContent("email", TMP_InputField.ContentType.EmailAddress);
        //    //CreateButton("Continue", delegate { AddNewExperiencePage(pageNumber + 1); });
        //}
        else if (pageNumber == 14) //summary of information
        {
            CreateHeaderText("Experience Summary", pageNumber + "/" + totalPages, "Please check all information.");
            EditButton.SetActive(false);
            AddButton.interactable = false;

            CreateDisplayGroup("\n<b><u>Role</u></b>");
            CreateDisplayGroup(_addNewExperienceData.RoleInExperience);

            CreateDisplayGroup("\n<b><u>Start & End Date</u></b>");
            CreateDisplayGroup(_addNewExperienceData.StartDate.ToShortDateString() + " -\n" + _addNewExperienceData.EndDate.ToShortDateString());

            CreateDisplayGroup("\n<b><u>Experience</u></b>");
            CreateDisplayGroup(_addNewExperienceData.ExperienceLocale);

            CreateDisplayGroup("\n<b><u>About</u></b>");
            CreateDisplayGroup(_addNewExperienceData.Description);

            CreateDisplayGroup("\n<b><u>Comments</u></b>");
            CreateDisplayGroup(_addNewExperienceData.Comments);


            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Confirm",
              delegate
              {

                  SaveExperience();
                  SaveExperienceWithFirebase();
                  _addNewExperienceData = null;
                  userInterfaceManager.Open_Files(); //Go to next page.
                  AddButton.interactable = true;
              }, 255, 255, 255, 255, 255, 255);

            //CreateButton("Continue", delegate { AddNewExperiencePage(pageNumber + 1); SaveExperience(); });


        }
        /*else if (pageNumber == 8)
        {
            CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "content text here");
        }
        else if (pageNumber == 9)
        {
            CreateHeaderText("Add new experience", pageNumber + "/" + totalPages, "content text here");
        }*/

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

        //CreateButton("Continue", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 255, 255);
    }

    /*public void AddDreamJob()
    {
        Transform _prefab = CreateToggleItem()

        *//*public Transform CreateDatePrefab(string content, System.DateTime defaultData)
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
        }*//*
    }*/

    //---------PRACTICE SKILLS---------------------
    public void AddPracticeSkills(int pageNumber)
    {
        DestroyCurrentScreens();
        int totalPages = 12;

        /*
         * Pages
         * 1 - Practice Skills Home
         * Work Coach, Text "How do you want to practice your desired skills?", Two buttons (Learning Resources, Browse Physical Practice Options)
         * 2 - Learning Resources - choose skill
         * 3 - Learning Resources - choose type of learning resource
         * 4 - List of Video/ Audio learning resources
         * 5 - List of Paper/ Article/ Blog learning resources
         * 6 - List of Free Courses/ Self-Assessment learning resources
         * 7 - List of Mini Games learning resources
         * 8 - Skill definition screen
         * 9 - BROWSE PHYSICAL PRACTICE OPTIONS - THIS WON'T BE AVAILABLE RIGHT AWAY (LOCATION-BASED SERVICES)
         * 10 - Add Own Learning Resource - Type of Learning Resource
         * 11 - Add Own Learning Resource - Details of Learning Resource
         * 12 - Add Own Learning Resource - Confirmation of custom Learning Resource
         */
        //1 - Practice Skills Home
        //Work Coach, Text "How do you want to practice your desired skills?", Two buttons(Learning Resources, Browse Physical Practice Options)
        if (pageNumber == 1)
        {
            Configure_Top_Banner(false, false, "Learning Resources", delegate { userInterfaceManager.ChangeWindow(userInterfaceManager.HomeScreen); }, null, null);
            EditButton.SetActive(false);

            CreateHeaderText(null, null, "How do you want to practice your desired skills?");
            CreateWorkCoach(null, "Practice makes perfect! For the time being, you can view internal learning resources with more physical practice options coming soon!");

            CreateButton("Learning Resources", delegate { AddPracticeSkills(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
            //TODO: MORE RESEARCH HAS TO BE DONE ON THIS - THIS MAY NOT BE POSSIBLE
            //CreateButton("Browse Physical Practice Options", delegate { AddPracticeSkills(pageNumber + 7); }, 255, 255, 255, 255, 255, 255, interactableCheck: false); //add 7 because page 8
        }
        else
        {
            Configure_Top_Banner(false, false, "Learning Resources", delegate { AddPracticeSkills(pageNumber - 1); }, null, null);
        }
        //2 - Learning Resources - choose skill
        if (pageNumber == 2)
        {
            EditButton.SetActive(false);
            //TODO: ADD WORK COACH IMAGE TO REPLACE THE HEADERTEXT
            CreateWorkCoach(null, "Which skills would you like to learn?");
            //CreateHeaderText(null, null, "Which skills would you like to learn?");

            CreateSkillsDropDown();

            CreateButton("Continue", delegate
            {
                GameObject prefab = GameObject.Find("SkillsListPrefab(Clone)");
                selectedSkill = prefab.GetComponentInChildren<TMP_Dropdown>();
                //selectedSkill = DropDownSkillsPrefab.GetComponentInChildren<TMP_Dropdown>();
                dropdownValue = selectedSkill.value;
                Debug.Log(dropdownValue);
                selectedSkillName = selectedSkill.options[selectedSkill.value].text;
                Debug.Log(selectedSkillName);

                AddPracticeSkills(pageNumber + 1);
            }, 255, 255, 255, 255, 255, 255);
            
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            //TODO: STILL TO ADD THIS FUNCTIONALITY
            /*CreateButton("Yes, I want to contribute my own resource", delegate
            {
                //AddPracticeSkills(pageNumber + 7);
            }, 255, 255, 255, 255, 255, 255, interactableCheck: false);*/
        }
        //3 - Learning Resources - choose type of learning resource
        else if (pageNumber == 3)
        {
            CreateWorkCoach(null, "There are various resources available for <b>"+ selectedSkillName + "</b>, choose a type of resource you wish to continue with to learn more about your preferred skill.");
            //CreateHeaderText(null, null, "There are various resources available for <SKILL>, choose a type of resource you wish to continue with to learn more about your preferred skill.");
            EditButton.SetActive(false);
            CreateButton("Videos/ Audio", delegate { videoAudio = true; paperArticleBlog = false; freeCourses = false; miniGames = false; AddPracticeSkills(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Papers/ Articles/ Blogs", delegate { videoAudio = false; paperArticleBlog = true; freeCourses = false; miniGames = false; AddPracticeSkills(pageNumber + 2); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Free Courses/ Self Assessment", delegate { videoAudio = false; paperArticleBlog = false; freeCourses = true; miniGames = false; AddPracticeSkills(pageNumber + 3); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Mini Games", delegate { videoAudio = false; paperArticleBlog = false; freeCourses = false; miniGames = true; AddPracticeSkills(pageNumber + 4); }, 255, 255, 255, 255, 255, 255);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Skill Details", delegate { AddPracticeSkills(pageNumber + 5); }, 255, 255, 255, 255, 255, 255);
        }
        //4 - List of Video/ Audio learning resources
        else if (pageNumber == 4)
        {
            Configure_Top_Banner(false, false, "Learning Resources", delegate { AddPracticeSkills(pageNumber - 1); }, null, null);
            CreateWorkCoach(null, "Here are a selection of videos and/ or audio for <b>" + selectedSkillName + "</b>. Click and they will externally take you to the resource.");
            //CreateHeaderText(null, null, "Video and Audio Resources");
            GetSkillsLearning(dropdownValue);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            EditButton.SetActive(false);
            CreateButton("Return",
            delegate
            {
                videoAudio = false; paperArticleBlog = false; freeCourses = false; miniGames = false;
                AddPracticeSkills(pageNumber - 1);
            }, 255, 255, 255, 255, 255, 255);
        }
        //5 - List of Paper/ Article/ Blog learning resources
        else if (pageNumber == 5)
        {
            Configure_Top_Banner(false, false, "Learning Resources", delegate { AddPracticeSkills(pageNumber - 2); }, null, null);
            CreateWorkCoach(null, "Here are a selection of papers, articles & blog resources for <b>" + selectedSkillName + "</b>. Click and they will externally take you to the resource.");
            //CreateHeaderText(null, null, "Paper, Article & Blog Resources");
            GetSkillsLearning(dropdownValue);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            EditButton.SetActive(false);
            CreateButton("Return",
            delegate
            {
                videoAudio = false; paperArticleBlog = false; freeCourses = false; miniGames = false;
                AddPracticeSkills(pageNumber - 2);
            }, 255, 255, 255, 255, 255, 255);
        }
        //6 - List of Free Courses/ Self-Assessment learning resources
        else if (pageNumber == 6)
        {
            Configure_Top_Banner(false, false, "Learning Resources", delegate { AddPracticeSkills(pageNumber - 3); }, null, null);
            CreateWorkCoach(null, "Here are a selection of free courses and self-assessment learning resources for <b>" + selectedSkillName + "</b>. Click and they will externally take you to the resource.");
            //CreateHeaderText(null, null, "Free Courses and Self-Assessment Learning Resources");
            GetSkillsLearning(dropdownValue);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            EditButton.SetActive(false);
            CreateButton("Return",
            delegate
            {
                videoAudio = false; paperArticleBlog = false; freeCourses = false; miniGames = false;
                AddPracticeSkills(pageNumber - 3);
            }, 255, 255, 255, 255, 255, 255);
        }
        //7 - List of Mini Games learning resources
        else if (pageNumber == 7)
        {
            Configure_Top_Banner(false, false, "Learning Resources", delegate { AddPracticeSkills(pageNumber - 4); }, null, null);
            CreateWorkCoach(null, "Here are a selection of mini game resources for <b>" + selectedSkillName + "</b>. Click and they will externally take you to the resource.");
            //CreateHeaderText(null, null, "Mini-Game Resources");
            GetSkillsLearning(dropdownValue);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            EditButton.SetActive(false);
            CreateButton("Return",
            delegate
            {
                videoAudio = false; paperArticleBlog = false; freeCourses = false; miniGames = false;
                AddPracticeSkills(pageNumber - 4);
            }, 255, 255, 255, 255, 255, 255);
        }
        //8 - Skill definition screen
        else if (pageNumber == 8)
        {
            Configure_Top_Banner(false, false, "Learning Resources", delegate { AddPracticeSkills(pageNumber - 5); }, null, null);
            videoAudio = false; paperArticleBlog = false; freeCourses = false; miniGames = false;
            GetSkillsLearning(dropdownValue);
            EditButton.SetActive(false);
            CreateWorkCoach(null, "Find below the definition of <b>" + learningSkill + "</b>.");
            CreateDisplayGroup("<br><br><br>Definition:<br><br>" + learningSkillDefinition);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Return", delegate { AddPracticeSkills(pageNumber - 5); }, 255, 255, 255, 255, 255, 255);

        }
        //9 - BROWSE PHYSICAL PRACTICE OPTIONS - THIS WON'T BE AVAILABLE RIGHT AWAY (LOCATION-BASED SERVICES)
        else if (pageNumber == 9)
        {
            //will add in due course
        }
        //10 - Add Own Learning Resource - Type of Learning Resource
        else if (pageNumber == 10)
        {
            //will add in due course
        }
        //11 - Add Own Learning Resource - Details of Learning Resource
        else if (pageNumber == 11)
        {
            //will add in due course
        }
        //12 - Add Own Learning Resource - Confirmation of custom Learning Resource
        else if (pageNumber == 12)
        {
            //will add in due course
        }
    }

    //-------------------------------------------------------

    #region Employment Readiness

    private string ListToText(List<string> list)
    {
        string result = "";
        foreach (var listMember in list)
        {
            result += listMember.ToString() + "\n";
        }
        return result;
    }

    public void AddEmploymentReadiness(int pageNumber)
    {
        DestroyCurrentScreens();
        int totalPages = 6;

        /*
         * Pages
         * 1 - Employment Readiness
         * 2 - Create CV
         * 3 - Exporting CV Data
         * 4 - Create CV (displayed information)
         * 5 - Practice Interview
         * 6 - Practice Interview Resource list, depending on what was selected before
         */
        //1 - Employment Readiness
        if (pageNumber == 1)
        {
            Configure_Top_Banner(false, false, "Employment Readiness", delegate { userInterfaceManager.ChangeWindow(userInterfaceManager.HomeScreen); Debug.Log("back button pressed"); }, null, null);
            EditButton.SetActive(false);

            CreateWorkCoach(null, "Choose an option to prepare yourself for applying for a job.");

            CreateButton("Create CV", delegate { AddEmploymentReadiness(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
            CreateDisplayGroup("Job interview practice \ncoming soon.");
            //CreateButton("Practice Interview", delegate { AddEmploymentReadiness(pageNumber + 7); }, 255, 255, 255, 255, 255, 255, interactableCheck: false); //add 7 because page 8
            //TODO: Check button below about what this does? Also remember to change the pageNumber + ? if needed.
            //CreateButton("Job Search", delegate { AddPracticeSkills(pageNumber + 7); }, 255, 255, 255, 255, 255, 255, interactableCheck: false);
        }
        else
        {
            Configure_Top_Banner(false, false, "Employment Readiness", delegate { AddEmploymentReadiness(pageNumber - 1); if (pageNumber == 1) { userInterfaceManager.ChangeWindow(userInterfaceManager.HomeScreen); } }, null, null);
        }
        //2 - Create CV
        if (pageNumber == 2)
        {
            Configure_Top_Banner(false, true, "Employment Readiness", delegate { AddEmploymentReadiness(pageNumber - 1); }, null, null);
            EditButton.SetActive(false);

            CreateWorkCoach(null, "Choose information you want to add to your CV.");
            CreateHeaderText(null, null, "This information will be exported in a text file and saved to your device.");

            CreateDisplayGroup("Skills:");
            ContentDataIdentiferUI _toggleHolderSkills = CreateToggleCVList(getDatabaseSkills(accountManager.localUserAccount._skills), false);

            CreateDisplayGroup("Experience:");
            ContentDataIdentiferUI _toggleHolderExperience = CreateToggleCVList(getDatabaseExperiences(accountManager.localUserAccount._experiences), false);

            CreateDisplayGroup("Artifacts");
            ContentDataIdentiferUI _toggleHolderArtifacts = CreateToggleCVList(getDatabaseArtifacts(accountManager.localUserAccount._artifacts), true);

            CreateDisplayGroup("References");
            ContentDataIdentiferUI _toggleHolderReferences = CreateToggleCVList(getDatabaseReference(accountManager.localUserAccount._references), false);

            CreateButton("Add", delegate
            {
                //Skills
                ContentDataIdentiferUI[] _listItemsSkills = _toggleHolderSkills.GetComponentsInChildren<ContentDataIdentiferUI>();
                for (int i = 0; i < _listItemsSkills.Length; i++)
                {
                    if (_listItemsSkills[i]._toggleItem != null && _listItemsSkills[i]._toggleItem.isOn)
                    {
                        CVSkills.Add(_listItemsSkills[i]._ToggleItemName.text);
                        foreach (var x in CVSkills)
                        {
                            Debug.Log(x.ToString());
                        }
                    }
                }
                //Experience
                ContentDataIdentiferUI[] _listItemsExperience = _toggleHolderExperience.GetComponentsInChildren<ContentDataIdentiferUI>();
                for (int i = 0; i < _listItemsExperience.Length; i++)
                {
                    if (_listItemsExperience[i]._toggleItem != null && _listItemsExperience[i]._toggleItem.isOn)
                    {
                        CVExperiences.Add(_listItemsExperience[i]._ToggleItemName.text);
                        foreach (var x in CVExperiences)
                        {
                            Debug.Log(x.ToString());
                        }
                    }
                }
                //Artifacts
                ContentDataIdentiferUI[] _listItemsArtifacts = _toggleHolderArtifacts.GetComponentsInChildren<ContentDataIdentiferUI>();
                for (int i = 0; i < _listItemsArtifacts.Length; i++)
                {
                    if (_listItemsArtifacts[i]._toggleItem != null && _listItemsArtifacts[i]._toggleItem.isOn)
                    {
                        CVArtifacts.Add(_listItemsArtifacts[i]._ToggleItemName.text);
                        foreach (var x in CVArtifacts)
                        {
                            Debug.Log(x.ToString());
                        }
                    }
                }
                //References
                ContentDataIdentiferUI[] _listItemsReferences = _toggleHolderReferences.GetComponentsInChildren<ContentDataIdentiferUI>();
                for (int i = 0; i < _listItemsReferences.Length; i++)
                {
                    if (_listItemsReferences[i]._toggleItem != null && _listItemsReferences[i]._toggleItem.isOn)
                    {
                        CVReferences.Add(_listItemsReferences[i]._ToggleItemName.text);
                        foreach (var x in CVReferences)
                        {
                            Debug.Log(x.ToString());
                        }
                    }
                }

                AddEmploymentReadiness(pageNumber + 1);
            }, 255, 255, 255, 255, 255, 255);
        }
        //3 - Create CV - See list of CV chosen skills, experience, artifacts and references to export
        else if (pageNumber == 3)
        {
            CreateWorkCoach(null, "Now that you have selected your chosen skills, experience, artifacts and references please preview below and export, saving to your device.");
            //CreateHeaderText(null, null, "There are various resources available for <SKILL>, choose a type of resource you wish to continue with to learn more about your preferred skill.");
            EditButton.SetActive(false);
            CreateDisplayGroup("Skills:<br>" + ListToText(CVSkills) + "\nExperience:<br>" + ListToText(CVExperiences) + "\nArtifacts:<br>" + ListToText(CVArtifacts) + "\nReferences:<br>" + ListToText(CVReferences));


            CreateButton("Export Data", delegate { /*AddEmploymentReadiness(pageNumber + 1);*/ Debug.Log("CV has successfully been exported."); OverlayWindow.SetActive(true); textFileManager.CreateTextFile("Skills:\n" + ListToText(CVSkills) + "\nExperience:\n" + ListToText(CVExperiences) + "\nArtifacts:\n" + ListToText(CVArtifacts) + "\nReferences:\n" + ListToText(CVReferences)); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Copy to Clipboard", delegate { textFileManager.CopyToClipboard("Skills:\n" + ListToText(CVSkills) + "\nExperience:\n" + ListToText(CVExperiences) + "\nArtifacts:\n" + ListToText(CVArtifacts) + "\nReferences:\n" + ListToText(CVReferences)); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Return", delegate { AddEmploymentReadiness(pageNumber - 1); CVSkills.Clear(); CVExperiences.Clear(); CVArtifacts.Clear(); CVReferences.Clear(); }, 255, 255, 255, 255, 255, 255);
        }
        //TODO:NOT NEEDED RIGHT NOW
        //4 - List of Video/ Audio learning resources
        else if (pageNumber == 4)
        {
            Configure_Top_Banner(false, false, "Learning Resources", delegate { AddPracticeSkills(pageNumber - 1); }, null, null);
            CreateWorkCoach(null, "Here is a selection of videos and/ or audio for <b>" + selectedSkillName + "</b>.");
            //CreateHeaderText(null, null, "Video and Audio Resources");
            GetSkillsLearning(dropdownValue);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            EditButton.SetActive(false);
            CreateButton("Return",
            delegate
            {
                videoAudio = false; paperArticleBlog = false; freeCourses = false; miniGames = false;
                AddPracticeSkills(pageNumber - 1);
            }, 255, 255, 255, 255, 255, 255);
        }
        //5 - List of Paper/ Article/ Blog learning resources
        else if (pageNumber == 5)
        {
            Configure_Top_Banner(false, false, "Learning Resources", delegate { AddPracticeSkills(pageNumber - 2); }, null, null);
            CreateWorkCoach(null, "Here is a selection of papers, articles & blog resources for <b>" + selectedSkillName + "</b>.");
            //CreateHeaderText(null, null, "Paper, Article & Blog Resources");
            GetSkillsLearning(dropdownValue);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            EditButton.SetActive(false);
            CreateButton("Return",
            delegate
            {
                videoAudio = false; paperArticleBlog = false; freeCourses = false; miniGames = false;
                AddPracticeSkills(pageNumber - 2);
            }, 255, 255, 255, 255, 255, 255);
        }
        //6 - List of Free Courses/ Self-Assessment learning resources
        else if (pageNumber == 6)
        {
            Configure_Top_Banner(false, false, "Learning Resources", delegate { AddPracticeSkills(pageNumber - 3); }, null, null);
            CreateWorkCoach(null, "Here is a selection of free courses and self-assessment learning resources for <b>" + selectedSkillName + "</b>.");
            //CreateHeaderText(null, null, "Free Courses and Self-Assessment Learning Resources");
            GetSkillsLearning(dropdownValue);
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            EditButton.SetActive(false);
            CreateButton("Return",
            delegate
            {
                videoAudio = false; paperArticleBlog = false; freeCourses = false; miniGames = false;
                AddPracticeSkills(pageNumber - 3);
            }, 255, 255, 255, 255, 255, 255);
        }
    }

    #endregion Employment Readiness

    public void OverlayScreenWindow()
    {
        userInterfaceManager.AddEditInformationScreen.GetComponent<DynamicInterfaceAreaUI>().AddEmploymentReadiness(3);
        OverlayWindow.SetActive(false);
    }

    public void CaptureDate(ref System.DateTime dataItem, ContentDataIdentiferUI _dataSource)
    {
        dataItem = _dataSource.GetDate();
    }

    public void CaptureStringData(ref string dataItem, ContentDataIdentiferUI _dataSource)
    {
        dataItem = _dataSource._inputField.text.ToString();
    }

    public void SaveSkill()
    {
        accountManager.localUserAccount.SaveSkill(_addNewSkillData);
    }

    public void SaveSkillWithFirebase()
    {
        firebaseDatabaseManager.CallSendSkills(_addNewSkillData.Name, _addNewSkillData.LevelName, _addNewSkillData.Level);
    }

    public void SaveArtifact()
    {
        accountManager.localUserAccount.SaveArtifact(_addNewArtifactData);
    }

    public void SaveArtifactWithFirebase()
    {
        firebaseDatabaseManager.CallSendArtifacts(_addNewArtifactData.type.ToString(), _addNewArtifactData.Title, _addNewArtifactData.Description, _addNewArtifactData.URL, _addNewArtifactData.ArtificatContent, _addNewArtifactData.Skills, _addNewArtifactData.Experiences);
    }

    public void SaveReference()
    {
        accountManager.localUserAccount.SaveReference(_addNewReferenceData);
    }

    public void SaveReferenceWithFirebase()
    {
        firebaseDatabaseManager.CallSendReferences(_addNewReferenceData.Name, _addNewReferenceData.Email, _addNewReferenceData.Position, _addNewReferenceData.PhoneNumber, _addNewReferenceData.Skills);
    }

    public void SaveExperience()
    {
        accountManager.localUserAccount.SaveExperience(_addNewExperienceData);
    }

    public void SaveExperienceWithFirebase()
    {
        firebaseDatabaseManager.CallSendExperiences(_addNewExperienceData.ExperienceLocale, _addNewExperienceData.RoleInExperience, _addNewExperienceData.StartDate.ToShortDateString(), _addNewExperienceData.EndDate.ToShortDateString(), _addNewExperienceData.Description, _addNewExperienceData.Comments, _addNewExperienceData.Skills/*, _addNewExperienceData.CourseOccured*/);
    }

    public List<string> getDatabaseReference(List<ReferenceData> _references)
    {
        List<string> _data = new List<string>();
        _data.Add("Not Applicable");

        for (int i = 0; i < _references.Count; i++)
        {
            _data.Add(_references[i].Name);
        }

        return _data;
    }

    public List<string> getDatabaseExperiences(List<ExperienceData> _experiences)
    {
        List<string> _data = new List<string>();
        _data.Add("Not Applicable");

        for (int i = 0; i < _experiences.Count; i++)
        {
            _data.Add(_experiences[i].RoleInExperience);
        }

        return _data;
    }

    public List<string> getDatabaseArtifacts(List<ArtifactData> _artifacts)
    {
        List<string> _data = new List<string>();
        _data.Add("Not Applicable");

        for (int i = 0; i < _artifacts.Count; i++)
        {
            _data.Add(_artifacts[i].ArtificatContent);
            _data.Add(_artifacts[i].Title);
        }

        return _data;
    }

    public List<string> getDatabaseSkills(List<SkillData> _skillData)
    {
        List<string> _data = new List<string>();
        _data.Add("Not Applicable");

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

    public void CreateWorkCoach(Image image, string speech)
    {
        Image workCoachImage = WorkCoachPrefab.transform.GetComponentInChildren<Image>();
        workCoachImage = image;

        TMP_Text speechText = WorkCoachPrefab.transform.GetComponentInChildren<TMP_Text>();
        speechText.text = speech;

        CreatePrefab(WorkCoachPrefab);
    }

    public void CreateSkillsDropDown()
    {
        CreatePrefab(DropDownSkillsPrefab);
    }

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

    public void CreateURLButtons(List<string> contents, List<UnityAction> _events)
    {
        Transform ButtonHolder = CreatePrefab(EditURLButtonPrefab);

        for (int i = 0; i < contents.Count; i++)
        {
            Transform _button = CreateURLButton(contents[i], _events[i]);
        }
    }

    public Transform CreateURLButton(string content, UnityAction _event)
    {
        Transform ButtonHolder = CreatePrefab(EditURLButtonPrefab);
        ButtonHolder.GetComponent<HorizontalLayoutGroup>().padding.bottom = 0;
        URLButtonPrefab.GetComponentInChildren<TMP_Text>().text = content;
        Transform _buttonCreated = CreatePrefab(URLButtonPrefab, ButtonHolder);
        _buttonCreated.GetComponent<Button>().onClick.AddListener(_event);
        return _buttonCreated;
    }

    /*public void CreateURLText(string text, string url)
    {
        URLButtonPrefab.GetComponentInChildren<TMP_Text>().text = text;
        //URLButtonPrefab.GetComponent<Button>().onClick

        Transform URLButtonHolder = CreatePrefab(EditURLButtonPrefab);

        *//*public void CreateButtons(List<string> contents, List<UnityAction> _events)
        {
            
        }*/

    /*Transform ButtonHolder = CreatePrefab(EditButtonPrefab);

    for (int i = 0; i < contents.Count; i++)
    {
        Transform _button = CreateButton(contents[i], _events[i], 255, 255, 255, 255, 255, 255, false);
        //Debug.Log(_button.name + i);
        if (i == 0)
            ContinueButton = _button.GetComponent<Button>();
        //ButtonPrefab.GetComponentInChildren<TMP_Text>().text = contents[i];
        //ButtonPrefab.GetComponent<Button>().onClick.AddListener(_events[i]);
        //CreatePrefab(ButtonPrefab, ButtonHolder);
    }*//*
}*/

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

    public ContentDataIdentiferUI CreateToggleCVList(List<string> content, bool forceSingleToggleGroup = false)
    {
        Transform _toggleGroupHolder = CreatePrefab(EditToggleListGroupPrefab);
        _toggleGroupHolder.GetComponent<ToggleGroup>().allowSwitchOff = !forceSingleToggleGroup;

        for (int i = 0; i < content.Count; i++)
        {
            EditToggleItemPrefab.GetComponentInChildren<TMP_Text>().text = content[i];
            EditToggleItemPrefab.GetComponentInChildren<Toggle>().isOn = false;
            if (forceSingleToggleGroup == true)
                EditToggleItemPrefab.GetComponentInChildren<Toggle>().group = _toggleGroupHolder.GetComponent<ToggleGroup>();
            else
                EditToggleItemPrefab.GetComponentInChildren<Toggle>().group = null;


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
            Transform _button = CreateButton(contents[i], _events[i], 255, 255, 255, 255, 255, 255, false);
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

    public Transform CreateButton(string content, UnityAction _event, float r, float g, float b, float tr, float tg, float tb, bool overrideCheck = true, bool interactableCheck = true)
    {
        Transform ButtonHolder = CreatePrefab(EditButtonPrefab);
        ButtonHolder.GetComponent<HorizontalLayoutGroup>().padding.bottom = 0;
        ButtonPrefab.GetComponentInChildren<TMP_Text>().text = content;
        ButtonPrefab.GetComponentInChildren<TMP_Text>().color = new Color(tr, tg, tb); //text colours
        ButtonPrefab.GetComponentInChildren<Image>().color = new Color(r, g, b); //button colours
        Transform _buttonCreated = CreatePrefab(ButtonPrefab, ButtonHolder);
        _buttonCreated.GetComponent<Button>().onClick.AddListener(_event);
        if (overrideCheck)
            ContinueButton = _buttonCreated.GetComponent<Button>();

        if (interactableCheck)
        {
            _buttonCreated.GetComponent<Button>().interactable = true;
        }
        else
        {
            _buttonCreated.GetComponent<Button>().interactable = false;
        }
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
            CreateDisplayGroup("<b><align=center>You have not added any skills yet. Press '+' to begin.</align></b>");
            for (int i = 0; i < BottomBannerButtons.Length; i++)
            {
                BottomBannerButtons[i].interactable = false;
            }
            return;
        } else
        {
            for (int i = 0; i < BottomBannerButtons.Length; i++)
            {
                BottomBannerButtons[i].interactable = true;
            }
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
        EditButton.SetActive(false);
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
        EditButton.SetActive(false);
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
        EditButton.SetActive(false);
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

    public void GetSkillsLearning(int index)
    {
        //TODO: Add in the skill definition etc; this will be dependant on the skill that is selected.
        //Max is 61
        switch (index)
        {
            case 0:
                learningSkill = "Ability to accept Criticism";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 1:
                learningSkill = "Active Listening to Others";
                learningSkillDefinition = "Active listening involves listening with all senses, focusing on the verbal and non-verbal communication of the person who is speaking, and demonstrating that you are engaged with the conversation. It is the process of listening attentively while someone else speaks without inturrupting, and then paraphrasing and reflecting back what is said, and withholding judgment and advice.";
                if (videoAudio)
                {
                    CreateURLButton("The Verywell Mind Podcast with Amy Morin on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/3otb0ywmfUEQjok2w1on1q?si=mhqviIuERBCPQ8maJGsyqw&nd=1"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Active Listening: The Art of Empathetic Conversation - Article", delegate { Application.OpenURL("https://positivepsychology.com/active-listening/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("How Good a Listener Are You? - TraningCourseMaterial.com", delegate { Application.OpenURL("https://www.trainingcoursematerial.com/free-assessment-tools/how-good-a-listener-are-you-quiz"); });
                }

                if (miniGames)
                {
                    CreateURLButton("7 Targeted Active Listening Games, Exercises and Activities for Adults by TheGLSProject", delegate { Application.OpenURL("https://www.goodlisteningskills.org/active-listening-games-exercises-activities/"); });
                    CreateURLButton("Active Listening Exercises: 10 Team Activities to Improve Active Listening by Minute Leader", delegate { Application.OpenURL("https://www.the10minuteleader.com/active-listening-exercises-10-team-activities-to-improve-active-listening/"); });
                }
                break;
            case 2:
                learningSkill = "Adaptability";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 3:
                learningSkill = "Analytical Thinking";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 4:
                learningSkill = "Applied Knowledge";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 5:
                learningSkill = "Approachableness";
                learningSkillDefinition = "Ability to engage involving or encouraging friendliness or pleasant companionship with other people.";
                if (videoAudio)
                {
                    CreateURLButton("Social Skills Coaching <br><b>Podcast</b><br> by Patrick King on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/0P8evY8Hm9Q8rOryrgbe7k"); });
                    CreateURLButton("Social Skills Mastery <br><b>Podcast</b><br> by Susan Callender on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/5NbNq5iMFmjErQtyW3m1e4"); });
                    CreateURLButton("The Social Skills Lab <br><b>Podcast</b><br> by Quick Social Skills Tips For You on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/1hbgk44GnbSB3QeQphV3ce"); });
                    CreateURLButton("10 Tips To Improving Your Social Skills <br><b>Podcast</b><br> by Sylvester Mack on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/7CHmL0yS21pfyeueu7owaS"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Social Skills as Exercise by Justin Mares", delegate { Application.OpenURL("https://www.linkedin.com/pulse/20141003232108-67056463-social-skills-as-exercise/"); });
                    CreateURLButton("102 Conversation Starters for Every Situation by Korra Shay", delegate { Application.OpenURL("https://korrashay.com/2020/10/08/102-conversation-starters-for-every-situation/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("Are You A People Person by TrainingCourseMaterial.com", delegate { Application.OpenURL("https://www.trainingcoursematerial.com/free-assessment-tools/are-you-a-people-person-questionnaire/"); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 6:
                learningSkill = "Argumentation/ Discussion Skills";
                learningSkillDefinition = "The ability to to <b>interact and respond to what other people are saying</b>. Talking with confidence and letting others speak without inturrpution or talking over them. Building on what others say, by critical thinking and asking thoughtful questions.";
                if (videoAudio)
                {
                    CreateURLButton("The Art of Negotiation: How to Get More of What You Want<br><b> Podcast </b><br> by Think Fast, Talk Smart: Communication Techniques on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/0xIYkNWLVkArj7OFuD375N"); });
                    CreateURLButton("Disagree Better<br><b> Podcast </b><br> by Tammy Lenski on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/3btjuyoOtF9k1nQNPUspV0"); });
                    CreateURLButton("Science of Persuasion<br><b> Video </b><br> by InfluenceAtWork.com on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=cFdCzN7RYbw"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Negotiation: 5 Ways to Hone Your Skill<br><b> Blog </b><br> by Jeremy Clark", delegate { Application.OpenURL("https://jeremyclark.com/negotiation-5-ways-to-hone-your-skill/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 7:
                learningSkill = "Attention to Detail";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 8:
                learningSkill = "Authenticity";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 9:
                learningSkill = "Business and Commercial Awareness";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 10:
                learningSkill = "Career Management";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 11:
                learningSkill = "Communicativeness";
                learningSkillDefinition = "Ability to conduct effective and efficient communication which includes getting your message across clearly and managing challenging conversations with positive outcomes.";
                if (videoAudio)
                {
                    CreateURLButton("How Can I Say This... <br><b>Podcast</b><br> Beth Buelow on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/55AaTIqnERpvxwTVMxToC5"); });
                    CreateURLButton("The Jounrey to Mastery: How Self Reflection Can Improve Communication <br><b>Podcast</b><br> Think Fast, Talk Smart: Communication Techniques on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/1h6kl8LFAVNFOG2nSy14Ky"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("The Accidential Communicator <br><b>Blog</b>", delegate { Application.OpenURL("http://theaccidentalcommunicator.com/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Communication Origami<br>TrainingCourseMaterial.com", delegate { Application.OpenURL("https://www.trainingcoursematerial.com/free-games-activities/communication-skills-activities/communication-origami"); });
                }
                break;
            case 12:
                learningSkill = "Confidence";
                learningSkillDefinition = "Ability to trust in your own judgment, capacities and abilities, not with arrogance but in a realistic and secure way.";
                if (videoAudio)
                {
                    CreateURLButton("7 Habits of Highly Confident People and 7 Ways to Build Self Confidence<br><b>Podcast</b><br> On Purpose with Jay Shetty on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/55AaTIqnERpvxwTVMxToC5"); });
                    CreateURLButton("5 Keys To Build Your Confidence<br><b>Podcast</b><br>The Mindset Mentor on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/7HQCircvSvsuKqqD5p6bHt"); });
                    CreateURLButton("Tony Robbins: Creating Unstoppable Self Confidence<br><b>Podcast</b><br>Motiv8 - The Motivation and Inspiration Podcast on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/7ueK61ZWOyuGiv8MuTxtan"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("18 Tips to Boost Confidence Right Now, Because You're Awesome<br><b>Artcle</b><br>Alexandra Duron", delegate { Application.OpenURL("https://greatist.com/grow/easy-confidence-boosters#Confidence-boosting-tips"); });
                    CreateURLButton("25 Killer Actions to Boost Your Self Confidence<br><b>Artcle</b><br>Leo Babauta", delegate { Application.OpenURL("https://zenhabits.net/25-killer-actions-to-boost-your-self-confidence/"); });
                    CreateURLButton("Your 7 Day Confidence Plan<br><b>Artcle</b><br>Realbuss Team", delegate { Application.OpenURL("https://www.realbuzz.com/articles-interests/health/article/your-7-day-confidence-plan/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 13:
                learningSkill = "Confidentiality";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 14:
                learningSkill = "Conflict Resolution";
                learningSkillDefinition = "Ability to remain calm, non-defensive and respectful during conflict. It involves having the capacity to empathize with other’s viewpoint, and having the ability to seek compromise and collaboration.";
                if (videoAudio)
                {
                    CreateURLButton("5 Conflict Resolution Techniques<br><b>Podcast</b><br> The Brendon Show on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/0HmaH9xfqAgXc5qMhwk4pA"); });
                    CreateURLButton("Skill 1: Win-Win - Partners or Opponents?<br><b>Podcast</b><br>Resolve Conflict: Everyone Can Win on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/6ZsmDBi0f2yWyV77EuvQNs"); });
                    CreateURLButton("Conflict Resolution Activities: A Mental Trick for Getting Out of Our Own Way<br><b>Podcast</b><br>Disagree Better on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/3EUKwlXvj0Qus6jzE8oCdS"); });
                    CreateURLButton("Conflict Resolution is like driving at night in the fog<br><b>Podcast</b><br>Disagree Better on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/2jsBkTLL0moI1v6VdEbqwa"); });
                    CreateURLButton("How to Resolve Conflict in the Workplace<br><b>Podcast</b><br>The Leadership 480 Podcast Series on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/5cUurSL43AJGvxJOqDWh06"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("What's Your Conflict Management Style?", delegate { Application.OpenURL("https://quiz.tryinteract.com/#/5abec210dfbc8e0014bd665a"); });
                }

                if (miniGames)
                {
                    CreateURLButton("Conflict Management Assessment<br><b>Google Play Store</b>", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.bl.conflictmanagementassessment"); });
                }
                break;
            case 15:
                learningSkill = "Creative/ Innovative Thinking";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 16:
                learningSkill = "Critical Thinking";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("What is Critical Thinking?<br><b>Video</b><br>GCFLearnFree.org on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=-eEBuqwY-nE&ab_channel=GCFLearnFree.org"); });
                    CreateURLButton("Critical Thinking and Problem Solving<br><b>Webinar Video</b><br>UMN College of Continuing & Professional Studies on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=qSfq3BQjKN4&ab_channel=UMNCollegeofContinuing%26ProfessionalStudies"); });
                    CreateURLButton("This tool will help improve your critical thinking<br><b>Video</b><br>Erick Wilberding, TED-Ed on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=vNDYUlxNIAA&ab_channel=TED-Ed"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Center for Critical Thinking Community<br><b>Article</b><br>The Critical Thinking Community", delegate { Application.OpenURL("https://www.criticalthinking.org/pages/defining-critical-thinking/766"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 17:
                learningSkill = "Customer Service";
                learningSkillDefinition = "Ability to provide support to both prospective and existing customers with Professionalism, Patience and ‘People First’ attitude.";
                if (videoAudio)
                {
                    CreateURLButton("Speaking Without a Net: How to Master Impromptu Communication<br><b>Podcast</b><br>Think Fast, Talk Smart: Communication Techniques on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/0r5eLIrMso6vbsvXtaRpa2"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Improv Encyclopedia<br><b>Blog</b>", delegate { Application.OpenURL("http://improvencyclopedia.org/"); });
                    CreateURLButton("Customer Service phrases that will help in any tough situation<br><b>Article</b><br>Rebecca Sewkarran, TheJobNetwork.com", delegate { Application.OpenURL("https://www.thejobnetwork.com/customer-service-phrases-that-will-help-in-a-tough-situation-wc/?utm_content=buffer7a1a6&utm_medium=social&utm_source=pinterest.com&utm_campaign=buffer"); });
                    CreateURLButton("10 Words to Avoid in Customer Service - and Alternatives<br><b>Article</b><br>Cutting for Business", delegate { Application.OpenURL("https://cuttingforbusiness.com/10-words-to-avoid-in-customer-service-and-alternatives/"); });
                    CreateURLButton("18 Tips For Being Confident From Within<br><b>Article</b><br>Tony Robbins", delegate { Application.OpenURL("https://www.tonyrobbins.com/building-confidence/how-to-be-confident/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 18:
                learningSkill = "Decision Making";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Headspax - Meditation, Mindfulness & Motivation<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.bagarwa.myapplication"); });
                }
                break;
            case 19:
                learningSkill = "Digital Literacy";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 20:
                learningSkill = "Diplomacy";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 21:
                learningSkill = "Discipline";
                learningSkillDefinition = "The way of effective fulfilling tasks, where person is able to control themselves to act in accordance with expectations, sticks to the plan, is resistant to situational and environmental factors and is able to complete the task of action. ";
                if (videoAudio)
                {
                    CreateURLButton("How to Be More Disciplined - 6 Ways to Master Self Control<br><b>Video</b><br>Thomas Frank on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=X3vRK2P9lSU"); });
                    CreateURLButton("How To Build Self Discipline<br><b>Video</b><br>Marcus Aurelius, Philosophies for Life on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=njDLNt-1ugM"); });
                    CreateURLButton("How to Practice Self Discipline?<br><b>Video</b><br>21st Century Skills on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=3PFUnf2YFn8"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("5 Ways To Improve Self-Discipline at Work<br><b>Article</b><br>Indeed Editorial Team", delegate { Application.OpenURL("https://www.indeed.com/career-advice/career-development/self-discipline"); });
                    CreateURLButton("The Most Important Self-Discipline Skills (With Examples)<br><b>Article</b><br>Chris Kolmar, Zippia", delegate { Application.OpenURL("https://www.zippia.com/advice/self-discipline-skills-2/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Daily Self Discipline Book<br>MixLabPro<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.mixlabpro.self_discipline"); });
                    CreateURLButton("Discipline<br>Sergey Podatelev<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.alabama.discipline"); });
                    CreateURLButton("Self Discipline Guide - Build Your Self Discipline<br>Site Master<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=selfdisciplineguide.sitemaster.selfdisciplineguide"); });
                    CreateURLButton("365 Days With Self-Discipline<br>Tutorials Ground<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.tutorialsground.self_discipline"); });
                }
                break;
            case 22:
                learningSkill = "Flexibility";
                learningSkillDefinition = "Easy and hassle free adjusting to changes in plans, tasks at work. ";
                if (videoAudio)
                {
                    CreateURLButton("Thinking Skills: Flexibility<br><b>Video</b><br>LearningWorks4Kids on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=5vgwp8nrY0U"); });
                    CreateURLButton("Work Flexibility<br><b>Video</b><br>APA Psychologically Healthy Workplace Program on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=oOSTXUGWpg0"); });
                    CreateURLButton("10 Soft Skills | Module 7 | Adaptability/ Flexibility<br><b>Video</b><br>Teach International Support on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=uQFublFUp2A"); });
                    CreateURLButton("Module 1: 10 Soft Skills You Need<br><b>Misc</b><br>Alison.com", delegate { Application.OpenURL("https://alison.com/topic/learn/94473/adaptabilite-souplesse"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("How To Be Flexible At Work<br><b>Article</b><br>Harappa Diaries", delegate { Application.OpenURL("https://harappa.education/harappa-diaries/workplace-flexibility-skills/"); });
                    CreateURLButton("How To Be Flexible At Work (With Tips and Examples)<br><b>Article</b><br>Indeed Editorial Team", delegate { Application.OpenURL("https://www.indeed.com/career-advice/career-development/how-to-be-flexible-at-work"); });
                    CreateURLButton("The Most Important Flexibility Skills (with Examples)<br><b>Article</b><br>Chris Kolmar, Zippia", delegate { Application.OpenURL("https://www.zippia.com/advice/flexibility-skills/"); });
                    CreateURLButton("How to Be Flexible At Work<br><b>Article</b><br>Mind Tools", delegate { Application.OpenURL("https://www.mindtools.com/pages/article/flexibility-at-work.htm"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Big Brain - Functional Brain Training<br>PocketLand<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.leodesol.games.big.brain.training.puzzle&gl=NO"); });
                    CreateURLButton("MindPal - Brain Training<br>Elektron Labs Inc.<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.elektron.mindpal"); });
                }
                break;
            case 23:
                learningSkill = "Good Judgement";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 24:
                learningSkill = "Growth Orientation";
                learningSkillDefinition = "Growth-orientation is a disposition of self-improvement by setting new and challenging goals for oneself. It is associated with a growth mindset, which means believing that one’s skills can improve over time thanks to hard work. ";
                if (videoAudio)
                {
                    CreateURLButton("The 7 Essential Pillars of Personal Development<br><b>Video</b><br>Brian Tracy on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=AWGayyX9I6o"); });
                    CreateURLButton("4 Steps to Developing a Growth Mindset<br><b>Video</b><br>Quality Insolvency Services Ltd on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=aNHas97iE78"); });
                    CreateURLButton("11 Growth Mindset Strategies: Overcome Your Fix Mindset to Grow as a Person<br><b>Video</b><br>Develop Good Habits on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=g7u6UwtmGyE"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Five Signs of Growth-Oriented Individuals<br><b>Article</b><br>Harshit Tyagi, Medium.com", delegate { Application.OpenURL("https://medium.com/swlh/five-signs-of-growth-oriented-individuals-here-is-what-you-need-to-change-9ec95a7bac19"); });
                    CreateURLButton("A Summary of Growth and Fixed Mindsets<br><b>Article</b><br>Carol Dweck", delegate { Application.OpenURL("https://fs.blog/carol-dweck-mindset/"); });
                    CreateURLButton("Growth Mindset vs Fixed Mindset: How what you think affects what you achieve<br><b>Article</b><br>Jennifer Smith, Mindset Health", delegate { Application.OpenURL("https://www.mindsethealth.com/matter/growth-vs-fixed-mindset"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("The Growth Mindset<br>Coursera", delegate { Application.OpenURL("https://www.coursera.org/learn/growth-mindset"); });
                    CreateURLButton("Personal and Professional Development: Growth Mindset<br>Virtual College", delegate { Application.OpenURL("https://www.virtual-college.co.uk/courses/professional/personal-professional-development-growth-mindset"); });
                    CreateURLButton("Developing A Growth Mindset<br>SkillSoft", delegate { Application.OpenURL("https://www.skillsoft.com/course/developing-a-growth-mindset-d2606e30-3352-11e8-94be-db6d3766eb17"); });
                }

                if (miniGames)
                {
                    CreateURLButton("Good App, Self Improvement App for Beginners<br>Team GoodApp<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=in.goodapps.besuccessful"); });
                    CreateURLButton("JoyupGenie: Personal Growth<br>Staticloop<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.staticloop.jug"); });
                }
                break;
            case 25:
                learningSkill = "Independence at Work";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 26:
                learningSkill = "Initiative";
                learningSkillDefinition = "Independent setting of tasks and goals at work, doing things without being asked, solving not obvious problems and undertaking own actions and activities.";
                if (videoAudio)
                {
                    CreateURLButton("Newcomers and the Workplace: Taking the Initiative<br><b>Video</b><br>Alberta Workforce Essential Skills on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=F45Mw6LB4XI"); });
                    CreateURLButton("1. What is Initiative<br><b>Video</b><br>Digital Learning Pills International on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=Cn30VRmXkn0"); });
                    CreateURLButton("25 Ways to Take Initiative at Work<br><b>Video</b><br>ThriveYard on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=FAjHsMCzjBw"); });
                    CreateURLButton("The Power of Taking Initiative and Being More Proactive - How to Take the Initiative<br><b>Video</b><br>Karma Rays on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=_-l7kNWEPGE"); });
                    CreateURLButton("The Science of Taking Action<br><b>Video</b><br>Steve Garguilo, TedxCarthage on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=hn9so1zVfR0"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("What Is Initiative And Why Is It Important?<br><b>Article</b><br>Laura-Jane Rawlings, Youth Employment UK", delegate { Application.OpenURL("https://www.youthemployment.org.uk/what-is-initiative-and-why-is-it-important/"); });
                    CreateURLButton("Taking Initiative<br><b>Article</b><br>MindTools", delegate { Application.OpenURL("https://www.mindtools.com/pages/article/initiative.htm"); });
                    CreateURLButton("How to improve initiative skills?<br><b>Article</b><br>MovieCultists", delegate { Application.OpenURL("https://moviecultists.com/how-to-improve-initiative-skills"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 27:
                learningSkill = "Leadership";
                learningSkillDefinition = "Leadership is the ability to motivate a group of people to act toward achieving a common goal. It involves setting direction, building an inspiring vision, and creating something new. Leadership is about mapping out where you need to go to succeed as a team or an organization; and it is dynamic, exciting, and inspiring.";
                if (videoAudio)
                {
                    CreateURLButton("#2 Michael Lombardi: Leadership on the Field<br><b>Podcast</b><br>The Knowledge Project on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/4jpI5rTLeyuRMg4Yo7uV7C?si=QNMCB0JURkK_ma0pHngcyQ&dl_branch=1&nd=1"); });
                    CreateURLButton("4 Tips to Improve Leadership Skills<br><b>Video</b><br>Brian Tracy on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=61OzhSrgsd8"); });
                    CreateURLButton("The Leadership Practitioner Podcast<br><b>Podcast</b><br>Leadership Practitioner on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/4pCmpN2wKpJdIu99qgvzjt"); });
                    CreateURLButton("Episode #1: Discovering Your Leadership Voice<br><b>Podcast</b><br>Inspired Leadership with Dr. Scott Vinciguerra on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/4aI8SP06NSz8fJZ20DkGwT"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("The 10 Minute Leader<br><b>Blog</b>", delegate { Application.OpenURL("https://www.the10minuteleader.com/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 28:
                learningSkill = "Listening";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 29:
                learningSkill = "Logical Reasoning";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("5 Games For Building Critical Thinking Skills<br><b>Blog</b><br>Micah Owens, CoolMath Games", delegate { Application.OpenURL("https://www.coolmathgames.com/blog/5-games-for-building-critical-thinking-skills"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 30:
                learningSkill = "Multitasking";
                learningSkillDefinition = "Easily switching attention and focus between task which makes an impression of working on a multiple tasks in the same time and leads to their effective fulfillments simultaneously.";
                if (videoAudio)
                {
                    CreateURLButton("What multitasking does to your brain<br><b>Video</b><br>BBC Ideas on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=tMiyzuO1qMs"); });
                    CreateURLButton("Multitasking: When to Do It, When (and How) to Avoid It<br><b>Video</b><br>Thomas Frank on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=Obf0tB285is"); });
                    CreateURLButton("Can You Really Multitask?<br><b>Podcast</b><br>Leadership Practitioner on Spotify", delegate { Application.OpenURL("https://www.youtube.com/watch?v=hEPCTFuuqgY"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Important Multitasking Skills Employers Value<br><b>Article</b>", delegate { Application.OpenURL("https://www.thebalancecareers.com/multitasking-skills-with-examples-2059692"); });
                    CreateURLButton("Split-tasking vs. Multitasking - The New Way to Get Things Done<br><b>Article</b>", delegate { Application.OpenURL("https://www.knowledgecity.com/blog/split-tasking-vs-multitasking-new-way-get-things-done/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("Willpower and Focus<br>Alison.com", delegate { Application.OpenURL("https://alison.com/course/willpower-and-focus"); });
                }

                if (miniGames)
                {
                    CreateURLButton("Multitask Brain Teaser - Equilibrium Games<br>IDC Games<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.idcgames.MultiTask"); });
                    CreateURLButton("Multitasking Brain Training<br>Otto Inspire<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.OttoInspire.MultitaskingBrainTraining"); });
                    CreateURLButton("Rocket Duo: Multitasking Brain Game<br>Shubham Sanglikar<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.shubhamsanglikar.rocketduo"); });
                }
                break;
            case 31:
                learningSkill = "Patience";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 32:
                learningSkill = "Persistance";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 33:
                learningSkill = "Planning";
                learningSkillDefinition = "The process of preparing the list of tasks and making decisions about the number and order of actions, their timeline, and ways of realization to achieve a proper and correct fulfillment of project or complex tasks.";
                if (videoAudio)
                {
                    CreateURLButton("Planning Skills for Managers: How To Be a Better Planner (Top 30 Tips)<br><b>Video</b><br>BizMove on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=KsereNgBIHM"); });
                    CreateURLButton("Thinking Skills: Planning<br><b>Video</b><br>LearningWorks4Kids on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=drj4TlfXN50"); });
                    CreateURLButton("How to Create an Effective Action Plan<br><b>Video</b><br>Brian Tracy on Spotify", delegate { Application.OpenURL("https://www.youtube.com/watch?v=haRCpUOCG_M"); });
                    CreateURLButton("How Can People Who Lack Planning Skills Improve Without Discouragement?<br><b>Video</b><br>Cal Newport on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=LvRV02dbNYU"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Planning<br><b>Article</b><br>CleverISM", delegate { Application.OpenURL("https://www.cleverism.com/skills-and-tools/planning/"); });
                    CreateURLButton("10 Planning Skills Every Child Should Learn<br><b>Article</b><br>Amy Sipply, Life Skills Advocate", delegate { Application.OpenURL("https://lifeskillsadvocate.com/blog/10-planning-skills-every-child-should-learn/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Trello", delegate { Application.OpenURL("https://trello.com/"); });
                    CreateURLButton("Asana", delegate { Application.OpenURL("https://asana.com/"); });
                    CreateURLButton("My Study Life - Digital School Planner You Need<br>My Study Life, Ltd.<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.virblue.mystudylife"); });
                    CreateURLButton("Planning Note<br>PlanBook<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.planningnote.standard"); });
                }
                break;
            case 34:
                learningSkill = "Presentation Skills";
                learningSkillDefinition = "Presenting information about yourself, your work and your viewpoint clearly and effectively.";
                if (videoAudio)
                {
                    CreateURLButton("Hacking your Speaking Anxiety: How Lessons from Neuroscience Can Help You Communicate Confidently<br><b>Podcast</b><br>Think Fast, Talk Smart: Communication Techniques on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/6424W5a7Yl5F43hoLdGa0H"); });
                    CreateURLButton("The Presentation Boss Podcast<br><b>Podcast</b><br>Presentation Boss on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/5danVXvLsKAoQMmLbe7TgH"); });
                    CreateURLButton("Presentation Skills - How to Engage Your Audience<br><b>Podcast</b><br>ted Learning Podcast on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/2q7UzwCyiwQAZAyyjqcgR9"); });
                    CreateURLButton("Presentation Skills - Public Speaking<br><b>Video</b><br>Enjoy Life on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/7FsDWq2Vd2skwifwinovtc"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Presentation Zen TIPS<br><b>Articles/ Blogs</b><br>Garr Reynolds", delegate { Application.OpenURL("https://www.garrreynolds.com/tips"); });
                    CreateURLButton("10 Tips for Achieving Presentation Zen on Stage<br><b>Article</b><br>Presentation Zen", delegate { Application.OpenURL("https://www.presentationzen.com/presentationzen/"); });
                    CreateURLButton("20 Ways to Improve Your Presentation Skills<br><b>Blog</b><br>Larry Kim, WordStream by LocaliQ", delegate { Application.OpenURL("https://www.wordstream.com/blog/ws/2014/11/19/how-to-improve-presentation-skills"); });
                    CreateURLButton("What Is Your Interview Body Language Saying About You?<br><b>Article</b><br>Ronda Suder, TopResume", delegate { Application.OpenURL("https://www.topresume.com/career-advice/what-is-your-body-language-saying-about-you-during-an-interview"); });
                    CreateURLButton("Make a Good Impression at Your First Job<br><b>Article</b><br>Dawn Rosenberg McKay, The Balance Careers", delegate { Application.OpenURL("https://www.thebalancecareers.com/your-first-job-524792"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("Dynamic Public Speaking Specialization<br>Coursera", delegate { Application.OpenURL("https://www.coursera.org/specializations/public-speaking"); });
                    CreateURLButton("Evaluate Your Current Level of Presentation Skills<br>TrainingCourseMaterial.com", delegate { Application.OpenURL("https://www.trainingcoursematerial.com/free-assessment-tools/evaluate-your-current-level-of-presentation-skills-quiz?view=quiz"); });
                }

                if (miniGames)
                {
                    CreateURLButton("Public Speaking Activities<br><b>Activity</b><br>Cel Amande on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=qImBpYxW4os"); });
                }
                    break;
            case 35:
                learningSkill = "Prioritising";
                learningSkillDefinition = "Determining the order of execution of tasks, the amount of time devoted to them based on their importance, urgency as well as time and personnel resources.";
                if (videoAudio)
                {
                    CreateURLButton("How to Prioritize<br><b>Video</b><br>MindToolsVideos on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=TRkSLJLh2NM"); });
                    CreateURLButton("How to Prioritize Tasks Effectively: GET THINGS DONE<br><b>Video</b><br>Ways To Grow on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=czh4rmk75jc"); });
                    CreateURLButton("How to Prioritize Tasks at Work [PRO PRIORITIZATION TECHNIQUES]<br><b>Video</b><br>Adriana Girdler on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=VQErD3oUwvM"); });
                    CreateURLButton("How Do You Prioritize Your Work | Interview Question<br><b>Video</b><br>Petra Pearce on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=RZ94GU30wps"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("How to prioritize work when everything's important<br><b>Article</b><br>Caitlin Bishop, WeWork Ideas", delegate { Application.OpenURL("https://www.wework.com/ideas/professional-development/creativity-culture/how-to-prioritize-work"); });
                    CreateURLButton("How to Prioritize Work When Everything Seems Important<br><b>Article</b><br>Jeremy Diamond, Lifehack", delegate { Application.OpenURL("https://www.lifehack.org/858070/how-to-prioritize"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Trello", delegate { Application.OpenURL("https://trello.com/"); });
                    CreateURLButton("Asana", delegate { Application.OpenURL("https://asana.com/"); });
                    CreateURLButton("Priority Matrix<br>Appfluence Inc<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.appfluence.prioritymatrix"); });
                    CreateURLButton("Task Manager - ToDo List<br>Rathi Developers<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.effective.task.manager"); });
                    CreateURLButton("Priority Manager<br>Alexandre M Montebelo<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.alexandre9865.priorizador"); });
                    CreateURLButton("Prioritize Me! - Goals & Todos<br>II Aliens<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.citrus.priority"); });
                    CreateURLButton("Big Brain - Functional Brain Training<br>PocketLand<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.leodesol.games.big.brain.training.puzzle&gl=NO"); });
                }
                break;
            case 36:
                learningSkill = "Problem Solving";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Lumosity: Brain Training<br>Lumos Labs, Inc.<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.lumoslabs.lumosity"); });
                    CreateURLButton("Peak - Brain Games & Training<br>PopReach Incorporated<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.brainbow.peak.app"); });
                    CreateURLButton("MindPal = Brain Training<br>Elektron Labs Inc.<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.elektron.mindpal"); });
                    CreateURLButton("Creative Everywhere<br>Ognev<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.ognev.creativity&hl=en&gl=US"); });
                    CreateURLButton("Big Brain - Functional Brain Traning<br>PocketLand<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.leodesol.games.big.brain.training.puzzle&gl=NO"); });
                }
                break;
            case 37:
                learningSkill = "Project Management";
                learningSkillDefinition = "Project management is primarily: planning, management and control skills.";
                if (videoAudio)
                {
                    CreateURLButton("12 Terms You Should Know | Project Management Fundamentals<br><b>Video</b><br>Psoda on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=qTQsdJFG4SQ"); });
                    CreateURLButton("Project Management for Beginners: A Simple Guide (2020)<br><b>Video</b><br>Psoda on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=oC9fUwQyriE"); });
                    CreateURLButton("Project Management 101 | Introduction to Project Management | Project Management Basics<br><b>Video</b><br>PMC Lounge on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=HDXkITHLZSI"); });
                    CreateURLButton("Project Management Tutorial (Complete Course)<br><b>Video</b><br>Nerd's Lesson on YouTube", delegate { Application.OpenURL("https://youtu.be/t7EYicEBfdQ"); });
                    CreateURLButton("What is project management?<br><b>Video</b><br>Association for Project Management on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=Jk-JwtScIlw"); });
                    CreateURLButton("Introduction to Project Management (2020)<br><b>Video</b><br>365 Careers on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=rBSCvPYGnTc"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Project Management Isn't Just For Project Managers: 4 Skills You Need To Know<br><b>Article</b><br>Dana Brownlee, Forbes", delegate { Application.OpenURL("https://www.forbes.com/sites/danabrownlee/2019/07/14/project-management-isnt-just-for-project-managers-4-skills-you-need-to-know/?sh=115376fa1a8e"); });
                    CreateURLButton("The Four Phases of Project Management<br><b>Article</b><br>HBR Editors, Harvard Business Review", delegate { Application.OpenURL("https://hbr.org/2016/11/the-four-phases-of-project-management"); });
                    CreateURLButton("Podcasts<br><b>Blog</b><br>PRINCE2.com", delegate { Application.OpenURL("https://www.prince2.com/uk/podcasts"); });
                    CreateURLButton("Project Selection Methods: A Primer for the Project Manager<br><b>Article</b><br>Project Management Academy", delegate { Application.OpenURL("https://projectmanagementacademy.net/articles/project-selection-methods/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("Project Management and Agile Simulations<br>SPL - Simulation Powered Learning", delegate { Application.OpenURL("https://simulationpl.com/"); });
                    CreateURLButton("Project Management Institute", delegate { Application.OpenURL("https://www.pmi.org/kickoff"); });
                    CreateURLButton("Google Project Management: Professional Certificate<br>Coursera", delegate { Application.OpenURL("https://www.coursera.org/professional-certificates/google-project-management"); });
                }

                if (miniGames)
                {
                    CreateURLButton("PMP Exam Terminology Crossworld Puzzle<br>Project Management Academy", delegate { Application.OpenURL("https://projectmanagementacademy.net/games/pmp-terminology-crossword-puzzle.php"); });
                    CreateURLButton("Manager Simulator<br>WeekDone", delegate { Application.OpenURL("https://weekdone.com/manager-simulator/"); });
                    CreateURLButton("Monday", delegate { Application.OpenURL("https://monday.com/"); });
                    CreateURLButton("Trello", delegate { Application.OpenURL("https://trello.com/"); });
                    CreateURLButton("Design: Gantt Chart<br>DK CONSULTING, TOV<br>Mac App Store", delegate { Application.OpenURL("https://apps.apple.com/pl/app/projekt-wykres-gantta/id1311323204?l=pl&mt=12"); });
                    CreateURLButton("Jira Cloud<br>Atlassian<br>Mac App Store", delegate { Application.OpenURL("https://apps.apple.com/app/jira-cloud-by-atlassian/id1475897096"); });
                    CreateURLButton("Tasks: To Do Lists & Planner<br>Mustafa Yusuf<br>Mac App Store", delegate { Application.OpenURL("https://apps.apple.com/pl/app/tasks-to-do-lists-planner/id1502903102?l=en"); });
                }
                break;
            case 38:
                learningSkill = "Propriety/ Personal Culture";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 39:
                learningSkill = "Providing Feedback";
                learningSkillDefinition = "The ability to give effective feedback that is timely, specific, realistic and balanced while being supportive.";
                if (videoAudio)
                {
                    CreateURLButton("The Essentials: Giving Feedback<br><b>Podcast</b><br>Women at Work on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/2fwDfXGTkY2duM53rqCXTM"); });
                    CreateURLButton("Ep.8: How to Give Feedback to Your Boss<br><b>Podcast</b><br>Radical Candor on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/6xZNH0bFI6GrV3LLDuflo7"); });
                    CreateURLButton("3 Simple Steps for How to Give Feedback that Improves your Team<br><b>Podcast</b><br>Creating High Performing Teams on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/0usmeFMCNBHKNm02cBWHCY"); });
                    CreateURLButton("Feedback Made Easy: Mastering the skill of giving feedback to your team<br><b>Podcast</b><br>No Bullsh!t Leadership on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/0FpLxSkkicdpBVhT1n2vvq"); });
                    CreateURLButton("Providing Effective Feedback<br><b>Podcast</b><br>Healthcare Communication on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/2joA9s7L2GuwC9cyVyC2xd"); });
                    CreateURLButton("Give Better Feedback<br><b>Podcast</b><br>The Insightful Leader on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/6qZjLsReTMv2rB6u0cg6KZ"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("How to Respond to a Bad Performance Review<br><b>Article</b><br>Dawn Rosenberg McKay, The Balance Careers", delegate { Application.OpenURL("https://www.thebalancecareers.com/bad-performance-review-524880"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 40:
                learningSkill = "Punctuality";
                learningSkillDefinition = "Punctuality means being on time: both to work and on deadlines.";
                if (videoAudio)
                {
                    CreateURLButton("What is Punctuality | Explained in 2 min<br><b>Video</b><br>Productivity Guy on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=Nvh16ao_D9Q"); });
                    CreateURLButton("5 Tips To Being Punctual<br><b>Video</b><br>Tero Trainers on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=opOu6N103sQ"); });
                    CreateURLButton("How to Avoid Being Late for School or Work >> 10 Tips to Be On Time<br><b>Video</b><br>Ways To Grow on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=1KwmSENS0qk"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Punctuality | Career Advice<br><b>Article</b><br>Robert Half Talent Solutions", delegate { Application.OpenURL("https://www.roberthalf.com.au/career-advice/career-development/punctuality-skills"); });
                    CreateURLButton("12 Tips for Being Punctual - How to Be On Time<br><b>Article</b><br>Marelisa, Daring to Live Fully", delegate { Application.OpenURL("https://daringtolivefully.com/tips-for-being-punctual"); });
                    CreateURLButton("How to Be On Time Every Time<br><b>Article</b><br>Dustin Wax, Lifehack", delegate { Application.OpenURL("https://www.lifehack.org/articles/featured/how-to-be-on-time-every-time.html"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 41:
                learningSkill = "Reading";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Elevate - Brain Training Games<br>Elevate Labs<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.wonder"); });
                }
                break;
            case 42:
                learningSkill = "Research Skills";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 43:
                learningSkill = "Responsibility/ Commitment";
                learningSkillDefinition = "Responsibility can be defined as a high level of commitment to one’s duties. Being responsible means taking accountability for one’s actions, words, and performance at work.";
                if (videoAudio)
                {
                    CreateURLButton("What Is Responsibility & Accountability At Work?<br><b>Video</b><br>Everything Sherry on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=42dLWo9v0HM"); });
                    CreateURLButton("How To Be Responsible and Accountable by Jeff Muir<br><b>Video</b><br>Jeff Muir on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=rXgPNTgC0dQ"); });
                    CreateURLButton("How To Hold Yourself Accountable (ACCOUNTABILITY FOR YOUR GOALS!)<br><b>Video</b><br>Howell Consultations on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=BSx7to7hT_w"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Professionalism: Personal Responsibility<br><b>Article</b><br>Stefan Jacobson, Conover", delegate { Application.OpenURL("https://www.conovercompany.com/professionalism-personal-responsibility/"); });
                    CreateURLButton("The Importance of Work Responsibility and How to Achieve More<br><b>Article</b><br>GlassDoor", delegate { Application.OpenURL("https://www.glassdoor.com/blog/guide/work-responsibility/"); });
                    CreateURLButton("5 Tips on How to be a More Responsible Person<br><b>Article</b><br>Djordje Todorovic, Lifehack", delegate { Application.OpenURL("https://www.lifehack.org/378031/5-tips-how-more-responsible-person"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("5 Corporate Social Responsibility Courses you can take for free<br>Global Peace Careers", delegate { Application.OpenURL("https://globalpeacecareers.com/magazine/corporate-social-responsibility-courses/"); });
                }

                if (miniGames)
                {
                    CreateURLButton("The Don't Do List<br>Feeling Game Company<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.feelinggame.no&hl=en&gl=US"); });
                }
                break;
            case 44:
                learningSkill = "Self-Awareness";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 45:
                learningSkill = "Self-Motivation";
                learningSkillDefinition = "Self-motivation is the internal state that helps you initiate and continue a goal-oriented activity, despite obstacles, until it is completed.";
                if (videoAudio)
                {
                    CreateURLButton("Importance of Self-Efficacy<br><b>Video</b><br>Transforming Education on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=VW5v6PQ5PEc"); });
                    CreateURLButton("How To Stay Motivated - The Locus Rule<br><b>Video</b><br>Improvement Pill on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=8ZhoeSaPF-k"); });
                    CreateURLButton("The psychology of self-motivation | Scott Geller | TedxVirginiaTech<br><b>Video</b><br>Tedx Talks on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=7sxpKhIbr0E"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Self Motivation: Staying Motivated to Reach Your Goals<br><b>Article</b><br>SoulSalt", delegate { Application.OpenURL("https://soulsalt.com/self-motivation/"); });
                    CreateURLButton("Self-Motivation Explained + 10 Ways To Motivate Yourself<br><b>Article</b><br>Courtney E. Ackerman, MA., PositivePsychology.com", delegate { Application.OpenURL("https://positivepsychology.com/self-motivation/"); });
                    CreateURLButton("Self-Motivation: Definition, Examples, and Tips<br><b>Article</b><br>Tchiki Davis, MA, PhD, Berkeley Well-Being Institute", delegate { Application.OpenURL("https://www.berkeleywellbeing.com/self-motivation.html"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("Motivation - Power Guide To Motivating Yourself & Others<br>Alison.com", delegate { Application.OpenURL("https://alison.com/course/motivation-power-guide-to-motivating-yourself-and-others"); });
                    CreateURLButton("Introduction to Self-Determination Theory: An approach to motivation, development and wellness<br>Richard Ryan, Coursera", delegate { Application.OpenURL("https://www.coursera.org/learn/self-determination-theory"); });
                    CreateURLButton("Motivation Mastery: How to get and stay Motivated<br>Skillshare, ClassCentral", delegate { Application.OpenURL("https://www.classcentral.com/course/skillshare-motivation-mastery-how-to-get-and-stay-motivated-34786"); });
                }

                if (miniGames)
                {
                    CreateURLButton("What Are Self-Motivational Games and Their Types?<br>Medium.com", delegate { Application.OpenURL("https://medium.com/gameful-life/what-are-self-motivational-games-and-their-types-e2cb86fdcf5d"); });
                    CreateURLButton("Pomodoro Technique<br>Wikipedia", delegate { Application.OpenURL("https://en.wikipedia.org/wiki/Pomodoro_Technique"); });
                    CreateURLButton("Elevate - Brain Training Games<br>Elevate Labs<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.wonder"); });
                    CreateURLButton("Make Me Better - Motivation App<br>Crafty Studio<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=app.story.craftystudio.shortstory&hl=en&gl=US"); });
                }
                break;
            case 46:
                learningSkill = "Self-Presentation";
                learningSkillDefinition = "Self-presentation is behavior with which people try to affect how they are perceived and judged by others; much social behavior is influenced by self-presentational motives and goals (Miller & Rowland, 2019).";
                if (videoAudio)
                {
                    CreateURLButton("How to introduce yourself | Kevin Bahler | TedxLehighRiver<br><b>Video</b><br>Tedx Talks on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=V1xt7zgnuK0"); });
                    CreateURLButton("Self-Presentation... What is it?<br><b>Video</b><br>Psych2Go on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=OHlJNWDLWSY"); });
                    CreateURLButton("PSY 2510 Social Psychology: Self-Presentation<br><b>Video</b><br>Frank M. LoSchiavo on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=sjzsTHmBZXo"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("Poselennova, O. A. ., Kovardakova , M. A. ., Aryabkina, I. V. ., Chernova , Y. A. ., & Ravodin, K. O. . (2021). Creative self-presentation skills among students in the educational process of a higher education institution. Revista Eduweb, 15(2), 208–228. https://doi.org/10.46502/issn.1856-7576/2021.15.02.17", delegate { Application.OpenURL("https://revistaeduweb.org/index.php/eduweb/article/view/377"); });
                    CreateURLButton("Schlenker, B. R. (2012). Self-presentation. In M. R. Leary & J. P. Tangney (Eds.), Handbook of self and identity (pp. 542–570). The Guilford Press.", delegate { Application.OpenURL("https://psycnet.apa.org/record/2012-10435-025"); });
                    CreateURLButton("Social Phobia: Diagnosis, Assessment, and Treatment<br><b>Book</b><br>Richard G. Heimberg, Google Books", delegate { Application.OpenURL("https://books.google.pl/books?id=rXrekuSy2bsC&lpg=PA94&ots=gTbL7jC_bk&dq=self%20presentation&lr&hl=pl&pg=PA96#v=onepage&q=self%20presentation&f=false"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("My Dreams: Self Improvement & Life Coach<br>Self Help Apps<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.MyDreams.MyDreams"); });
                    CreateURLButton("Presentation Skills<br>MSPLDevelopers<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=quick.presentation.skills&hl=en&gl=US"); });
                }
                break;
            case 47:
                learningSkill = "Self-Reflection";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 48:
                learningSkill = "Sense of Humor";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 49:
                learningSkill = "Social Media Management";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 50:
                learningSkill = "Speaking";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 51:
                learningSkill = "Speaking Fluency";
                learningSkillDefinition = "Ability to speak easily, reasonably quickly and without having to stop and pause a lot in your language of communication.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("7 Tips And Activities To Promote Speaking Fluency With Your Adult ESL Students<br><b>Article</b><br>Everyday ESL Language Resources", delegate { Application.OpenURL("https://everydayesl.com/blog/speaking-fluency"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 52:
                learningSkill = "Specialistic Industry Skills";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 53:
                learningSkill = "Stamina";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 54:
                learningSkill = "Statistical Skills";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 55:
                learningSkill = "Stress Management";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("The Verywell Mind Podcast with Amy Morin<br><b>Podcast</b><br>Amy Morin on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/2WGnJfJon5RKU9bXBUSWHU"); });
                    CreateURLButton("The Verywell Mind Podcast with Amy Morin (Video Version)<br><b>Podcast</b><br>Verywell Mind on Spotify", delegate { Application.OpenURL("https://open.spotify.com/show/1MP1aFuU65EHyIcCSPNiCB"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("The Art of Coping: Strategies and Skills to Help Your Clients Cope<br>Article<br>Jeremy Sutton, Ph.D., PositivePsychology.com", delegate { Application.OpenURL("https://positivepsychology.com/coping-strategies-skills/"); });
                    CreateURLButton("What is Emotion Regluation? + 6 Emotional Skills and Strategies<br>Article<br>Madhuleena Roy Chowdhury, BA, PositivePsychology.com", delegate { Application.OpenURL("https://positivepsychology.com/emotion-regulation/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Wysa: Anxiety, therapy chatbot<br>Touchkin<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=bot.touchkin&hl=en_GB&gl=US"); });
                }
                break;
            case 56:
                learningSkill = "Teamwork";
                learningSkillDefinition = "Teamwork is the collaborative effort of a group to achieve a common goal or to complete a task in the most effective and efficient way.";
                if (videoAudio)
                {
                    CreateURLButton("Perfecting Teamwork: Building High-Performing Teams By Encouraging Learning<br><b>Podcast</b><br>Think Fast, Talk Smart: Communication Techniques on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/7pHpUwOMGOC2FXcikLvySt"); });
                    CreateURLButton("Team Management | The No Bullsh*t Guide to Managing a Team<br><b>Podcast</b><br>The No Bullsh*t Guide to a Happier Life on Spotify", delegate { Application.OpenURL("https://open.spotify.com/episode/2Q4j9UfVYbgYrJxxYvc5xQ"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("7 Important Teamwork Skills You Need in School and Your Career<br>Article<br>Herzing University", delegate { Application.OpenURL("https://www.herzing.edu/blog/7-important-teamwork-skills-you-need-school-and-your-career"); });
                    CreateURLButton("Employability Skills<br>Article<br>Wigan & Leigh College", delegate { Application.OpenURL("https://libguides.wigan-leigh.ac.uk/c.php?g=667800&p=4736457"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Free Team Building Activities<br>TrainerBubble", delegate { Application.OpenURL("https://www.trainerbubble.com/downloads/category/free-training-resources/free-team-building-activities/"); });
                }
                break;
            case 57:
                learningSkill = "Time Management";
                learningSkillDefinition = "Time management refers to the planning, prioritizing, and scheduling of tasks to create work efficiency in an environment of competing demands.";
                if (videoAudio)
                {
                    CreateURLButton("How to manage your time more effectively (according to machines) - Brian Christian<br><b>Video</b><br>Ted-Ed on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=iDbdXTMnOmE"); });
                    CreateURLButton("Jordan Peterson's Ultimate Advice for Students and College Grads - STOP WASTING TIME<br><b>Video</b><br>Motivation2Study on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=wsNzAuYDgy0"); });
                    CreateURLButton("How I Manage My Time - 10 Time Management Tips<br><b>Video</b><br>Ali Abdaal on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=iONDebHX9qk"); });
                    CreateURLButton("Time Management<br><b>Video</b><br>Tutorials Point (India) Ltd. on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=KJLHlOIdqA4"); });
                    CreateURLButton("The Philosophy of Time Management | Brad Aeon | TedxConcordia<br><b>Video</b><br>Tedx Talks on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=WXBA4eWskrc"); });
                    CreateURLButton("Timeboxing: Elon Musk's Time Management Method<br><b>Video</b><br>Thomas Frank on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=fbAYK4KQrso"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("6 time management techniques for time poor professionals<br><b>Article</b><br>Robert Half Talent Solutions", delegate { Application.OpenURL("https://www.roberthalf.com.au/blog/jobseekers/6-time-management-techniques-time-poor-professionals"); });
                    CreateURLButton("Time Management Is About More Than Life Hacks<br><b>Article</b><br>Erich C. Dierdorff, Harvard Business Review", delegate { Application.OpenURL("https://hbr.org/2020/01/time-management-is-about-more-than-life-hacks"); });
                    CreateURLButton("Articles on Time Management<br><b>Article(s)</b><br>MSG - Management Study Guide", delegate { Application.OpenURL("https://www.managementstudyguide.com/time-management-articles.htm"); });
                    CreateURLButton("Time management skills that improve student learning<br><b>Blog</b><br>Australian Christian College", delegate { Application.OpenURL("https://www.acc.edu.au/blog/time-management-skills-student-learning/"); });
                }

                if (freeCourses)
                {
                    CreateURLButton("How to increase productivity at work<br>Google Digital Workshop", delegate { Application.OpenURL("https://learndigital.withgoogle.com/digitalworkshop-eu/course/increase-productivity"); });
                    CreateURLButton("A Mini Course on Time Management<br>Udemy", delegate { Application.OpenURL("https://www.udemy.com/course/manageyourtime/"); });
                    CreateURLButton("Time Management Courses<br>Alison.com", delegate { Application.OpenURL("https://alison.com/courses/personal-development/time-management"); });
                    CreateURLButton("Work Smarter, Not Harder: Time Management for Personal & Professional Productivity<br>University of California, Irvine via Coursera, Class Central", delegate { Application.OpenURL("https://www.classcentral.com/course/worksmarter-2727"); });
                }

                if (miniGames)
                {
                    CreateURLButton("Game Dev Story<br>Kairosoft<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=net.kairosoft.android.gamedev3en"); });
                    CreateURLButton("Cook It! - Cooking Games<br>Flowmotion Entertainment: Top Free Fun Addictive Cool Games Inc.<br>Apple App Store", delegate { Application.OpenURL("https://apps.apple.com/pl/app/cook-it-cooking-games/id1399688008?l=pl"); });
                    CreateURLButton("MasterChef: Let's Cook!<br>Tilting Point LLC<br>Apple App Store", delegate { Application.OpenURL("https://apps.apple.com/pl/app/masterchef-lets-cook/id1536038028?l=pl"); });
                    CreateURLButton("Focus City - Pomodoro Timer<br>Aravindham Parasuram<br>Apple Mac App Store", delegate { Application.OpenURL("https://apps.apple.com/pl/app/focus-city-pomodoro-timer/id1468463467?l=pl&mt=12"); });
                    CreateURLButton("Monday", delegate { Application.OpenURL("https://monday.com/"); });
                    CreateURLButton("Trello", delegate { Application.OpenURL("https://trello.com/"); });
                    CreateURLButton("Harvest", delegate { Application.OpenURL("https://www.getharvest.com/"); });
                    CreateURLButton("Toggl", delegate { Application.OpenURL("https://toggl.com/"); });
                    CreateURLButton("Session - Pomodoro Focus Timer<br>Philip Young Gunawan<br>Apple Mac App Store", delegate { Application.OpenURL("https://apps.apple.com/pl/app/session-pomodoro-focus-timer/id1521432881?l=pl"); });
                }
                break;
            case 58:
                learningSkill = "Work Ethic";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 59:
                learningSkill = "Work Under Pressure";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
            case 60:
                learningSkill = "Writing";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("Elevate - Brain Training Games<br>Elevate Labs<br>Google Play Store", delegate { Application.OpenURL("https://play.google.com/store/apps/details?id=com.wonder"); });
                }
                break;
            case 61:
                learningSkill = "Writing Skills";
                learningSkillDefinition = "Having knowledge of writing structure and ability to effectively communicate your ideas through writing with your target audience in mind (e.g. informal, formal and technical writing).";
                if (videoAudio)
                {
                    CreateURLButton("How To Write A Dissertation Or Thesis - 8 Step Tutorial + Examples<br><b>Video</b><br>Grad Coach on YouTube", delegate { Application.OpenURL("https://www.youtube.com/watch?v=1Ir9z_O4P3A"); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("Creative Writing Exercises<br>Language is a Virus", delegate { Application.OpenURL("https://www.languageisavirus.com/creative-writing-exercises/index.php#.YjIjxKinyUk"); });
                    CreateURLButton("Creative Writing Techniques<br>Language is a Virus", delegate { Application.OpenURL("https://www.languageisavirus.com/creative-writing-techniques/index.php#.YjIlM6inyUk"); });
                    CreateURLButton("41 Free & Printable Story Map Templates [PDF/ Word]<br>TemplateLAB", delegate { Application.OpenURL("https://templatelab.com/story-map-templates/"); });
                    CreateURLButton("50 Smart Literature Review Templates (APA)<br>TemplateLAB", delegate { Application.OpenURL("https://templatelab.com/literature-review/"); });
                    CreateURLButton("45 Perfect Thesis Statement Templates (+ Examples)<br>TemplateLAB", delegate { Application.OpenURL("https://templatelab.com/thesis-statements/"); });
                    CreateURLButton("50 Statement Of Purpose Examples (Graduate School, MBA, PhD)<br>TemplateLAB", delegate { Application.OpenURL("https://templatelab.com/statement-of-purpose/"); });
                    CreateURLButton("50 Best Reflective Essay Examples (+ Topic Samples)<br>TemplateLAB", delegate { Application.OpenURL("https://templatelab.com/reflective-essay/"); });
                    CreateURLButton("50 Free Persuasive Essay Examples (+ BEST Topics)<br>TemplateLAB", delegate { Application.OpenURL("https://templatelab.com/persuasive-essay/"); });
                }

                if (miniGames)
                {
                    CreateURLButton("Creative Writing Games<br>Language is a Virus", delegate { Application.OpenURL("https://www.languageisavirus.com/writing-games.php#.YjIjwainyUl"); });
                }
                break;
            default:
                learningSkill = "Ability to accept Criticism";
                learningSkillDefinition = "No skill definition available. Coming soon.";
                if (videoAudio)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (paperArticleBlog)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (freeCourses)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }

                if (miniGames)
                {
                    CreateURLButton("No learning resources available", delegate { Application.OpenURL(null); });
                }
                break;
        }
    }
}
