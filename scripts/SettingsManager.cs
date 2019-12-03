using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public Slider keyboardSettings;//slider ref
    public ScriptableVars scriptableVars;//scriptable object ref

    void Start()
    {
        keyboardSettings.value = scriptableVars.keyboardLayout;
    }

    void Update()
    {
        scriptableVars.keyboardLayout = Mathf.RoundToInt(keyboardSettings.value);//float to int
     }
}
