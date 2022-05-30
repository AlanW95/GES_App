using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserAccount
{
    public string firstName = "DEFAULT";
    public string lastName = "DEFAULT";
    public string university = "University of the West of Scotland";

    public string userName = "DEFAULT";
    public string emailAddress = "DEFAULT";

    public List<ExperienceData> _experiences = new List<ExperienceData>();
    public List<ArtifactData> _artifacts = new List<ArtifactData>();
    public List<ReferenceData> _references = new List<ReferenceData>();
    public List<SkillData> _skills = new List<SkillData>();
    public List<CVData> _cv = new List<CVData>();

    public UserAccount() {}

    public UserAccount(string userName, string emailAddress)
    {
        this.userName = userName;
        this.emailAddress = emailAddress;
    }

    public void SaveExperience(ExperienceData experienceData)
    {
        _experiences.Add(experienceData);
    }

    public void SaveArtifact(ArtifactData artifactData)
    {
        _artifacts.Add(artifactData);
    }

    public void SaveReference(ReferenceData referenceData)
    {
        _references.Add(referenceData);
    }

    public void SaveSkill(SkillData skillData)
    {
        _skills.Add(skillData);
    }

    public void SaveCV(CVData cvData)
    {
        _cv.Add(cvData);
    }
}

public class SkillData
{
    public string Name;
    public int Level;
    public string LevelName;

    public SkillData() { }

}

public class ExperienceData
{
    public System.DateTime StartDate;
    public System.DateTime EndDate;
    public List<string> Skills = new List<string>();
    public List<string> CourseOccured = new List<string>();
    public string ExperienceLocale;
    public string Description;
    public string RoleInExperience;
    public string Comments;

    public ExperienceData() { }
}

public class ArtifactData
{
    public enum ArtifactType { Document, Image, Link, Repository, Video, Note }
    public ArtifactType type;
    public string ArtificatContent;
    public List<string> Skills = new List<string>();
    public List<string> Experiences = new List<string>();
    public string ExperienceLocale;
    public string Reference;

    public string Title;
    public string Description;
    public string URL;

    public ArtifactData() { }
}

public class ReferenceData
{
    public string Name;
    public string Email;
    public string Position;
    public string PhoneNumber;
    public List<string> Skills = new List<string>();

    public ReferenceData() { }
}

public class CVData
{
    public string Name;
    public List<string> Skills = new List<string>();

    public CVData() { }
}

public class DreamJobData
{
    public string Name;
    public string Category;
    //public string 
}

public class AccountManager : MonoBehaviour
{
    public UserAccount localUserAccount = new UserAccount();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
