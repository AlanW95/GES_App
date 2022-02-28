using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SkillsRepository : MonoBehaviour
{
    /*
     * 
     *  SkillsRepositry script has been adapted and expanded from a YouTube tutorial about CSVReader
     *      https://youtu.be/tI9NEm02EuE
     *      
     */

    public TextAsset HardSkillsTextData;
    public TextAsset OrganisationalSkillsTextData;
    public TextAsset CommunicationAndInterpersonalSkillsTextData;
    public TextAsset PersonalSkillsValuesAndAttitudesTextData;
    public TextAsset GeneralWorkplaceSkillsTextData;
    public TextAsset ForeignLanguagesSkillsTextData;

    [System.Serializable]
    public class Skills
    {
        public string Skill;
        public string Definition;
    }
    [System.Serializable]
    public class SkillsList
    {
        public Skills[] skills;
    }

    public SkillsList hardSkillList = new SkillsList();
    public SkillsList organisationalSkillList = new SkillsList();
    public SkillsList commIntSkillList = new SkillsList();
    public SkillsList psvaSkillList = new SkillsList();
    public SkillsList generalWorkplaceSkillList = new SkillsList();
    public SkillsList foreignLanguageSkillList = new SkillsList();

    // Start is called before the first frame update
    void Start()
    {
        ReadSkills();
    }

    void ReadSkills()
    {
        string[] hardSkillsData = HardSkillsTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries); //originally StringSplitOptions.None
        string[] organisationalSkillsData = OrganisationalSkillsTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries); //originally StringSplitOptions.None
        string[] communicationAndInterpersonalSkillsData = CommunicationAndInterpersonalSkillsTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries); //originally StringSplitOptions.None
        string[] personalSkillsvalueAndAttitudesSkillsData = PersonalSkillsValuesAndAttitudesTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries); //originally StringSplitOptions.None
        string[] generalWorkplaceSkillsData = GeneralWorkplaceSkillsTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries); //originally StringSplitOptions.None
        string[] foreignLanguagesSkillsData = ForeignLanguagesSkillsTextData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries); //originally StringSplitOptions.None

        int hsTableSize = hardSkillsData.Length / 2 - 1;
        hardSkillList.skills = new Skills[hsTableSize];

        for (int i = 0; i < hsTableSize; i++)
        {
            hardSkillList.skills[i] = new Skills();

            hardSkillList.skills[i].Skill = hardSkillsData[2 * (i + 1)];
            hardSkillList.skills[i].Definition = hardSkillsData[2 * (i + 1) + 1];
        }

        int oTableSize = organisationalSkillsData.Length / 2 - 1;
        organisationalSkillList.skills = new Skills[oTableSize];

        for (int i = 0; i < oTableSize; i++)
        {
            organisationalSkillList.skills[i] = new Skills();

            organisationalSkillList.skills[i].Skill = organisationalSkillsData[2 * (i + 1)];
            organisationalSkillList.skills[i].Definition = organisationalSkillsData[2 * (i + 1) + 1];
        }

        int caiTableSize = communicationAndInterpersonalSkillsData.Length / 2 - 1;
        commIntSkillList.skills = new Skills[caiTableSize];

        for (int i = 0; i < caiTableSize; i++)
        {
            commIntSkillList.skills[i] = new Skills();

            commIntSkillList.skills[i].Skill = communicationAndInterpersonalSkillsData[2 * (i + 1)];
            commIntSkillList.skills[i].Definition = communicationAndInterpersonalSkillsData[2 * (i + 1) + 1];
        }

        int psvaTableSize = personalSkillsvalueAndAttitudesSkillsData.Length / 2 - 1;
        psvaSkillList.skills = new Skills[psvaTableSize];

        for (int i = 0; i < psvaTableSize; i++)
        {
            psvaSkillList.skills[i] = new Skills();

            psvaSkillList.skills[i].Skill = personalSkillsvalueAndAttitudesSkillsData[2 * (i + 1)];
            psvaSkillList.skills[i].Definition = personalSkillsvalueAndAttitudesSkillsData[2 * (i + 1) + 1];
        }

        int gwTableSize = generalWorkplaceSkillsData.Length / 2 - 1;
        generalWorkplaceSkillList.skills = new Skills[gwTableSize];

        for (int i = 0; i < gwTableSize; i++)
        {
            generalWorkplaceSkillList.skills[i] = new Skills();

            generalWorkplaceSkillList.skills[i].Skill = generalWorkplaceSkillsData[2 * (i + 1)];
            generalWorkplaceSkillList.skills[i].Definition = generalWorkplaceSkillsData[2 * (i + 1) + 1];
        }

        int flTableSize = foreignLanguagesSkillsData.Length / 2 - 1;
        foreignLanguageSkillList.skills = new Skills[flTableSize];

        for (int i = 0; i < flTableSize; i++)
        {
            foreignLanguageSkillList.skills[i] = new Skills();

            foreignLanguageSkillList.skills[i].Skill = foreignLanguagesSkillsData[2 * (i + 1)];
            foreignLanguageSkillList.skills[i].Definition = foreignLanguagesSkillsData[2 * (i + 1) + 1];
        }
    }
}
