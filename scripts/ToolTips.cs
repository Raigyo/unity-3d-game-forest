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
    private ToolTips toolTipsScripts;

    public void Start()
    {
        toolTipsScripts = gameObject.GetComponent<ToolTips>();
    }

    //Show panel
    public void ShowPanel()
    {
        toolTipWindow.SetActive(true);
        if (toolTipWindow != null)
        {
            displayName.text = objectName;
            displayInfo.text = objectInfo;
        }
    }//!!!drag and drop the tooltip.cs script component from the currentbutton to the object on trigger event manager

    //Hide panel
    public void HidePanel()
    {
        toolTipWindow.SetActive(false);
    }//!!!drag and drop the tooltip.cs script component from the currentbutton to the object on trigger event manager

    //Disable panel
    public void DisablePanel()
    {
        toolTipsScripts.enabled = false;
    }
}
