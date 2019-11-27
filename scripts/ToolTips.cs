using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//ToolTips: displays tooltip on menu when a button is hovered

public class ToolTips : MonoBehaviour
{
    [Header("Object Text Information")]
    public string objectName;
    [TextArea]
    public string objectInfo;

    [Header("Display the information")]
    public GameObject toolTipWindow;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI displayInfo;


    //Show panel
    public void ShowPanel()
    {
        toolTipWindow.SetActive(true);
        if (toolTipWindow != null)
        {
            displayName.text = objectName;
            displayInfo.text = objectInfo;
        }
    }

    //Hide panel
    public void HidePanel()
    {
        toolTipWindow.SetActive(false);
    }
}
