using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using TMPro;

//ActionKeysManager: Manage the keyboard shortcuts ingame and what's displayed / hidden then


public class ActionKeysManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;//pause pannel reference
    //[SerializeField] private GameObject timePanel;//time pannel reference
    [SerializeField] private GameObject miniMap;//minimap reference
    [SerializeField] private GameObject fpsControler;//ref to fpsControler to disable it
    [SerializeField] private GameObject hitBehaviour;//ref to hitbahaviour to disable raycast
    [SerializeField] private GameObject crossHair;//ref to crossHair to hide it

    public ScriptableVars scriptableVars;//scriptable object ref to know if we are in walking mode or game mode

    void Start()
    {
        //set pause pannel unactive
        pausePanel.SetActive(false);
        //set time pannel unactive
        //timePanel.SetActive(false);    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            else 
            {
                ContinueGame();
            }
        }
        //enable / disable pause pannel using 'p' or 'escape' key

        if (Input.GetKeyDown(KeyCode.F2) && Time.timeScale == 1 && scriptableVars.walkingMode == false)
        {
            if (!miniMap.activeInHierarchy)
            {
                miniMap.SetActive(true);
            }
            else
            {
                miniMap.SetActive(false);
            }
        }
        //enable / disable minimap using 'F2' key

        if (Input.GetKeyDown(KeyCode.F2) && Time.timeScale == 1 && scriptableVars.walkingMode == true)
        {
            //EnviroSkyLite.instance.SetTime(2018, 1, 6, 0, 0);
            EnviroSkyLite.instance.SetInternalTimeOfDay(7.0f);
        }

        if (Input.GetKeyDown(KeyCode.F3) && Time.timeScale == 1 && scriptableVars.walkingMode == true)
        {
            //EnviroSkyLite.instance.SetTime(2018, 1, 15, 0, 0);
            EnviroSkyLite.instance.SetInternalTimeOfDay(14.0f);
        }

        if (Input.GetKeyDown(KeyCode.F4) && Time.timeScale == 1 && scriptableVars.walkingMode == true)
        {
            //EnviroSkyLite.instance.SetTime(2018, 1, 20, 0, 0);
            EnviroSkyLite.instance.SetInternalTimeOfDay(20.0f);
        }
        //switch between time of the day
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        //Freeze the game - Disable scripts that still work while timescale is set to 0
        fpsControler.GetComponent<FirstPersonControllerEdited>().enabled = false;
        hitBehaviour.SetActive(false);
        crossHair.SetActive(false);
        //timePanel.SetActive(false);
        //all these elements are disabled
        pausePanel.SetActive(true);
        //set pause pannel active
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //use of mouse to click buttons
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        //unfreeze game
        fpsControler.GetComponent<FirstPersonControllerEdited>().enabled = true;
        hitBehaviour.SetActive(true);
        crossHair.SetActive(true);
        //all these elements are enabled
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //disable mouse to use raycast instead
        pausePanel.SetActive(false);
        //set pause pannel unactive
    }
}
