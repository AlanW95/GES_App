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
        public DreamJobs[] dreamJobs;
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
        #region Law Data
        string[] lawData = LawTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int lawTableSize = lawData.Length / 2 - 1;
        lawList.dreamJobs = new DreamJobs[lawTableSize];

        for (int i = 0; i < lawTableSize; i++)
        {
            lawList.dreamJobs[i] = new DreamJobs();

            lawList.dreamJobs[i].Name = lawData[2 * (i + 1)];
            lawList.dreamJobs[i].IndustrySkills = lawData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            lawList.dreamJobs[i].GES = lawData[2 * (i + 1) + 1]; //Need to connect with GES skills
            lawList.dreamJobs[i].Personality = lawData[2 * (i + 1) + 1];
            lawList.dreamJobs[i].References = lawData[2 * (i + 1) + 1];
        }
        #endregion Law Data

        #region Education Data
        string[] educationData = EducationTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int educationTableSize = educationData.Length / 2 - 1;
        educationList.dreamJobs = new DreamJobs[educationTableSize];

        for (int i = 0; i < educationTableSize; i++)
        {
            educationList.dreamJobs[i] = new DreamJobs();

            educationList.dreamJobs[i].Name = educationData[2 * (i + 1)];
            educationList.dreamJobs[i].IndustrySkills = educationData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            educationList.dreamJobs[i].GES = educationData[2 * (i + 1) + 1]; //Need to connect with GES skills
            educationList.dreamJobs[i].Personality = educationData[2 * (i + 1) + 1];
            educationList.dreamJobs[i].References = educationData[2 * (i + 1) + 1];
        }
        #endregion Education Data

        #region Literature Data
        string[] literatureData = LiteratureTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int literatureTableSize = literatureData.Length / 2 - 1;
        literatureList.dreamJobs = new DreamJobs[literatureTableSize];

        for (int i = 0; i < literatureTableSize; i++)
        {
            literatureList.dreamJobs[i] = new DreamJobs();

            literatureList.dreamJobs[i].Name = literatureData[2 * (i + 1)];
            literatureList.dreamJobs[i].IndustrySkills = literatureData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            literatureList.dreamJobs[i].GES = literatureData[2 * (i + 1) + 1]; //Need to connect with GES skills
            literatureList.dreamJobs[i].Personality = literatureData[2 * (i + 1) + 1];
            literatureList.dreamJobs[i].References = literatureData[2 * (i + 1) + 1];
        }
        #endregion Literature Data

        #region Linguistics Data
        string[] linguisticsData = LinguisticsTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int linquisticsTableSize = linguisticsData.Length / 2 - 1;
        linguisticsList.dreamJobs = new DreamJobs[linquisticsTableSize];

        for (int i = 0; i < linquisticsTableSize; i++)
        {
            linguisticsList.dreamJobs[i] = new DreamJobs();

            linguisticsList.dreamJobs[i].Name = linguisticsData[2 * (i + 1)];
            linguisticsList.dreamJobs[i].IndustrySkills = linguisticsData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            linguisticsList.dreamJobs[i].GES = linguisticsData[2 * (i + 1) + 1]; //Need to connect with GES skills
            linguisticsList.dreamJobs[i].Personality = linguisticsData[2 * (i + 1) + 1];
            linguisticsList.dreamJobs[i].References = linguisticsData[2 * (i + 1) + 1];
        }
        #endregion Linguistics Data

        #region Humanities Data
        string[] humanitiesData = HumanitiesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int humanitiesTableSize = humanitiesData.Length / 2 - 1;
        humanitiesList.dreamJobs = new DreamJobs[humanitiesTableSize];

        for (int i = 0; i < humanitiesTableSize; i++)
        {
            humanitiesList.dreamJobs[i] = new DreamJobs();

            humanitiesList.dreamJobs[i].Name = humanitiesData[2 * (i + 1)];
            humanitiesList.dreamJobs[i].IndustrySkills = humanitiesData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            humanitiesList.dreamJobs[i].GES = humanitiesData[2 * (i + 1) + 1]; //Need to connect with GES skills
            humanitiesList.dreamJobs[i].Personality = humanitiesData[2 * (i + 1) + 1];
            humanitiesList.dreamJobs[i].References = humanitiesData[2 * (i + 1) + 1];
        }
        #endregion Humanities Data

        #region Media Sciences Data
        string[] mediaSciencesData = MediaSciencesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int mediaSciencesTableSize = mediaSciencesData.Length / 2 - 1;
        mediaSciencesList.dreamJobs = new DreamJobs[mediaSciencesTableSize];

        for (int i = 0; i < mediaSciencesTableSize; i++)
        {
            mediaSciencesList.dreamJobs[i] = new DreamJobs();

            mediaSciencesList.dreamJobs[i].Name = mediaSciencesData[2 * (i + 1)];
            mediaSciencesList.dreamJobs[i].IndustrySkills = mediaSciencesData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            mediaSciencesList.dreamJobs[i].GES = mediaSciencesData[2 * (i + 1) + 1]; //Need to connect with GES skills
            mediaSciencesList.dreamJobs[i].Personality = mediaSciencesData[2 * (i + 1) + 1];
            mediaSciencesList.dreamJobs[i].References = mediaSciencesData[2 * (i + 1) + 1];
        }
        #endregion Media Sciences Data

        #region Psychology Data
        string[] psychologyData = PsychologyTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int psychologyTableSize = psychologyData.Length / 2 - 1;
        psychologyList.dreamJobs = new DreamJobs[psychologyTableSize];

        for (int i = 0; i < psychologyTableSize; i++)
        {
            psychologyList.dreamJobs[i] = new DreamJobs();

            psychologyList.dreamJobs[i].Name = psychologyData[2 * (i + 1)];
            psychologyList.dreamJobs[i].IndustrySkills = psychologyData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            psychologyList.dreamJobs[i].GES = psychologyData[2 * (i + 1) + 1]; //Need to connect with GES skills
            psychologyList.dreamJobs[i].Personality = psychologyData[2 * (i + 1) + 1];
            psychologyList.dreamJobs[i].References = psychologyData[2 * (i + 1) + 1];
        }
        #endregion Psychology Data

        #region Business Studies Data
        string[] businessStudiesData = BusinessStudiesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int businessStudiesTableSize = businessStudiesData.Length / 2 - 1;
        businessStudiesList.dreamJobs = new DreamJobs[businessStudiesTableSize];

        for (int i = 0; i < businessStudiesTableSize; i++)
        {
            businessStudiesList.dreamJobs[i] = new DreamJobs();

            businessStudiesList.dreamJobs[i].Name = businessStudiesData[2 * (i + 1)];
            businessStudiesList.dreamJobs[i].IndustrySkills = businessStudiesData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            businessStudiesList.dreamJobs[i].GES = businessStudiesData[2 * (i + 1) + 1]; //Need to connect with GES skills
            businessStudiesList.dreamJobs[i].Personality = businessStudiesData[2 * (i + 1) + 1];
            businessStudiesList.dreamJobs[i].References = businessStudiesData[2 * (i + 1) + 1];
        }
        #endregion Business Studies Data

        #region Science and Engineering Data
        string[] scienceAndEngineeringData = ScienceAndEngineeringTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int scienceAndEngineeringTableSize = scienceAndEngineeringData.Length / 2 - 1;
        scienceAndEngineeringList.dreamJobs = new DreamJobs[scienceAndEngineeringTableSize];

        for (int i = 0; i < scienceAndEngineeringTableSize; i++)
        {
            scienceAndEngineeringList.dreamJobs[i] = new DreamJobs();

            scienceAndEngineeringList.dreamJobs[i].Name = scienceAndEngineeringData[2 * (i + 1)];
            scienceAndEngineeringList.dreamJobs[i].IndustrySkills = scienceAndEngineeringData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            scienceAndEngineeringList.dreamJobs[i].GES = scienceAndEngineeringData[2 * (i + 1) + 1]; //Need to connect with GES skills
            scienceAndEngineeringList.dreamJobs[i].Personality = scienceAndEngineeringData[2 * (i + 1) + 1];
            scienceAndEngineeringList.dreamJobs[i].References = scienceAndEngineeringData[2 * (i + 1) + 1];
        }
        #endregion Science and Engineering Data

        #region Geography and Earth Science Data
        string[] geographyAndEarthScienceData = GeographyAndEarthSciencesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int geographyAndEarthScienceTableSize = geographyAndEarthScienceData.Length / 2 - 1;
        geographyAndEarthScienceList.dreamJobs = new DreamJobs[geographyAndEarthScienceTableSize];

        for (int i = 0; i < geographyAndEarthScienceTableSize; i++)
        {
            geographyAndEarthScienceList.dreamJobs[i] = new DreamJobs();

            geographyAndEarthScienceList.dreamJobs[i].Name = geographyAndEarthScienceData[2 * (i + 1)];
            geographyAndEarthScienceList.dreamJobs[i].IndustrySkills = geographyAndEarthScienceData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            geographyAndEarthScienceList.dreamJobs[i].GES = geographyAndEarthScienceData[2 * (i + 1) + 1]; //Need to connect with GES skills
            geographyAndEarthScienceList.dreamJobs[i].Personality = geographyAndEarthScienceData[2 * (i + 1) + 1];
            geographyAndEarthScienceList.dreamJobs[i].References = geographyAndEarthScienceData[2 * (i + 1) + 1];
        }
        #endregion Geography and Earth Science Data

        #region Medicine, Veterinary and Life Sciences
        string[] medicineVeterinaryAndLifeSciencesData = MedicineVeterinaryAndLifeSciencesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int medicineVeterinaryAndLifeSciencesTableSize = medicineVeterinaryAndLifeSciencesData.Length / 2 - 1;
        medicineVeterinaryAndLifeSciencesList.dreamJobs = new DreamJobs[medicineVeterinaryAndLifeSciencesTableSize];

        for (int i = 0; i < medicineVeterinaryAndLifeSciencesTableSize; i++)
        {
            medicineVeterinaryAndLifeSciencesList.dreamJobs[i] = new DreamJobs();

            medicineVeterinaryAndLifeSciencesList.dreamJobs[i].Name = medicineVeterinaryAndLifeSciencesData[2 * (i + 1)];
            medicineVeterinaryAndLifeSciencesList.dreamJobs[i].IndustrySkills = medicineVeterinaryAndLifeSciencesData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            medicineVeterinaryAndLifeSciencesList.dreamJobs[i].GES = medicineVeterinaryAndLifeSciencesData[2 * (i + 1) + 1]; //Need to connect with GES skills
            medicineVeterinaryAndLifeSciencesList.dreamJobs[i].Personality = medicineVeterinaryAndLifeSciencesData[2 * (i + 1) + 1];
            medicineVeterinaryAndLifeSciencesList.dreamJobs[i].References = medicineVeterinaryAndLifeSciencesData[2 * (i + 1) + 1];
        }
        #endregion Medicine, Veterinary and Life Sciences

        #region Architecture
        string[] architectureData = ArchitectureTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int architectureTableSize = architectureData.Length / 2 - 1;
        architectureList.dreamJobs = new DreamJobs[architectureTableSize];

        for (int i = 0; i < architectureTableSize; i++)
        {
            architectureList.dreamJobs[i] = new DreamJobs();

            architectureList.dreamJobs[i].Name = architectureData[2 * (i + 1)];
            architectureList.dreamJobs[i].IndustrySkills = architectureData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            architectureList.dreamJobs[i].GES = architectureData[2 * (i + 1) + 1]; //Need to connect with GES skills
            architectureList.dreamJobs[i].Personality = architectureData[2 * (i + 1) + 1];
            architectureList.dreamJobs[i].References = architectureData[2 * (i + 1) + 1];
        }
        #endregion Architecture

        #region Arts Data
        string[] artsData = ArtsTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int artsTableSize = artsData.Length / 2 - 1;
        artsList.dreamJobs = new DreamJobs[artsTableSize];

        for (int i = 0; i < artsTableSize; i++)
        {
            artsList.dreamJobs[i] = new DreamJobs();

            artsList.dreamJobs[i].Name = artsData[2 * (i + 1)];
            artsList.dreamJobs[i].IndustrySkills = artsData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            artsList.dreamJobs[i].GES = artsData[2 * (i + 1) + 1]; //Need to connect with GES skills
            artsList.dreamJobs[i].Personality = artsData[2 * (i + 1) + 1];
            artsList.dreamJobs[i].References = artsData[2 * (i + 1) + 1];
        }
        #endregion Arts Data

        #region Theology and Religious Studies Data
        string[] theologyAndReligiousStudiesData = TheologyAndReligiousStudiesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int theologyAndReligiousStudiesTableSize = theologyAndReligiousStudiesData.Length / 2 - 1;
        theologyAndReligiousStudiesList.dreamJobs = new DreamJobs[theologyAndReligiousStudiesTableSize];

        for (int i = 0; i < theologyAndReligiousStudiesTableSize; i++)
        {
            theologyAndReligiousStudiesList.dreamJobs[i] = new DreamJobs();

            theologyAndReligiousStudiesList.dreamJobs[i].Name = theologyAndReligiousStudiesData[2 * (i + 1)];
            theologyAndReligiousStudiesList.dreamJobs[i].IndustrySkills = theologyAndReligiousStudiesData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            theologyAndReligiousStudiesList.dreamJobs[i].GES = theologyAndReligiousStudiesData[2 * (i + 1) + 1]; //Need to connect with GES skills
            theologyAndReligiousStudiesList.dreamJobs[i].Personality = theologyAndReligiousStudiesData[2 * (i + 1) + 1];
            theologyAndReligiousStudiesList.dreamJobs[i].References = theologyAndReligiousStudiesData[2 * (i + 1) + 1];
        }
        #endregion Theology and Religious Studies Data

        #region Social and Political Sciences
        string[] socialAndPoliticalSciencesData = SocialAndPoliticalSciencesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int socialAndPoliticalSciencesTableSize = socialAndPoliticalSciencesData.Length / 2 - 1;
        socialAndPoliticalSciencesList.dreamJobs = new DreamJobs[socialAndPoliticalSciencesTableSize];

        for (int i = 0; i < socialAndPoliticalSciencesTableSize; i++)
        {
            socialAndPoliticalSciencesList.dreamJobs[i] = new DreamJobs();

            socialAndPoliticalSciencesList.dreamJobs[i].Name = socialAndPoliticalSciencesData[2 * (i + 1)];
            socialAndPoliticalSciencesList.dreamJobs[i].IndustrySkills = socialAndPoliticalSciencesData[2 * (i + 1) + 1]; //need to connect with Hard Skills
            socialAndPoliticalSciencesList.dreamJobs[i].GES = socialAndPoliticalSciencesData[2 * (i + 1) + 1]; //Need to connect with GES skills
            socialAndPoliticalSciencesList.dreamJobs[i].Personality = socialAndPoliticalSciencesData[2 * (i + 1) + 1];
            socialAndPoliticalSciencesList.dreamJobs[i].References = socialAndPoliticalSciencesData[2 * (i + 1) + 1];
        }
        #endregion Social and Political Sciences
    }
}
