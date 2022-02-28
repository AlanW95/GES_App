using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DreamJobRepository : SkillsRepository
{
    //UWS Dream Jobs
    public TextAsset LawTextData;
    public TextAsset EducationTextData;
    public TextAsset LiteratureTextData;
    public TextAsset LinguisticsTextData;

    //UKSW Dream Jobs
    public TextAsset HumanitiesTextData;
    public TextAsset MediaSciencesTextData;
    public TextAsset PsychologyTextData;

    //NTNU Dream Jobs
    public TextAsset BusinessStudiesTextData;
    public TextAsset ScienceAndEngineeringTextData;
    public TextAsset GeographyAndEarthSciencesTextData;
    public TextAsset MedicineVeterinaryAndLifeSciencesTextData;

    //UOP Dream Jobs
    public TextAsset ArchitectureTextData;
    public TextAsset ArtsTextData;
    public TextAsset TheologyAndReligiousStudiesTextData;
    public TextAsset SocialAndPoliticalSciencesTextData;

    [System.Serializable]
    public class DreamJobs
    {
        public string Name;
        public string IndustrySkills;
        public string GES;
        public string Personality;
        public string References;
    }

    [System.Serializable]
    public class DreamJobList
    {
        public DreamJobs[] dreamjobs;
    }

    public DreamJobList lawList = new DreamJobList();
    public DreamJobList educationList = new DreamJobList();
    public DreamJobList literatureList = new DreamJobList();
    public DreamJobList linguisticsList = new DreamJobList();
    public DreamJobList humanitiesList = new DreamJobList();
    public DreamJobList mediaSciencesList = new DreamJobList();
    public DreamJobList psychologyList = new DreamJobList();
    public DreamJobList businessStudiesList = new DreamJobList();
    public DreamJobList scienceAndEngineeringList = new DreamJobList();
    public DreamJobList geographyAndEarthScienceList = new DreamJobList();
    public DreamJobList medicineVeterinaryAndLifeSciencesList = new DreamJobList();
    public DreamJobList architectureList = new DreamJobList();
    public DreamJobList artsList = new DreamJobList();
    public DreamJobList theologyAndReligiousStudiesList = new DreamJobList();
    public DreamJobList socialAndPoliticalSciencesList = new DreamJobList();

    // Start is called before the first frame update
    void Start()
    {
        ReadDreamJobs();
    }

    void ReadDreamJobs()
    {
        string[] lawData = LawTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries); int lawTableSize = lawData.Length / 2 - 1;
        lawList.dreamjobs = new DreamJobs[lawTableSize];

        for (int i = 0; i < lawTableSize; i++)
        {
            lawList.dreamjobs[i] = new DreamJobs();

            lawList.dreamjobs[i].Name = lawData[2 * (i + 1)];
            lawList.dreamjobs[i].IndustrySkills = lawData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            lawList.dreamjobs[i].GES = lawData[2 * (i + 1) + 1]; //Need to connect with GES skills
            lawList.dreamjobs[i].Personality = lawData[2 * (i + 1) + 1];
            lawList.dreamjobs[i].References = lawData[2 * (i + 1) + 1];
        }

        string[] educationData = EducationTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] literatureData = LiteratureTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] linguisticsData = LinguisticsTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] humanitiesData = HumanitiesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] mediaSciencesData = MediaSciencesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] psychologyData = PsychologyTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] businessStudiesData = BusinessStudiesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] scienceAndEngineeringData = ScienceAndEngineeringTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] geographyAndEarthScienceData = GeographyAndEarthSciencesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] medicineVeterinaryAndLifeSciencesData = MedicineVeterinaryAndLifeSciencesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] architextureData = ArchitectureTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] artsData = ArtsTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] theologyAndReligiousStudiesData = TheologyAndReligiousStudiesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string[] socialAndPoliticalSciencesData = SocialAndPoliticalSciencesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        
    }
}
