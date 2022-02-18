using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsRepository : FirebaseManager
{
    private string[] skills;

    #region IEnumerator's writing different categorised skills to database
    private IEnumerator WriteHardSkillsToDatabase(string _skill)
    {
        yield return new WaitForSeconds(1); //may have to change this value to 5

        var DBTask = DBreference.Child("skills").Child("hard").SetValueAsync(_skill);

        Debug.Log("Hard Skills have been added to the database");

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database hard skills is now updated
        }
    }

    private IEnumerator WriteOrganisationalSkillsToDatabase(string _skill)
    {
        yield return new WaitForSeconds(1); //may have to change this value to 5 or possibly less? 0.5 or 0.25?

        var DBTask = DBreference.Child("skills").Child("organisational").SetValueAsync(_skill);

        Debug.Log("Organisation Skills have been added to the database");

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database organisational skills is now updated
        }
    }

    private IEnumerator WriteCommunInterSkillsToDatabase(string _skill)
    {
        yield return new WaitForSeconds(1); //may have to change this value to 5 or poossibly less? 0.5 or 0.25?

        var DBTask = DBreference.Child("skills").Child("communication-interpersonal").SetValueAsync(_skill);

        Debug.Log("Communication and Interpersonal Skills have been added to the database");

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database communication and interpersonal skills is now updated
        }
    }

    private IEnumerator WritePersonalValueAttitudesSkillsToDataBase(string _skill)
    {
        yield return new WaitForSeconds(1); //may have to change this value to 5 or possible less? 0.5 or 0.25?

        var DBTask = DBreference.Child("skills").Child("personal-values-attitudes").SetValueAsync(_skill);

        Debug.Log("Personal skills, values and attitude skills have been added to the database");

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database personal skills, values and attitudes is now updated
        }
    }

    private IEnumerator WriteGeneralWorkplaceSkillsToDatabase(string _skill)
    {
        yield return new WaitForSeconds(1); //may have to change this value to 5 or possibly less? 0.5 or 0.25?

        var DBTask = DBreference.Child("skills").Child("general-workplace").SetValueAsync(_skill);

        Debug.Log("General Workplace skills have been added to the database");

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database general workplace skills is now updated
        }
    }

    private IEnumerator WriteForeignLanguagesSkillsToDatabase(string _skill)
    {
        yield return new WaitForSeconds(1); //may have to change this value to 5 or possibly less? 0.5 or 0.25?

        var DBTask = DBreference.Child("skills").Child("foreign-languages").SetValueAsync(_skill);

        Debug.Log("Foreign languages skills have been added to the database");

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database foreign languages skills is now updated
        }
    }
    #endregion
}
