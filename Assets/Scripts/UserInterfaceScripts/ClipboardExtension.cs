using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardExtension : MonoBehaviour
{
    public void CopyToClipboard(string text)
    {
        GUIUtility.systemCopyBuffer = text;
    }
}


