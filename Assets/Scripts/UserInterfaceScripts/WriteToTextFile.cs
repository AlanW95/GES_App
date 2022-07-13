using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class WriteToTextFile : MonoBehaviour
{
    string filename = "";

    //Use Application.persistentDataPath when running game on android device
    void Start()
    {
        //create the folder
        Directory.CreateDirectory(Application.persistentDataPath + "/Exported_CV_GES/");
    }

    public void CreateTextFile(string text)
    {

        string textDocumentName = "";

        textDocumentName = Application.persistentDataPath + "/CVDetails.txt";

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //create the text file to download it
            textDocumentName = Application.persistentDataPath + "/CVDetails.txt";
        }
        else
        {
            //create the text file at the already created directory in the start function
            textDocumentName = Application.persistentDataPath + "/Exported_CV/" + "CVDetails" + ".txt";
        }
        

        //check to make sure the text exists or not... if it doesn't, create one... if it does, move on
        if (!File.Exists(textDocumentName))
        {
            //we are going to add a heading inside the .txt file we created for this data
            File.WriteAllText(textDocumentName, "CV Details \n\n" + text);

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                var fullPath = "C:/<file-name>.txt";
                File.WriteAllText(fullPath, "CV Details \n\n" + text);
            }
        }
    }
}
