using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UserInterfaceManagerUI : MonoBehaviour
{
    public Transform _holderScreen;

    public GameObject SplashScreen;
    public GameObject CoachSelectionScreen;
    public GameObject CoachQuestionScreen;
    public GameObject ProfileScreen;
    public GameObject EditProfileScreen;
    public GameObject LoginScreen;
    public GameObject RegisterScreen;
    public GameObject HomeScreen;
    public GameObject AddSkillsScreen;
    public GameObject AddEditInformationScreen;
    public GameObject SettingsScreen;
    public GameObject CalanderScreen;
    public GameObject FilesScreen;



    public GameObject Add_SubMenu;

    public GameObject BannerTop;
    public GameObject BannerBottom; //added for login screen so we can make visible after details are logged in or registered
    public Button BannerTop_Back_Button;
    public Button BannerTop_Secondary_Button;
    public TMP_Text BannerTop_Text;
    public Image BannerTop_Image;

    public GameObject BottomBanner_Button_Settings;
    public GameObject BottomBanner_Button_Calander;
    public GameObject BottomBanner_Button_Home;
    public GameObject BottomBanner_Button_Files;
    public GameObject BottomBanner_Button_Add;

    public Color HighlightedColour;
    public Color DefaultColour;

    public TMP_Text TotalSkillsAddedText;
    public TMP_Text HomeScreenExpCountText;
    public TMP_Text HomeScreenProjCountText;
    public TMP_Text HomeScreenRefCountText;

    public List<GameObject> ListofAvailableScreens = new List<GameObject>();

    private AccountManager accountManager;

    //public Sprite LoadResources_Icon() { return new Sprite(); }

    private void Awake()
    {
        accountManager = FindObjectOfType<AccountManager>();
        LoadAllReferenceScreens();
        DefaultColour = BottomBanner_Button_Add.GetComponent<Image>().color;
    }

    private void Start()
    {
        accountManager = FindObjectOfType<AccountManager>();
        //Determine which scene opens first
        //Open_Home();
        //Open_Login();
        Open_SplashScreen();
        BannerTop.SetActive(false);
        BannerBottom.SetActive(false);
    }

    void LoadAllReferenceScreens()
    {
        for(int i=0; i< _holderScreen.transform.childCount; i++)
        {
            ListofAvailableScreens.Add(_holderScreen.transform.GetChild(i).gameObject);
        }
    }

    public void Configure_Top_Banner(bool _disable = false, bool _throwWarning = false, string title = "", UnityAction _backButtonAction = null, Sprite _UI_Image = null, UnityAction _secondaryButtonAction = null)
    {
        if(_throwWarning)
        {
            return;
        }

        if (_disable == false)
        {
            BannerTop_Text.text = title;
            BannerTop_Back_Button.onClick.RemoveAllListeners();
            BannerTop_Secondary_Button.onClick.RemoveAllListeners();
            // BannerTop_Image.sprite = _UI_Image
            if(_backButtonAction != null)
                BannerTop_Back_Button.onClick.AddListener(() => _backButtonAction());

            if(_secondaryButtonAction != null)
                BannerTop_Secondary_Button.onClick.AddListener(() => _secondaryButtonAction());
        }

        if(_UI_Image == null)
        {
            BannerTop_Secondary_Button.gameObject.SetActive(true);
        }

        BannerTop.gameObject.SetActive(!_disable);
    }

    public void Configure_Bottom_Banner(/*bool _disable = false, bool _throwWarning = false, string title = "", UnityAction _backButtonAction = null, Sprite _UI_Image = null, UnityAction _secondaryButtonAction = null*/) 
    {
        BannerBottom.SetActive(true);
    }

    public void ChangeWindow(int value)
    {
        for(int i=0; i<ListofAvailableScreens.Count; i++)
        {
            ListofAvailableScreens[i].SetActive(false);
        }
        ListofAvailableScreens[value].SetActive(true);
    }

    public void ChangeWindow(GameObject predefinedScreen)
    {
        for (int i = 0; i < ListofAvailableScreens.Count; i++)
        {
            ListofAvailableScreens[i].SetActive(false);
        }

        Open_Add_SubMenu(true);
        predefinedScreen.SetActive(true);
    }

    public void Open_SplashScreen() { ChangeWindow(SplashScreen); Configure_Top_Banner(true); }
    public void Open_CoachSelectionScreen() { ChangeWindow(CoachSelectionScreen); Configure_Top_Banner(true); }
    public void Open_CoachQuestionScreen() { ChangeWindow(CoachQuestionScreen); Configure_Top_Banner(true); }
    public void Open_Profile() { ChangeWindow(ProfileScreen); Configure_Top_Banner(false, false, "Profile", delegate { ChangeWindow(HomeScreen); }, null, delegate { ChangeWindow(EditProfileScreen); }); }
    public void Open_EditProfile() { ChangeWindow(EditProfileScreen); Configure_Top_Banner(false, false, "Edit Profile", delegate { ChangeWindow(ProfileScreen); }); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_Login() { ChangeWindow(LoginScreen); Configure_Top_Banner(false, false, "Login", delegate { ChangeWindow(LoginScreen); }, null, null); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_Register() { ChangeWindow(RegisterScreen); Configure_Top_Banner(false, false, "Register", delegate { ChangeWindow(RegisterScreen); }); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_Home() { ChangeWindow(HomeScreen); Configure_Top_Banner(true); TotalSkillsAddedText.text = accountManager.localUserAccount._skills.Count + " Skills"; HomeScreenExpCountText.text = accountManager.localUserAccount._experiences.Count.ToString(); HomeScreenProjCountText.text = accountManager.localUserAccount._artifacts.Count.ToString(); HomeScreenRefCountText.text = accountManager.localUserAccount._references.Count.ToString(); BannerBottom.SetActive(true); }
    public void Open_Setting() { ChangeWindow(SettingsScreen); Configure_Top_Banner(false, false, "Settings", delegate { ChangeWindow(HomeScreen); }); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_Calander() { ChangeWindow(CalanderScreen); Configure_Top_Banner(false, false, "Calendar", delegate { ChangeWindow(HomeScreen); }); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_Files() { ChangeWindow(FilesScreen); ChangeWindow(AddEditInformationScreen); Configure_Top_Banner(false, false, "Files", delegate { ChangeWindow(HomeScreen); }); AddEditInformationScreen.GetComponent<DynamicInterfaceAreaUI>().CreateSkillsDisplayContent(); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_AddSkills() { ChangeWindow(AddSkillsScreen); Configure_Top_Banner(false, false, "Add a Skill", delegate { ChangeWindow(HomeScreen); }, null, null ); BannerTop_Secondary_Button.gameObject.SetActive(false); }


    public void Open_Add_SubMenu(bool forceClose = false) 
    {
        //Debug.Log(Add_SubMenu.activeInHierarchy);
        if(Add_SubMenu.activeInHierarchy || forceClose == true)
        {
            BottomBanner_Button_Add.GetComponent<Image>().color = DefaultColour;
        } else
        {
            BottomBanner_Button_Add.GetComponent<Image>().color = HighlightedColour;
        }

        Add_SubMenu.SetActive(!Add_SubMenu.activeInHierarchy);

        if (forceClose)
            Add_SubMenu.SetActive(false);
    }

    public void Open_AddReference() { Open_Add_SubMenu(true);  ChangeWindow(AddEditInformationScreen); Configure_Top_Banner(false, false, "Add Reference", delegate { ChangeWindow(HomeScreen); }); AddEditInformationScreen.GetComponent<DynamicInterfaceAreaUI>().AddNewReference(1); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_AddExperience() { Open_Add_SubMenu(true);  ChangeWindow(AddEditInformationScreen); Configure_Top_Banner(false, false, "Add Experience", delegate { ChangeWindow(HomeScreen); }); AddEditInformationScreen.GetComponent<DynamicInterfaceAreaUI>().AddNewExperiencePage(1); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_AddSkill() { Open_Add_SubMenu(true);  ChangeWindow(AddEditInformationScreen); Configure_Top_Banner(false, false, "Add Skill", delegate { ChangeWindow(HomeScreen); }); AddEditInformationScreen.GetComponent<DynamicInterfaceAreaUI>().AddNewSkill(2); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_AddArtifact() { Open_Add_SubMenu(true);  ChangeWindow(AddEditInformationScreen); Configure_Top_Banner(false, false, "Add Artifact", delegate { ChangeWindow(HomeScreen); }); AddEditInformationScreen.GetComponent<DynamicInterfaceAreaUI>().AddNewArtifact(1); BannerTop_Secondary_Button.gameObject.SetActive(false); }

    public void Open_AllExperiences() { ChangeWindow(FilesScreen); ChangeWindow(AddEditInformationScreen); Configure_Top_Banner(false, false, "Experiences", delegate { ChangeWindow(HomeScreen); }); AddEditInformationScreen.GetComponent<DynamicInterfaceAreaUI>().DisplayAllExperiences(); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_AllArtefacts() { ChangeWindow(FilesScreen); ChangeWindow(AddEditInformationScreen); Configure_Top_Banner(false, false, "Projects", delegate { ChangeWindow(HomeScreen); }); AddEditInformationScreen.GetComponent<DynamicInterfaceAreaUI>().DisplayAllArtefacts(); BannerTop_Secondary_Button.gameObject.SetActive(false); }
    public void Open_AllReferences() { ChangeWindow(FilesScreen); ChangeWindow(AddEditInformationScreen); Configure_Top_Banner(false, false, "References", delegate { ChangeWindow(HomeScreen); }); AddEditInformationScreen.GetComponent<DynamicInterfaceAreaUI>().DisplayAllReferences(); BannerTop_Secondary_Button.gameObject.SetActive(false); }

}
