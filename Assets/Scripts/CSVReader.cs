using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    /*
     * 
     *  CSVReader script has been adapted and expanded from a YouTube tutorial
     *      https://youtu.be/tI9NEm02EuE
     * 
     *  Instead of the class make arrays of the size you want. Then when you initial the player the class, 
     *  instead initialise all the arrays and finally instead of reading 
     *  in to each of the player class variables, just read directly into the arrays.
     */

    public TextAsset textAssetData;

    [System.Serializable]
    public class Skills
    {
        public string[] HardSkills;
        public string[] OrganisationSkills;
        public string[] CommunicationAndInterpersonalSkills;
        public string[] PersonalSkillsValuesAndAttitudes;
        public string[] GeneralWorkplaceSkills;
        public string[] ForeignLanguagesSkills;
    }
    /*[System.Serializable]
    public class SkillsList
    {
        public Skills[] skills;
    }*/

    public Skills mySkillsList = new Skills();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries); //originally StringSplitOptions.None

        int tableSize = data.Length / 6;
        //mySkillsList.skills = new Skills[tableSize];

        /*for (int i = 0; i < tableSize; i++)
        {
            mySkillsList.HardSkills[i] = data[6 * (i + 1)];
            mySkillsList.skills[i] = new Skills();
            mySkillsList.skills[i].HardSkills = data[6 * (i + 1)];
            mySkillsList.skills[i].OrganisationSkills = data[6 * (i + 1) + 1];
            mySkillsList.skills[i].CommunicationAndInterpersonalSkills = data[6 * (i + 1) + 2];
            mySkillsList.skills[i].PersonalSkillsValuesAndAttitudes = data[6 * (i + 1) + 3];
            mySkillsList.skills[i].GeneralWorkplaceSkills = data[6 * (i + 1) + 4];
            mySkillsList.skills[i].ForeignLanguagesSkills = data[6 * (i + 1) + 5];
        }*/

        /*for (int i = 0; i < tableSize; i++)
        {
            mySkillsList.HardSkills[i] = data[i + 1];
        }*/
    }
}
