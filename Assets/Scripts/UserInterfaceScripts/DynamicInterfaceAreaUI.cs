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
    public SkillsInfo skillInfoManager;

    [SerializeField]
    private TMP_InputField selectedSkill;

    private void Awake()
    {
        accountManager = FindObjectOfType<AccountManager>();
        userInterfaceManager = FindObjectOfType<UserInterfaceManagerUI>();
        skillInfoManager = FindObjectOfType<SkillsInfo>();
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
        }

        selectedSkill.text = _addNewSkillData.Name;
        //_addNewSkillData.Name = selectedSkill.text;
        skillInfoManager.TransferSkillData();

        //ContentDataIdentiferUI _skillName = CreateEditInformationContent("Name", _addNewSkillData.Name, TMP_InputField.ContentType.Name);
        /*_skillName.enabled = false;*/
        //CaptureStringData(ref _addNewSkillData.Name, _skillName);
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
            CreateHeaderText("Skill Summary", pageNumber + "/" + totalPages, "Please check all information before proceeding.");
            CreateDisplayGroup("<b><u>Skill</b></u>");
            /*StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));*/
            Transform _holder = CreateDisplayGroup("<br>" + _addNewSkillData.Name).parent;

            CreateSkillButton(_addNewSkillData.LevelName, _addNewSkillData.Name, _addNewSkillData.Level, null);
            CreateDisplayGroup("<br><br>" + _addNewSkillData.LevelName, _holder);
            CreateDisplayGroup("<br>" + _addNewSkillData.Level.ToString(), _holder);

            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Confirm",
              delegate
              {
                  SaveSkill();
                  _addNewSkillData = null;
                  userInterfaceManager.Open_Files();// (userInterfaceManager.FilesScreen);                   //Go to next page.
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
        }
        else
        {
            Configure_Top_Banner(false, false, "Add Artifact", delegate { AddNewArtifact(pageNumber - 1); }, null, null);
        }


        if (pageNumber == 1)
        {
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
            }, 255, 255, 255, 255, 255, 255);
        }
        else if (pageNumber == 5)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "<b>Artifact Summary</b><br><br>Please check all information before proceeding.");
            CreateDisplayGroup("<b>Artifact</b>");
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
              }, 255, 255, 255, 255, 255, 255);
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
        }
        else
        {
            Configure_Top_Banner(false, false, "Add Experience", delegate { AddNewExperiencePage(pageNumber - 1); }, null, null);
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
            ContentDataIdentiferUI _startDate = CreateDatePrefab("Start Date", _addNewExperienceData.StartDate).GetComponent<ContentDataIdentiferUI>();
            ContentDataIdentiferUI _endDate = CreateDatePrefab("End Date", _addNewExperienceData.EndDate).GetComponent<ContentDataIdentiferUI>();
            StartCoroutine(CreateSpaceFiller(GetSpaceFillerIndex()));
            CreateButton("Continue", delegate { CaptureDate(ref _addNewExperienceData.StartDate, _startDate); CaptureDate(ref _addNewExperienceData.EndDate, _endDate); AddNewExperiencePage(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);

        } else if (pageNumber == 3)
        {


            CreateHeaderText(null, pageNumber + "/" + totalPages, "What skills did this experience help you improve at? You may choose more than one.");
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
            CreateButton("Academic", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);
            CreateButton("Practical", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 255, 255, 255, 255, 255);

        }
        else if (pageNumber == 5)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "Give a brief description of the experience.");
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
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 7)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How committed were you to the team success?");
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 8)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How committed were you to the team leadership?");
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 9)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How committed were you to the team process?");
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 10)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How accountable were you towards your work?");
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 11)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How well did you communicate with the rest of the team?");
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 12)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "How well did you cooperate with the rest of the team?");
            CreateButton("Not at all", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 112, 112, 0, 0, 0);
            CreateButton("Little", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 167, 112, 0, 0, 0);
            CreateButton("Moderately", delegate { AddNewExperiencePage(pageNumber + 1); }, 255, 236, 112, 0, 0, 0);
            CreateButton("Good", delegate { AddNewExperiencePage(pageNumber + 1); }, 155, 255, 112, 0, 0, 0);
            CreateButton("Very good", delegate { AddNewExperiencePage(pageNumber + 1); }, 112, 255, 126, 0, 0, 0);
        }
        else if (pageNumber == 13)
        {
            CreateHeaderText(null, pageNumber + "/" + totalPages, "Additional comments.");
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

    public Transform CreateButton(string content, UnityAction _event, float r, float g, float b, float tr, float tg, float tb, bool overrideCheck = true)
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
