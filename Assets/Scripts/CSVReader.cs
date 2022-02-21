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
     */

    public TextAsset textAssetData;

    [System.Serializable]
    public class Skills
    {
        public string HardSkills;
        public string OrganisationSkills;
        public string CommunicationAndInterpersonalSkills;
        public string PersonalSkillsValuesAndAttitudes;
        public string GeneralWorkplaceSkills;
        public string ForeignLanguagesSkills;
    }
    [System.Serializable]
    public class SkillsList
    {
        public Skills[] skills;
    }

    public SkillsList mySkillsList = new SkillsList();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        /*var lines = textAssetData.text.Split('\n');

        var lists = new List<List<string>>();
        var columns = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var data = lines[i].Split(',');
            var list = new List<string>(data); //turn this into a list
            lists.Add(list); // add this list into a big list
            columns = Mathf.Max(columns, data.Length); //can tell max number 
        }

        for (int row = 0; row < lists.Count; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                try
                {
                    print(lists[row][col]);
                }
                catch
                {
                    print("*");
                }
            }
        }*/


        //Old Solution for rows and colums
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries); //originally StringSplitOptions.None

        int tableSize = data.Length / 6 - 1;
        mySkillsList.skills = new Skills[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            mySkillsList.skills[i] = new Skills();
            mySkillsList.skills[i].HardSkills = data[i];
            mySkillsList.skills[i].OrganisationSkills = data[6 * (i + 1) + 1];
            mySkillsList.skills[i].CommunicationAndInterpersonalSkills = data[6 * (i + 1) + 2];
            mySkillsList.skills[i].PersonalSkillsValuesAndAttitudes = data[6 * (i + 1) + 3];
            mySkillsList.skills[i].GeneralWorkplaceSkills = data[6 * (i + 1) + 4];
            mySkillsList.skills[i].ForeignLanguagesSkills = data[6 * (i + 1) + 5];
        }
    }
}
