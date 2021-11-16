using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContentDataIdentiferUI : MonoBehaviour
{
    public bool requireInput = true;

    public enum ContentDataType { Inputbox, Date, Toggle }
    public ContentDataType contentDataType;

    public TMP_InputField _inputField;
    
    public TMP_InputField _DOB_DD;
    public TMP_InputField _DOB_MM;
    public TMP_InputField _DOB_YY;

    public Toggle _toggleItem;
    public TMP_Text _ToggleItemName;

    public bool getCanContinue()
    {
        if (contentDataType == ContentDataType.Inputbox)
        {
            if (requireInput && (_inputField != null && _inputField.text == ""))
                return false;
        } else if(contentDataType == ContentDataType.Date)
        {
            if (requireInput && (_DOB_DD.text == "" || _DOB_MM.text == "" || _DOB_YY.text == ""))
                return false;
        }

        return true;
    }

    public System.DateTime GetDate()
    {
        return new System.DateTime(int.Parse(_DOB_YY.text), int.Parse(_DOB_MM.text), int.Parse(_DOB_DD.text));
        //return _DOB_DD.text + "/" + _DOB_MM + "/" + _DOB_YY;
    }

    public string GetStringData() { return _inputField.text; }

    public List<string> GetGroupStringData() 
    {
        List<string> dataText = new List<string>();
        ContentDataIdentiferUI[] ToggleItems = this.transform.GetComponentsInChildren<ContentDataIdentiferUI>(); 
        for(int i=0; i<ToggleItems.Length; i++)
        {
            if(ToggleItems[i].transform.GetComponentInChildren<Toggle>().isOn)
            {
                dataText.Add(ToggleItems[i].transform.GetComponentInChildren<TMP_Text>().text);
            }
        }

        return dataText;
    }
}
